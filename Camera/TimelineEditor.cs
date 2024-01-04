using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Camera
{
    public partial class CameraForm
    {
        private List<Keyframe> keyframes = new List<Keyframe>();
        private bool isDragging = false;
        private Point dragStartPoint = Point.Empty;
        private double dragStartTime = 0;
        private int selectedKeyframeIndex = -1;
        private double maxTime = 10; // Default maximum time

        private void InitializeTimeline()
        {
            timelinePanel.Paint += TimelinePanel_Paint;
            timelinePanel.MouseDown += TimelinePanel_MouseDown;
            timelinePanel.MouseMove += TimelinePanel_MouseMove;
            timelinePanel.MouseUp += TimelinePanel_MouseUp;
            timelinePanel.MouseClick += TimelinePanel_MouseClick;
            pathDuration.TextChanged += PathDuration_TextChanged;
        }

        private double currentTimeElapsed = 0; // Initialize with 0
        private float measurementLineSpacing = 1f; // Adjust this spacing as needed

        private void TimelinePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            int timelineY = timelinePanel.Height / 2;
            e.Graphics.DrawLine(Pens.Black, 50, timelineY, timelinePanel.Width - 50, timelineY);

            int centerX = timelinePanel.Width / 2;
            e.Graphics.FillEllipse(Brushes.Blue, centerX - 5, timelineY - 5, 10, 10);

            int prevX = -1;

            foreach (var keyframe in keyframes)
            {
                int x = ConvertTimeToPosition(keyframe.Time);
                int y = timelineY;

                if (prevX != -1)
                {
                    e.Graphics.DrawLine(Pens.Green, prevX, y, x, y);
                }

                e.Graphics.FillEllipse(Brushes.Red, x - 5, y - 5, 10, 10);

                // Draw the label with the keyframe number
                string label = keyframe.Number.ToString();
                SizeF labelSize = e.Graphics.MeasureString(label, Font);
                e.Graphics.DrawString(label, Font, Brushes.Black, x - labelSize.Width / 2, y + 15);

                prevX = x;
            }

            // Draw the time elapsed indicator at the top of the timeline
            int timeElapsedX = ConvertTimeToPosition(currentTimeElapsed);
            e.Graphics.DrawLine(Pens.Blue, timeElapsedX, 0, timeElapsedX, timelineY);

            // Draw duration lines and labels
            int measurementLinesCount = (int)(maxTime / measurementLineSpacing);
            for (int i = 0; i <= measurementLinesCount; i++)
            {
                double time = i * measurementLineSpacing;
                int x = ConvertTimeToPosition(time);

                // Draw the line below the timeline
                e.Graphics.DrawLine(Pens.Gray, x, timelineY + 5, x, timelineY + 20);

                // Draw the label above the timeline
                string label = time.ToString();
                SizeF labelSize = e.Graphics.MeasureString(label, Font);
                e.Graphics.DrawString(label, Font, Brushes.Gray, x - labelSize.Width / 2, timelineY - 30);
            }
        }

        private void PathDuration_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(pathDuration.Text, out double newMaxTime) && newMaxTime > 0)
            {
                maxTime = newMaxTime;
                timelinePanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Invalid duration value. Please enter a positive number.");
            }
        }

        private void TimelinePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < keyframes.Count; i++)
                {
                    if (Math.Abs(ConvertTimeToPosition(keyframes[i].Time) - e.X) <= 5)
                    {
                        selectedKeyframeIndex = i;
                        isDragging = true;
                        dragStartPoint = e.Location;
                        dragStartTime = keyframes[i].Time;
                        break;
                    }
                }
            }
        }

        private void TimelinePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left && selectedKeyframeIndex != -1 && !ModifierKeys.HasFlag(Keys.Control))
            {
                double currentTime = ConvertPositionToTime(e.X);
                double delta = currentTime - dragStartTime;

                keyframes[selectedKeyframeIndex].Time = Math.Max(MinTime, Math.Min(MaxTime, keyframes[selectedKeyframeIndex].Time + delta));

                dragStartPoint = e.Location;
                dragStartTime = currentTime;
                timelinePanel.Invalidate();
            }
        }

        private void TimelinePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                selectedKeyframeIndex = -1;
            }
        }

        private void TimelinePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                double time = ConvertPositionToTime(e.X);

                if (time >= MinTime && time <= MaxTime)
                {
                    keyframes.Add(new Keyframe { Time = time });
                    timelinePanel.Invalidate();
                }
            }
        }

        private double ConvertPositionToTime(int x)
        {
            double totalDuration = timelinePanel.Width - 100;
            return (x - 50) * TotalDuration / totalDuration;
        }

        private int ConvertTimeToPosition(double time)
        {
            double totalDuration = timelinePanel.Width - 100;
            return (int)(50 + (time / TotalDuration) * totalDuration);
        }

        private double TotalDuration => 10;
        private double MinTime => 0;
        private double MaxTime => TotalDuration;

        private class Keyframe
        {
            public double Time { get; set; }
            public int Number { get; } // Number is assigned upon creation
            private static int _nextKeyframeNumber = 1;

            public Keyframe()
            {
                Number = _nextKeyframeNumber++;
            }
        }
    }
}
