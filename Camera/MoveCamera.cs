using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Camera.CameraEasing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Camera
{
    public partial class CameraForm
    {
        private Vector3 targetPosition;

        static Tuple<float, float, float, float, float, float, float> CatmullRomPositionInterpolation(Tuple<float, float, float, float, float, float, float> p0, Tuple<float, float, float, float, float, float, float> p1, Tuple<float, float, float, float, float, float, float> p2, Tuple<float, float, float, float, float, float, float> p3, float t)
        {
            float t2 = t * t; float t3 = t2 * t; float[] coefficients = new float[4] { -0.5f * t3 + t2 - 0.5f * t, 1.5f * t3 - 2.5f * t2 + 1.0f, -1.5f * t3 + 2.0f * t2 + 0.5f * t, 0.5f * t3 - 0.5f * t2 };

            float x = coefficients[0] * p0.Item1 + coefficients[1] * p1.Item1 + coefficients[2] * p2.Item1 + coefficients[3] * p3.Item1;
            float y = coefficients[0] * p0.Item2 + coefficients[1] * p1.Item2 + coefficients[2] * p2.Item2 + coefficients[3] * p3.Item2;
            float z = coefficients[0] * p0.Item3 + coefficients[1] * p1.Item3 + coefficients[2] * p2.Item3 + coefficients[3] * p3.Item3;
            float yaw = coefficients[0] * p0.Item4 + coefficients[1] * p1.Item4 + coefficients[2] * p2.Item4 + coefficients[3] * p3.Item4;
            float pitch = coefficients[0] * p0.Item5 + coefficients[1] * p1.Item5 + coefficients[2] * p2.Item5 + coefficients[3] * p3.Item5;
            float roll = coefficients[0] * p0.Item6 + coefficients[1] * p1.Item6 + coefficients[2] * p2.Item6 + coefficients[3] * p3.Item6;
            float fov = coefficients[0] * p0.Item7 + coefficients[1] * p1.Item7 + coefficients[2] * p2.Item7 + coefficients[3] * p3.Item7;

            return new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, fov);
        }

        static Tuple<float, float, float> CatmullRomRotationInterpolation(Tuple<float, float, float, float, float, float, float> p1, Tuple<float, float, float, float, float, float, float> p2, float t)
        {
            // Convert angles to radians for interpolation
            float startAngleYaw = p1.Item4 * (float)Math.PI / 180;
            float endAngleYaw = p2.Item4 * (float)Math.PI / 180;

            float startAnglePitch = p1.Item5 * (float)Math.PI / 180;
            float endAnglePitch = p2.Item5 * (float)Math.PI / 180;

            float startAngleRoll = p1.Item6 * (float)Math.PI / 180;
            float endAngleRoll = p2.Item6 * (float)Math.PI / 180;

            // Perform rotation interpolation
            float interpolatedYaw = InterpolateAngle(startAngleYaw, endAngleYaw, t);
            float interpolatedPitch = InterpolateAngle(startAnglePitch, endAnglePitch, t);
            float interpolatedRoll = InterpolateAngle(startAngleRoll, endAngleRoll, t);

            // Convert angles back to degrees
            interpolatedYaw = interpolatedYaw * 180 / (float)Math.PI;
            interpolatedPitch = interpolatedPitch * 180 / (float)Math.PI;
            interpolatedRoll = interpolatedRoll * 180 / (float)Math.PI;

            return new Tuple<float, float, float>(interpolatedYaw, interpolatedPitch, interpolatedRoll);
        }

        static float InterpolateAngle(float start, float end, float t)
        {
            float pi2 = 2 * (float)Math.PI;
            float delta = (end - start) % pi2;
            float shortest = 2 * delta % pi2 - delta;

            return start + shortest * t;
        }

        private void SetupDataGridView()
        {
            // Create columns for X, Y, Z, Roll, Pitch, Yaw, FOV, and Transition Time
            keyframeDataGridView.ColumnCount = 8;

            keyframeDataGridView.Columns[0].Name = "X";
            keyframeDataGridView.Columns[1].Name = "Y";
            keyframeDataGridView.Columns[2].Name = "Z";
            keyframeDataGridView.Columns[5].Name = "Yaw";
            keyframeDataGridView.Columns[4].Name = "Pitch";
            keyframeDataGridView.Columns[3].Name = "Roll";
            keyframeDataGridView.Columns[6].Name = "FOV";
            keyframeDataGridView.Columns[7].Name = "Transition Time";

            // Set DataGridView properties
            keyframeDataGridView.AllowUserToAddRows = false;
            keyframeDataGridView.AllowUserToDeleteRows = false;
            keyframeDataGridView.AllowUserToOrderColumns = true;
            keyframeDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            keyframeDataGridView.MultiSelect = false;
        }

        public async Task MoveCameraAsync()
        {
            List<Tuple<float, float, float, float, float, float, float>> keyPoints = new List<Tuple<float, float, float, float, float, float, float>>();
            List<float> transitionTimes = new List<float>();

            // Load key points and transition times from DataGridView
            for (int i = 0; i < keyframeDataGridView.Rows.Count; i++)
            {
                float x = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["X"].Value);
                float y = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Y"].Value);
                float z = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Z"].Value);
                float yaw = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Yaw"].Value);
                float pitch = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Pitch"].Value);
                float roll = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Roll"].Value);
                float fov = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["FOV"].Value);
                float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Transition Time"].Value);

                keyPoints.Add(new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, fov));
                transitionTimes.Add(transitionTime);
            }

            float fps = 60.0f;
            float deltaTime = 1.0f / fps;

            for (int i = 0; i < keyPoints.Count - 1; i++)
            {
                var p0 = i == 0 ? keyPoints[0] : keyPoints[i - 1];
                var p1 = keyPoints[i];
                var p2 = keyPoints[i + 1];
                var p3 = (i + 2 < keyPoints.Count) ? keyPoints[i + 2] : p2;

                float segmentTime = transitionTimes[i];
                int frames = (int)(segmentTime * fps);

                for (int frame = 0; frame <= frames; frame++)
                {
                    float tNormalized = (float)frame / frames;

                    var interpolatedPosition = CatmullRomPositionInterpolation(p0, p1, p2, p3, tNormalized);
                    var interpolatedRotation = CatmullRomRotationInterpolation(p1, p2, tNormalized);

                    memory.WriteMemory(xPos, "float", $"{interpolatedPosition.Item1}");
                    memory.WriteMemory(yPos, "float", $"{interpolatedPosition.Item2}");
                    memory.WriteMemory(zPos, "float", $"{interpolatedPosition.Item3}");
                    memory.WriteMemory(yawAng, "float", $"{interpolatedRotation.Item1}");
                    memory.WriteMemory(pitchAng, "float", $"{interpolatedRotation.Item2}");
                    memory.WriteMemory(rollAng, "float", $"{interpolatedRotation.Item3}");
                    memory.WriteMemory(playerFov, "float", $"{interpolatedPosition.Item7}");

                    await Task.Delay((int)(1000 / fps)); // Delay based on frames per second
                }
            }
        }

        private void setTarget_Click(object sender, EventArgs e)
        {
            // Read the current camera position from memory
            float cameraPosX = memory.ReadFloat(xPos);
            float cameraPosY = memory.ReadFloat(yPos);
            float cameraPosZ = memory.ReadFloat(zPos);

            // Set targetPosition using the obtained values
            targetPosition = new Vector3(cameraPosX, cameraPosY, cameraPosZ);

            targetTextbox.Text = targetPosition.ToString();
            Console.WriteLine(targetPosition.ToString());
        }

        private void GotoSelectedKeypoint()
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = keyframeDataGridView.SelectedRows[0];
                int rowIndex = selectedRow.Index;

                if (rowIndex >= 0 && rowIndex < keyPoints.Count) // Ensure the selected row index is valid
                {
                    var selectedKeypoint = keyPoints[rowIndex]; // Get the keypoint corresponding to the selected row

                    // Write the selected keypoint's values to memory
                    memory.WriteMemory(xPos, "float", $"{selectedKeypoint.Item1}");
                    memory.WriteMemory(yPos, "float", $"{selectedKeypoint.Item2}");
                    memory.WriteMemory(zPos, "float", $"{selectedKeypoint.Item3}");
                    memory.WriteMemory(yawAng, "float", $"{selectedKeypoint.Item4}");
                    memory.WriteMemory(pitchAng, "float", $"{selectedKeypoint.Item5}");
                    memory.WriteMemory(rollAng, "float", $"{selectedKeypoint.Item6}");
                    memory.WriteMemory(playerFov, "float", $"{selectedKeypoint.Item7}");

                    Console.WriteLine($"Teleported to keypoint: {selectedKeypoint.Item1}, {selectedKeypoint.Item2}, {selectedKeypoint.Item3}, {selectedKeypoint.Item4}, {selectedKeypoint.Item5}, {selectedKeypoint.Item6}, {selectedKeypoint.Item7} ");
                }
            }
        }

        private async Task Data()
        {
            fovTextbox.Text = memory.ReadFloat(playerFov).ToString();
            rollAngle.Text = memory.ReadFloat(rollAng).ToString();
            quickAccessSpeed.Text = memory.ReadFloat(speedCamera).ToString();

            for (; ; )
            {
                cameraX.Text = memory.ReadFloat(xPos).ToString();
                cameraY.Text = memory.ReadFloat(yPos).ToString();
                cameraZ.Text = memory.ReadFloat(zPos).ToString();

                cameraYaw.Text = memory.ReadFloat(yawAng).ToString();
                cameraPitch.Text = memory.ReadFloat(pitchAng).ToString();
                cameraRoll.Text = memory.ReadFloat(rollAng).ToString();

                cameraSpeed.Text = memory.ReadFloat(speedCamera).ToString();

                fovStatus.Text = memory.ReadFloat(playerFov).ToString();

                await Task.Delay(1);
            }
        }

        private void increaseRoll()
        {
            float initalFloat = memory.ReadFloat(rollAng);
            float decFloat = initalFloat + 0.1f;

            memory.WriteMemory(rollAng, "float", decFloat.ToString());
        }

        private void decreaseRoll()
        {
            float initalFloat = memory.ReadFloat(rollAng);
            float decFloat = initalFloat - 0.1f;

            memory.WriteMemory(rollAng, "float", decFloat.ToString());
        }

        private void increaseFOV()
        {
            float initalFloat = memory.ReadFloat(playerFov);
            float decFloat = initalFloat + 0.1f;

            if (decFloat >= 150.0f) return;

            memory.WriteMemory(playerFov, "float", decFloat.ToString());
        }

        private void decreaseFOV()
        {
            float initalFloat = memory.ReadFloat(playerFov);
            float decFloat = initalFloat - 0.1f;

            if (decFloat <= 1.0f) return;

            memory.WriteMemory(playerFov, "float", decFloat.ToString());
        }
    }

    public class Keypoint
    {
        public float XPos { get; set; }
        public float YPos { get; set; }
        public float ZPos { get; set; }
        public float YawAng { get; set; }
        public float PitchAng { get; set; }
        public float RollAng { get; set; }
        public float PlayerFov { get; set; }
    }
}