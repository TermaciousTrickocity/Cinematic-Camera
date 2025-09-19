namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        private Thread cameraThread;

        public string xPos; // Scan for float; Camera 'X'
        public string yPos; // +4
        public string zPos; // +8
        public string yawAng; // +12
        public string pitchAng; // +16
        public string rollAng; // +20
        public string playerFov; // Scan for float;

        static Tuple<float, float, float, float, float, float, float> CatmullRomPositionInterpolation(Tuple<float, float, float, float, float, float, float> p0, Tuple<float, float, float, float, float, float, float> p1, Tuple<float, float, float, float, float, float, float> p2, Tuple<float, float, float, float, float, float, float> p3, float t)
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
            float fov = coefficients[0] * p0.Item7 + coefficients[1] * p1.Item7 + coefficients[2] * p2.Item7 + coefficients[3] * p3.Item7;

            return new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, fov);
        }

        public async Task MoveCamera()
        {
            try
            {
                int originalSelectedRow = keyframeDataGridView.CurrentCell.RowIndex;
                int originalSelectedColumn = keyframeDataGridView.CurrentCell.ColumnIndex;

                float fps = Convert.ToSingle(fpsTextbox.Text);

                List<Tuple<float, float, float, float, float, float, float>> keyPoints = new List<Tuple<float, float, float, float, float, float, float>>();
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
                    float playerFov = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["FOV"].Value);
                    float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Transition Time"].Value);

                    keyPoints.Add(new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, playerFov));
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

                        memory.WriteMemory(xPos, "float", $"{interpolatedPosition.Item1}");
                        memory.WriteMemory(yPos, "float", $"{interpolatedPosition.Item2}");
                        memory.WriteMemory(zPos, "float", $"{interpolatedPosition.Item3}");

                        memory.WriteMemory(yawAng, "float", $"{interpolatedPosition.Item4}");
                        memory.WriteMemory(pitchAng, "float", $"{interpolatedPosition.Item5}");
                        memory.WriteMemory(rollAng, "float", $"{interpolatedPosition.Item6}");
                        memory.WriteMemory(playerFov, "float", $"{interpolatedPosition.Item7}");

                        await Task.Delay((int)(1000 / fps));
                        currentFrame++;
                    }
                }
                pathStart_checkbox.Checked = false;

                keyframeDataGridView.Rows[originalSelectedRow].Cells[originalSelectedColumn].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.ToString(), MessageBoxButtons.OK);
                pathStart_checkbox.Checked = false;
            }
        }
    }
}