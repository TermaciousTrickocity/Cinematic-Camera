using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camera
{
    public partial class CameraForm
    {
        private void InitializeDataGridView()
        {
            // Set DataGridView properties
            keyframeDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            keyframeDataGridView.AllowUserToResizeColumns = false; // Disable column resizing
            keyframeDataGridView.AllowUserToResizeRows = false;    // Disable row resizing
            keyframeDataGridView.AllowUserToOrderColumns = true; // Enable column sorting

            // Add columns to the DataGridView
            keyframeDataGridView.Columns.Add("X", "X");
            keyframeDataGridView.Columns.Add("Y", "Y");
            keyframeDataGridView.Columns.Add("Z", "Z");
            keyframeDataGridView.Columns.Add("Yaw", "Yaw");
            keyframeDataGridView.Columns.Add("Pitch", "Pitch");
            keyframeDataGridView.Columns.Add("Roll", "Roll");
            keyframeDataGridView.Columns.Add("FOV", "FOV"); // Add FOV column

            // Subscribe to the CellEndEdit event
            keyframeDataGridView.CellEndEdit += keyframeDataGridView_CellEndEdit;
            keyframeDataGridView.ColumnHeaderMouseClick += keyframeDataGridView_ColumnHeaderMouseClick;

            foreach (var keypoint in keyPoints)
            {
                keyframeDataGridView.Rows.Add(keypoint.Item1, keypoint.Item2, keypoint.Item3, keypoint.Item4, keypoint.Item5, keypoint.Item6, keypoint.Item7);
            }
        }

        private void RemoveSelectedKeypoint()
        {
            List<int> selectedIndices = new List<int>();
            foreach (DataGridViewRow row in keyframeDataGridView.SelectedRows)
            {
                selectedIndices.Add(row.Index);
            }

            // Sort in descending order to avoid shifting indices while removing
            selectedIndices.Sort((a, b) => b.CompareTo(a));

            foreach (int selectedIndex in selectedIndices)
            {
                //Check if the index is valid
                if (selectedIndex >= 0 && selectedIndex < keyPoints.Count)
                {
                    keyPoints.RemoveAt(selectedIndex);
                }
                
            }

            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            keyframeDataGridView.Rows.Clear();
            foreach (var keypoint in keyPoints)
            {
                keyframeDataGridView.Rows.Add(keypoint.Item1, keypoint.Item2, keypoint.Item3, keypoint.Item4, keypoint.Item5, keypoint.Item6, keypoint.Item7);
            }
        }

        private void keyframeDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var editedValue = keyframeDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (editedValue != null && float.TryParse(editedValue.ToString(), out float newValue))
                {
                    //Check if the index is valid return if not
                    if (e.RowIndex >= 0 && !(e.RowIndex < keyPoints.Count))
                    {
                        return;
                    }
                    

                    var editedKeyPoint = keyPoints[e.RowIndex];
                    switch (e.ColumnIndex)
                    {
                        case 0: editedKeyPoint.Item1 = newValue; break; // X
                        case 1: editedKeyPoint.Item2 = newValue; break; // Y
                        case 2: editedKeyPoint.Item3 = newValue; break; // Z
                        case 3: editedKeyPoint.Item4 = newValue; break; // Yaw
                        case 4: editedKeyPoint.Item5 = newValue; break; // Pitch
                        case 5: editedKeyPoint.Item6 = newValue; break; // Roll
                        case 6: editedKeyPoint.Item7 = newValue; break; // FOV
                    }

                    keyPoints[e.RowIndex] = editedKeyPoint;
                }
            }
        }

        private void keyframeDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                DataGridViewColumn clickedColumn = keyframeDataGridView.Columns[e.ColumnIndex];

                // Determine sorting order (ascending or descending)
                ListSortDirection direction = (ListSortDirection)SortOrder.Ascending;
                if (clickedColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                {
                    direction = (ListSortDirection)SortOrder.Descending;
                }

                // Sort the DataGridView using a custom sorting function
                keyframeDataGridView.Sort(new DataGridViewCustomComparer(e.ColumnIndex, direction));

                // Set the sort glyph direction
                clickedColumn.HeaderCell.SortGlyphDirection = (SortOrder)direction;
            }
        }

        private void keyframeDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedKeypoint();
            }
        }

        private void dupeStartToEnd_Click(object sender, EventArgs e)
        {
            if (keyPoints.Count > 0)
            {
                keyPoints.Add(keyPoints[0]);
                UpdateDataGridView();
            }
        }
    }

    public class DataGridViewCustomComparer : IComparer
    {
        private int columnIndex;
        private ListSortDirection direction;

        public DataGridViewCustomComparer(int columnIndex, ListSortDirection direction)
        {
            this.columnIndex = columnIndex;
            this.direction = direction;
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow row1 = (DataGridViewRow)x;
            DataGridViewRow row2 = (DataGridViewRow)y;

            // Retrieve the cell values you want to compare
            float value1 = Convert.ToSingle(row1.Cells[columnIndex].Value);
            float value2 = Convert.ToSingle(row2.Cells[columnIndex].Value);

            // Compare the values based on the desired column and direction
            int result = value1.CompareTo(value2);

            // Invert the result if sorting in descending order
            if (direction == ListSortDirection.Descending)
            {
                result = -result;
            }

            return result;
        }
    }
}
