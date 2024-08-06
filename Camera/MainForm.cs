using System.Diagnostics;
using Newtonsoft.Json;
using XRPCLib;
using XRPCPlusPlus;
using XDevkit;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        public string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
        XRPC XDK = new XRPC();
        bool stopped;

        public uint Camera;
        public uint CameraX;
        public uint CameraY;
        public uint CameraZ;
        public uint CameraYaw;
        public uint CameraPitch;
        public uint CameraRoll;

        private Thread cameraThread;

        public async Task ConnectToConsole()
        {
            try
            {
                Console.WriteLine($"Awaiting connection...");
                XDK.Connect();

                if (XDK.activeConnection)
                {
                    XDK.xbCon.DebugTarget.Go(out stopped);

                    Console.WriteLine("Connected successfully! - (" + XDK.xbCon.Name.ToString() + ")");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public MainForm()
        {
            ConnectToConsole();
            InitializeComponent();
            SetupDataGridView();

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
                FileInfo[] Files = d.GetFiles("*.json");

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

        private int CalculateOffsetX(string selectedItem, int targetBaseOffset, int offsetMagic, int difference)
        {
            switch (selectedItem)
            {
                case "Item 1":
                    return targetBaseOffset + offsetMagic;
                case "Item 2":
                    return targetBaseOffset + offsetMagic + difference;
                case "Item 3":
                    return targetBaseOffset + offsetMagic + difference * 2;
                case "Item 4":
                    return targetBaseOffset + offsetMagic + difference * 4;
                case "Item 5":
                    return targetBaseOffset + offsetMagic + difference * 6;
                case "Item 6":
                    return targetBaseOffset + offsetMagic + difference * 8;
                case "Item 7":
                    return targetBaseOffset + offsetMagic + difference * 10;
                case "Item 8":
                    return targetBaseOffset + offsetMagic + difference * 12;
                case "Item 9":
                    return targetBaseOffset + offsetMagic + difference * 14;
                case "Item 10":
                    return targetBaseOffset + offsetMagic + difference * 16;
                case "Item 11":
                    return targetBaseOffset + offsetMagic + difference * 18;
                case "Item 12":
                    return targetBaseOffset + offsetMagic + difference * 20;
                case "Item 13":
                    return targetBaseOffset + offsetMagic + difference * 22;
                case "Item 14":
                    return targetBaseOffset + offsetMagic + difference * 24;
                case "(havok proxies)":
                    targetTracking.Checked = false;
                    return targetBaseOffset = 0;
                default:
                    return targetBaseOffset = 0;
            }
        }

        static Tuple<float, float, float, float, float, float> CatmullRomPositionInterpolation(Tuple<float, float, float, float, float, float> p0, Tuple<float, float, float, float, float, float> p1, Tuple<float, float, float, float, float, float> p2, Tuple<float, float, float, float, float, float> p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;
            float[] coefficients = new float[4] { -0.5f * t3 + t2 - 0.5f * t, 1.5f * t3 - 2.5f * t2 + 1.0f, -1.5f * t3 + 2.0f * t2 + 0.5f * t, 0.5f * t3 - 0.5f * t2 };

            float x = coefficients[0] * p0.Item1 + coefficients[1] * p1.Item1 + coefficients[2] * p2.Item1 + coefficients[3] * p3.Item1;
            float y = coefficients[0] * p0.Item2 + coefficients[1] * p1.Item2 + coefficients[2] * p2.Item2 + coefficients[3] * p3.Item2;
            float z = coefficients[0] * p0.Item3 + coefficients[1] * p1.Item3 + coefficients[2] * p2.Item3 + coefficients[3] * p3.Item3;
            float yaw = coefficients[0] * p0.Item4 + coefficients[1] * p1.Item4 + coefficients[2] * p2.Item4 + coefficients[3] * p3.Item4;
            float pitch = coefficients[0] * p0.Item5 + coefficients[1] * p1.Item5 + coefficients[2] * p2.Item5 + coefficients[3] * p3.Item5;
            float roll = coefficients[0] * p0.Item6 + coefficients[1] * p1.Item6 + coefficients[2] * p2.Item6 + coefficients[3] * p3.Item6;

            return new Tuple<float, float, float, float, float, float>(x, y, z, yaw, pitch, roll);
        }

        public async Task MoveCamera()
        {
            try
            {
                float fps = Convert.ToSingle(fpsTextbox.Text);

                List<Tuple<float, float, float, float, float, float>> keyPoints = new List<Tuple<float, float, float, float, float, float>>();
                List<float> transitionTimes = new List<float>();

                int startIndex;
                if (startFromSelection_checkbox.Checked && keyframeDataGridView.SelectedRows.Count > 0)
                {
                    startIndex = keyframeDataGridView.SelectedRows[0].Index;
                }
                else
                {
                    startIndex = 0;
                }

                for (int i = startIndex; i < keyframeDataGridView.Rows.Count; i++)
                {
                    float x = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["X"].Value);
                    float y = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Y"].Value);
                    float z = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Z"].Value);
                    float yaw = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Yaw"].Value);
                    float pitch = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Pitch"].Value);
                    float roll = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Roll"].Value);
                    float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Transition Time"].Value);

                    keyPoints.Add(new Tuple<float, float, float, float, float, float>(x, y, z, yaw, pitch, roll));
                    transitionTimes.Add(transitionTime);
                }

                int totalFrames = 0;
                foreach (var time in transitionTimes)
                {
                    totalFrames += (int)(time * fps);
                }

                int currentFrame = 0;
                for (int i = 0; i < keyPoints.Count - 1; i++)
                {
                    var p0 = (i == 0) ? keyPoints[0] : keyPoints[i - 1];
                    var p1 = keyPoints[i];
                    var p2 = keyPoints[i + 1];
                    var p3 = (i + 2 < keyPoints.Count) ? keyPoints[i + 2] : p2;

                    float segmentTime = transitionTimes[i];
                    int frames = (int)(segmentTime * fps);

                    for (int frame = 0; frame <= frames; frame++)
                    {
                        if (pathStart_checkbox.Checked == false) return;

                        float tNormalized = (float)frame / frames;
                        float t = tNormalized;
                        var interpolatedPosition = CatmullRomPositionInterpolation(p0, p1, p2, p3, t);

                        XDK.WriteFloat(CameraX, interpolatedPosition.Item1);
                        XDK.WriteFloat(CameraY, interpolatedPosition.Item2);
                        XDK.WriteFloat(CameraZ, interpolatedPosition.Item3);

                        XDK.WriteFloat(CameraYaw, interpolatedPosition.Item4);
                        XDK.WriteFloat(CameraPitch, interpolatedPosition.Item5);
                        XDK.WriteFloat(CameraRoll, interpolatedPosition.Item6);

                        await Task.Delay((int)(1000 / fps));
                        currentFrame++;
                    }
                }

                pathStart_checkbox.Checked = false;
            }
            catch (Exception ex)
            {
                pathStart_checkbox.Checked = false;
            }

        }

        private void SetupDataGridView()
        {
            keyframeDataGridView.ColumnCount = 8;

            keyframeDataGridView.Columns[0].Name = "X";
            keyframeDataGridView.Columns[1].Name = "Y";
            keyframeDataGridView.Columns[2].Name = "Z";
            keyframeDataGridView.Columns[3].Name = "Yaw";
            keyframeDataGridView.Columns[4].Name = "Pitch";
            keyframeDataGridView.Columns[5].Name = "Roll";
            keyframeDataGridView.Columns[6].Name = "Transition Time";

            keyframeDataGridView.AllowUserToAddRows = false;
            keyframeDataGridView.AllowUserToDeleteRows = false;
            keyframeDataGridView.AllowUserToOrderColumns = false;
            keyframeDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            keyframeDataGridView.MultiSelect = false;

            keyframeDataGridView.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            keyframeDataGridView.Dock = DockStyle.Fill;
        }

        private void AddKeyPointRow(float x, float y, float z, float yaw, float pitch, float roll, float transitionTime)
        {
            keyframeDataGridView.Rows.Add(x, y, z, yaw, pitch, roll, transitionTime);
        }

        void addKey()
        {
            float x = XDK.ReadFloat(CameraX);
            float y = XDK.ReadFloat(CameraY);
            float z = XDK.ReadFloat(CameraZ);
            float yaw = XDK.ReadFloat(CameraYaw);
            float pitch = XDK.ReadFloat(CameraPitch);
            float roll = XDK.ReadFloat(CameraRoll);
            float transitionTime = 1;

            AddKeyPointRow(x, y, z, yaw, pitch, roll, transitionTime);
        }

        void rotateCameraClockwise()
        {
            float current = XDK.ReadFloat(CameraX);
            float moveAng = current + 0.1f;
            XDK.WriteFloat(CameraRoll, moveAng);
        }

        void rotateCameraCounterclockwise()
        {
            float current = XDK.ReadFloat(CameraX);
            float moveAng = current - 0.1f;
            XDK.WriteFloat(CameraRoll, moveAng);
        }

        void unlockUI()
        {
            //groupBox1.Enabled = true;
            groupBox8.Enabled = true;
            groupBox9.Enabled = true;
            BackColor = Color.FromArgb(245, 245, 245);
        }

        private void AOBCameraButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("This should only take about a minute (Make sure you're in first person, on forgeworld, and in custom games!)");
            Console.Write("Starting AOB scans... ");

            byte[] CameraBytes = new byte[] { 0x0E, 0x90, 0xE2, 0xB3, 0x00, 0x44, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0xAE, 0x40, 0xF1 };
            byte[] CameraAOB = XDK.GetMemory(0xC2000000, 0x6FFFFFF);

            Console.WriteLine("Done.");

            for (int i = 0; i < CameraAOB.Length - CameraBytes.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < CameraBytes.Length; j++)
                {
                    if (CameraAOB[i + j] != CameraBytes[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    int address = (int)(0xC2000000 + i);
                    Console.WriteLine("Camera Address: 0x" + address.ToString("X"));

                    Camera = (uint)address;
                    CameraX = Camera + 0x0E;
                    CameraY = CameraX + 0x04;
                    CameraZ = CameraY + 0x04;
                    CameraYaw = CameraZ + 0x04;
                    CameraPitch = CameraYaw + 0x04;
                    CameraRoll = CameraPitch + 0x04;

                    cameraAddresTextbox.Text = "0x" + address.ToString("X");
                    unlockUI();
                    break;
                }
            }
        }

        private void SetAddressButton_Click(object sender, EventArgs e)
        {
            string textaddress = cameraAddresTextbox.Text;

            uint address = Convert.ToUInt32(textaddress, 16);

            Camera = (uint)address;
            CameraX = Camera + 0x0E;
            CameraY = CameraX + 0x04;
            CameraZ = CameraY + 0x04;
            CameraYaw = CameraZ + 0x04;
            CameraPitch = CameraYaw + 0x04;
            CameraRoll = CameraPitch + 0x04;

            Console.WriteLine("Camera Address: 0x" + address.ToString("X"));
            unlockUI();
        }

        private async void SaveAndSmooth_Button_Click(object sender, EventArgs e)
        {
            SaveMasterFrames();

            //await ApplySmoothOnPath();
        }


        private void importPath_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string json = File.ReadAllText(openFileDialog.FileName);
                    List<KeypointExports> keyPoints = JsonConvert.DeserializeObject<List<KeypointExports>>(json);

                    // Clear existing rows
                    keyframeDataGridView.Rows.Clear();

                    // Add imported key points to DataGridView
                    foreach (KeypointExports keypoint in keyPoints)
                    {
                        AddKeyPointRow(keypoint.xPos, keypoint.yPos, keypoint.zPos, keypoint.yawAng, keypoint.pitchAng, keypoint.rollAng, keypoint.transitionTime);
                    }

                    MessageBox.Show("Key points imported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error importing key points: " + ex.Message);
                }
            }
        }

        private void exportPath_Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveMasterFrames(); // Ensure master frames are up to date
                    string json = JsonConvert.SerializeObject(masterFrames.Select(kp => new KeypointExports
                    {
                        xPos = kp.Item1,
                        yPos = kp.Item2,
                        zPos = kp.Item3,
                        yawAng = kp.Item4,
                        pitchAng = kp.Item5,
                        rollAng = kp.Item6,
                        transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[masterFrames.IndexOf(kp)].Cells["Transition Time"].Value)
                    }));
                    File.WriteAllText(saveFileDialog.FileName, json);
                    MessageBox.Show("Key points exported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting key points: " + ex.Message);
                }
            }
        }

        private void deleteKey_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                keyframeDataGridView.Rows.RemoveAt(keyframeDataGridView.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void resetCameraRotation_Button_Click(object sender, EventArgs e)
        {
            XDK.WriteFloat(CameraYaw, 100000f);
            XDK.WriteFloat(CameraPitch, 100000f);
            XDK.WriteFloat(CameraRoll, 100000f);
        }

        private void teleportToSelection_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                float x = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["X"].Value);
                float y = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Y"].Value);
                float z = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Z"].Value);
                float yaw = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Yaw"].Value);
                float pitch = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Pitch"].Value);
                float roll = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Roll"].Value);

                XDK.WriteFloat(CameraX, x);
                XDK.WriteFloat(CameraY, y);
                XDK.WriteFloat(CameraZ, z);
                XDK.WriteFloat(CameraYaw, yaw);
                XDK.WriteFloat(CameraPitch, pitch);
                XDK.WriteFloat(CameraRoll, roll);
            }
            else
            {
                Console.WriteLine("Please select a row to teleport to!");
            }
        }

        private void replaceCurrent_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                float x = XDK.ReadFloat(CameraX);
                float y = XDK.ReadFloat(CameraY);
                float z = XDK.ReadFloat(CameraZ);
                float yaw = XDK.ReadFloat(CameraYaw);
                float pitch = XDK.ReadFloat(CameraPitch);
                float roll = XDK.ReadFloat(CameraRoll);
                float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value);

                keyframeDataGridView.Rows[selectedIndex].Cells["X"].Value = x;
                keyframeDataGridView.Rows[selectedIndex].Cells["Y"].Value = y;
                keyframeDataGridView.Rows[selectedIndex].Cells["Z"].Value = z;
                keyframeDataGridView.Rows[selectedIndex].Cells["Yaw"].Value = yaw;
                keyframeDataGridView.Rows[selectedIndex].Cells["Pitch"].Value = pitch;
                keyframeDataGridView.Rows[selectedIndex].Cells["Roll"].Value = roll;
                keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value = transitionTime;
            }
            else
            {
                MessageBox.Show("Please select a row to replace.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void teleportCamera_Button_Click(object sender, EventArgs e)
        {
            //memory.WriteMemory(xPos, "float", teleportX.Text.ToString());
            //memory.WriteMemory(yPos, "float", teleportY.Text.ToString());
            //memory.WriteMemory(zPos, "float", teleportZ.Text.ToString());
        }

        private void clearList_Button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all rows from the list?\n(You are going lose everything!)", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

            if (result == DialogResult.Yes)
            {
                keyframeDataGridView.Rows.Clear();
            }
        }

        private void teleportToPlayer_Button_Click(object sender, EventArgs e)
        {
            XDK.WriteFloat(CameraX, 100000f);
            XDK.WriteFloat(CameraY, 100000f);
            XDK.WriteFloat(CameraZ, 100000f);
        }

        private void dupeSelection_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                float x = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["X"].Value);
                float y = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Y"].Value);
                float z = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Z"].Value);
                float yaw = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Yaw"].Value);
                float pitch = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Pitch"].Value);
                float roll = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Roll"].Value);
                float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value);

                AddKeyPointRow(x, y, z, yaw, pitch, roll, transitionTime);
            }
            else
            {
                MessageBox.Show("Please select a row to duplicate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sortUp_button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                if (selectedIndex > 0)
                {
                    DataGridViewRow selectedRow = keyframeDataGridView.Rows[selectedIndex];
                    keyframeDataGridView.Rows.Remove(selectedRow);
                    keyframeDataGridView.Rows.Insert(selectedIndex - 1, selectedRow);
                    keyframeDataGridView.Rows[selectedIndex - 1].Selected = true;
                }
                else
                {
                    MessageBox.Show("The selected row is already at the top.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to sort up.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sortDown_button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                if (selectedIndex < keyframeDataGridView.Rows.Count - 1)
                {
                    DataGridViewRow selectedRow = keyframeDataGridView.Rows[selectedIndex];
                    keyframeDataGridView.Rows.Remove(selectedRow);
                    keyframeDataGridView.Rows.Insert(selectedIndex + 1, selectedRow);
                    keyframeDataGridView.Rows[selectedIndex + 1].Selected = true;
                }
                else
                {
                    MessageBox.Show("The selected row is already at the bottom.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to sort down.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void teleportToLookTarget_Click(object sender, EventArgs e)
        {

        }

        private void lookTracking_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pathStart_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (pathStart_checkbox.Checked == true)
            {
                MoveCamera();
            }
            else if (pathStart_checkbox.Checked == false)
            {
                pathStart_checkbox.Checked = false;
                return;
            }
        }

        private void importPathWithOffset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Warning!\n\nYou will lose any unsaved progress.\nAre you sure you want to import a new existing path?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

            if (result == DialogResult.Yes)
            {
                float currentX = XDK.ReadFloat(CameraX);
                float currentY = XDK.ReadFloat(CameraY);
                float currentZ = XDK.ReadFloat(CameraZ);

                OpenFileDialog openFileDialog = new OpenFileDialog();
                string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string subfolderPath = Path.Combine(programDirectory, "paths");
                openFileDialog.InitialDirectory = subfolderPath;
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string json = File.ReadAllText(openFileDialog.FileName);
                        List<KeypointExports> keyPoints = JsonConvert.DeserializeObject<List<KeypointExports>>(json);

                        keyframeDataGridView.Rows.Clear();

                        float offsetX = currentX - keyPoints[0].xPos;
                        float offsetY = currentY - keyPoints[0].yPos;
                        float offsetZ = currentZ - keyPoints[0].zPos;

                        foreach (KeypointExports keypoint in keyPoints)
                        {
                            float newX = keypoint.xPos + offsetX;
                            float newY = keypoint.yPos + offsetY;
                            float newZ = keypoint.zPos + offsetZ;

                            AddKeyPointRow(newX, newY, newZ, keypoint.yawAng, keypoint.pitchAng, keypoint.rollAng, keypoint.transitionTime);
                        }

                        MessageBox.Show("Key points imported with offset successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error importing key points with offset: " + ex.Message);
                    }
                }
            }
        }

        List<Tuple<float, float, float, float, float, float>> masterFrames = new List<Tuple<float, float, float, float, float, float>>();

        // Method to save master frames
        private void SaveMasterFrames()
        {
            masterFrames.Clear(); // Clear existing frames

            // Iterate through each row in the keyframeDataGridView
            foreach (DataGridViewRow row in keyframeDataGridView.Rows)
            {
                if (row.IsNewRow) // Skip the new row if present
                    continue;

                // Extract data from the row and create a tuple representing the frame
                float x = Convert.ToSingle(row.Cells["X"].Value);
                float y = Convert.ToSingle(row.Cells["Y"].Value);
                float z = Convert.ToSingle(row.Cells["Z"].Value);
                float yaw = Convert.ToSingle(row.Cells["Yaw"].Value);
                float pitch = Convert.ToSingle(row.Cells["Pitch"].Value);
                float roll = Convert.ToSingle(row.Cells["Roll"].Value);

                // Add the frame to the masterFrames list
                masterFrames.Add(new Tuple<float, float, float, float, float, float>(x, y, z, yaw, pitch, roll));
            }
        }

        private void startPath_Button_Click(object sender, EventArgs e)
        {
            cameraThread = new Thread(async () => { await MoveCamera(); });

            cameraThread.Start();
        }

        private void AddKey_Button_Click(object sender, EventArgs e)
        {
            addKey();
        }

        private void FreezeConsoleButton_Click(object sender, EventArgs e)
        {
            XDK.xbCon.DebugTarget.Stop(out stopped);
        }

        private void Unfreeze_Click(object sender, EventArgs e)
        {
            XDK.xbCon.DebugTarget.Go(out stopped);
        }
    }
}