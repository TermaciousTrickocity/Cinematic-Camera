using System.Diagnostics;
using Memory;
using Newtonsoft.Json;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        bool startup = false;
        public Mem memory = new Mem();
        Process p;

        public string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private string selectedProcessName;
        public bool modulesUpdated = false;

        private void updateModules_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
            fileDialog.Filter = "Plugin and JSON files (*.plugin, *.json)|*.plugin;*.json|Plugin files (*.plugin)|*.plugin|JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(fileDialog.FileName)) MessageBox.Show("The selected JSON file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                try
                {
                    string jsonData = File.ReadAllText(fileDialog.FileName);
                    List<string> addresses = JsonConvert.DeserializeObject<List<string>>(jsonData);

                    if (addresses.Count >= 7)
                    {
                        selectedProcessName = addresses[0];
                        xPos = addresses[1];
                        yPos = addresses[2];
                        zPos = addresses[3];
                        yawAng = addresses[4];
                        pitchAng = addresses[5];
                        rollAng = addresses[6];
                        playerFov = addresses[7];

                        GetModules();
                        unlockUI();

                        updateModules.Text = $"Loaded: {Path.GetFileName(fileDialog.FileName)}\n(click again to change plugins)";
                    }
                    else
                    {
                        MessageBox.Show("The selected JSON file does not contain the expected number of elements.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading JSON file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public async Task GetModules()
        {
            try
            {
                Process[] processes = Process.GetProcesses();

                p = Process.GetProcessesByName(selectedProcessName)[0];
                memory.OpenProcess(p.Id);

                if (memory == null) return;
                if (memory.theProc == null) return;

                if (startup == false)
                {
                    if (selectedProcessName == null)
                    {
                        Console.WriteLine("Process not found! (Is it running?)");
                        startup = false;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Found: " + selectedProcessName.ToString() + " (" + p.Id + ")");
                        startup = true;
                    }
                }

                memory.theProc.Refresh();
                memory.modules.Clear();

                foreach (ProcessModule Module in memory.theProc.Modules)
                {
                    if (!string.IsNullOrEmpty(Module.ModuleName) && !memory.modules.ContainsKey(Module.ModuleName))
                        memory.modules.Add(Module.ModuleName, Module.BaseAddress);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
