using Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            SetupDataGridView();

            _proc = HookCallback;
            _hookID = SetHook(_proc);

            string subfolderNamePlugins = "plugins";
            string subfolderPathPlugins = Path.Combine(programDirectory, subfolderNamePlugins);

            string subfolderNamePaths = "paths";
            string subfolderPathPaths = Path.Combine(programDirectory, subfolderNamePaths);

            DirectoryInfo d = new DirectoryInfo(@"plugins/");
            DirectoryInfo b = new DirectoryInfo(@"paths/");

            if (Directory.Exists(subfolderPathPlugins))
            {
                FileInfo[] Files = d.GetFiles("*.json");
                string[] jsonFiles = Directory.GetFiles(subfolderPathPlugins, "*.json");

                foreach (var file in Files)
                {
                    string fileName = Path.GetFileName(file.ToString());
                }
            }
            else
            {
                Console.WriteLine("The 'plugins' folder does not exist. Creating one now...");
                Directory.CreateDirectory(subfolderPathPlugins);
            }

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
                Console.WriteLine("The 'paths' folder does not exist. Creating one now...");
                Directory.CreateDirectory(subfolderPathPaths);
            }

            teleportToSelection_Button.Click += teleportToSelection_Button_Click;
            replaceCurrent_Button.Click += replaceCurrent_Button_Click;
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
            BackColor = Color.FromArgb(245, 245, 245);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            lookTracking = !lookTracking;
        }
    }
}
