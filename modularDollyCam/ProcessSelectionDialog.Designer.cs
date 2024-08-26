namespace modularDollyCam
{
    partial class ProcessSelectionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            processListBox = new ListBox();
            selectButton = new Button();
            refreshProcessList = new Button();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // processListBox
            // 
            processListBox.FormattingEnabled = true;
            processListBox.ItemHeight = 15;
            processListBox.Location = new Point(6, 22);
            processListBox.Name = "processListBox";
            processListBox.Size = new Size(290, 379);
            processListBox.TabIndex = 0;
            // 
            // selectButton
            // 
            selectButton.Location = new Point(127, 407);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(169, 33);
            selectButton.TabIndex = 1;
            selectButton.Text = "Attach";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += selectButton_Click;
            // 
            // refreshProcessList
            // 
            refreshProcessList.Location = new Point(6, 407);
            refreshProcessList.Name = "refreshProcessList";
            refreshProcessList.Size = new Size(115, 33);
            refreshProcessList.TabIndex = 2;
            refreshProcessList.Text = "Refresh list";
            refreshProcessList.UseVisualStyleBackColor = true;
            refreshProcessList.Click += refreshProcessList_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(processListBox);
            groupBox1.Controls.Add(refreshProcessList);
            groupBox1.Controls.Add(selectButton);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(302, 448);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Processes";
            // 
            // ProcessSelectionDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(329, 470);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            MaximumSize = new Size(345, 509);
            MinimizeBox = false;
            MinimumSize = new Size(345, 509);
            Name = "ProcessSelectionDialog";
            ShowInTaskbar = false;
            Text = "Select a process";
            TopMost = true;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox processListBox;
        private Button selectButton;
        private Button refreshProcessList;
        private GroupBox groupBox1;
    }
}