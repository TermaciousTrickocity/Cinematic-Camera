using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        private Thread cameraThread;

        private bool lookTracking = false;
        private Vector3 targetPosition;
        private string theaterTime;
        public float startSync;
        public string trackingTargetAddress;

        public int startTick;

        public string xPos;
        public string yPos;
        public string zPos;
        public string yawAng;
        public string pitchAng;
        public string rollAng;
        public string playerFov;
        public string tickCount;
        public string tickSpeed;
        public string playerList;


        public List<string> playerListOffsets = new List<string>();

        List<Keyframe> keyFrames = new List<Keyframe>();

        private void SetupDataGridView()
        {
            keyframeDataGridView.ColumnCount = 9;

            keyframeDataGridView.Columns[0].Name = "X";
            keyframeDataGridView.Columns[1].Name = "Y";
            keyframeDataGridView.Columns[2].Name = "Z";
            keyframeDataGridView.Columns[3].Name = "Yaw";
            keyframeDataGridView.Columns[4].Name = "Pitch";
            keyframeDataGridView.Columns[5].Name = "Roll";
            keyframeDataGridView.Columns[6].Name = "FOV";
            keyframeDataGridView.Columns[7].Name = "Transition Time";
            keyframeDataGridView.Columns[8].Name = "Tick Speed";

            keyframeDataGridView.AllowUserToAddRows = false;
            keyframeDataGridView.AllowUserToDeleteRows = false;
            keyframeDataGridView.AllowUserToOrderColumns = false;
            keyframeDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            keyframeDataGridView.MultiSelect = false;
        }

        private void AddKeyPointRow(float x, float y, float z, float yaw, float pitch, float roll, float fov, float transitionTime, float tickSpeed)
        {
            keyframeDataGridView.Rows.Add(x, y, z, yaw, pitch, roll, fov, transitionTime, tickSpeed);
        }

        public class Keyframe
        {
            public float X, Y, Z;
            public float Yaw, Pitch, Roll;
            public float FOV;
            public float TickSpeed;
            public float TransitionTime;

            public Keyframe(float x, float y, float z, float yaw, float pitch, float roll, float fov, float transitionTime, float tickSpeed)
            {
                X = x; Y = y; Z = z;
                Yaw = yaw; Pitch = pitch; Roll = roll;
                FOV = fov;
                TransitionTime = transitionTime;
                TickSpeed = tickSpeed;
            }
        }

        static Keyframe CatmullRomInterpolation(Keyframe p0, Keyframe p1, Keyframe p2, Keyframe p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;

            float c0 = -0.5f * t3 + t2 - 0.5f * t;
            float c1 = 1.5f * t3 - 2.5f * t2 + 1.0f;
            float c2 = -1.5f * t3 + 2.0f * t2 + 0.5f * t;
            float c3 = 0.5f * t3 - 0.5f * t2;

            float x = c0 * p0.X + c1 * p1.X + c2 * p2.X + c3 * p3.X;
            float y = c0 * p0.Y + c1 * p1.Y + c2 * p2.Y + c3 * p3.Y;
            float z = c0 * p0.Z + c1 * p1.Z + c2 * p2.Z + c3 * p3.Z;
            float yaw = c0 * p0.Yaw + c1 * p1.Yaw + c2 * p2.Yaw + c3 * p3.Yaw;
            float pitch = c0 * p0.Pitch + c1 * p1.Pitch + c2 * p2.Pitch + c3 * p3.Pitch;
            float roll = c0 * p0.Roll + c1 * p1.Roll + c2 * p2.Roll + c3 * p3.Roll;
            float fov = c0 * p0.FOV + c1 * p1.FOV + c2 * p2.FOV + c3 * p3.FOV;
            float tick = c0 * p0.TickSpeed + c1 * p1.TickSpeed + c2 * p2.TickSpeed + c3 * p3.TickSpeed;

            return new Keyframe(x, y, z, yaw, pitch, roll, fov, 0, tick);
        }

        public async Task MoveCamera()
        {
            try
            {
                // Tick timesync
                if (timesyncCheckbox.Checked && tickCount != null)
                {
                    while (true)
                    {
                        int time = memory.ReadInt(tickCount);
                        if (time >= startTick) break;
                    }
                }

                // Delay start
                if (int.TryParse(StartDelayTextbox.Text, out int delayStartSeconds) && delayStartSeconds > 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(delayStartSeconds));
                }

                int originalSelectedRow = keyframeDataGridView.CurrentCell.RowIndex;
                int originalSelectedColumn = keyframeDataGridView.CurrentCell.ColumnIndex;

                float targetHz = float.Parse(hzTextbox.Text);
                int delayMs = (int)(1000f / targetHz);

                keyFrames.Clear();

                int startIndex = (startFromSelection_checkbox.Checked && keyframeDataGridView.SelectedRows.Count > 0) ? keyframeDataGridView.SelectedRows[0].Index : 0;

                for (int i = startIndex; i < keyframeDataGridView.Rows.Count; i++)
                {
                    var row = keyframeDataGridView.Rows[i];
                    float x = Convert.ToSingle(row.Cells["X"].Value);
                    float y = Convert.ToSingle(row.Cells["Y"].Value);
                    float z = Convert.ToSingle(row.Cells["Z"].Value);
                    float yaw = Convert.ToSingle(row.Cells["Yaw"].Value);
                    float pitch = Convert.ToSingle(row.Cells["Pitch"].Value);
                    float roll = Convert.ToSingle(row.Cells["Roll"].Value);
                    float fov = Convert.ToSingle(row.Cells["FOV"].Value);
                    float transitionTime = Convert.ToSingle(row.Cells["Transition Time"].Value);
                    float tick = Convert.ToSingle(row.Cells["Tick Speed"].Value);

                    keyFrames.Add(new Keyframe(x, y, z, yaw, pitch, roll, fov, transitionTime, tick));
                }

                for (int i = 0; i < keyFrames.Count - 1; i++)
                {
                    var p0 = (i == 0) ? keyFrames[0] : keyFrames[i - 1];
                    var p1 = keyFrames[i];
                    var p2 = keyFrames[i + 1];
                    var p3 = (i + 2 < keyFrames.Count) ? keyFrames[i + 2] : p2;

                    float segmentTime = p1.TransitionTime;

                    Stopwatch sw = Stopwatch.StartNew();

                    while (sw.Elapsed.TotalSeconds < segmentTime)
                    {
                        if (!pathStart_checkbox.Checked) return;

                        float t = (float)(sw.Elapsed.TotalSeconds / segmentTime);
                        t = Math.Clamp(t, 0f, 1f);

                        var interpolated = CatmullRomInterpolation(p0, p1, p2, p3, t);

                        memory.WriteMemory(xPos, "float", $"{interpolated.X}");
                        memory.WriteMemory(yPos, "float", $"{interpolated.Y}");
                        memory.WriteMemory(zPos, "float", $"{interpolated.Z}");
                        memory.WriteMemory(yawAng, "float", $"{interpolated.Yaw}");
                        memory.WriteMemory(pitchAng, "float", $"{interpolated.Pitch}");
                        memory.WriteMemory(rollAng, "float", $"{interpolated.Roll}");
                        memory.WriteMemory(playerFov, "float", $"{interpolated.FOV}");
                        
                        if (tickSpeed != null)
                        {
                            memory.WriteMemory(tickSpeed, "float", $"{interpolated.TickSpeed}");
                        }

                        if (lookTracking && trackingTargetAddress != null)
                        {
                            int selectedIndex = trackListCombo.SelectedIndex;
                            long baseAddr = long.Parse(trackingTargetAddress, System.Globalization.NumberStyles.HexNumber);
                            long offset = (selectedIndex == 0) ? 0xA8 : 0x538 + (selectedIndex - 1) * 0x490;

                            long trackXAddr = baseAddr + offset;
                            long trackYAddr = trackXAddr + 0x04;
                            long trackZAddr = trackXAddr + 0x08;

                            Vector3 cameraPosition = new Vector3(
                                memory.ReadFloat(xPos, "", false),
                                memory.ReadFloat(yPos, "", false),
                                memory.ReadFloat(zPos, "", false)
                            );

                            targetPosition = new Vector3(
                                memory.ReadFloat("0x" + trackXAddr.ToString("X"), "", false),
                                memory.ReadFloat("0x" + trackYAddr.ToString("X"), "", false),
                                memory.ReadFloat("0x" + trackZAddr.ToString("X"), "", false)
                            );

                            Vector3 direction = targetPosition - cameraPosition;
                            float yaw = (float)Math.Atan2(direction.Y, direction.X) % (2 * (float)Math.PI);
                            float pitch = (float)Math.Atan2(direction.Z, direction.Length()) % (2 * (float)Math.PI);

                            memory.WriteMemory(yawAng, "float", $"{yaw}");
                            memory.WriteMemory(pitchAng, "float", $"{pitch}");
                        }

                        await Task.Delay(1);
                    }
                }

                pathStart_checkbox.Checked = false;
                keyframeDataGridView.Rows[originalSelectedRow].Cells[originalSelectedColumn].Selected = true;

                // Delay end + just end after path is done
                if (pauseTicks.Checked == true && tickSpeed != null)
                {
                    if (int.TryParse(endDelay.Text, out int delayEndSeconds) && delayEndSeconds > 0)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(delayEndSeconds));
                    }

                    memory.WriteMemory(tickSpeed, "float", $"0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
                pathStart_checkbox.Checked = false;
            }
        }
        void setCurrentTick()
        {
            startTick = memory.ReadInt(tickCount);
            timeSyncTextbox.Text = startTick.ToString();
        }

        public async Task getPlayerList()
        {
            trackListCombo.Items.Clear();

            List<int> offsetsList = new List<int>();

            if (playerListOffsets != null && playerListOffsets.Count > 0)
            {
                foreach (var s in playerListOffsets)
                {
                    if (string.IsNullOrWhiteSpace(s)) continue;
                    string t = s.Trim();

                    try
                    {
                        int parsed;
                        if (t.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        {
                            parsed = int.Parse(t.Substring(2), NumberStyles.HexNumber);
                        }
                        else
                        {
                            bool hasHexChar = t.IndexOfAny(new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f' }) >= 0;
                            parsed = hasHexChar
                                ? int.Parse(t, NumberStyles.HexNumber)
                                : int.Parse(t, NumberStyles.Integer);
                        }

                        offsetsList.Add(parsed);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            long baseAddr = long.Parse(trackingTargetAddress, System.Globalization.NumberStyles.HexNumber);

            foreach (int offset in offsetsList)
            {
                string currentAddress = (baseAddr + offset).ToString("X");
                byte[] name = memory.ReadBytes(currentAddress, 30);
                string result = Encoding.Unicode.GetString(name).TrimEnd('\0');

                if (string.IsNullOrEmpty(result))
                    break;

                trackListCombo.Items.Add(result);
            }
        }
    }
}
