using Colorful;
using Memory;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Console = Colorful.Console;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        private GameConfiguration config;

        public MainForm()
        {
            InitializeComponent();
            SetupDataGridView();

            _proc = HookCallback;
            _hookID = SetHook(_proc);

            string subfolderNamePaths = "paths";
            string subfolderPathPaths = Path.Combine(programDirectory, subfolderNamePaths);
            DirectoryInfo b = new DirectoryInfo(@"paths/");

            if (Directory.Exists(subfolderPathPaths))
            {
                FileInfo[] Files = b.GetFiles("*.json");

                string[] jsonFiles = Directory.GetFiles(subfolderPathPaths, "*.json");

                foreach (var file in Files)
                {
                    string fileName = Path.GetFileName(file.ToString());
                }
            }
            else
            {
                Directory.CreateDirectory(subfolderPathPaths);
            }

            teleportToSelection_Button.Click += teleportToSelection_Button_Click;
            replaceCurrent_Button.Click += replaceCurrent_Button_Click;
            timeSyncTextbox.TextChanged += timeSyncTextbox_TextChanged;

            LoadConfiguration();
            unlockUI();
        }

        private void LoadConfiguration()
        {
            config = ConfigLoader.Load("config.xml");

            comboBoxProcesses.DataSource = config.Processes;

            comboBoxGames.DataSource = config.Games.Select(g => g.Name).ToList();

            comboBoxGames.SelectedIndexChanged += ComboBoxGames_SelectedIndexChanged;
            comboBoxBuilds.SelectedIndexChanged += ComboBoxBuilds_SelectedIndexChanged;

            if (comboBoxGames.Items.Count > 0)
                ComboBoxGames_SelectedIndexChanged(null, null);
        }

        private void ComboBoxGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGameName = comboBoxGames.SelectedItem as string;
            var game = config.Games.FirstOrDefault(g => g.Name == selectedGameName);

            if (game != null)
            {
                // Parse build numbers as Version and sort descending so the largest version is first.
                var sortedBuilds = game.Builds
                    .OrderByDescending(b =>
                    {
                        if (Version.TryParse(b.Number, out var v))
                            return v;
                        // fallback very small version if parsing fails (shouldn't happen for 1.xxxx.0.0)
                        return new Version(0, 0, 0, 0);
                    })
                    .ToList();

                comboBoxBuilds.DataSource = sortedBuilds.Select(b => b.Number).ToList();

                // select the largest build (first in the sorted list)
                if (comboBoxBuilds.Items.Count > 0)
                {
                    comboBoxBuilds.SelectedIndex = 0;
                    ComboBoxBuilds_SelectedIndexChanged(null, null);
                }

                GetModules();
            }
        }

        private void ComboBoxBuilds_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGameName = comboBoxGames.SelectedItem as string;
            string selectedBuildNumber = comboBoxBuilds.SelectedItem as string;

            var game = config.Games.FirstOrDefault(g => g.Name == selectedGameName);
            var build = game?.Builds.FirstOrDefault(b => b.Number == selectedBuildNumber);

            if (build != null)
            {

                selectedProcessName = comboBoxProcesses.SelectedItem as string;

                var p = build.Pointers;

                xPos = p?.X;
                yPos = p?.Y;
                zPos = p?.Z;
                yawAng = p?.Yaw;
                pitchAng = p?.Pitch;
                rollAng = p?.Roll;
                playerFov = p?.FOV;
                theaterTime = p?.TickCount;

                GetModules();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            UnhookWindowsHookEx(_hookID);
        }

        void unlockUI()
        {
            groupBox8.Enabled = true;
            groupBox9.Enabled = true;
            saveGroupbox.Enabled = true;
            BackColor = Color.FromArgb(245, 245, 245);
        }

        private void setSyncStart_Click(object sender, EventArgs e)
        {

        }

        private void timeSyncTextbox_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
