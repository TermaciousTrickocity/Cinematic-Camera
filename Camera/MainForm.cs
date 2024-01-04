using System;
using System.Windows.Forms;
using Memory;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Collections.Specialized;

namespace Camera
{
    public partial class CameraForm : Form
    {
        public Mem memory = new Mem();
        Process p;

        private Timer focusCheckTimer;

        public bool modulesUpdated = false;
        bool tagscurrentlyloaded;

        public string xPos;
        public string yPos;
        public string zPos;
        public string yawAng;
        public string pitchAng;
        public string rollAng;
        public string speedCamera;
        public string playerFov;
        public string vehicleFov;
        public string targetAddr;

        public string mccProcessSteam = "MCC-Win64-Shipping";
        public string mccProcessWinstore = "MCCWinStore-Win64-Shipping";
        private string selectedProcessName;

        public CameraForm()
        {
            Console.Title = "https://github.com/TermaciousTrickocity/Cinematic-Camera";

            InitializeComponent();
            LoadProcesses();
            TrySelectPredefinedProcess();
            InitializeDataGridView();
            InitializeTimeline();
            InitializeHotKeyTimer();
            InitializeHotkeys();
            KeyDown += new KeyEventHandler(Form_KeyDown);
            this.KeyPreview = true;
            try
            {
                string programDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string subfolderName = "plugins";
                string subfolderPath = Path.Combine(programDirectory, subfolderName);

                DirectoryInfo d = new DirectoryInfo(@"plugins/");
                FileInfo[] Files = d.GetFiles("*.json");

                if (Directory.Exists(subfolderPath))
                {
                    string[] jsonFiles = Directory.GetFiles(subfolderPath, "*.json");

                    foreach (var file in Files)
                    {
                        string fileName = Path.GetFileName(file.ToString());
                        pluginAddressCombobox.Items.Add(fileName);
                    }
                }
                else
                {
                    Console.WriteLine("The 'plugins' folder does not exist.");
                    Directory.CreateDirectory(subfolderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProcesses()
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                processCombobox.Items.Add($"{process.ProcessName} (ID: {process.Id})");
            }
        }

        private void TrySelectPredefinedProcess()
        {
            foreach (var item in processCombobox.Items)
            {
                if (item.ToString().Contains(mccProcessSteam))
                {
                    processCombobox.SelectedItem = item;
                    selectedProcessName = mccProcessSteam;
                    Console.WriteLine($"Automatically found: {selectedProcessName}");
                    return;
                }
                if (item.ToString().Contains(mccProcessWinstore))
                {
                    processCombobox.SelectedItem = item;
                    selectedProcessName = mccProcessWinstore;
                    Console.WriteLine($"Automatically found: {selectedProcessName}");
                    return;
                }
            }
        }

        private void createKeyframeButton_Click(object sender, EventArgs e)
        {
            createKeyframe();
        }

        private void deleteKeyframeButton_Click(object sender, EventArgs e)
        {
            RemoveSelectedKeypoint();
        }

        

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedKeypoint();
            }
        }

        private void enablePathing_CheckedChanged(object sender, EventArgs e)
        {
            if (enablePathing.Checked == true)
            {
                MoveCameraAsync();
            }
        }

        public void updateModules_Click(object sender, EventArgs e)
        {
            if (processCombobox.SelectedIndex >= 0)
            {
                string selectedItem = processCombobox.SelectedItem.ToString();
                selectedProcessName = selectedItem.Split('(')[0].Trim();

                p = Process.GetProcessesByName(selectedProcessName)[0];
                memory.OpenProcess(p.Id);

                int PID = memory.GetProcIdFromName(selectedProcessName);
                if (PID == 0) return;

                UpdateModules();

            }
            else
            {
                MessageBox.Show("Please select a process.", "Process Selection");
            }
        }

        //Give system.generic.list<sting> as parameter
        public void SetupPos(List<string> StringList)
        {
            targetAddr = StringList[3];
            var lastOffset = "0x0";
            xPos = StringList[0] + lastOffset;
            lastOffset = "0x4";
            yPos = StringList[0] + lastOffset;
            lastOffset = "0x8";
            zPos = StringList[0] + lastOffset.ToString();
            lastOffset = "0xC";
            yawAng = StringList[0] + lastOffset.ToString();
            lastOffset = "0x10";
            pitchAng = StringList[0] + lastOffset.ToString();
            lastOffset = "0x14";
            rollAng = StringList[0] + lastOffset.ToString();
            speedCamera = StringList[1];
            playerFov = StringList[2];
        }

        private void pluginAddressCombobox_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (pluginAddressCombobox.Text == "None (Select one!)") return;

            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string subfolderName = "plugins";

                string subfolderPath = Path.Combine(programDirectory, subfolderName);



                string selectedFilePath = Path.Combine(subfolderPath, comboBox.SelectedItem.ToString());
                GlobalData.LoadJSONFile(selectedFilePath);
                SetupPos(GlobalData.StringList);



                //vehicleFov = GlobalData.StringList[8];
            }
        }

        private void refreshProcess_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void gotoSelectedKey_Click(object sender, EventArgs e)
        {
            GotoSelectedKeypoint();
        }

        private void replaceWithCurrentPos_Click(object sender, EventArgs e)
        {
            ReplaceSelectedKeypointWithCurrentLocation();
        }

        private void resetCameraRotation_Click(object sender, EventArgs e)
        {
            memory.WriteMemory(yawAng, "float", "0");
            memory.WriteMemory(pitchAng, "float", "0");
            memory.WriteMemory(rollAng, "float", "0");
        }

        private void teleportToOrigin_Click(object sender, EventArgs e)
        {
            memory.WriteMemory(xPos, "float", "0");
            memory.WriteMemory(yPos, "float", "0");
            memory.WriteMemory(zPos, "float", "0");
        }

        private void applyModifiers_Click(object sender, EventArgs e)
        {
            memory.WriteMemory(playerFov, "float", fovTextbox.Text);
            memory.WriteMemory(rollAng, "float", rollAngle.Text);
            memory.WriteMemory(speedCamera, "float", quickAccessSpeed.Text);
        }

        private void teleportCameraButton_Click(object sender, EventArgs e)
        {
            memory.WriteMemory(xPos, "float", teleportCameraX.Text);
            memory.WriteMemory(yPos, "float", teleportCameraY.Text);
            memory.WriteMemory(zPos, "float", teleportCameraZ.Text);
        }


    }
}