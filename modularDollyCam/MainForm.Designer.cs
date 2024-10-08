﻿namespace modularDollyCam
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            keyframeDataGridView = new DataGridView();
            AddKey_Button = new Button();
            SaveAndSmooth_Button = new Button();
            importPath_Button = new Button();
            exportPath_Button = new Button();
            teleportToPlayer_Button = new Button();
            teleportZ = new TextBox();
            teleportY = new TextBox();
            teleportX = new TextBox();
            resetCameraRotation_Button = new Button();
            teleportCamera_Button = new Button();
            teleportToSelection_Button = new Button();
            targetTracking = new CheckBox();
            deleteKey_Button = new Button();
            replaceCurrent_Button = new Button();
            dupeSelection_Button = new Button();
            saveGroupbox = new GroupBox();
            importPathWithOffset = new Button();
            label1 = new Label();
            linearSmoothingFactor_Textbox = new TextBox();
            startFromSelection_checkbox = new CheckBox();
            pathStart_checkbox = new CheckBox();
            clearList_Button = new Button();
            fpsTextbox = new TextBox();
            label2 = new Label();
            updateModules = new Button();
            sortUp_button = new Button();
            sortDown_button = new Button();
            groupBox9 = new GroupBox();
            groupBox1 = new GroupBox();
            trackingDataGrid = new DataGridView();
            keyframeDataGridGroupBox = new GroupBox();
            label3 = new Label();
            CurrentTimeTextbox = new TextBox();
            useTheaterTime = new CheckBox();
            label7 = new Label();
            button1 = new Button();
            groupBox8 = new GroupBox();
            groupBox11 = new GroupBox();
            button2 = new Button();
            label6 = new Label();
            moveTracking = new CheckBox();
            movementOffsetX = new TextBox();
            moveTracking_combobox = new ComboBox();
            movementOffsetY = new TextBox();
            movementOffsetZ = new TextBox();
            groupBox10 = new GroupBox();
            directLookCheckbox = new CheckBox();
            lookTrackingSmoothing_Textbox = new TextBox();
            label5 = new Label();
            lookTracking_Combobox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)keyframeDataGridView).BeginInit();
            saveGroupbox.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackingDataGrid).BeginInit();
            keyframeDataGridGroupBox.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox10.SuspendLayout();
            SuspendLayout();
            // 
            // keyframeDataGridView
            // 
            keyframeDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            keyframeDataGridView.Location = new Point(42, 55);
            keyframeDataGridView.Name = "keyframeDataGridView";
            keyframeDataGridView.RowTemplate.Height = 25;
            keyframeDataGridView.Size = new Size(857, 408);
            keyframeDataGridView.TabIndex = 0;
            // 
            // AddKey_Button
            // 
            AddKey_Button.Location = new Point(6, 22);
            AddKey_Button.Name = "AddKey_Button";
            AddKey_Button.Size = new Size(74, 29);
            AddKey_Button.TabIndex = 1;
            AddKey_Button.Text = "Add";
            AddKey_Button.UseVisualStyleBackColor = true;
            AddKey_Button.Click += AddKey_Button_Click;
            // 
            // SaveAndSmooth_Button
            // 
            SaveAndSmooth_Button.Enabled = false;
            SaveAndSmooth_Button.Location = new Point(524, 14);
            SaveAndSmooth_Button.Name = "SaveAndSmooth_Button";
            SaveAndSmooth_Button.Size = new Size(97, 29);
            SaveAndSmooth_Button.TabIndex = 4;
            SaveAndSmooth_Button.Text = "Apply linear";
            SaveAndSmooth_Button.UseVisualStyleBackColor = true;
            SaveAndSmooth_Button.Visible = false;
            SaveAndSmooth_Button.Click += SaveAndSmooth_Button_Click;
            // 
            // importPath_Button
            // 
            importPath_Button.Location = new Point(86, 25);
            importPath_Button.Name = "importPath_Button";
            importPath_Button.Size = new Size(74, 29);
            importPath_Button.TabIndex = 5;
            importPath_Button.Text = "Load";
            importPath_Button.UseVisualStyleBackColor = true;
            importPath_Button.Click += importPath_Button_Click;
            // 
            // exportPath_Button
            // 
            exportPath_Button.Location = new Point(6, 25);
            exportPath_Button.Name = "exportPath_Button";
            exportPath_Button.Size = new Size(74, 29);
            exportPath_Button.TabIndex = 6;
            exportPath_Button.Text = "Save";
            exportPath_Button.UseVisualStyleBackColor = true;
            exportPath_Button.Click += exportPath_Button_Click;
            // 
            // teleportToPlayer_Button
            // 
            teleportToPlayer_Button.Location = new Point(632, 62);
            teleportToPlayer_Button.Name = "teleportToPlayer_Button";
            teleportToPlayer_Button.Size = new Size(129, 29);
            teleportToPlayer_Button.TabIndex = 34;
            teleportToPlayer_Button.Text = "Teleport to player";
            teleportToPlayer_Button.UseVisualStyleBackColor = true;
            teleportToPlayer_Button.Click += teleportToPlayer_Button_Click;
            // 
            // teleportZ
            // 
            teleportZ.Location = new Point(862, 31);
            teleportZ.Name = "teleportZ";
            teleportZ.Size = new Size(45, 25);
            teleportZ.TabIndex = 17;
            teleportZ.Text = "0.00";
            // 
            // teleportY
            // 
            teleportY.Location = new Point(811, 31);
            teleportY.Name = "teleportY";
            teleportY.Size = new Size(45, 25);
            teleportY.TabIndex = 16;
            teleportY.Text = "0.00";
            // 
            // teleportX
            // 
            teleportX.Location = new Point(760, 31);
            teleportX.Name = "teleportX";
            teleportX.Size = new Size(45, 25);
            teleportX.TabIndex = 15;
            teleportX.Text = "0.00";
            // 
            // resetCameraRotation_Button
            // 
            resetCameraRotation_Button.Location = new Point(168, 57);
            resetCameraRotation_Button.Name = "resetCameraRotation_Button";
            resetCameraRotation_Button.Size = new Size(117, 29);
            resetCameraRotation_Button.TabIndex = 7;
            resetCameraRotation_Button.Text = "Reset orientation";
            resetCameraRotation_Button.UseVisualStyleBackColor = true;
            resetCameraRotation_Button.Click += resetCameraRotation_Button_Click;
            // 
            // teleportCamera_Button
            // 
            teleportCamera_Button.Location = new Point(666, 28);
            teleportCamera_Button.Name = "teleportCamera_Button";
            teleportCamera_Button.Size = new Size(88, 29);
            teleportCamera_Button.TabIndex = 8;
            teleportCamera_Button.Text = "Teleport camera";
            teleportCamera_Button.UseVisualStyleBackColor = true;
            teleportCamera_Button.Click += teleportCamera_Button_Click;
            // 
            // teleportToSelection_Button
            // 
            teleportToSelection_Button.Location = new Point(767, 62);
            teleportToSelection_Button.Name = "teleportToSelection_Button";
            teleportToSelection_Button.Size = new Size(140, 29);
            teleportToSelection_Button.TabIndex = 6;
            teleportToSelection_Button.Text = "Teleport to selection";
            teleportToSelection_Button.UseVisualStyleBackColor = true;
            teleportToSelection_Button.Click += teleportToSelection_Button_Click;
            // 
            // targetTracking
            // 
            targetTracking.AutoSize = true;
            targetTracking.Location = new Point(6, 25);
            targetTracking.Name = "targetTracking";
            targetTracking.Size = new Size(105, 21);
            targetTracking.TabIndex = 32;
            targetTracking.Text = "Look tracking";
            targetTracking.UseVisualStyleBackColor = true;
            // 
            // deleteKey_Button
            // 
            deleteKey_Button.Location = new Point(8, 57);
            deleteKey_Button.Name = "deleteKey_Button";
            deleteKey_Button.Size = new Size(74, 29);
            deleteKey_Button.TabIndex = 4;
            deleteKey_Button.Text = "Delete";
            deleteKey_Button.UseVisualStyleBackColor = true;
            deleteKey_Button.Click += deleteKey_Button_Click;
            // 
            // replaceCurrent_Button
            // 
            replaceCurrent_Button.Location = new Point(86, 22);
            replaceCurrent_Button.Name = "replaceCurrent_Button";
            replaceCurrent_Button.Size = new Size(74, 29);
            replaceCurrent_Button.TabIndex = 5;
            replaceCurrent_Button.Text = "Replace";
            replaceCurrent_Button.UseVisualStyleBackColor = true;
            replaceCurrent_Button.Click += replaceCurrent_Button_Click;
            // 
            // dupeSelection_Button
            // 
            dupeSelection_Button.Location = new Point(166, 22);
            dupeSelection_Button.Name = "dupeSelection_Button";
            dupeSelection_Button.Size = new Size(74, 29);
            dupeSelection_Button.TabIndex = 7;
            dupeSelection_Button.Text = "Duplicate";
            dupeSelection_Button.UseVisualStyleBackColor = true;
            dupeSelection_Button.Click += dupeSelection_Button_Click;
            // 
            // saveGroupbox
            // 
            saveGroupbox.Controls.Add(importPath_Button);
            saveGroupbox.Controls.Add(importPathWithOffset);
            saveGroupbox.Controls.Add(exportPath_Button);
            saveGroupbox.Enabled = false;
            saveGroupbox.Location = new Point(666, 12);
            saveGroupbox.Name = "saveGroupbox";
            saveGroupbox.Size = new Size(272, 63);
            saveGroupbox.TabIndex = 9;
            saveGroupbox.TabStop = false;
            saveGroupbox.Text = "Save/load";
            // 
            // importPathWithOffset
            // 
            importPathWithOffset.Location = new Point(166, 25);
            importPathWithOffset.Name = "importPathWithOffset";
            importPathWithOffset.Size = new Size(96, 29);
            importPathWithOffset.TabIndex = 7;
            importPathWithOffset.Text = "Import offset";
            importPathWithOffset.UseVisualStyleBackColor = true;
            importPathWithOffset.Click += importPathWithOffset_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(524, 52);
            label1.Name = "label1";
            label1.Size = new Size(78, 17);
            label1.TabIndex = 9;
            label1.Text = "(smoothing)";
            label1.Visible = false;
            // 
            // linearSmoothingFactor_Textbox
            // 
            linearSmoothingFactor_Textbox.Enabled = false;
            linearSmoothingFactor_Textbox.Location = new Point(603, 49);
            linearSmoothingFactor_Textbox.Name = "linearSmoothingFactor_Textbox";
            linearSmoothingFactor_Textbox.Size = new Size(40, 25);
            linearSmoothingFactor_Textbox.TabIndex = 8;
            linearSmoothingFactor_Textbox.Text = "0.5";
            linearSmoothingFactor_Textbox.Visible = false;
            // 
            // startFromSelection_checkbox
            // 
            startFromSelection_checkbox.AutoSize = true;
            startFromSelection_checkbox.Location = new Point(254, 25);
            startFromSelection_checkbox.Name = "startFromSelection_checkbox";
            startFromSelection_checkbox.Size = new Size(141, 21);
            startFromSelection_checkbox.TabIndex = 15;
            startFromSelection_checkbox.Text = "Start from selection";
            startFromSelection_checkbox.UseVisualStyleBackColor = true;
            // 
            // pathStart_checkbox
            // 
            pathStart_checkbox.AutoSize = true;
            pathStart_checkbox.Location = new Point(164, 25);
            pathStart_checkbox.Name = "pathStart_checkbox";
            pathStart_checkbox.Size = new Size(84, 21);
            pathStart_checkbox.TabIndex = 14;
            pathStart_checkbox.Text = "Start path";
            pathStart_checkbox.UseVisualStyleBackColor = true;
            pathStart_checkbox.CheckedChanged += pathStart_checkbox_CheckedChanged;
            // 
            // clearList_Button
            // 
            clearList_Button.Location = new Point(88, 57);
            clearList_Button.Name = "clearList_Button";
            clearList_Button.Size = new Size(74, 29);
            clearList_Button.TabIndex = 7;
            clearList_Button.Text = "Delete all";
            clearList_Button.UseVisualStyleBackColor = true;
            clearList_Button.Click += clearList_Button_Click;
            // 
            // fpsTextbox
            // 
            fpsTextbox.Location = new Point(74, 23);
            fpsTextbox.Name = "fpsTextbox";
            fpsTextbox.Size = new Size(40, 25);
            fpsTextbox.TabIndex = 11;
            fpsTextbox.Text = "60";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 26);
            label2.Name = "label2";
            label2.Size = new Size(28, 17);
            label2.TabIndex = 10;
            label2.Text = "FPS";
            // 
            // updateModules
            // 
            updateModules.Location = new Point(12, 14);
            updateModules.Name = "updateModules";
            updateModules.Size = new Size(302, 46);
            updateModules.TabIndex = 19;
            updateModules.Text = "Attach to process";
            updateModules.UseVisualStyleBackColor = true;
            updateModules.Click += updateModules_Click;
            // 
            // sortUp_button
            // 
            sortUp_button.Location = new Point(6, 64);
            sortUp_button.Name = "sortUp_button";
            sortUp_button.Size = new Size(29, 84);
            sortUp_button.TabIndex = 12;
            sortUp_button.Text = "^ ^ ^";
            sortUp_button.UseVisualStyleBackColor = true;
            sortUp_button.Click += sortUp_button_Click;
            // 
            // sortDown_button
            // 
            sortDown_button.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sortDown_button.Location = new Point(6, 171);
            sortDown_button.Name = "sortDown_button";
            sortDown_button.Size = new Size(29, 84);
            sortDown_button.TabIndex = 13;
            sortDown_button.Text = "v  v  v";
            sortDown_button.UseVisualStyleBackColor = true;
            sortDown_button.Click += sortDown_button_Click;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(groupBox1);
            groupBox9.Controls.Add(teleportZ);
            groupBox9.Controls.Add(resetCameraRotation_Button);
            groupBox9.Controls.Add(teleportToPlayer_Button);
            groupBox9.Controls.Add(teleportY);
            groupBox9.Controls.Add(keyframeDataGridGroupBox);
            groupBox9.Controls.Add(button1);
            groupBox9.Controls.Add(teleportX);
            groupBox9.Controls.Add(teleportCamera_Button);
            groupBox9.Controls.Add(teleportToSelection_Button);
            groupBox9.Controls.Add(replaceCurrent_Button);
            groupBox9.Controls.Add(dupeSelection_Button);
            groupBox9.Controls.Add(clearList_Button);
            groupBox9.Controls.Add(AddKey_Button);
            groupBox9.Controls.Add(deleteKey_Button);
            groupBox9.Enabled = false;
            groupBox9.Location = new Point(12, 74);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(926, 578);
            groupBox9.TabIndex = 14;
            groupBox9.TabStop = false;
            groupBox9.Text = "Control";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(trackingDataGrid);
            groupBox1.Location = new Point(923, 128);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(449, 651);
            groupBox1.TabIndex = 35;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tracking";
            groupBox1.Visible = false;
            // 
            // trackingDataGrid
            // 
            trackingDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            trackingDataGrid.Location = new Point(6, 25);
            trackingDataGrid.Name = "trackingDataGrid";
            trackingDataGrid.RowTemplate.Height = 25;
            trackingDataGrid.Size = new Size(437, 619);
            trackingDataGrid.TabIndex = 0;
            // 
            // keyframeDataGridGroupBox
            // 
            keyframeDataGridGroupBox.Controls.Add(label3);
            keyframeDataGridGroupBox.Controls.Add(keyframeDataGridView);
            keyframeDataGridGroupBox.Controls.Add(CurrentTimeTextbox);
            keyframeDataGridGroupBox.Controls.Add(pathStart_checkbox);
            keyframeDataGridGroupBox.Controls.Add(startFromSelection_checkbox);
            keyframeDataGridGroupBox.Controls.Add(useTheaterTime);
            keyframeDataGridGroupBox.Controls.Add(label2);
            keyframeDataGridGroupBox.Controls.Add(fpsTextbox);
            keyframeDataGridGroupBox.Controls.Add(sortDown_button);
            keyframeDataGridGroupBox.Controls.Add(sortUp_button);
            keyframeDataGridGroupBox.Controls.Add(label7);
            keyframeDataGridGroupBox.Location = new Point(8, 97);
            keyframeDataGridGroupBox.Name = "keyframeDataGridGroupBox";
            keyframeDataGridGroupBox.Size = new Size(909, 473);
            keyframeDataGridGroupBox.TabIndex = 34;
            keyframeDataGridGroupBox.TabStop = false;
            keyframeDataGridGroupBox.Text = "Keyframes";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(815, 26);
            label3.Name = "label3";
            label3.Size = new Size(88, 17);
            label3.TabIndex = 34;
            label3.Text = "(Current time)";
            // 
            // CurrentTimeTextbox
            // 
            CurrentTimeTextbox.Enabled = false;
            CurrentTimeTextbox.Location = new Point(740, 23);
            CurrentTimeTextbox.Name = "CurrentTimeTextbox";
            CurrentTimeTextbox.Size = new Size(73, 25);
            CurrentTimeTextbox.TabIndex = 36;
            CurrentTimeTextbox.Text = "00:00:00";
            // 
            // useTheaterTime
            // 
            useTheaterTime.AutoSize = true;
            useTheaterTime.Location = new Point(611, 25);
            useTheaterTime.Name = "useTheaterTime";
            useTheaterTime.Size = new Size(123, 21);
            useTheaterTime.TabIndex = 35;
            useTheaterTime.Text = "Use theater time";
            useTheaterTime.UseVisualStyleBackColor = true;
            useTheaterTime.CheckedChanged += useTheaterTime_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(0, 151);
            label7.Name = "label7";
            label7.Size = new Size(39, 17);
            label7.TabIndex = 16;
            label7.Text = "(sort)";
            // 
            // button1
            // 
            button1.Location = new Point(291, 57);
            button1.Name = "button1";
            button1.Size = new Size(97, 29);
            button1.TabIndex = 17;
            button1.Text = "Relocate path";
            button1.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(groupBox11);
            groupBox8.Controls.Add(groupBox10);
            groupBox8.Enabled = false;
            groupBox8.Location = new Point(1390, 27);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(302, 256);
            groupBox8.TabIndex = 33;
            groupBox8.TabStop = false;
            groupBox8.Text = "Tracking";
            groupBox8.Visible = false;
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(button2);
            groupBox11.Controls.Add(label6);
            groupBox11.Controls.Add(moveTracking);
            groupBox11.Controls.Add(movementOffsetX);
            groupBox11.Controls.Add(moveTracking_combobox);
            groupBox11.Controls.Add(movementOffsetY);
            groupBox11.Controls.Add(movementOffsetZ);
            groupBox11.Location = new Point(6, 125);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(290, 119);
            groupBox11.TabIndex = 35;
            groupBox11.TabStop = false;
            groupBox11.Text = "Move tracking";
            // 
            // button2
            // 
            button2.Location = new Point(172, 83);
            button2.Name = "button2";
            button2.Size = new Size(101, 26);
            button2.TabIndex = 40;
            button2.Text = "Teleport camera";
            button2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(159, 57);
            label6.Name = "label6";
            label6.Size = new Size(112, 17);
            label6.TabIndex = 40;
            label6.Text = "(X, Y, Z +/- offset)";
            // 
            // moveTracking
            // 
            moveTracking.AutoSize = true;
            moveTracking.Location = new Point(6, 25);
            moveTracking.Name = "moveTracking";
            moveTracking.Size = new Size(110, 21);
            moveTracking.TabIndex = 33;
            moveTracking.Text = "Move tracking";
            moveTracking.UseVisualStyleBackColor = true;
            // 
            // movementOffsetX
            // 
            movementOffsetX.Location = new Point(6, 53);
            movementOffsetX.Name = "movementOffsetX";
            movementOffsetX.Size = new Size(45, 25);
            movementOffsetX.TabIndex = 35;
            movementOffsetX.Text = "0.00";
            // 
            // moveTracking_combobox
            // 
            moveTracking_combobox.FormattingEnabled = true;
            moveTracking_combobox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10", "Item 11", "Item 12", "Item 13", "Item 14" });
            moveTracking_combobox.Location = new Point(6, 83);
            moveTracking_combobox.Name = "moveTracking_combobox";
            moveTracking_combobox.Size = new Size(160, 25);
            moveTracking_combobox.TabIndex = 39;
            moveTracking_combobox.Text = "(havok proxies)";
            // 
            // movementOffsetY
            // 
            movementOffsetY.Location = new Point(57, 53);
            movementOffsetY.Name = "movementOffsetY";
            movementOffsetY.Size = new Size(45, 25);
            movementOffsetY.TabIndex = 36;
            movementOffsetY.Text = "0.00";
            // 
            // movementOffsetZ
            // 
            movementOffsetZ.Location = new Point(108, 53);
            movementOffsetZ.Name = "movementOffsetZ";
            movementOffsetZ.Size = new Size(45, 25);
            movementOffsetZ.TabIndex = 37;
            movementOffsetZ.Text = "5.00";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(directLookCheckbox);
            groupBox10.Controls.Add(lookTrackingSmoothing_Textbox);
            groupBox10.Controls.Add(targetTracking);
            groupBox10.Controls.Add(label5);
            groupBox10.Controls.Add(lookTracking_Combobox);
            groupBox10.Location = new Point(6, 25);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(290, 93);
            groupBox10.TabIndex = 34;
            groupBox10.TabStop = false;
            groupBox10.Text = "Look tracking";
            // 
            // directLookCheckbox
            // 
            directLookCheckbox.AutoSize = true;
            directLookCheckbox.Location = new Point(172, 54);
            directLookCheckbox.Name = "directLookCheckbox";
            directLookCheckbox.Size = new Size(106, 21);
            directLookCheckbox.TabIndex = 37;
            directLookCheckbox.Text = "Don't smooth";
            directLookCheckbox.UseVisualStyleBackColor = true;
            // 
            // lookTrackingSmoothing_Textbox
            // 
            lookTrackingSmoothing_Textbox.Location = new Point(117, 22);
            lookTrackingSmoothing_Textbox.Name = "lookTrackingSmoothing_Textbox";
            lookTrackingSmoothing_Textbox.ReadOnly = true;
            lookTrackingSmoothing_Textbox.Size = new Size(49, 25);
            lookTrackingSmoothing_Textbox.TabIndex = 10;
            lookTrackingSmoothing_Textbox.Text = "0.5";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(172, 25);
            label5.Name = "label5";
            label5.Size = new Size(78, 17);
            label5.TabIndex = 10;
            label5.Text = "(smoothing)";
            // 
            // lookTracking_Combobox
            // 
            lookTracking_Combobox.FormattingEnabled = true;
            lookTracking_Combobox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10", "Item 11", "Item 12", "Item 13", "Item 14" });
            lookTracking_Combobox.Location = new Point(6, 53);
            lookTracking_Combobox.Name = "lookTracking_Combobox";
            lookTracking_Combobox.Size = new Size(160, 25);
            lookTracking_Combobox.TabIndex = 38;
            lookTracking_Combobox.Text = "(havok proxies)";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(954, 661);
            Controls.Add(updateModules);
            Controls.Add(groupBox8);
            Controls.Add(groupBox9);
            Controls.Add(SaveAndSmooth_Button);
            Controls.Add(linearSmoothingFactor_Textbox);
            Controls.Add(label1);
            Controls.Add(saveGroupbox);
            MaximizeBox = false;
            MaximumSize = new Size(970, 700);
            MinimumSize = new Size(970, 700);
            Name = "MainForm";
            Text = "Halo camera tool";
            ((System.ComponentModel.ISupportInitialize)keyframeDataGridView).EndInit();
            saveGroupbox.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackingDataGrid).EndInit();
            keyframeDataGridGroupBox.ResumeLayout(false);
            keyframeDataGridGroupBox.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox11.ResumeLayout(false);
            groupBox11.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView keyframeDataGridView;
        private Button AddKey_Button;
        private Button startPath_Button;
        private Button SaveAndSmooth_Button;
        private Button importPath_Button;
        private Button exportPath_Button;
        private Button button4;
        private Button button8;
        private Button teleportToSelection_Button;
        private Button replaceCurrent_Button;
        private Button dupeSelection_Button;
        private Button teleportCamera_Button;
        private Label label1;
        private TextBox linearSmoothingFactor_Textbox;
        private GroupBox saveGroupbox;
        private TextBox fpsTextbox;
        private Label label2;
        private Button button10;
        private Button button11;
        private TextBox teleportZ;
        private TextBox teleportY;
        private TextBox teleportX;
        private TextBox movementOffsetX;
        private TextBox movementOffsetY;
        private Button updateModules;
        private Button deleteKey_Button;
        private Button resetCameraRotation_Button;
        private Button sortUp_button;
        private Button sortDown_button;
        private Button clearList_Button;
        private Button teleportToPlayer_Button;
        private GroupBox groupBox9;
        private CheckBox targetTracking;
        private GroupBox groupBox8;
        private ComboBox moveTracking_combobox;
        private ComboBox lookTracking_Combobox;
        private TextBox movementOffsetZ;
        private Label label5;
        private TextBox lookTrackingSmoothing_Textbox;
        private CheckBox moveTracking;
        private Label label6;
        private GroupBox groupBox10;
        private GroupBox groupBox11;
        private Button button2;
        private CheckBox pathStart_checkbox;
        private CheckBox startFromSelection_checkbox;
        private Label label7;
        private Button importPathWithOffset;
        private Button button1;
        private GroupBox keyframeDataGridGroupBox;
        private CheckBox useTheaterTime;
        private CheckBox directLookCheckbox;
        private GroupBox groupBox1;
        private DataGridView trackingDataGrid;
        private TextBox CurrentTimeTextbox;
        private Label label3;
    }
}