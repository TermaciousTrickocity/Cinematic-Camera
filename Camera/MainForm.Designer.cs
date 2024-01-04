using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Camera
{
    partial class CameraForm
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

        double initialWidthRatio;
        double initialHeightRatio;

        // Define the event handler
        private void Form_Resize(object sender, EventArgs e)
        {
            //Set the width of the 'keyframeDataGridView' control to be half the width of the parent form
            //keyframeDataGridView.Width = (int)(this.Width * initialWidthRatio);
            //keyframeDataGridView.Height = (int)(this.Height * initialHeightRatio);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            
            this.Resize += new EventHandler(Form_Resize);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraForm));
            mainGroupBox = new System.Windows.Forms.GroupBox();
            scrollablePanel = new System.Windows.Forms.Panel();
            groupBox8 = new System.Windows.Forms.GroupBox();
            button1 = new System.Windows.Forms.Button();
            groupBox4 = new System.Windows.Forms.GroupBox();
            targetTextbox = new System.Windows.Forms.TextBox();
            enableLookTarget = new System.Windows.Forms.CheckBox();
            setTarget = new System.Windows.Forms.Button();
            groupBox3 = new System.Windows.Forms.GroupBox();
            resetAxis = new System.Windows.Forms.Button();
            checkBox6 = new System.Windows.Forms.CheckBox();
            checkBox5 = new System.Windows.Forms.CheckBox();
            checkBox4 = new System.Windows.Forms.CheckBox();
            checkBox3 = new System.Windows.Forms.CheckBox();
            checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            teleportToOrigin = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label27 = new System.Windows.Forms.Label();
            label26 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            teleportCameraY = new System.Windows.Forms.TextBox();
            teleportCameraZ = new System.Windows.Forms.TextBox();
            teleportCameraX = new System.Windows.Forms.TextBox();
            teleportCameraButton = new System.Windows.Forms.Button();
            resetCameraRotation = new System.Windows.Forms.Button();
            applyModifiers = new System.Windows.Forms.Button();
            label25 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            quickAccessSpeed = new System.Windows.Forms.TextBox();
            fovTextbox = new System.Windows.Forms.TextBox();
            rollAngle = new System.Windows.Forms.TextBox();
            groupBox9 = new System.Windows.Forms.GroupBox();
            decFovKeybind = new System.Windows.Forms.Button();
            decRollKeybind = new System.Windows.Forms.Button();
            incRollKeybind = new System.Windows.Forms.Button();
            addPointKeybind = new System.Windows.Forms.Button();
            incFovKeybind = new System.Windows.Forms.Button();
            stopPathingKeybind = new System.Windows.Forms.Button();
            enablePathingHotkeyButton = new System.Windows.Forms.Button();
            fovDecTextBox = new System.Windows.Forms.TextBox();
            fovIncTextBox = new System.Windows.Forms.TextBox();
            rollDecTextBox = new System.Windows.Forms.TextBox();
            rollIncTextBox = new System.Windows.Forms.TextBox();
            addPointTextBox = new System.Windows.Forms.TextBox();
            stopPathingTextBox = new System.Windows.Forms.TextBox();
            enablePathingTextBox = new System.Windows.Forms.TextBox();
            label17 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            refreshProcess = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            processCombobox = new System.Windows.Forms.ComboBox();
            updateModules = new System.Windows.Forms.Button();
            label13 = new System.Windows.Forms.Label();
            pluginAddressCombobox = new System.Windows.Forms.ComboBox();
            cameraGroupbox = new System.Windows.Forms.GroupBox();
            label22 = new System.Windows.Forms.Label();
            fovStatus = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            cameraX = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            cameraY = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            cameraZ = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            cameraYaw = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            cameraPitch = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            cameraRoll = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            cameraSpeed = new System.Windows.Forms.TextBox();
            //openPlugin = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            targetFPS = new System.Windows.Forms.TextBox();
            label19 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            keyProgress = new System.Windows.Forms.TextBox();
            label18 = new System.Windows.Forms.Label();
            replaceWithCurrentPos = new System.Windows.Forms.Button();
            gotoSelectedKey = new System.Windows.Forms.Button();
            pathDuration = new System.Windows.Forms.TextBox();
            label12 = new System.Windows.Forms.Label();
            dupeStartToEnd = new System.Windows.Forms.Button();
            enablePathing = new System.Windows.Forms.CheckBox();
            exportPathButton = new System.Windows.Forms.Button();
            keyFrameListLoop = new System.Windows.Forms.CheckBox();
            importPathButton = new System.Windows.Forms.Button();
            startToEndLoop = new System.Windows.Forms.CheckBox();
            deleteKeyframeButton = new System.Windows.Forms.Button();
            createKeyframeButton = new System.Windows.Forms.Button();
            keyframeDataGridView = new System.Windows.Forms.DataGridView();
            groupBox7 = new System.Windows.Forms.GroupBox();
            updateDuration = new System.Windows.Forms.Button();
            timelinePanel = new System.Windows.Forms.Panel();
            mainGroupBox.SuspendLayout();
            scrollablePanel.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox9.SuspendLayout();
            cameraGroupbox.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)keyframeDataGridView).BeginInit();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // mainGroupBox
            // 
            mainGroupBox.Controls.Add(scrollablePanel);
            mainGroupBox.Controls.Add(refreshProcess);
            mainGroupBox.Controls.Add(label8);
            mainGroupBox.Controls.Add(processCombobox);
            mainGroupBox.Controls.Add(updateModules);
            mainGroupBox.Controls.Add(label13);
            mainGroupBox.Controls.Add(pluginAddressCombobox);
            mainGroupBox.Controls.Add(cameraGroupbox);
            mainGroupBox.Location = new System.Drawing.Point(18, 38);
            mainGroupBox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            mainGroupBox.Name = "mainGroupBox";
            mainGroupBox.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            mainGroupBox.Size = new System.Drawing.Size(1137, 1992);
            mainGroupBox.TabIndex = 0;
            mainGroupBox.TabStop = false;
            mainGroupBox.Text = "Memory";
            // 
            // scrollablePanel
            // 
            scrollablePanel.AutoScroll = true;
            scrollablePanel.Controls.Add(groupBox8);
            scrollablePanel.Controls.Add(groupBox9);
            scrollablePanel.Location = new System.Drawing.Point(17, 723);
            scrollablePanel.Name = "scrollablePanel";
            scrollablePanel.Size = new System.Drawing.Size(1091, 917);
            scrollablePanel.TabIndex = 0;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(button1);
            groupBox8.Controls.Add(groupBox4);
            groupBox8.Controls.Add(groupBox3);
            groupBox8.Controls.Add(teleportToOrigin);
            groupBox8.Controls.Add(groupBox1);
            groupBox8.Controls.Add(resetCameraRotation);
            groupBox8.Controls.Add(applyModifiers);
            groupBox8.Controls.Add(label25);
            groupBox8.Controls.Add(label24);
            groupBox8.Controls.Add(label20);
            groupBox8.Controls.Add(quickAccessSpeed);
            groupBox8.Controls.Add(fovTextbox);
            groupBox8.Controls.Add(rollAngle);
            groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox8.Location = new System.Drawing.Point(0, 0);
            groupBox8.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox8.Size = new System.Drawing.Size(1040, 917);
            groupBox8.TabIndex = 24;
            groupBox8.TabStop = false;
            groupBox8.Text = "Modifiers";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(643, 294);
            button1.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(420, 77);
            button1.TabIndex = 57;
            button1.Text = "Teleport to world origin";
            button1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(targetTextbox);
            groupBox4.Controls.Add(enableLookTarget);
            groupBox4.Controls.Add(setTarget);
            groupBox4.Location = new System.Drawing.Point(694, 442);
            groupBox4.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox4.Size = new System.Drawing.Size(380, 320);
            groupBox4.TabIndex = 56;
            groupBox4.TabStop = false;
            groupBox4.Text = "Target";
            // 
            // targetTextbox
            // 
            targetTextbox.Location = new System.Drawing.Point(17, 224);
            targetTextbox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            targetTextbox.Name = "targetTextbox";
            targetTextbox.Size = new System.Drawing.Size(338, 55);
            targetTextbox.TabIndex = 61;
            targetTextbox.Text = "(none)";
            // 
            // enableLookTarget
            // 
            enableLookTarget.AutoSize = true;
            enableLookTarget.Location = new System.Drawing.Point(17, 144);
            enableLookTarget.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            enableLookTarget.Name = "enableLookTarget";
            enableLookTarget.Size = new System.Drawing.Size(355, 52);
            enableLookTarget.TabIndex = 41;
            enableLookTarget.Text = "Enable look target";
            enableLookTarget.UseVisualStyleBackColor = true;
            // 
            // setTarget
            // 
            setTarget.Location = new System.Drawing.Point(17, 58);
            setTarget.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            setTarget.Name = "setTarget";
            setTarget.Size = new System.Drawing.Size(274, 74);
            setTarget.TabIndex = 40;
            setTarget.Text = "Set look target";
            setTarget.UseVisualStyleBackColor = true;
            setTarget.Click += setTarget_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(resetAxis);
            groupBox3.Controls.Add(checkBox6);
            groupBox3.Controls.Add(checkBox5);
            groupBox3.Controls.Add(checkBox4);
            groupBox3.Controls.Add(checkBox3);
            groupBox3.Controls.Add(checkBox2);
            groupBox3.Controls.Add(checkBox1);
            groupBox3.Location = new System.Drawing.Point(17, 442);
            groupBox3.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox3.Size = new System.Drawing.Size(660, 320);
            groupBox3.TabIndex = 55;
            groupBox3.TabStop = false;
            groupBox3.Text = "Restrict axis";
            // 
            // resetAxis
            // 
            resetAxis.Location = new System.Drawing.Point(446, 58);
            resetAxis.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            resetAxis.Name = "resetAxis";
            resetAxis.Size = new System.Drawing.Size(197, 77);
            resetAxis.TabIndex = 61;
            resetAxis.Text = "Reset";
            resetAxis.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new System.Drawing.Point(209, 230);
            checkBox6.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new System.Drawing.Size(209, 52);
            checkBox6.TabIndex = 61;
            checkBox6.Text = "Lock Roll";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new System.Drawing.Point(209, 150);
            checkBox5.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new System.Drawing.Size(227, 52);
            checkBox5.TabIndex = 60;
            checkBox5.Text = "Lock Pitch";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new System.Drawing.Point(211, 70);
            checkBox4.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new System.Drawing.Size(210, 52);
            checkBox4.TabIndex = 59;
            checkBox4.Text = "Lock Yaw";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new System.Drawing.Point(17, 230);
            checkBox3.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new System.Drawing.Size(170, 52);
            checkBox3.TabIndex = 58;
            checkBox3.Text = "Lock Z";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new System.Drawing.Point(17, 150);
            checkBox2.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(169, 52);
            checkBox2.TabIndex = 57;
            checkBox2.Text = "Lock Y";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(17, 70);
            checkBox1.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(170, 52);
            checkBox1.TabIndex = 56;
            checkBox1.Text = "Lock X";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // teleportToOrigin
            // 
            teleportToOrigin.Location = new System.Drawing.Point(643, 154);
            teleportToOrigin.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            teleportToOrigin.Name = "teleportToOrigin";
            teleportToOrigin.Size = new System.Drawing.Size(420, 77);
            teleportToOrigin.TabIndex = 54;
            teleportToOrigin.Text = "Teleport to world origin";
            teleportToOrigin.UseVisualStyleBackColor = true;
            teleportToOrigin.Click += teleportToOrigin_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label27);
            groupBox1.Controls.Add(label26);
            groupBox1.Controls.Add(label23);
            groupBox1.Controls.Add(teleportCameraY);
            groupBox1.Controls.Add(teleportCameraZ);
            groupBox1.Controls.Add(teleportCameraX);
            groupBox1.Controls.Add(teleportCameraButton);
            groupBox1.Location = new System.Drawing.Point(1, 761);
            groupBox1.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox1.Size = new System.Drawing.Size(1057, 151);
            groupBox1.TabIndex = 49;
            groupBox1.TabStop = false;
            groupBox1.Text = "Teleportation";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new System.Drawing.Point(806, 83);
            label27.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label27.Name = "label27";
            label27.Size = new System.Drawing.Size(36, 48);
            label27.TabIndex = 60;
            label27.Text = "z";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new System.Drawing.Point(566, 83);
            label26.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label26.Name = "label26";
            label26.Size = new System.Drawing.Size(37, 48);
            label26.TabIndex = 59;
            label26.Text = "y";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new System.Drawing.Point(323, 86);
            label23.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(37, 48);
            label23.TabIndex = 56;
            label23.Text = "x";
            // 
            // teleportCameraY
            // 
            teleportCameraY.Location = new System.Drawing.Point(600, 74);
            teleportCameraY.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            teleportCameraY.Name = "teleportCameraY";
            teleportCameraY.Size = new System.Drawing.Size(190, 55);
            teleportCameraY.TabIndex = 57;
            teleportCameraY.Text = "0";
            // 
            // teleportCameraZ
            // 
            teleportCameraZ.Location = new System.Drawing.Point(843, 74);
            teleportCameraZ.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            teleportCameraZ.Name = "teleportCameraZ";
            teleportCameraZ.Size = new System.Drawing.Size(190, 55);
            teleportCameraZ.TabIndex = 56;
            teleportCameraZ.Text = "0";
            // 
            // teleportCameraX
            // 
            teleportCameraX.Location = new System.Drawing.Point(360, 74);
            teleportCameraX.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            teleportCameraX.Name = "teleportCameraX";
            teleportCameraX.Size = new System.Drawing.Size(190, 55);
            teleportCameraX.TabIndex = 58;
            teleportCameraX.Text = "0";
            // 
            // teleportCameraButton
            // 
            teleportCameraButton.Location = new System.Drawing.Point(17, 67);
            teleportCameraButton.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            teleportCameraButton.Name = "teleportCameraButton";
            teleportCameraButton.Size = new System.Drawing.Size(300, 77);
            teleportCameraButton.TabIndex = 55;
            teleportCameraButton.Text = "Teleport camera";
            teleportCameraButton.UseVisualStyleBackColor = true;
            teleportCameraButton.Click += teleportCameraButton_Click;
            // 
            // resetCameraRotation
            // 
            resetCameraRotation.Location = new System.Drawing.Point(643, 61);
            resetCameraRotation.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            resetCameraRotation.Name = "resetCameraRotation";
            resetCameraRotation.Size = new System.Drawing.Size(420, 77);
            resetCameraRotation.TabIndex = 53;
            resetCameraRotation.Text = "Reset camera rotation";
            resetCameraRotation.UseVisualStyleBackColor = true;
            resetCameraRotation.Click += resetCameraRotation_Click;
            // 
            // applyModifiers
            // 
            applyModifiers.Location = new System.Drawing.Point(34, 61);
            applyModifiers.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            applyModifiers.Name = "applyModifiers";
            applyModifiers.Size = new System.Drawing.Size(357, 77);
            applyModifiers.TabIndex = 33;
            applyModifiers.Text = "Apply all modifiers";
            applyModifiers.UseVisualStyleBackColor = true;
            applyModifiers.Click += applyModifiers_Click;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new System.Drawing.Point(31, 166);
            label25.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label25.Name = "label25";
            label25.Size = new System.Drawing.Size(250, 48);
            label25.TabIndex = 52;
            label25.Text = "Camera Speed";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new System.Drawing.Point(34, 358);
            label24.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label24.Name = "label24";
            label24.Size = new System.Drawing.Size(182, 48);
            label24.TabIndex = 51;
            label24.Text = "Roll Angle";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(31, 259);
            label20.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(207, 48);
            label20.TabIndex = 49;
            label20.Text = "Camera Fov";
            // 
            // quickAccessSpeed
            // 
            quickAccessSpeed.Location = new System.Drawing.Point(283, 157);
            quickAccessSpeed.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            quickAccessSpeed.Name = "quickAccessSpeed";
            quickAccessSpeed.Size = new System.Drawing.Size(155, 55);
            quickAccessSpeed.TabIndex = 23;
            // 
            // fovTextbox
            // 
            fovTextbox.Location = new System.Drawing.Point(283, 250);
            fovTextbox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            fovTextbox.Name = "fovTextbox";
            fovTextbox.Size = new System.Drawing.Size(155, 55);
            fovTextbox.TabIndex = 1;
            // 
            // rollAngle
            // 
            rollAngle.Location = new System.Drawing.Point(283, 349);
            rollAngle.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            rollAngle.Name = "rollAngle";
            rollAngle.Size = new System.Drawing.Size(155, 55);
            rollAngle.TabIndex = 21;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(decFovKeybind);
            groupBox9.Controls.Add(decRollKeybind);
            groupBox9.Controls.Add(incRollKeybind);
            groupBox9.Controls.Add(addPointKeybind);
            groupBox9.Controls.Add(incFovKeybind);
            groupBox9.Controls.Add(stopPathingKeybind);
            groupBox9.Controls.Add(enablePathingHotkeyButton);
            groupBox9.Controls.Add(fovDecTextBox);
            groupBox9.Controls.Add(fovIncTextBox);
            groupBox9.Controls.Add(rollDecTextBox);
            groupBox9.Controls.Add(rollIncTextBox);
            groupBox9.Controls.Add(addPointTextBox);
            groupBox9.Controls.Add(stopPathingTextBox);
            groupBox9.Controls.Add(enablePathingTextBox);
            groupBox9.Controls.Add(label17);
            groupBox9.Controls.Add(label16);
            groupBox9.Controls.Add(label15);
            groupBox9.Controls.Add(label14);
            groupBox9.Controls.Add(label11);
            groupBox9.Controls.Add(label10);
            groupBox9.Controls.Add(label9);
            groupBox9.Dock = System.Windows.Forms.DockStyle.Bottom;
            groupBox9.Location = new System.Drawing.Point(0, 917);
            groupBox9.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox9.Name = "groupBox9";
            groupBox9.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox9.Size = new System.Drawing.Size(1040, 351);
            groupBox9.TabIndex = 25;
            groupBox9.TabStop = false;
            groupBox9.Text = "Hotkeys";
            // 
            // decFovKeybind
            // 
            decFovKeybind.Location = new System.Drawing.Point(514, 608);
            decFovKeybind.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            decFovKeybind.Name = "decFovKeybind";
            decFovKeybind.Size = new System.Drawing.Size(166, 77);
            decFovKeybind.TabIndex = 48;
            decFovKeybind.Text = "Set";
            decFovKeybind.UseVisualStyleBackColor = true;
            // 
            // decRollKeybind
            // 
            decRollKeybind.Location = new System.Drawing.Point(514, 422);
            decRollKeybind.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            decRollKeybind.Name = "decRollKeybind";
            decRollKeybind.Size = new System.Drawing.Size(166, 77);
            decRollKeybind.TabIndex = 47;
            decRollKeybind.Text = "Set";
            decRollKeybind.UseVisualStyleBackColor = true;
            // 
            // incRollKeybind
            // 
            incRollKeybind.Location = new System.Drawing.Point(515, 277);
            incRollKeybind.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            incRollKeybind.Name = "incRollKeybind";
            incRollKeybind.Size = new System.Drawing.Size(166, 55);
            incRollKeybind.TabIndex = 46;
            incRollKeybind.Text = "Set";
            incRollKeybind.UseVisualStyleBackColor = true;
            // 
            // addPointKeybind
            // 
            addPointKeybind.Location = new System.Drawing.Point(515, 204);
            addPointKeybind.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            addPointKeybind.Name = "addPointKeybind";
            addPointKeybind.Size = new System.Drawing.Size(166, 55);
            addPointKeybind.TabIndex = 45;
            addPointKeybind.Text = "Set";
            addPointKeybind.UseVisualStyleBackColor = true;
            // 
            // incFovKeybind
            // 
            incFovKeybind.Location = new System.Drawing.Point(514, 518);
            incFovKeybind.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            incFovKeybind.Name = "incFovKeybind";
            incFovKeybind.Size = new System.Drawing.Size(166, 77);
            incFovKeybind.TabIndex = 45;
            incFovKeybind.Text = "Set";
            incFovKeybind.UseVisualStyleBackColor = true;
            // 
            // stopPathingKeybind
            // 
            stopPathingKeybind.Location = new System.Drawing.Point(515, 127);
            stopPathingKeybind.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            stopPathingKeybind.Name = "stopPathingKeybind";
            stopPathingKeybind.Size = new System.Drawing.Size(166, 57);
            stopPathingKeybind.TabIndex = 44;
            stopPathingKeybind.Text = "Set";
            stopPathingKeybind.UseVisualStyleBackColor = true;
            // 
            // enablePathingHotkeyButton
            // 
            enablePathingHotkeyButton.Location = new System.Drawing.Point(514, 51);
            enablePathingHotkeyButton.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            enablePathingHotkeyButton.Name = "enablePathingHotkeyButton";
            enablePathingHotkeyButton.Size = new System.Drawing.Size(166, 55);
            enablePathingHotkeyButton.TabIndex = 33;
            enablePathingHotkeyButton.Text = "Set";
            enablePathingHotkeyButton.UseVisualStyleBackColor = true;
            // 
            // fovDecTextBox
            // 
            fovDecTextBox.Location = new System.Drawing.Point(334, 608);
            fovDecTextBox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            fovDecTextBox.Name = "fovDecTextBox";
            fovDecTextBox.Size = new System.Drawing.Size(155, 55);
            fovDecTextBox.TabIndex = 32;
            fovDecTextBox.Text = "Y";
            // 
            // fovIncTextBox
            // 
            fovIncTextBox.Location = new System.Drawing.Point(334, 515);
            fovIncTextBox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            fovIncTextBox.Name = "fovIncTextBox";
            fovIncTextBox.Size = new System.Drawing.Size(155, 55);
            fovIncTextBox.TabIndex = 31;
            fovIncTextBox.Text = "U";
            // 
            // rollDecTextBox
            // 
            rollDecTextBox.Location = new System.Drawing.Point(334, 422);
            rollDecTextBox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            rollDecTextBox.Name = "rollDecTextBox";
            rollDecTextBox.Size = new System.Drawing.Size(155, 55);
            rollDecTextBox.TabIndex = 30;
            rollDecTextBox.Text = "I";
            // 
            // rollIncTextBox
            // 
            rollIncTextBox.Location = new System.Drawing.Point(334, 277);
            rollIncTextBox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            rollIncTextBox.Name = "rollIncTextBox";
            rollIncTextBox.Size = new System.Drawing.Size(155, 55);
            rollIncTextBox.TabIndex = 29;
            rollIncTextBox.Text = "P";
            // 
            // addPointTextBox
            // 
            addPointTextBox.Location = new System.Drawing.Point(334, 204);
            addPointTextBox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            addPointTextBox.Name = "addPointTextBox";
            addPointTextBox.Size = new System.Drawing.Size(155, 55);
            addPointTextBox.TabIndex = 28;
            addPointTextBox.Text = "K";
            // 
            // stopPathingTextBox
            // 
            stopPathingTextBox.Location = new System.Drawing.Point(334, 129);
            stopPathingTextBox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            stopPathingTextBox.Name = "stopPathingTextBox";
            stopPathingTextBox.Size = new System.Drawing.Size(155, 55);
            stopPathingTextBox.TabIndex = 27;
            stopPathingTextBox.Text = "N";
            // 
            // enablePathingTextBox
            // 
            enablePathingTextBox.Location = new System.Drawing.Point(334, 51);
            enablePathingTextBox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            enablePathingTextBox.Name = "enablePathingTextBox";
            enablePathingTextBox.Size = new System.Drawing.Size(155, 55);
            enablePathingTextBox.TabIndex = 24;
            enablePathingTextBox.Text = "M";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(17, 618);
            label17.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(224, 48);
            label17.TabIndex = 26;
            label17.Text = "Decrease fov";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(17, 525);
            label16.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(210, 48);
            label16.TabIndex = 25;
            label16.Text = "Increase fov";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(17, 432);
            label15.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(324, 48);
            label15.TabIndex = 24;
            label15.Text = "Decrease roll angle";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(14, 277);
            label14.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(310, 48);
            label14.TabIndex = 23;
            label14.Text = "Increase roll angle";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(18, 204);
            label11.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(178, 48);
            label11.TabIndex = 22;
            label11.Text = "Add point";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(18, 127);
            label10.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(223, 48);
            label10.TabIndex = 21;
            label10.Text = "Stop pathing";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(14, 58);
            label9.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(224, 48);
            label9.TabIndex = 20;
            label9.Text = "Start pathing";
            // 
            // refreshProcess
            // 
            refreshProcess.Location = new System.Drawing.Point(863, 58);
            refreshProcess.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            refreshProcess.Name = "refreshProcess";
            refreshProcess.Size = new System.Drawing.Size(229, 77);
            refreshProcess.TabIndex = 32;
            refreshProcess.Text = "Refresh";
            refreshProcess.UseVisualStyleBackColor = true;
            refreshProcess.Click += refreshProcess_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(34, 74);
            label8.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(140, 48);
            label8.TabIndex = 31;
            label8.Text = "Process";
            // 
            // processCombobox
            // 
            processCombobox.FormattingEnabled = true;
            processCombobox.Location = new System.Drawing.Point(186, 64);
            processCombobox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            processCombobox.Name = "processCombobox";
            processCombobox.Size = new System.Drawing.Size(653, 56);
            processCombobox.TabIndex = 30;
            processCombobox.Text = "MCC not found (Is it running?)";
            // 
            // updateModules
            // 
            updateModules.Location = new System.Drawing.Point(863, 154);
            updateModules.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            updateModules.Name = "updateModules";
            updateModules.Size = new System.Drawing.Size(229, 77);
            updateModules.TabIndex = 29;
            updateModules.Text = "Update";
            updateModules.UseVisualStyleBackColor = true;
            updateModules.Click += updateModules_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(34, 166);
            label13.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(119, 48);
            label13.TabIndex = 20;
            label13.Text = "Plugin";
            // 
            // pluginAddressCombobox
            // 
            pluginAddressCombobox.FormattingEnabled = true;
            pluginAddressCombobox.Location = new System.Drawing.Point(186, 157);
            pluginAddressCombobox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            pluginAddressCombobox.Name = "pluginAddressCombobox";
            pluginAddressCombobox.Size = new System.Drawing.Size(653, 56);
            pluginAddressCombobox.TabIndex = 26;
            pluginAddressCombobox.Text = "None (Select one!)";
            pluginAddressCombobox.SelectedIndexChanged += pluginAddressCombobox_SelectedIndexChangedAsync;
            // 
            // cameraGroupbox
            // 
            cameraGroupbox.Controls.Add(label22);
            cameraGroupbox.Controls.Add(fovStatus);
            cameraGroupbox.Controls.Add(label1);
            cameraGroupbox.Controls.Add(cameraX);
            cameraGroupbox.Controls.Add(label7);
            cameraGroupbox.Controls.Add(cameraY);
            cameraGroupbox.Controls.Add(label6);
            cameraGroupbox.Controls.Add(cameraZ);
            cameraGroupbox.Controls.Add(label5);
            cameraGroupbox.Controls.Add(cameraYaw);
            cameraGroupbox.Controls.Add(label4);
            cameraGroupbox.Controls.Add(cameraPitch);
            cameraGroupbox.Controls.Add(label3);
            cameraGroupbox.Controls.Add(cameraRoll);
            cameraGroupbox.Controls.Add(label2);
            cameraGroupbox.Controls.Add(cameraSpeed);
            cameraGroupbox.Location = new System.Drawing.Point(17, 250);
            cameraGroupbox.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraGroupbox.Name = "cameraGroupbox";
            cameraGroupbox.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraGroupbox.Size = new System.Drawing.Size(1091, 454);
            cameraGroupbox.TabIndex = 19;
            cameraGroupbox.TabStop = false;
            cameraGroupbox.Text = "Camera status:";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(17, 355);
            label22.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(207, 48);
            label22.TabIndex = 19;
            label22.Text = "Camera Fov";
            // 
            // fovStatus
            // 
            fovStatus.Location = new System.Drawing.Point(229, 346);
            fovStatus.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            fovStatus.Name = "fovStatus";
            fovStatus.ReadOnly = true;
            fovStatus.Size = new System.Drawing.Size(210, 55);
            fovStatus.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(17, 77);
            label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(172, 48);
            label1.TabIndex = 11;
            label1.Text = "Camera X";
            // 
            // cameraX
            // 
            cameraX.Location = new System.Drawing.Point(229, 67);
            cameraX.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraX.Name = "cameraX";
            cameraX.ReadOnly = true;
            cameraX.Size = new System.Drawing.Size(210, 55);
            cameraX.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(591, 262);
            label7.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(211, 48);
            label7.TabIndex = 17;
            label7.Text = "Camera Roll";
            // 
            // cameraY
            // 
            cameraY.Location = new System.Drawing.Point(229, 160);
            cameraY.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraY.Name = "cameraY";
            cameraY.ReadOnly = true;
            cameraY.Size = new System.Drawing.Size(210, 55);
            cameraY.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(591, 355);
            label6.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(250, 48);
            label6.TabIndex = 16;
            label6.Text = "Camera Speed";
            // 
            // cameraZ
            // 
            cameraZ.Location = new System.Drawing.Point(229, 253);
            cameraZ.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraZ.Name = "cameraZ";
            cameraZ.ReadOnly = true;
            cameraZ.Size = new System.Drawing.Size(210, 55);
            cameraZ.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(591, 170);
            label5.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(229, 48);
            label5.TabIndex = 15;
            label5.Text = "Camera Pitch";
            // 
            // cameraYaw
            // 
            cameraYaw.Location = new System.Drawing.Point(846, 67);
            cameraYaw.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraYaw.Name = "cameraYaw";
            cameraYaw.ReadOnly = true;
            cameraYaw.Size = new System.Drawing.Size(210, 55);
            cameraYaw.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(591, 77);
            label4.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(212, 48);
            label4.TabIndex = 14;
            label4.Text = "Camera Yaw";
            // 
            // cameraPitch
            // 
            cameraPitch.Location = new System.Drawing.Point(846, 160);
            cameraPitch.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraPitch.Name = "cameraPitch";
            cameraPitch.ReadOnly = true;
            cameraPitch.Size = new System.Drawing.Size(210, 55);
            cameraPitch.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(17, 262);
            label3.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(172, 48);
            label3.TabIndex = 13;
            label3.Text = "Camera Z";
            // 
            // cameraRoll
            // 
            cameraRoll.Location = new System.Drawing.Point(846, 253);
            cameraRoll.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraRoll.Name = "cameraRoll";
            cameraRoll.ReadOnly = true;
            cameraRoll.Size = new System.Drawing.Size(210, 55);
            cameraRoll.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(17, 170);
            label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(171, 48);
            label2.TabIndex = 12;
            label2.Text = "Camera Y";
            // 
            // cameraSpeed
            // 
            cameraSpeed.Location = new System.Drawing.Point(846, 346);
            cameraSpeed.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            cameraSpeed.Name = "cameraSpeed";
            cameraSpeed.ReadOnly = true;
            cameraSpeed.Size = new System.Drawing.Size(210, 55);
            cameraSpeed.TabIndex = 10;
            // 
            // openPlugin
            // 
            //openPlugin.Location = new System.Drawing.Point(2746, 102);
            //openPlugin.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            //openPlugin.Name = "openPlugin";
            //openPlugin.Size = new System.Drawing.Size(214, 74);
            //openPlugin.TabIndex = 0;
            //openPlugin.Text = "openPlugin";
            //openPlugin.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(targetFPS);
            groupBox2.Controls.Add(label19);
            groupBox2.Controls.Add(label21);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(keyProgress);
            groupBox2.Controls.Add(label18);
            groupBox2.Controls.Add(replaceWithCurrentPos);
            groupBox2.Controls.Add(gotoSelectedKey);
            groupBox2.Controls.Add(pathDuration);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(dupeStartToEnd);
            groupBox2.Controls.Add(enablePathing);
            groupBox2.Controls.Add(exportPathButton);
            groupBox2.Controls.Add(keyFrameListLoop);
            groupBox2.Controls.Add(importPathButton);
            groupBox2.Controls.Add(startToEndLoop);
            groupBox2.Controls.Add(deleteKeyframeButton);
            groupBox2.Controls.Add(createKeyframeButton);
            groupBox2.Location = new System.Drawing.Point(1189, 38);
            groupBox2.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox2.Size = new System.Drawing.Size(2160, 410);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Pathing";
            // 
            // targetFPS
            // 
            targetFPS.Location = new System.Drawing.Point(629, 234);
            targetFPS.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            targetFPS.Name = "targetFPS";
            targetFPS.Size = new System.Drawing.Size(150, 55);
            targetFPS.TabIndex = 39;
            targetFPS.Text = "60";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(411, 250);
            label19.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(206, 48);
            label19.TabIndex = 38;
            label19.Text = "Pathing FPS";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new System.Drawing.Point(1797, 330);
            label21.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(157, 48);
            label21.TabIndex = 35;
            label21.Text = "Progress";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(1403, 125);
            textBox1.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(150, 55);
            textBox1.TabIndex = 36;
            textBox1.Text = "5000";
            // 
            // keyProgress
            // 
            keyProgress.Location = new System.Drawing.Point(1986, 320);
            keyProgress.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            keyProgress.Name = "keyProgress";
            keyProgress.ReadOnly = true;
            keyProgress.Size = new System.Drawing.Size(150, 55);
            keyProgress.TabIndex = 34;
            keyProgress.Text = "0.00%";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(980, 134);
            label18.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(421, 48);
            label18.TabIndex = 37;
            label18.Text = "Loop delay (milliseconds)";
            // 
            // replaceWithCurrentPos
            // 
            replaceWithCurrentPos.Location = new System.Drawing.Point(1797, 144);
            replaceWithCurrentPos.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            replaceWithCurrentPos.Name = "replaceWithCurrentPos";
            replaceWithCurrentPos.Size = new System.Drawing.Size(346, 74);
            replaceWithCurrentPos.TabIndex = 31;
            replaceWithCurrentPos.Text = "Replace current";
            replaceWithCurrentPos.UseVisualStyleBackColor = true;
            replaceWithCurrentPos.Click += replaceWithCurrentPos_Click;
            // 
            // gotoSelectedKey
            // 
            gotoSelectedKey.Location = new System.Drawing.Point(1797, 237);
            gotoSelectedKey.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            gotoSelectedKey.Name = "gotoSelectedKey";
            gotoSelectedKey.Size = new System.Drawing.Size(346, 74);
            gotoSelectedKey.TabIndex = 30;
            gotoSelectedKey.Text = "Teleport to selection";
            gotoSelectedKey.UseVisualStyleBackColor = true;
            gotoSelectedKey.Click += gotoSelectedKey_Click;
            // 
            // pathDuration
            // 
            pathDuration.Location = new System.Drawing.Point(734, 122);
            pathDuration.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            pathDuration.Name = "pathDuration";
            pathDuration.Size = new System.Drawing.Size(150, 55);
            pathDuration.TabIndex = 32;
            pathDuration.Text = "10";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(411, 131);
            label12.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(318, 48);
            label12.TabIndex = 31;
            label12.Text = "Duration (seconds)";
            // 
            // dupeStartToEnd
            // 
            dupeStartToEnd.Location = new System.Drawing.Point(1797, 54);
            dupeStartToEnd.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            dupeStartToEnd.Name = "dupeStartToEnd";
            dupeStartToEnd.Size = new System.Drawing.Size(346, 70);
            dupeStartToEnd.TabIndex = 29;
            dupeStartToEnd.Text = "Add start to end";
            dupeStartToEnd.UseVisualStyleBackColor = true;
            dupeStartToEnd.Click += dupeStartToEnd_Click;
            // 
            // enablePathing
            // 
            enablePathing.AutoSize = true;
            enablePathing.Location = new System.Drawing.Point(411, 61);
            enablePathing.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            enablePathing.Name = "enablePathing";
            enablePathing.Size = new System.Drawing.Size(302, 52);
            enablePathing.TabIndex = 22;
            enablePathing.Text = "Enable pathing";
            enablePathing.UseVisualStyleBackColor = true;
            enablePathing.CheckedChanged += enablePathing_CheckedChanged;
            // 
            // exportPathButton
            // 
            exportPathButton.Location = new System.Drawing.Point(17, 320);
            exportPathButton.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            exportPathButton.Name = "exportPathButton";
            exportPathButton.Size = new System.Drawing.Size(300, 70);
            exportPathButton.TabIndex = 28;
            exportPathButton.Text = "Export path";
            exportPathButton.UseVisualStyleBackColor = true;
            exportPathButton.Click += exportPathButton_Click;
            // 
            // keyFrameListLoop
            // 
            keyFrameListLoop.AutoSize = true;
            keyFrameListLoop.Location = new System.Drawing.Point(980, 64);
            keyFrameListLoop.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            keyFrameListLoop.Name = "keyFrameListLoop";
            keyFrameListLoop.Size = new System.Drawing.Size(357, 52);
            keyFrameListLoop.TabIndex = 23;
            keyFrameListLoop.Text = "Loop keyframe list";
            keyFrameListLoop.UseVisualStyleBackColor = true;
            // 
            // importPathButton
            // 
            importPathButton.Location = new System.Drawing.Point(17, 234);
            importPathButton.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            importPathButton.Name = "importPathButton";
            importPathButton.Size = new System.Drawing.Size(300, 70);
            importPathButton.TabIndex = 27;
            importPathButton.Text = "Import path";
            importPathButton.UseVisualStyleBackColor = true;
            importPathButton.Click += importPathButton_Click;
            // 
            // startToEndLoop
            // 
            startToEndLoop.AutoSize = true;
            startToEndLoop.Location = new System.Drawing.Point(980, 237);
            startToEndLoop.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            startToEndLoop.Name = "startToEndLoop";
            startToEndLoop.Size = new System.Drawing.Size(386, 52);
            startToEndLoop.TabIndex = 24;
            startToEndLoop.Text = "Loop back and forth";
            startToEndLoop.UseVisualStyleBackColor = true;
            // 
            // deleteKeyframeButton
            // 
            deleteKeyframeButton.Location = new System.Drawing.Point(17, 144);
            deleteKeyframeButton.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            deleteKeyframeButton.Name = "deleteKeyframeButton";
            deleteKeyframeButton.Size = new System.Drawing.Size(300, 70);
            deleteKeyframeButton.TabIndex = 21;
            deleteKeyframeButton.Text = "Delete keyframe";
            deleteKeyframeButton.UseVisualStyleBackColor = true;
            deleteKeyframeButton.Click += deleteKeyframeButton_Click;
            // 
            // createKeyframeButton
            // 
            createKeyframeButton.Location = new System.Drawing.Point(17, 54);
            createKeyframeButton.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            createKeyframeButton.Name = "createKeyframeButton";
            createKeyframeButton.Size = new System.Drawing.Size(300, 70);
            createKeyframeButton.TabIndex = 20;
            createKeyframeButton.Text = "Create keyframe";
            createKeyframeButton.UseVisualStyleBackColor = true;
            createKeyframeButton.Click += createKeyframeButton_Click;
            // 
            // keyframeDataGridView
            // 
            keyframeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            keyframeDataGridView.Location = new System.Drawing.Point(1189, 467);
            keyframeDataGridView.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            keyframeDataGridView.Name = "keyframeDataGridView";
            keyframeDataGridView.RowHeadersWidth = 123;
            keyframeDataGridView.RowTemplate.Height = 50;
            keyframeDataGridView.Size = new System.Drawing.Size(2160, 1744);
            keyframeDataGridView.TabIndex = 1;
            
            //initialWidthRatio = (double)keyframeDataGridView.Width / this.Width;
            //initialHeightRatio = (double)keyframeDataGridView.Height / this.Height;
            //keyframeDataGridView.Dock = DockStyle.Fill;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(updateDuration);
            groupBox7.Controls.Add(timelinePanel);
            groupBox7.Location = new System.Drawing.Point(34, 2314);
            groupBox7.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            groupBox7.Size = new System.Drawing.Size(3314, 429);
            groupBox7.TabIndex = 33;
            groupBox7.TabStop = false;
            groupBox7.Text = "Timeline";
            // 
            // updateDuration
            // 
            updateDuration.Location = new System.Drawing.Point(20, 48);
            updateDuration.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            updateDuration.Name = "updateDuration";
            updateDuration.Size = new System.Drawing.Size(229, 77);
            updateDuration.TabIndex = 33;
            updateDuration.Text = "Update";
            updateDuration.UseVisualStyleBackColor = true;
            // 
            // timelinePanel
            // 
            timelinePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            timelinePanel.Location = new System.Drawing.Point(17, 144);
            timelinePanel.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            timelinePanel.Name = "timelinePanel";
            timelinePanel.Size = new System.Drawing.Size(3276, 261);
            timelinePanel.TabIndex = 33;
            // 
            // CameraForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(20F, 48F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(3389, 1911);
            Controls.Add(openPlugin);
            Controls.Add(keyframeDataGridView);
            Controls.Add(groupBox7);
            Controls.Add(mainGroupBox);
            Controls.Add(groupBox2);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            MinimumSize = new System.Drawing.Size(3367, 2014);
            Name = "CameraForm";
            Text = "Cinematic Tools";
            mainGroupBox.ResumeLayout(false);
            mainGroupBox.PerformLayout();
            scrollablePanel.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            cameraGroupbox.ResumeLayout(false);
            cameraGroupbox.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)keyframeDataGridView).EndInit();
            groupBox7.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion



        private System.Windows.Forms.GroupBox mainGroupBox;
        private System.Windows.Forms.Panel scrollablePanel; // Added new Panel

        private System.Windows.Forms.TextBox fovTextbox;
        private System.Windows.Forms.TextBox cameraY;
        private System.Windows.Forms.TextBox cameraX;
        private System.Windows.Forms.TextBox cameraSpeed;
        private System.Windows.Forms.TextBox cameraRoll;
        private System.Windows.Forms.TextBox cameraPitch;
        private System.Windows.Forms.TextBox cameraYaw;
        private System.Windows.Forms.TextBox cameraZ;
        private System.Windows.Forms.GroupBox cameraGroupbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cameraSway;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button createKeyframeButton;
        private System.Windows.Forms.DataGridView keyframeDataGridView;
        private System.Windows.Forms.Button deleteKeyframeButton;
        private System.Windows.Forms.CheckBox startToEndLoop;
        private System.Windows.Forms.CheckBox keyFrameListLoop;
        private System.Windows.Forms.CheckBox enablePathing;
        private System.Windows.Forms.Button exportPathButton;
        private System.Windows.Forms.Button importPathButton;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox fovStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox pathDuration;
        private System.Windows.Forms.TextBox quickAccessSpeed;
        private System.Windows.Forms.TextBox rollAngle;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Panel timelinePanel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox pluginAddressCombobox;
        private System.Windows.Forms.Button updateModules;
        private System.Windows.Forms.Button dupeStartToEnd;
        private System.Windows.Forms.Button openPlugin;
        private System.Windows.Forms.ComboBox processCombobox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button refreshProcess;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox fovDecTextBox;
        private System.Windows.Forms.TextBox fovIncTextBox;
        private System.Windows.Forms.TextBox rollDecTextBox;
        private System.Windows.Forms.TextBox rollIncTextBox;
        private System.Windows.Forms.TextBox addPointTextBox;
        private System.Windows.Forms.TextBox stopPathingTextBox;
        private System.Windows.Forms.TextBox enablePathingTextBox;
        private System.Windows.Forms.Button updateDuration;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox keyProgress;
        private System.Windows.Forms.Button decFovKeybind;
        private System.Windows.Forms.Button decRollKeybind;
        private System.Windows.Forms.Button incRollKeybind;
        private System.Windows.Forms.Button addPointKeybind;
        private System.Windows.Forms.Button incFovKeybind;
        private System.Windows.Forms.Button stopPathingKeybind;
        private System.Windows.Forms.Button enablePathingHotkeyButton;
        private System.Windows.Forms.Button gotoSelectedKey;
        private System.Windows.Forms.Button replaceWithCurrentPos;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox targetFPS;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox enableLookTarget;
        private System.Windows.Forms.Button setTarget;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button resetCameraRotation;
        private System.Windows.Forms.Button applyModifiers;
        private System.Windows.Forms.Button teleportToOrigin;
        private System.Windows.Forms.TextBox teleportCameraX;
        private System.Windows.Forms.TextBox teleportCameraY;
        private System.Windows.Forms.TextBox teleportCameraZ;
        private System.Windows.Forms.Button teleportCameraButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button resetAxis;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox targetTextbox;
        private System.Windows.Forms.Button button1;
    }
}
