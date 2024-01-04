using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camera
{
    public partial class CameraForm
    {
        private List<(float, float, float, float, float, float, float)> keyPoints = new List<(float, float, float, float, float, float, float)> { };

        public void createKeyframe()
        {
            try
            {
                if (xPos == null || yPos == null || zPos == null || yawAng == null || pitchAng == null || rollAng == null || playerFov == null) return;
                // Retrieve the current XYZ position from memory
                float x = memory.ReadFloat(xPos);
                float y = memory.ReadFloat(yPos);
                float z = memory.ReadFloat(zPos);

                // Retrieve the current yaw, pitch, and roll angles from memory
                float yaw = memory.ReadFloat(yawAng);
                float pitch = memory.ReadFloat(pitchAng);
                float roll = memory.ReadFloat(rollAng);

                // Retrieve the current FOV from memory
                float fov = memory.ReadFloat(playerFov);

                keyPoints.Add((x, y, z, yaw, pitch, roll, fov));

                keyframeDataGridView.Rows.Add(x, y, z, yaw, pitch, roll, fov);

                Console.WriteLine($"Created a keyframe at: {x}, {y}, {z}, {yaw}, {pitch}, {roll}, {fov}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving keypoint: " + ex.Message);
            }
        }

        private void ReplaceSelectedKeypointWithCurrentLocation()
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = keyframeDataGridView.SelectedRows[0];
                int rowIndex = selectedRow.Index;

                if (rowIndex >= 0 && rowIndex < keyPoints.Count) // Ensure the selected row index is valid
                {
                    // Read the current camera location from memory
                    float currentXPos = memory.ReadFloat(xPos);
                    float currentYPos = memory.ReadFloat(yPos);
                    float currentZPos = memory.ReadFloat(zPos);
                    float currentYawAng = memory.ReadFloat(yawAng);
                    float currentPitchAng = memory.ReadFloat(pitchAng);
                    float currentRollAng = memory.ReadFloat(rollAng);
                    float currentPlayerFov = memory.ReadFloat(playerFov);

                    // Update the corresponding keypoint in the keyPoints list
                    keyPoints[rowIndex] = (currentXPos, currentYPos, currentZPos, currentYawAng, currentPitchAng, currentRollAng, currentPlayerFov);

                    // Update the DataGridView with the new values
                    selectedRow.Cells[0].Value = currentXPos.ToString();
                    selectedRow.Cells[1].Value = currentYPos.ToString();
                    selectedRow.Cells[2].Value = currentZPos.ToString();
                    selectedRow.Cells[3].Value = currentYawAng.ToString();
                    selectedRow.Cells[4].Value = currentPitchAng.ToString();
                    selectedRow.Cells[5].Value = currentRollAng.ToString();
                    selectedRow.Cells[6].Value = currentPlayerFov.ToString();

                    // Refresh the DataGridView to reflect the changes
                    keyframeDataGridView.Refresh();

                    Console.WriteLine($"Updated keypoint at row {rowIndex} with current location.");
                }
            }
        }

        private void ImportKeyPoints(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                List<(float, float, float, float, float, float, float)> importedKeyPoints = JsonConvert.DeserializeObject<List<(float, float, float, float, float, float, float)>>(json);

                // Clear existing keyPoints and add the imported keyPoints
                keyPoints.Clear();
                keyPoints.AddRange(importedKeyPoints);

                UpdateDataGridView(); // Refresh DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing key points: " + ex.Message);
            }
        }

        private void ExportKeyPoints(string filePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(keyPoints, Formatting.Indented);
                File.WriteAllText(filePath, json);

                MessageBox.Show("Key points exported successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting key points: " + ex.Message);
            }
        }

        private void importPathButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportKeyPoints(openFileDialog.FileName);
                }
            }
        }

        private void exportPathButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportKeyPoints(saveFileDialog.FileName);
                }
            }
        }
    }

    public class Keyframe
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public float FOV { get; set; }
    }
}
