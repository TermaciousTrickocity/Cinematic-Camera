using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        private void importPath_Button_Click(object sender, EventArgs e)
        {
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

                    foreach (KeypointExports keypoint in keyPoints)
                    {
                        AddKeyPointRow(keypoint.xPos, keypoint.yPos, keypoint.zPos, keypoint.yawAng, keypoint.pitchAng, keypoint.rollAng, keypoint.playerFov, keypoint.transitionTime, keypoint.tickSpeed);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void exportPath_Button_Click(object sender, EventArgs e)
        {
            int dataRowCount = keyframeDataGridView.Rows.Cast<System.Windows.Forms.DataGridViewRow>().Count(r => !r.IsNewRow);
            if (dataRowCount == 0)
            {
                MessageBox.Show("There are no key points to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string subfolderPath = Path.Combine(programDirectory, "paths");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = subfolderPath;
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<KeypointExports> exports = new List<KeypointExports>();

                    foreach (DataGridViewRow row in keyframeDataGridView.Rows)
                    {
                        if (row.IsNewRow)
                            continue;

                        float ParseCell(object val)
                        {
                            if (val == null) return 0f;
                            if (val is float f) return f;
                            var s = val.ToString();
                            if (string.IsNullOrWhiteSpace(s)) return 0f;
                            if (float.TryParse(s, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.InvariantCulture, out var r))
                                return r;
                            if (float.TryParse(s, out r))
                                return r;
                            return 0f;
                        }

                        var kp = new KeypointExports
                        {
                            xPos = ParseCell(row.Cells["X"].Value),
                            yPos = ParseCell(row.Cells["Y"].Value),
                            zPos = ParseCell(row.Cells["Z"].Value),
                            yawAng = ParseCell(row.Cells["Yaw"].Value),
                            pitchAng = ParseCell(row.Cells["Pitch"].Value),
                            rollAng = ParseCell(row.Cells["Roll"].Value),
                            playerFov = ParseCell(row.Cells["FOV"].Value),
                            transitionTime = ParseCell(row.Cells["Transition Time"].Value),
                            tickSpeed = ParseCell(row.Cells["Tick Speed"].Value)
                        };

                        exports.Add(kp);
                    }

                    string json = JsonConvert.SerializeObject(exports, Formatting.Indented);
                    File.WriteAllText(saveFileDialog.FileName, json);
                }
                catch (Exception ex)
                {

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
            memory.WriteMemory(yawAng, "float", "0");
            memory.WriteMemory(pitchAng, "float", "0");
            memory.WriteMemory(rollAng, "float", "0");
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
                float fov = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["FOV"].Value);

                memory.WriteMemory(xPos, "float", $"{x}");
                memory.WriteMemory(yPos, "float", $"{y}");
                memory.WriteMemory(zPos, "float", $"{z}");
                memory.WriteMemory(yawAng, "float", $"{yaw}");
                memory.WriteMemory(pitchAng, "float", $"{pitch}");
                memory.WriteMemory(rollAng, "float", $"{roll}");
                memory.WriteMemory(playerFov, "float", $"{fov}");
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
                float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value);

                keyframeDataGridView.Rows[selectedIndex].Cells["X"].Value = memory.ReadFloat(xPos, "", false);
                keyframeDataGridView.Rows[selectedIndex].Cells["Y"].Value = memory.ReadFloat(yPos, "", false);
                keyframeDataGridView.Rows[selectedIndex].Cells["Z"].Value = memory.ReadFloat(zPos, "", false);
                keyframeDataGridView.Rows[selectedIndex].Cells["Yaw"].Value = memory.ReadFloat(yawAng, "", false);
                keyframeDataGridView.Rows[selectedIndex].Cells["Pitch"].Value = memory.ReadFloat(pitchAng, "", false);
                keyframeDataGridView.Rows[selectedIndex].Cells["Roll"].Value = memory.ReadFloat(rollAng, "", false);
                keyframeDataGridView.Rows[selectedIndex].Cells["FOV"].Value = memory.ReadFloat(playerFov);
                keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value = transitionTime;
                // keep existing Tick Speed value untouched to avoid losing user data
            }
        }


        private void clearList_Button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all rows from the list?\n(You are going lose everything!)", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

            if (result == DialogResult.Yes)
            {
                keyframeDataGridView.Rows.Clear();
            }
        }

        private void dupeSelection_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                float GetCellFloat(DataGridViewRow r, string name)
                {
                    var v = r.Cells[name].Value;
                    if (v == null) return 0f;
                    return Convert.ToSingle(v);
                }

                AddKeyPointRow(
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "X"),
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "Y"),
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "Z"),
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "Yaw"),
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "Pitch"),
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "Roll"),
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "FOV"),
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "Transition Time"),
                    GetCellFloat(keyframeDataGridView.Rows[selectedIndex], "Tick Speed")
                );
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
                float currentX = memory.ReadFloat(xPos);
                float currentY = memory.ReadFloat(yPos);
                float currentZ = memory.ReadFloat(zPos);

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

                            AddKeyPointRow(newX, newY, newZ, keypoint.yawAng, keypoint.pitchAng, keypoint.rollAng, keypoint.playerFov, keypoint.transitionTime, keypoint.tickSpeed);
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

        private void startPath_Button_Click(object sender, EventArgs e)
        {
            cameraThread = new Thread(async () => { await MoveCamera(); });

            cameraThread.Start();
        }

        private void AddKey_Button_Click(object sender, EventArgs e)
        {
            AddKeyPointRow(memory.ReadFloat(xPos, "", false),
                memory.ReadFloat(yPos, "", false), 
                memory.ReadFloat(zPos, "", false), 
                memory.ReadFloat(yawAng, "", false), 
                memory.ReadFloat(pitchAng, "", false),
                memory.ReadFloat(rollAng, "", false), 
                memory.ReadFloat(playerFov, "", false), 
                1,
                1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //lookTracking = !lookTracking;
        }

        private void setTop_CheckedChanged(object sender, EventArgs e)
        {
            if (setTop.Checked == true)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void StartDelayTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                e.Handled = true;
            }
        }

        private void hzTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                e.Handled = true;
            }
        }

        private void TimeSyncTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                e.Handled = true;
            }
        }

        private void EndDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                e.Handled = true;
            }
        }
    }
}
