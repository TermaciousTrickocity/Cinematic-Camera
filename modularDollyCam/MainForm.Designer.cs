namespace modularDollyCam
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
            startFromSelection_checkbox = new CheckBox();
            pathStart_checkbox = new CheckBox();
            clearList_Button = new Button();
            hzTextbox = new TextBox();
            label2 = new Label();
            sortUp_button = new Button();
            sortDown_button = new Button();
            groupBox9 = new GroupBox();
            keyframeDataGridGroupBox = new GroupBox();
            label3 = new Label();
            endDelay = new TextBox();
            pauseTicks = new CheckBox();
            BlockKeyPress = new CheckBox();
            setSyncStart = new Button();
            timeSyncTextbox = new TextBox();
            label1 = new Label();
            timesyncCheckbox = new CheckBox();
            StartDelayTextbox = new TextBox();
            button1 = new Button();
            trackingCheckbox = new CheckBox();
            trackListCombo = new ComboBox();
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
            TabControl = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            comboBoxProcesses = new ComboBox();
            comboBoxBuilds = new ComboBox();
            comboBoxGames = new ComboBox();
            tabPage3 = new TabPage();
            DebugTabpage = new TabPage();
            ((System.ComponentModel.ISupportInitialize)keyframeDataGridView).BeginInit();
            saveGroupbox.SuspendLayout();
            groupBox9.SuspendLayout();
            keyframeDataGridGroupBox.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox10.SuspendLayout();
            TabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            DebugTabpage.SuspendLayout();
            SuspendLayout();
            // 
            // keyframeDataGridView
            // 
            keyframeDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            keyframeDataGridView.Location = new Point(29, 45);
            keyframeDataGridView.Name = "keyframeDataGridView";
            keyframeDataGridView.Size = new Size(970, 368);
            keyframeDataGridView.TabIndex = 0;
            // 
            // AddKey_Button
            // 
            AddKey_Button.Location = new Point(6, 19);
            AddKey_Button.Name = "AddKey_Button";
            AddKey_Button.Size = new Size(74, 26);
            AddKey_Button.TabIndex = 1;
            AddKey_Button.Text = "Add";
            AddKey_Button.UseVisualStyleBackColor = true;
            AddKey_Button.Click += AddKey_Button_Click;
            // 
            // importPath_Button
            // 
            importPath_Button.Location = new Point(86, 16);
            importPath_Button.Name = "importPath_Button";
            importPath_Button.Size = new Size(74, 26);
            importPath_Button.TabIndex = 5;
            importPath_Button.Text = "Load";
            importPath_Button.UseVisualStyleBackColor = true;
            importPath_Button.Click += importPath_Button_Click;
            // 
            // exportPath_Button
            // 
            exportPath_Button.Location = new Point(6, 16);
            exportPath_Button.Name = "exportPath_Button";
            exportPath_Button.Size = new Size(74, 26);
            exportPath_Button.TabIndex = 6;
            exportPath_Button.Text = "Save";
            exportPath_Button.UseVisualStyleBackColor = true;
            exportPath_Button.Click += exportPath_Button_Click;
            // 
            // teleportToPlayer_Button
            // 
            teleportToPlayer_Button.Location = new Point(725, 48);
            teleportToPlayer_Button.Name = "teleportToPlayer_Button";
            teleportToPlayer_Button.Size = new Size(129, 26);
            teleportToPlayer_Button.TabIndex = 34;
            teleportToPlayer_Button.Text = "Teleport to player";
            teleportToPlayer_Button.UseVisualStyleBackColor = true;
            teleportToPlayer_Button.Click += teleportToPlayer_Button_Click;
            // 
            // teleportZ
            // 
            teleportZ.Location = new Point(962, 19);
            teleportZ.Name = "teleportZ";
            teleportZ.Size = new Size(45, 23);
            teleportZ.TabIndex = 17;
            teleportZ.Text = "0.00";
            // 
            // teleportY
            // 
            teleportY.Location = new Point(911, 19);
            teleportY.Name = "teleportY";
            teleportY.Size = new Size(45, 23);
            teleportY.TabIndex = 16;
            teleportY.Text = "0.00";
            // 
            // teleportX
            // 
            teleportX.Location = new Point(860, 19);
            teleportX.Name = "teleportX";
            teleportX.Size = new Size(45, 23);
            teleportX.TabIndex = 15;
            teleportX.Text = "0.00";
            // 
            // resetCameraRotation_Button
            // 
            resetCameraRotation_Button.Location = new Point(166, 51);
            resetCameraRotation_Button.Name = "resetCameraRotation_Button";
            resetCameraRotation_Button.Size = new Size(117, 26);
            resetCameraRotation_Button.TabIndex = 7;
            resetCameraRotation_Button.Text = "Reset orientation";
            resetCameraRotation_Button.UseVisualStyleBackColor = true;
            resetCameraRotation_Button.Click += resetCameraRotation_Button_Click;
            // 
            // teleportCamera_Button
            // 
            teleportCamera_Button.Location = new Point(766, 16);
            teleportCamera_Button.Name = "teleportCamera_Button";
            teleportCamera_Button.Size = new Size(88, 26);
            teleportCamera_Button.TabIndex = 8;
            teleportCamera_Button.Text = "Teleport camera";
            teleportCamera_Button.UseVisualStyleBackColor = true;
            teleportCamera_Button.Click += teleportCamera_Button_Click;
            // 
            // teleportToSelection_Button
            // 
            teleportToSelection_Button.Location = new Point(860, 48);
            teleportToSelection_Button.Name = "teleportToSelection_Button";
            teleportToSelection_Button.Size = new Size(147, 26);
            teleportToSelection_Button.TabIndex = 6;
            teleportToSelection_Button.Text = "Teleport to selection";
            teleportToSelection_Button.UseVisualStyleBackColor = true;
            teleportToSelection_Button.Click += teleportToSelection_Button_Click;
            // 
            // targetTracking
            // 
            targetTracking.AutoSize = true;
            targetTracking.Location = new Point(6, 22);
            targetTracking.Name = "targetTracking";
            targetTracking.Size = new Size(98, 19);
            targetTracking.TabIndex = 32;
            targetTracking.Text = "Look tracking";
            targetTracking.UseVisualStyleBackColor = true;
            // 
            // deleteKey_Button
            // 
            deleteKey_Button.Location = new Point(8, 50);
            deleteKey_Button.Name = "deleteKey_Button";
            deleteKey_Button.Size = new Size(72, 26);
            deleteKey_Button.TabIndex = 4;
            deleteKey_Button.Text = "Delete";
            deleteKey_Button.UseVisualStyleBackColor = true;
            deleteKey_Button.Click += deleteKey_Button_Click;
            // 
            // replaceCurrent_Button
            // 
            replaceCurrent_Button.Location = new Point(86, 19);
            replaceCurrent_Button.Name = "replaceCurrent_Button";
            replaceCurrent_Button.Size = new Size(74, 26);
            replaceCurrent_Button.TabIndex = 5;
            replaceCurrent_Button.Text = "Replace";
            replaceCurrent_Button.UseVisualStyleBackColor = true;
            replaceCurrent_Button.Click += replaceCurrent_Button_Click;
            // 
            // dupeSelection_Button
            // 
            dupeSelection_Button.Location = new Point(166, 19);
            dupeSelection_Button.Name = "dupeSelection_Button";
            dupeSelection_Button.Size = new Size(74, 26);
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
            saveGroupbox.Location = new Point(727, 429);
            saveGroupbox.Name = "saveGroupbox";
            saveGroupbox.Size = new Size(272, 48);
            saveGroupbox.TabIndex = 9;
            saveGroupbox.TabStop = false;
            saveGroupbox.Text = "Save/load";
            // 
            // importPathWithOffset
            // 
            importPathWithOffset.Location = new Point(166, 16);
            importPathWithOffset.Name = "importPathWithOffset";
            importPathWithOffset.Size = new Size(96, 26);
            importPathWithOffset.TabIndex = 7;
            importPathWithOffset.Text = "Import offset";
            importPathWithOffset.UseVisualStyleBackColor = true;
            importPathWithOffset.Click += importPathWithOffset_Click;
            // 
            // startFromSelection_checkbox
            // 
            startFromSelection_checkbox.AutoSize = true;
            startFromSelection_checkbox.Location = new Point(187, 18);
            startFromSelection_checkbox.Name = "startFromSelection_checkbox";
            startFromSelection_checkbox.Size = new Size(129, 19);
            startFromSelection_checkbox.TabIndex = 15;
            startFromSelection_checkbox.Text = "Start from selection";
            startFromSelection_checkbox.UseVisualStyleBackColor = true;
            // 
            // pathStart_checkbox
            // 
            pathStart_checkbox.AutoSize = true;
            pathStart_checkbox.Location = new Point(41, 18);
            pathStart_checkbox.Name = "pathStart_checkbox";
            pathStart_checkbox.Size = new Size(48, 19);
            pathStart_checkbox.TabIndex = 14;
            pathStart_checkbox.Text = "Play";
            pathStart_checkbox.UseVisualStyleBackColor = true;
            pathStart_checkbox.CheckedChanged += pathStart_checkbox_CheckedChanged;
            // 
            // clearList_Button
            // 
            clearList_Button.Location = new Point(86, 50);
            clearList_Button.Name = "clearList_Button";
            clearList_Button.Size = new Size(74, 26);
            clearList_Button.TabIndex = 7;
            clearList_Button.Text = "Delete all";
            clearList_Button.UseVisualStyleBackColor = true;
            clearList_Button.Click += clearList_Button_Click;
            // 
            // hzTextbox
            // 
            hzTextbox.Location = new Point(923, 16);
            hzTextbox.Name = "hzTextbox";
            hzTextbox.Size = new Size(41, 23);
            hzTextbox.TabIndex = 11;
            hzTextbox.Text = "60";
            hzTextbox.TextAlign = HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(965, 20);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 10;
            label2.Text = "hz";
            // 
            // sortUp_button
            // 
            sortUp_button.Location = new Point(6, 45);
            sortUp_button.Name = "sortUp_button";
            sortUp_button.Size = new Size(11, 162);
            sortUp_button.TabIndex = 12;
            sortUp_button.Text = "^ ^ ^";
            sortUp_button.UseVisualStyleBackColor = true;
            sortUp_button.Click += sortUp_button_Click;
            // 
            // sortDown_button
            // 
            sortDown_button.Font = new Font("Segoe UI", 9F);
            sortDown_button.Location = new Point(6, 251);
            sortDown_button.Name = "sortDown_button";
            sortDown_button.Size = new Size(11, 162);
            sortDown_button.TabIndex = 13;
            sortDown_button.Text = "v  v  v";
            sortDown_button.UseVisualStyleBackColor = true;
            sortDown_button.Click += sortDown_button_Click;
            // 
            // groupBox9
            // 
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
            groupBox9.Location = new Point(6, 13);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(1019, 575);
            groupBox9.TabIndex = 14;
            groupBox9.TabStop = false;
            groupBox9.Text = "Control";
            // 
            // keyframeDataGridGroupBox
            // 
            keyframeDataGridGroupBox.Controls.Add(label3);
            keyframeDataGridGroupBox.Controls.Add(endDelay);
            keyframeDataGridGroupBox.Controls.Add(saveGroupbox);
            keyframeDataGridGroupBox.Controls.Add(pauseTicks);
            keyframeDataGridGroupBox.Controls.Add(BlockKeyPress);
            keyframeDataGridGroupBox.Controls.Add(setSyncStart);
            keyframeDataGridGroupBox.Controls.Add(timeSyncTextbox);
            keyframeDataGridGroupBox.Controls.Add(label1);
            keyframeDataGridGroupBox.Controls.Add(startFromSelection_checkbox);
            keyframeDataGridGroupBox.Controls.Add(timesyncCheckbox);
            keyframeDataGridGroupBox.Controls.Add(StartDelayTextbox);
            keyframeDataGridGroupBox.Controls.Add(keyframeDataGridView);
            keyframeDataGridGroupBox.Controls.Add(pathStart_checkbox);
            keyframeDataGridGroupBox.Controls.Add(label2);
            keyframeDataGridGroupBox.Controls.Add(hzTextbox);
            keyframeDataGridGroupBox.Controls.Add(sortDown_button);
            keyframeDataGridGroupBox.Controls.Add(sortUp_button);
            keyframeDataGridGroupBox.Location = new Point(8, 86);
            keyframeDataGridGroupBox.Name = "keyframeDataGridGroupBox";
            keyframeDataGridGroupBox.Size = new Size(1005, 483);
            keyframeDataGridGroupBox.TabIndex = 34;
            keyframeDataGridGroupBox.TabStop = false;
            keyframeDataGridGroupBox.Text = "Keyframes";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(216, 451);
            label3.Name = "label3";
            label3.Size = new Size(123, 15);
            label3.TabIndex = 43;
            label3.Text = "delay pause (seconds)";
            // 
            // endDelay
            // 
            endDelay.Location = new Point(137, 448);
            endDelay.Name = "endDelay";
            endDelay.Size = new Size(73, 23);
            endDelay.TabIndex = 42;
            endDelay.Text = "0";
            // 
            // pauseTicks
            // 
            pauseTicks.AutoSize = true;
            pauseTicks.Location = new Point(29, 450);
            pauseTicks.Name = "pauseTicks";
            pauseTicks.Size = new Size(107, 19);
            pauseTicks.TabIndex = 41;
            pauseTicks.Text = "Pause after end";
            pauseTicks.UseVisualStyleBackColor = true;
            // 
            // BlockKeyPress
            // 
            BlockKeyPress.AutoSize = true;
            BlockKeyPress.Checked = true;
            BlockKeyPress.CheckState = CheckState.Checked;
            BlockKeyPress.Location = new Point(592, 450);
            BlockKeyPress.Name = "BlockKeyPress";
            BlockKeyPress.Size = new Size(105, 19);
            BlockKeyPress.TabIndex = 37;
            BlockKeyPress.Text = "Block keybinds";
            BlockKeyPress.UseVisualStyleBackColor = true;
            // 
            // setSyncStart
            // 
            setSyncStart.Location = new Point(216, 419);
            setSyncStart.Name = "setSyncStart";
            setSyncStart.Size = new Size(84, 23);
            setSyncStart.TabIndex = 36;
            setSyncStart.Text = "grab current";
            setSyncStart.UseVisualStyleBackColor = true;
            setSyncStart.Click += setSyncStart_Click;
            // 
            // timeSyncTextbox
            // 
            timeSyncTextbox.Location = new Point(137, 419);
            timeSyncTextbox.Name = "timeSyncTextbox";
            timeSyncTextbox.Size = new Size(73, 23);
            timeSyncTextbox.TabIndex = 40;
            timeSyncTextbox.Text = "0";
            timeSyncTextbox.TextChanged += timeSyncTextbox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(137, 18);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 39;
            label1.Text = "delay";
            // 
            // timesyncCheckbox
            // 
            timesyncCheckbox.AutoSize = true;
            timesyncCheckbox.Location = new Point(29, 421);
            timesyncCheckbox.Name = "timesyncCheckbox";
            timesyncCheckbox.Size = new Size(104, 19);
            timesyncCheckbox.TabIndex = 17;
            timesyncCheckbox.Text = "Start time sync";
            timesyncCheckbox.UseVisualStyleBackColor = true;
            // 
            // StartDelayTextbox
            // 
            StartDelayTextbox.Location = new Point(95, 16);
            StartDelayTextbox.Name = "StartDelayTextbox";
            StartDelayTextbox.Size = new Size(41, 23);
            StartDelayTextbox.TabIndex = 38;
            StartDelayTextbox.Text = "0";
            StartDelayTextbox.TextAlign = HorizontalAlignment.Right;
            // 
            // button1
            // 
            button1.Location = new Point(289, 50);
            button1.Name = "button1";
            button1.Size = new Size(97, 26);
            button1.TabIndex = 17;
            button1.Text = "Relocate path";
            button1.UseVisualStyleBackColor = true;
            // 
            // trackingCheckbox
            // 
            trackingCheckbox.AutoSize = true;
            trackingCheckbox.Location = new Point(248, 128);
            trackingCheckbox.Name = "trackingCheckbox";
            trackingCheckbox.Size = new Size(70, 19);
            trackingCheckbox.TabIndex = 37;
            trackingCheckbox.Text = "Tracking";
            trackingCheckbox.UseVisualStyleBackColor = true;
            trackingCheckbox.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // trackListCombo
            // 
            trackListCombo.FormattingEnabled = true;
            trackListCombo.Items.AddRange(new object[] { "none" });
            trackListCombo.Location = new Point(324, 126);
            trackListCombo.Name = "trackListCombo";
            trackListCombo.Size = new Size(126, 23);
            trackListCombo.TabIndex = 36;
            trackListCombo.Text = "Players";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(groupBox11);
            groupBox8.Controls.Add(groupBox10);
            groupBox8.Enabled = false;
            groupBox8.Location = new Point(1683, 52);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(302, 226);
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
            groupBox11.Location = new Point(6, 110);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(290, 105);
            groupBox11.TabIndex = 35;
            groupBox11.TabStop = false;
            groupBox11.Text = "Move tracking";
            // 
            // button2
            // 
            button2.Location = new Point(172, 73);
            button2.Name = "button2";
            button2.Size = new Size(101, 23);
            button2.TabIndex = 40;
            button2.Text = "Teleport camera";
            button2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(159, 50);
            label6.Name = "label6";
            label6.Size = new Size(102, 15);
            label6.TabIndex = 40;
            label6.Text = "(X, Y, Z +/- offset)";
            // 
            // moveTracking
            // 
            moveTracking.AutoSize = true;
            moveTracking.Location = new Point(6, 22);
            moveTracking.Name = "moveTracking";
            moveTracking.Size = new Size(102, 19);
            moveTracking.TabIndex = 33;
            moveTracking.Text = "Move tracking";
            moveTracking.UseVisualStyleBackColor = true;
            // 
            // movementOffsetX
            // 
            movementOffsetX.Location = new Point(6, 47);
            movementOffsetX.Name = "movementOffsetX";
            movementOffsetX.Size = new Size(45, 23);
            movementOffsetX.TabIndex = 35;
            movementOffsetX.Text = "0.00";
            // 
            // moveTracking_combobox
            // 
            moveTracking_combobox.FormattingEnabled = true;
            moveTracking_combobox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10", "Item 11", "Item 12", "Item 13", "Item 14" });
            moveTracking_combobox.Location = new Point(6, 73);
            moveTracking_combobox.Name = "moveTracking_combobox";
            moveTracking_combobox.Size = new Size(160, 23);
            moveTracking_combobox.TabIndex = 39;
            moveTracking_combobox.Text = "(havok proxies)";
            // 
            // movementOffsetY
            // 
            movementOffsetY.Location = new Point(57, 47);
            movementOffsetY.Name = "movementOffsetY";
            movementOffsetY.Size = new Size(45, 23);
            movementOffsetY.TabIndex = 36;
            movementOffsetY.Text = "0.00";
            // 
            // movementOffsetZ
            // 
            movementOffsetZ.Location = new Point(108, 47);
            movementOffsetZ.Name = "movementOffsetZ";
            movementOffsetZ.Size = new Size(45, 23);
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
            groupBox10.Location = new Point(6, 22);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(290, 82);
            groupBox10.TabIndex = 34;
            groupBox10.TabStop = false;
            groupBox10.Text = "Look tracking";
            // 
            // directLookCheckbox
            // 
            directLookCheckbox.AutoSize = true;
            directLookCheckbox.Location = new Point(172, 48);
            directLookCheckbox.Name = "directLookCheckbox";
            directLookCheckbox.Size = new Size(99, 19);
            directLookCheckbox.TabIndex = 37;
            directLookCheckbox.Text = "Don't smooth";
            directLookCheckbox.UseVisualStyleBackColor = true;
            // 
            // lookTrackingSmoothing_Textbox
            // 
            lookTrackingSmoothing_Textbox.Location = new Point(117, 19);
            lookTrackingSmoothing_Textbox.Name = "lookTrackingSmoothing_Textbox";
            lookTrackingSmoothing_Textbox.ReadOnly = true;
            lookTrackingSmoothing_Textbox.Size = new Size(49, 23);
            lookTrackingSmoothing_Textbox.TabIndex = 10;
            lookTrackingSmoothing_Textbox.Text = "0.5";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(172, 22);
            label5.Name = "label5";
            label5.Size = new Size(73, 15);
            label5.TabIndex = 10;
            label5.Text = "(smoothing)";
            // 
            // lookTracking_Combobox
            // 
            lookTracking_Combobox.FormattingEnabled = true;
            lookTracking_Combobox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10", "Item 11", "Item 12", "Item 13", "Item 14" });
            lookTracking_Combobox.Location = new Point(6, 47);
            lookTracking_Combobox.Name = "lookTracking_Combobox";
            lookTracking_Combobox.Size = new Size(160, 23);
            lookTracking_Combobox.TabIndex = 38;
            lookTracking_Combobox.Text = "(havok proxies)";
            // 
            // TabControl
            // 
            TabControl.Controls.Add(tabPage1);
            TabControl.Controls.Add(tabPage2);
            TabControl.Controls.Add(tabPage3);
            TabControl.Controls.Add(DebugTabpage);
            TabControl.Location = new Point(12, 12);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(1039, 622);
            TabControl.TabIndex = 36;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox9);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1031, 594);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Control";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(comboBoxProcesses);
            tabPage2.Controls.Add(comboBoxBuilds);
            tabPage2.Controls.Add(comboBoxGames);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1031, 594);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Advanced";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboBoxProcesses
            // 
            comboBoxProcesses.FormattingEnabled = true;
            comboBoxProcesses.Location = new Point(11, 68);
            comboBoxProcesses.Name = "comboBoxProcesses";
            comboBoxProcesses.Size = new Size(206, 23);
            comboBoxProcesses.TabIndex = 2;
            // 
            // comboBoxBuilds
            // 
            comboBoxBuilds.FormattingEnabled = true;
            comboBoxBuilds.Location = new Point(11, 39);
            comboBoxBuilds.Name = "comboBoxBuilds";
            comboBoxBuilds.Size = new Size(206, 23);
            comboBoxBuilds.TabIndex = 1;
            // 
            // comboBoxGames
            // 
            comboBoxGames.FormattingEnabled = true;
            comboBoxGames.Location = new Point(11, 10);
            comboBoxGames.Name = "comboBoxGames";
            comboBoxGames.Size = new Size(206, 23);
            comboBoxGames.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1031, 594);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Memory";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // DebugTabpage
            // 
            DebugTabpage.Controls.Add(trackListCombo);
            DebugTabpage.Controls.Add(trackingCheckbox);
            DebugTabpage.Location = new Point(4, 24);
            DebugTabpage.Name = "DebugTabpage";
            DebugTabpage.Size = new Size(1031, 594);
            DebugTabpage.TabIndex = 3;
            DebugTabpage.Text = "Debug";
            DebugTabpage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(1060, 644);
            Controls.Add(TabControl);
            Controls.Add(groupBox8);
            MaximizeBox = false;
            MinimumSize = new Size(985, 658);
            Name = "MainForm";
            Text = "Halo camera tool";
            ((System.ComponentModel.ISupportInitialize)keyframeDataGridView).EndInit();
            saveGroupbox.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            keyframeDataGridGroupBox.ResumeLayout(false);
            keyframeDataGridGroupBox.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox11.ResumeLayout(false);
            groupBox11.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            TabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            DebugTabpage.ResumeLayout(false);
            DebugTabpage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView keyframeDataGridView;
        private Button AddKey_Button;
        private Button startPath_Button;
        private Button importPath_Button;
        private Button exportPath_Button;
        private Button button4;
        private Button button8;
        private Button teleportToSelection_Button;
        private Button replaceCurrent_Button;
        private Button dupeSelection_Button;
        private Button teleportCamera_Button;
        private GroupBox saveGroupbox;
        private TextBox hzTextbox;
        private Label label2;
        private Button button10;
        private Button button11;
        private TextBox teleportZ;
        private TextBox teleportY;
        private TextBox teleportX;
        private TextBox movementOffsetX;
        private TextBox movementOffsetY;
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
        private Button importPathWithOffset;
        private Button button1;
        private GroupBox keyframeDataGridGroupBox;
        private CheckBox directLookCheckbox;
        private CheckBox timesyncCheckbox;
        private ComboBox trackListCombo;
        private CheckBox trackingCheckbox;
        private Label label1;
        private TextBox StartDelayTextbox;
        private TextBox timeSyncTextbox;
        private Button setSyncStart;
        private TabControl TabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private GroupBox groupBox3;
        private TabPage DebugTabpage;
        private CheckBox BlockKeyPress;
        private ComboBox comboBoxGames;
        private ComboBox comboBoxBuilds;
        private ComboBox comboBoxProcesses;
        private Label label3;
        private TextBox endDelay;
        private CheckBox pauseTicks;
    }
}