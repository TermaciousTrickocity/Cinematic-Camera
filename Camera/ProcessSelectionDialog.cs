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
            processListBox.Items.Clear(); // Clear the existing items

            SortedList<int, string> sortedProcesses = new SortedList<int, string>(Comparer<int>.Create((x, y) => y.CompareTo(x))); // Process names sorted by PID in descending order
            SortedList<int, string> backgroundProcesses = new SortedList<int, string>(Comparer<int>.Create((x, y) => y.CompareTo(x))); // Background process names sorted by PID in descending order
            HashSet<string> uniqueNames = new HashSet<string>(); // Unique process names

            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                string processName = process.ProcessName;

                // Check if the process name is unique
                if (!uniqueNames.Contains(processName))
                {
                    // Check if the process has a main window handle
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        uniqueNames.Add(processName);
                        sortedProcesses.Add(process.Id, $"{processName} (PID: {process.Id})");
                    }
                    else
                    {
                        // If the process doesn't have a main window handle, label it as "(background)"
                        backgroundProcesses.Add(process.Id, $"{processName} (PID: {process.Id}) (background)");
                    }
                }
            }

            // Add the sorted process strings to the ListBox
            foreach (var processString in sortedProcesses.Values)
            {
                processListBox.Items.Add(processString);
            }

            // Add the sorted background process strings to the ListBox
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
                this.Close(); // Close the dialog after selecting a process
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
