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
                        AddKeyPointRow(keypoint.xPos, keypoint.yPos, keypoint.zPos, keypoint.yawAng, keypoint.pitchAng, keypoint.rollAng, keypoint.playerFov, keypoint.transitionTime);
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
                    string json = JsonConvert.SerializeObject(keyFrames.Select(kp => new KeypointExports
                    {
                        xPos = kp.Item1,
                        yPos = kp.Item2,
                        zPos = kp.Item3,
                        yawAng = kp.Item4,
                        pitchAng = kp.Item5,
                        rollAng = kp.Item6,
                        playerFov = kp.Item7,
                        transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[keyFrames.IndexOf(kp)].Cells["Transition Time"].Value)
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
            memory.WriteMemory(yawAng, "Float", "0");
            memory.WriteMemory(pitchAng, "Float", "0");
            memory.WriteMemory(rollAng, "Float", "0");
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
            }
        }

        private void teleportCamera_Button_Click(object sender, EventArgs e)
        {
            memory.WriteMemory(xPos, "float", teleportX.Text.ToString());
            memory.WriteMemory(yPos, "float", teleportY.Text.ToString());
            memory.WriteMemory(zPos, "float", teleportZ.Text.ToString());
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
            memory.WriteMemory(xPos, "float", "100000.0");
            memory.WriteMemory(yPos, "float", "100000.0");
            memory.WriteMemory(zPos, "float", "100000.0");
        }

        private void dupeSelection_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                AddKeyPointRow(Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["X"].Value), Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Y"].Value), Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Z"].Value), Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Yaw"].Value), Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Pitch"].Value), Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Roll"].Value), Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["FOV"].Value), Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value));
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

                            AddKeyPointRow(newX, newY, newZ, keypoint.yawAng, keypoint.pitchAng, keypoint.rollAng, keypoint.playerFov, keypoint.transitionTime);
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
                1);
        }
    }
}
