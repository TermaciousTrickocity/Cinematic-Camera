using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        List<Tuple<float, float, float, float, float, float, float>> keyFrames = new List<Tuple<float, float, float, float, float, float, float>>();

        private Thread cameraThread;

        public string xPos; // Scan for float; Camera 'X'
        public string yPos; // +4
        public string zPos; // +8
        public string yawAng; // +12
        public string pitchAng; // +16
        public string rollAng; // +20
        public string playerFov; // Scan for float;

        bool lookTracking = false;
        public string trackingTargetAddress;
        private Vector3 targetPosition;

        private string mapHeader;

        private void SetupDataGridView()
        {
            keyframeDataGridView.ColumnCount = 8;

            keyframeDataGridView.Columns[0].Name = "X";
            keyframeDataGridView.Columns[1].Name = "Y";
            keyframeDataGridView.Columns[2].Name = "Z";
            keyframeDataGridView.Columns[3].Name = "Yaw";
            keyframeDataGridView.Columns[4].Name = "Pitch";
            keyframeDataGridView.Columns[5].Name = "Roll";
            keyframeDataGridView.Columns[6].Name = "FOV";
            keyframeDataGridView.Columns[7].Name = "Transition Time";

            keyframeDataGridView.AllowUserToAddRows = false;
            keyframeDataGridView.AllowUserToDeleteRows = false;
            keyframeDataGridView.AllowUserToOrderColumns = false;
            keyframeDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            keyframeDataGridView.MultiSelect = false;
        }

        private void AddKeyPointRow(float x, float y, float z, float yaw, float pitch, float roll, float fov, float transitionTime)
        {
            keyframeDataGridView.Rows.Add(x, y, z, yaw, pitch, roll, fov, transitionTime);
        }

        static Tuple<float, float, float, float, float, float, float> CatmullRomInterpolation(Tuple<float, float, float, float, float, float, float> p0, Tuple<float, float, float, float, float, float, float> p1, Tuple<float, float, float, float, float, float, float> p2, Tuple<float, float, float, float, float, float, float> p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;

            float c0 = -0.5f * t3 + t2 - 0.5f * t;
            float c1 = 1.5f * t3 - 2.5f * t2 + 1.0f;
            float c2 = -1.5f * t3 + 2.0f * t2 + 0.5f * t;
            float c3 = 0.5f * t3 - 0.5f * t2;

            float x = c0 * p0.Item1 + c1 * p1.Item1 + c2 * p2.Item1 + c3 * p3.Item1;
            float y = c0 * p0.Item2 + c1 * p1.Item2 + c2 * p2.Item2 + c3 * p3.Item2;
            float z = c0 * p0.Item3 + c1 * p1.Item3 + c2 * p2.Item3 + c3 * p3.Item3;
            float yaw = c0 * p0.Item4 + c1 * p1.Item4 + c2 * p2.Item4 + c3 * p3.Item4;
            float pitch = c0 * p0.Item5 + c1 * p1.Item5 + c2 * p2.Item5 + c3 * p3.Item5;
            float roll = c0 * p0.Item6 + c1 * p1.Item6 + c2 * p2.Item6 + c3 * p3.Item6;
            float fov = c0 * p0.Item7 + c1 * p1.Item7 + c2 * p2.Item7 + c3 * p3.Item7;

            return new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, fov);
        }

        public async Task MoveCamera()
        {
            try
            {
                int originalSelectedRow = keyframeDataGridView.CurrentCell.RowIndex;
                int originalSelectedColumn = keyframeDataGridView.CurrentCell.ColumnIndex;

                float targetHz = float.Parse(hzTextbox.Text);
                int delayMs = (int)(1000f / targetHz);

                List<Tuple<float, float, float, float, float, float, float>> keyPoints = new List<Tuple<float, float, float, float, float, float, float>>();
                List<float> transitionTimes = new List<float>();

                int startIndex;
                if (startFromSelection_checkbox.Checked && keyframeDataGridView.SelectedRows.Count > 0)
                    startIndex = keyframeDataGridView.SelectedRows[0].Index;
                else
                    startIndex = 0;

                for (int i = startIndex; i < keyframeDataGridView.Rows.Count; i++)
                {
                    float x = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["X"].Value);
                    float y = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Y"].Value);
                    float z = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Z"].Value);
                    float yaw = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Yaw"].Value);
                    float pitch = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Pitch"].Value);
                    float roll = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Roll"].Value);
                    float playerFovVal = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["FOV"].Value);
                    float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Transition Time"].Value);

                    keyPoints.Add(new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, playerFovVal));
                    transitionTimes.Add(transitionTime);
                }

                for (int i = 0; i < keyPoints.Count - 1; i++)
                {
                    var p0 = (i == 0) ? keyPoints[0] : keyPoints[i - 1];
                    var p1 = keyPoints[i];
                    var p2 = keyPoints[i + 1];
                    var p3 = (i + 2 < keyPoints.Count) ? keyPoints[i + 2] : p2;

                    float segmentTime = transitionTimes[i];

                    Stopwatch sw = Stopwatch.StartNew();

                    while (sw.Elapsed.TotalSeconds < segmentTime)
                    {
                        if (!pathStart_checkbox.Checked) return;

                        float t = (float)(sw.Elapsed.TotalSeconds / segmentTime);
                        t = Math.Clamp(t, 0f, 1f);

                        var interpolatedPosition = CatmullRomInterpolation(p0, p1, p2, p3, t);

                        memory.WriteMemory(xPos, "float", $"{interpolatedPosition.Item1}");
                        memory.WriteMemory(yPos, "float", $"{interpolatedPosition.Item2}");
                        memory.WriteMemory(zPos, "float", $"{interpolatedPosition.Item3}");

                        if (lookTracking == true)
                        {
                            int selectedIndex = trackListCombo.SelectedIndex;

                            long baseAddr = long.Parse(trackingTargetAddress, System.Globalization.NumberStyles.HexNumber);
                            long offset = 0;

                            offset = (selectedIndex == 0) ? 0xA8 : 0x538 + (selectedIndex - 1) * 0x490;

                            long trackXAddr = baseAddr + offset;
                            long trackYAddr = trackXAddr + 0x04;
                            long trackZAddr = trackXAddr + 0x08;

                            string trackXHex = "0x" + trackXAddr.ToString("X");
                            string trackYHex = "0x" + trackYAddr.ToString("X");
                            string trackZHex = "0x" + trackZAddr.ToString("X");

                            targetPosition = new Vector3(memory.ReadFloat(trackXHex, "", false), memory.ReadFloat(trackYHex, "", false), memory.ReadFloat(trackZHex, "", false));

                            Vector3 cameraPosition = new Vector3(memory.ReadFloat(xPos, "", false), memory.ReadFloat(yPos, "", false), memory.ReadFloat(zPos, "", false));
                            Vector3 direction = targetPosition - cameraPosition;

                            float yaw = (float)Math.Atan2(direction.Y, direction.X) % (2 * (float)Math.PI);
                            float pitch = (float)Math.Atan2(direction.Z, direction.Length()) % (2 * (float)Math.PI);

                            memory.WriteMemory(yawAng, "float", $"{yaw}");
                            memory.WriteMemory(pitchAng, "float", $"{pitch}");
                        }
                        else
                        {
                            memory.WriteMemory(yawAng, "float", $"{interpolatedPosition.Item4}");
                            memory.WriteMemory(pitchAng, "float", $"{interpolatedPosition.Item5}");
                        }

                        memory.WriteMemory(rollAng, "float", $"{interpolatedPosition.Item6}");
                        memory.WriteMemory(playerFov, "float", $"{interpolatedPosition.Item7}");

                        await Task.Delay(delayMs);
                    }
                }

                pathStart_checkbox.Checked = false;
                keyframeDataGridView.Rows[originalSelectedRow].Cells[originalSelectedColumn].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK);
                pathStart_checkbox.Checked = false;
            }
        }

        public async Task getPlayerList()
        {
            trackListCombo.Items.Clear();

            int[] offsets = // diff 0x490
            {
                0x120,      // Player 0
                0x5B0,      // Player 1
                0xA40,      // ...
                0xED0,      // ..
                0x1360,     // .
                0x17F0,     // 
                0x1C80,
                0x2110,     // you get the point
                0x25A0,
                0x2A30,
                0x2EC0,
                0x3350,
                0x37E0,
                0x3C70,
                0x4100,
                0x4590      // Player 16
            };

            long baseAddr = long.Parse(trackingTargetAddress, System.Globalization.NumberStyles.HexNumber);

            foreach (int offset in offsets)
            {
                string currentAddress = (baseAddr + offset).ToString("X");
                byte[] name = memory.ReadBytes(currentAddress, 30);
                string result = Encoding.Unicode.GetString(name);

                if (result == null) 
                    break;

                trackListCombo.Items.Add(result);
            }
        }
    }
}