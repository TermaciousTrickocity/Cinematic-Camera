using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace modularDollyCam
{
    public partial class ProcessSelectionDialog : Form
    {
        public string SelectedProcess { get; private set; }

        public ProcessSelectionDialog()
        {
            InitializeComponent();
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            processListBox.Items.Clear();

            SortedList<int, string> sortedProcesses = new SortedList<int, string>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
            SortedList<int, string> backgroundProcesses = new SortedList<int, string>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
            HashSet<string> uniqueNames = new HashSet<string>();

            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                string processName = process.ProcessName;

                if (!uniqueNames.Contains(processName))
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        uniqueNames.Add(processName);
                        sortedProcesses.Add(process.Id, $"{processName} (PID: {process.Id})");
                    }
                    else
                    {
                        backgroundProcesses.Add(process.Id, $"{processName} (PID: {process.Id}) (background)");
                    }
                }
            }

            foreach (var processString in sortedProcesses.Values)
            {
                processListBox.Items.Add(processString);
            }

            foreach (var processString in backgroundProcesses.Values)
            {
                processListBox.Items.Add(processString);
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (processListBox.SelectedItem != null)
            {
                SelectedProcess = processListBox.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshProcessList_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }
    }
}
