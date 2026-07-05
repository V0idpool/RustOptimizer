namespace RustOptimizer
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            ROGroupBox1 = new GUI.ROGroupBox();
            launchButton1 = new GUI.LaunchButton();
            ROLabel1 = new GUI.ROLabel();
            linkLabel2 = new LinkLabel();
            gamePathString = new GUI.ROTextBox();
            gamePathSelectBtn = new GUI.ROButton();
            ROGroupBox2 = new GUI.ROGroupBox();
            lblRAMInfo = new Label();
            lblGPUInfo = new Label();
            lblCPUInfo = new Label();
            ROLabel4 = new GUI.ROLabel();
            ROLabel3 = new GUI.ROLabel();
            ROLabel2 = new GUI.ROLabel();
            ROGroupBox3 = new GUI.ROGroupBox();
            autoDetectBtn = new GUI.ROButton();
            optimizeBtn = new GUI.ROButton();
            ROLabel5 = new GUI.ROLabel();
            profileDropdown = new GUI.ROComboBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            backUpLocationToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            restoreBackupToolStripMenuItem = new ToolStripMenuItem();
            defaultSettingsToolStripMenuItem = new ToolStripMenuItem();
            exportProfileToolStripMenuItem = new ToolStripMenuItem();
            importProfileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem1 = new ToolStripMenuItem();
            openLogFileToolStripMenuItem = new ToolStripMenuItem();
            openLogFilePathToolStripMenuItem = new ToolStripMenuItem();
            openSettingsFilePathToolStripMenuItem = new ToolStripMenuItem();
            supportToolStripMenuItem = new ToolStripMenuItem();
            donateToolStripMenuItem = new ToolStripMenuItem();
            flushRamToolStripMenuItem = new ToolStripMenuItem();
            minimizeToTrayToolStripMenuItem = new ToolStripMenuItem();
            ROGroupBox4 = new GUI.ROGroupBox();
            saveBackupBtn = new GUI.ROButton();
            restoreBackupBtn = new GUI.ROButton();
            ROLabel6 = new GUI.ROLabel();
            backupDropdown = new GUI.ROComboBox();
            ROGroupBox5 = new GUI.ROGroupBox();
            ROTabControl1 = new GUI.ROTabControl();
            tabPage2 = new TabPage();
            ROLabel10 = new GUI.ROLabel();
            highPriority = new GUI.ROCheckBox();
            tabPage1 = new TabPage();
            ROLabel7 = new GUI.ROLabel();
            autoFlushChk = new GUI.ROCheckBox();
            ROLabel9 = new GUI.ROLabel();
            autoFlushinterval = new NumericUpDown();
            ROLabel8 = new GUI.ROLabel();
            autoFlushMinHour = new GUI.ROComboBox();
            autoFlushSound = new GUI.ROCheckBox();
            ROSeperator1 = new GUI.ROSeperator();
            saveAdvancedCfgBtn = new GUI.ROButton();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            linkLabel3 = new LinkLabel();
            ROGroupBox1.SuspendLayout();
            ROGroupBox2.SuspendLayout();
            ROGroupBox3.SuspendLayout();
            menuStrip1.SuspendLayout();
            ROGroupBox4.SuspendLayout();
            ROGroupBox5.SuspendLayout();
            ROTabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)autoFlushinterval).BeginInit();
            SuspendLayout();
            // 
            // ROGroupBox1
            // 
            ROGroupBox1.BackColor = Color.Transparent;
            ROGroupBox1.Controls.Add(launchButton1);
            ROGroupBox1.Controls.Add(label1);
            ROGroupBox1.Controls.Add(ROLabel1);
            ROGroupBox1.Controls.Add(linkLabel1);
            ROGroupBox1.Controls.Add(gamePathString);
            ROGroupBox1.Controls.Add(gamePathSelectBtn);
            ROGroupBox1.Font = new Font("Segoe UI", 10F);
            ROGroupBox1.Location = new Point(12, 37);
            ROGroupBox1.Name = "ROGroupBox1";
            ROGroupBox1.Padding = new Padding(10, 45, 10, 10);
            ROGroupBox1.Size = new Size(642, 137);
            ROGroupBox1.SubTitle = "Select the folder your game is installed in.";
            ROGroupBox1.TabIndex = 0;
            ROGroupBox1.Text = "ROGroupBox1";
            ROGroupBox1.Title = "Game Path";
            // 
            // launchButton1
            // 
            launchButton1.BackColor = Color.Transparent;
            launchButton1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            launchButton1.Location = new Point(15, 82);
            launchButton1.Name = "launchButton1";
            launchButton1.Size = new Size(613, 48);
            launchButton1.TabIndex = 6;
            launchButton1.Text = "LAUNCH RUST";
            launchButton1.Click += launchButton1_Click;
            // 
            // ROLabel1
            // 
            ROLabel1.BackColor = Color.FromArgb(40, 40, 40);
            ROLabel1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            ROLabel1.Location = new Point(9, 46);
            ROLabel1.Name = "ROLabel1";
            ROLabel1.Size = new Size(98, 23);
            ROLabel1.TabIndex = 3;
            ROLabel1.Text = "ROLabel1";
            ROLabel1.Value1 = " ";
            ROLabel1.Value2 = " Game Path:";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel2.LinkColor = Color.Orange;
            linkLabel2.Location = new Point(12, 679);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(125, 17);
            linkLabel2.TabIndex = 14;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "https://voidtech.xyz/";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // gamePathString
            // 
            gamePathString.BackColor = Color.Transparent;
            gamePathString.Location = new Point(113, 46);
            gamePathString.MaxLength = 32767;
            gamePathString.Multiline = false;
            gamePathString.Name = "gamePathString";
            gamePathString.ReadOnly = false;
            gamePathString.Size = new Size(432, 29);
            gamePathString.TabIndex = 2;
            gamePathString.TextAlign = HorizontalAlignment.Left;
            gamePathString.UseSystemPasswordChar = false;
            // 
            // gamePathSelectBtn
            // 
            gamePathSelectBtn.BackColor = Color.Transparent;
            gamePathSelectBtn.Font = new Font("Segoe UI", 9.5F);
            gamePathSelectBtn.Location = new Point(551, 45);
            gamePathSelectBtn.Name = "gamePathSelectBtn";
            gamePathSelectBtn.Size = new Size(75, 30);
            gamePathSelectBtn.TabIndex = 0;
            gamePathSelectBtn.Text = "Open...";
            gamePathSelectBtn.Click += gamePathSelectBtn_Click;
            // 
            // ROGroupBox2
            // 
            ROGroupBox2.BackColor = Color.Transparent;
            ROGroupBox2.Controls.Add(lblRAMInfo);
            ROGroupBox2.Controls.Add(lblGPUInfo);
            ROGroupBox2.Controls.Add(lblCPUInfo);
            ROGroupBox2.Controls.Add(ROLabel4);
            ROGroupBox2.Controls.Add(ROLabel3);
            ROGroupBox2.Controls.Add(ROLabel2);
            ROGroupBox2.Font = new Font("Segoe UI", 10F);
            ROGroupBox2.Location = new Point(12, 180);
            ROGroupBox2.Name = "ROGroupBox2";
            ROGroupBox2.Padding = new Padding(10, 45, 10, 10);
            ROGroupBox2.Size = new Size(642, 144);
            ROGroupBox2.SubTitle = "These are your hardware specs.";
            ROGroupBox2.TabIndex = 1;
            ROGroupBox2.Text = "ROGroupBox2";
            ROGroupBox2.Title = "Hardware Specs";
            // 
            // lblRAMInfo
            // 
            lblRAMInfo.AutoSize = true;
            lblRAMInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRAMInfo.Location = new Point(56, 107);
            lblRAMInfo.Name = "lblRAMInfo";
            lblRAMInfo.Size = new Size(0, 21);
            lblRAMInfo.TabIndex = 5;
            // 
            // lblGPUInfo
            // 
            lblGPUInfo.AutoSize = true;
            lblGPUInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGPUInfo.Location = new Point(56, 78);
            lblGPUInfo.Name = "lblGPUInfo";
            lblGPUInfo.Size = new Size(0, 21);
            lblGPUInfo.TabIndex = 4;
            // 
            // lblCPUInfo
            // 
            lblCPUInfo.AutoSize = true;
            lblCPUInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCPUInfo.Location = new Point(55, 49);
            lblCPUInfo.Name = "lblCPUInfo";
            lblCPUInfo.Size = new Size(0, 21);
            lblCPUInfo.TabIndex = 3;
            // 
            // ROLabel4
            // 
            ROLabel4.BackColor = Color.FromArgb(40, 40, 40);
            ROLabel4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            ROLabel4.Location = new Point(9, 107);
            ROLabel4.Name = "ROLabel4";
            ROLabel4.Size = new Size(41, 23);
            ROLabel4.TabIndex = 2;
            ROLabel4.Text = "ROLabel4";
            ROLabel4.Value1 = " ";
            ROLabel4.Value2 = "Ram:";
            // 
            // ROLabel3
            // 
            ROLabel3.BackColor = Color.FromArgb(40, 40, 40);
            ROLabel3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            ROLabel3.Location = new Point(9, 78);
            ROLabel3.Name = "ROLabel3";
            ROLabel3.Size = new Size(41, 23);
            ROLabel3.TabIndex = 1;
            ROLabel3.Text = "ROLabel3";
            ROLabel3.Value1 = " ";
            ROLabel3.Value2 = "GPU:";
            // 
            // ROLabel2
            // 
            ROLabel2.BackColor = Color.FromArgb(40, 40, 40);
            ROLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            ROLabel2.Location = new Point(9, 49);
            ROLabel2.Name = "ROLabel2";
            ROLabel2.Size = new Size(41, 23);
            ROLabel2.TabIndex = 0;
            ROLabel2.Text = "ROLabel2";
            ROLabel2.Value1 = " ";
            ROLabel2.Value2 = "CPU:";
            // 
            // ROGroupBox3
            // 
            ROGroupBox3.BackColor = Color.Transparent;
            ROGroupBox3.Controls.Add(autoDetectBtn);
            ROGroupBox3.Controls.Add(optimizeBtn);
            ROGroupBox3.Controls.Add(ROLabel5);
            ROGroupBox3.Controls.Add(profileDropdown);
            ROGroupBox3.Font = new Font("Segoe UI", 10F);
            ROGroupBox3.Location = new Point(12, 330);
            ROGroupBox3.Name = "ROGroupBox3";
            ROGroupBox3.Padding = new Padding(10, 45, 10, 10);
            ROGroupBox3.Size = new Size(311, 144);
            ROGroupBox3.SubTitle = "Select a profile to apply a set of optimized settings.";
            ROGroupBox3.TabIndex = 2;
            ROGroupBox3.Text = "ROGroupBox3";
            ROGroupBox3.Title = "Optimization Profiles";
            // 
            // autoDetectBtn
            // 
            autoDetectBtn.BackColor = Color.Transparent;
            autoDetectBtn.Font = new Font("Segoe UI", 9.5F);
            autoDetectBtn.Location = new Point(14, 102);
            autoDetectBtn.Name = "autoDetectBtn";
            autoDetectBtn.Size = new Size(98, 30);
            autoDetectBtn.TabIndex = 7;
            autoDetectBtn.Text = "Auto-Detect...";
            autoDetectBtn.Click += autoDetectBtn_Click;
            // 
            // optimizeBtn
            // 
            optimizeBtn.BackColor = Color.Transparent;
            optimizeBtn.Font = new Font("Segoe UI", 9.5F);
            optimizeBtn.Location = new Point(149, 102);
            optimizeBtn.Name = "optimizeBtn";
            optimizeBtn.Size = new Size(146, 30);
            optimizeBtn.TabIndex = 4;
            optimizeBtn.Text = "Apply Optimizations...";
            optimizeBtn.Click += optimizeBtn_Click;
            // 
            // ROLabel5
            // 
            ROLabel5.BackColor = Color.FromArgb(40, 40, 40);
            ROLabel5.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            ROLabel5.Location = new Point(9, 54);
            ROLabel5.Name = "ROLabel5";
            ROLabel5.Size = new Size(56, 23);
            ROLabel5.TabIndex = 6;
            ROLabel5.Text = "ROLabel5";
            ROLabel5.Value1 = " ";
            ROLabel5.Value2 = "Profile:";
            // 
            // profileDropdown
            // 
            profileDropdown.BackColor = Color.FromArgb(50, 50, 50);
            profileDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            profileDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            profileDropdown.Font = new Font("Segoe UI", 10F);
            profileDropdown.ForeColor = Color.White;
            profileDropdown.FormattingEnabled = true;
            profileDropdown.ItemHeight = 22;
            profileDropdown.Items.AddRange(new object[] { "Competitive (Max FPS)", "Balanced (Good-looking & Fast)", "Recommended (Optimized)", "Ultra (Maximum Visuals)" });
            profileDropdown.Location = new Point(71, 54);
            profileDropdown.Name = "profileDropdown";
            profileDropdown.Size = new Size(224, 28);
            profileDropdown.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(40, 40, 40);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem, supportToolStripMenuItem, donateToolStripMenuItem, flushRamToolStripMenuItem, minimizeToTrayToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            menuStrip1.Size = new Size(666, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backUpLocationToolStripMenuItem, exitToolStripMenuItem, restoreBackupToolStripMenuItem, defaultSettingsToolStripMenuItem, exportProfileToolStripMenuItem, importProfileToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem1 });
            fileToolStripMenuItem.ForeColor = Color.White;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // backUpLocationToolStripMenuItem
            // 
            backUpLocationToolStripMenuItem.BackColor = SystemColors.Control;
            backUpLocationToolStripMenuItem.ForeColor = Color.White;
            backUpLocationToolStripMenuItem.Name = "backUpLocationToolStripMenuItem";
            backUpLocationToolStripMenuItem.Size = new Size(164, 22);
            backUpLocationToolStripMenuItem.Text = "Back-ups Path...";
            backUpLocationToolStripMenuItem.Click += backUpLocationToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.BackColor = SystemColors.Control;
            exitToolStripMenuItem.ForeColor = Color.White;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(164, 22);
            exitToolStripMenuItem.Text = "Save Back-up...";
            exitToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // restoreBackupToolStripMenuItem
            // 
            restoreBackupToolStripMenuItem.BackColor = SystemColors.Control;
            restoreBackupToolStripMenuItem.ForeColor = Color.White;
            restoreBackupToolStripMenuItem.Name = "restoreBackupToolStripMenuItem";
            restoreBackupToolStripMenuItem.Size = new Size(164, 22);
            restoreBackupToolStripMenuItem.Text = "Restore Backup...";
            restoreBackupToolStripMenuItem.Click += restoreBackupToolStripMenuItem_Click;
            // 
            // defaultSettingsToolStripMenuItem
            // 
            defaultSettingsToolStripMenuItem.BackColor = SystemColors.Control;
            defaultSettingsToolStripMenuItem.ForeColor = Color.White;
            defaultSettingsToolStripMenuItem.Name = "defaultSettingsToolStripMenuItem";
            defaultSettingsToolStripMenuItem.Size = new Size(164, 22);
            defaultSettingsToolStripMenuItem.Text = "Load Defaults...";
            defaultSettingsToolStripMenuItem.Click += defaultSettingsToolStripMenuItem_Click;
            // 
            // exportProfileToolStripMenuItem
            // 
            exportProfileToolStripMenuItem.BackColor = SystemColors.Control;
            exportProfileToolStripMenuItem.ForeColor = Color.White;
            exportProfileToolStripMenuItem.Name = "exportProfileToolStripMenuItem";
            exportProfileToolStripMenuItem.Size = new Size(164, 22);
            exportProfileToolStripMenuItem.Text = "Export Profile...";
            exportProfileToolStripMenuItem.Click += exportProfileToolStripMenuItem_Click;
            // 
            // importProfileToolStripMenuItem
            // 
            importProfileToolStripMenuItem.BackColor = SystemColors.Control;
            importProfileToolStripMenuItem.ForeColor = Color.White;
            importProfileToolStripMenuItem.Name = "importProfileToolStripMenuItem";
            importProfileToolStripMenuItem.Size = new Size(164, 22);
            importProfileToolStripMenuItem.Text = "Import Profile...";
            importProfileToolStripMenuItem.Click += importProfileToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(161, 6);
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.BackColor = SystemColors.Control;
            exitToolStripMenuItem1.ForeColor = Color.White;
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(164, 22);
            exitToolStripMenuItem1.Text = "Exit";
            exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem1, openLogFileToolStripMenuItem, openLogFilePathToolStripMenuItem, openSettingsFilePathToolStripMenuItem });
            aboutToolStripMenuItem.ForeColor = Color.White;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(44, 20);
            aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.ForeColor = Color.White;
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new Size(205, 22);
            aboutToolStripMenuItem1.Text = "About";
            aboutToolStripMenuItem1.Click += aboutToolStripMenuItem1_Click;
            // 
            // openLogFileToolStripMenuItem
            // 
            openLogFileToolStripMenuItem.ForeColor = Color.White;
            openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            openLogFileToolStripMenuItem.Size = new Size(205, 22);
            openLogFileToolStripMenuItem.Text = "Open Log File...";
            openLogFileToolStripMenuItem.Click += openLogFileToolStripMenuItem_Click;
            // 
            // openLogFilePathToolStripMenuItem
            // 
            openLogFilePathToolStripMenuItem.ForeColor = Color.White;
            openLogFilePathToolStripMenuItem.Name = "openLogFilePathToolStripMenuItem";
            openLogFilePathToolStripMenuItem.Size = new Size(205, 22);
            openLogFilePathToolStripMenuItem.Text = "Open Log File Path...";
            openLogFilePathToolStripMenuItem.Click += openLogFilePathToolStripMenuItem_Click;
            // 
            // openSettingsFilePathToolStripMenuItem
            // 
            openSettingsFilePathToolStripMenuItem.ForeColor = Color.White;
            openSettingsFilePathToolStripMenuItem.Name = "openSettingsFilePathToolStripMenuItem";
            openSettingsFilePathToolStripMenuItem.Size = new Size(205, 22);
            openSettingsFilePathToolStripMenuItem.Text = "Open Settings File Path...";
            openSettingsFilePathToolStripMenuItem.Click += openSettingsFilePathToolStripMenuItem_Click;
            // 
            // supportToolStripMenuItem
            // 
            supportToolStripMenuItem.ForeColor = Color.White;
            supportToolStripMenuItem.Name = "supportToolStripMenuItem";
            supportToolStripMenuItem.Size = new Size(61, 20);
            supportToolStripMenuItem.Text = "Support";
            supportToolStripMenuItem.Click += supportToolStripMenuItem_Click;
            // 
            // donateToolStripMenuItem
            // 
            donateToolStripMenuItem.ForeColor = Color.White;
            donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            donateToolStripMenuItem.Size = new Size(57, 20);
            donateToolStripMenuItem.Text = "Donate";
            donateToolStripMenuItem.Click += donateToolStripMenuItem_Click;
            // 
            // flushRamToolStripMenuItem
            // 
            flushRamToolStripMenuItem.ForeColor = Color.White;
            flushRamToolStripMenuItem.Name = "flushRamToolStripMenuItem";
            flushRamToolStripMenuItem.Size = new Size(83, 20);
            flushRamToolStripMenuItem.Text = "Flush Ram...";
            flushRamToolStripMenuItem.Click += flushRamToolStripMenuItem_Click;
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            minimizeToTrayToolStripMenuItem.ForeColor = Color.White;
            minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            minimizeToTrayToolStripMenuItem.Size = new Size(116, 20);
            minimizeToTrayToolStripMenuItem.Text = "Minimize to Tray...";
            minimizeToTrayToolStripMenuItem.Click += minimizeToTrayToolStripMenuItem_Click;
            // 
            // ROGroupBox4
            // 
            ROGroupBox4.BackColor = Color.Transparent;
            ROGroupBox4.Controls.Add(saveBackupBtn);
            ROGroupBox4.Controls.Add(restoreBackupBtn);
            ROGroupBox4.Controls.Add(ROLabel6);
            ROGroupBox4.Controls.Add(backupDropdown);
            ROGroupBox4.Font = new Font("Segoe UI", 10F);
            ROGroupBox4.Location = new Point(329, 330);
            ROGroupBox4.Name = "ROGroupBox4";
            ROGroupBox4.Padding = new Padding(10, 45, 10, 10);
            ROGroupBox4.Size = new Size(325, 144);
            ROGroupBox4.SubTitle = "Back-up, and Restore your game settings";
            ROGroupBox4.TabIndex = 7;
            ROGroupBox4.Text = "ROGroupBox4";
            ROGroupBox4.Title = "Back-up & Restore";
            // 
            // saveBackupBtn
            // 
            saveBackupBtn.BackColor = Color.Transparent;
            saveBackupBtn.Font = new Font("Segoe UI", 9.5F);
            saveBackupBtn.Location = new Point(13, 102);
            saveBackupBtn.Name = "saveBackupBtn";
            saveBackupBtn.Size = new Size(104, 30);
            saveBackupBtn.TabIndex = 7;
            saveBackupBtn.Text = "Save Back-Up";
            saveBackupBtn.Click += saveBackupBtn_Click;
            // 
            // restoreBackupBtn
            // 
            restoreBackupBtn.BackColor = Color.Transparent;
            restoreBackupBtn.Font = new Font("Segoe UI", 9.5F);
            restoreBackupBtn.Location = new Point(221, 102);
            restoreBackupBtn.Name = "restoreBackupBtn";
            restoreBackupBtn.Size = new Size(88, 30);
            restoreBackupBtn.TabIndex = 4;
            restoreBackupBtn.Text = "Restore...";
            restoreBackupBtn.Click += restoreBackupBtn_Click;
            // 
            // ROLabel6
            // 
            ROLabel6.BackColor = Color.FromArgb(40, 40, 40);
            ROLabel6.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            ROLabel6.Location = new Point(9, 54);
            ROLabel6.Name = "ROLabel6";
            ROLabel6.Size = new Size(77, 23);
            ROLabel6.TabIndex = 6;
            ROLabel6.Text = "ROLabel6";
            ROLabel6.Value1 = " ";
            ROLabel6.Value2 = "Back-ups:";
            // 
            // backupDropdown
            // 
            backupDropdown.BackColor = Color.FromArgb(50, 50, 50);
            backupDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            backupDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            backupDropdown.Font = new Font("Segoe UI", 10F);
            backupDropdown.ForeColor = Color.White;
            backupDropdown.FormattingEnabled = true;
            backupDropdown.ItemHeight = 22;
            backupDropdown.Location = new Point(92, 54);
            backupDropdown.Name = "backupDropdown";
            backupDropdown.Size = new Size(217, 28);
            backupDropdown.TabIndex = 0;
            // 
            // ROGroupBox5
            // 
            ROGroupBox5.BackColor = Color.Transparent;
            ROGroupBox5.Controls.Add(ROTabControl1);
            ROGroupBox5.Controls.Add(saveAdvancedCfgBtn);
            ROGroupBox5.Font = new Font("Segoe UI", 10F);
            ROGroupBox5.Location = new Point(12, 480);
            ROGroupBox5.Name = "ROGroupBox5";
            ROGroupBox5.Padding = new Padding(10, 45, 10, 10);
            ROGroupBox5.Size = new Size(642, 196);
            ROGroupBox5.SubTitle = "Set RAM to Auto Flush, and more.";
            ROGroupBox5.TabIndex = 6;
            ROGroupBox5.Text = "ROGroupBox5";
            ROGroupBox5.Title = "Optimization Tools";
            // 
            // ROTabControl1
            // 
            ROTabControl1.Alignment = TabAlignment.Left;
            ROTabControl1.Controls.Add(tabPage2);
            ROTabControl1.Controls.Add(tabPage1);
            ROTabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            ROTabControl1.ItemSize = new Size(28, 150);
            ROTabControl1.Location = new Point(3, 49);
            ROTabControl1.Multiline = true;
            ROTabControl1.Name = "ROTabControl1";
            ROTabControl1.SelectedIndex = 0;
            ROTabControl1.Size = new Size(639, 109);
            ROTabControl1.SizeMode = TabSizeMode.Fixed;
            ROTabControl1.TabIndex = 15;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(40, 40, 40);
            tabPage2.Controls.Add(ROLabel10);
            tabPage2.Controls.Add(highPriority);
            tabPage2.Location = new Point(154, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(481, 101);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "General Tools";
            // 
            // ROLabel10
            // 
            ROLabel10.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            ROLabel10.Location = new Point(6, 7);
            ROLabel10.Name = "ROLabel10";
            ROLabel10.Size = new Size(158, 23);
            ROLabel10.TabIndex = 14;
            ROLabel10.Text = "ROLabel10";
            ROLabel10.Value1 = " ";
            ROLabel10.Value2 = "Rust CPU High Priority:";
            // 
            // highPriority
            // 
            highPriority.BackColor = Color.Transparent;
            highPriority.Checked = false;
            highPriority.Font = new Font("Segoe UI", 10F);
            highPriority.Location = new Point(170, 7);
            highPriority.Name = "highPriority";
            highPriority.Size = new Size(120, 23);
            highPriority.TabIndex = 13;
            highPriority.Text = "Enable/Disable";
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(40, 40, 40);
            tabPage1.Controls.Add(ROLabel7);
            tabPage1.Controls.Add(autoFlushChk);
            tabPage1.Controls.Add(ROLabel9);
            tabPage1.Controls.Add(autoFlushinterval);
            tabPage1.Controls.Add(ROLabel8);
            tabPage1.Controls.Add(autoFlushMinHour);
            tabPage1.Controls.Add(autoFlushSound);
            tabPage1.Controls.Add(ROSeperator1);
            tabPage1.Location = new Point(154, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(481, 101);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Auto Flush Settings";
            // 
            // ROLabel7
            // 
            ROLabel7.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            ROLabel7.Location = new Point(6, 46);
            ROLabel7.Name = "ROLabel7";
            ROLabel7.Size = new Size(158, 23);
            ROLabel7.TabIndex = 8;
            ROLabel7.Text = "ROLabel7";
            ROLabel7.Value1 = " ";
            ROLabel7.Value2 = "Auto Flush Interval:";
            // 
            // autoFlushChk
            // 
            autoFlushChk.BackColor = Color.Transparent;
            autoFlushChk.Checked = false;
            autoFlushChk.Font = new Font("Segoe UI", 10F);
            autoFlushChk.Location = new Point(170, 7);
            autoFlushChk.Name = "autoFlushChk";
            autoFlushChk.Size = new Size(120, 23);
            autoFlushChk.TabIndex = 9;
            autoFlushChk.Text = "Enable/Disable";
            autoFlushChk.CheckedChanged += autoFlushChk_CheckedChanged;
            // 
            // ROLabel9
            // 
            ROLabel9.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            ROLabel9.Location = new Point(6, 7);
            ROLabel9.Name = "ROLabel9";
            ROLabel9.Size = new Size(156, 23);
            ROLabel9.TabIndex = 12;
            ROLabel9.Text = "ROLabel9";
            ROLabel9.Value1 = " ";
            ROLabel9.Value2 = "Auto Flush Enable:";
            // 
            // autoFlushinterval
            // 
            autoFlushinterval.BackColor = Color.FromArgb(70, 70, 70);
            autoFlushinterval.ForeColor = Color.White;
            autoFlushinterval.Location = new Point(170, 44);
            autoFlushinterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            autoFlushinterval.Name = "autoFlushinterval";
            autoFlushinterval.Size = new Size(117, 25);
            autoFlushinterval.TabIndex = 0;
            autoFlushinterval.Value = new decimal(new int[] { 15, 0, 0, 0 });
            autoFlushinterval.ValueChanged += autoFlushinterval_ValueChanged;
            // 
            // ROLabel8
            // 
            ROLabel8.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            ROLabel8.Location = new Point(6, 73);
            ROLabel8.Name = "ROLabel8";
            ROLabel8.Size = new Size(158, 23);
            ROLabel8.TabIndex = 10;
            ROLabel8.Text = "ROLabel8";
            ROLabel8.Value1 = " ";
            ROLabel8.Value2 = "RAM Flush SFX:";
            // 
            // autoFlushMinHour
            // 
            autoFlushMinHour.BackColor = Color.FromArgb(50, 50, 50);
            autoFlushMinHour.DrawMode = DrawMode.OwnerDrawFixed;
            autoFlushMinHour.DropDownStyle = ComboBoxStyle.DropDownList;
            autoFlushMinHour.Font = new Font("Segoe UI", 10F);
            autoFlushMinHour.ForeColor = Color.White;
            autoFlushMinHour.FormattingEnabled = true;
            autoFlushMinHour.ItemHeight = 22;
            autoFlushMinHour.Items.AddRange(new object[] { "Minutes", "Hours" });
            autoFlushMinHour.Location = new Point(293, 43);
            autoFlushMinHour.Name = "autoFlushMinHour";
            autoFlushMinHour.Size = new Size(176, 28);
            autoFlushMinHour.TabIndex = 8;
            // 
            // autoFlushSound
            // 
            autoFlushSound.BackColor = Color.Transparent;
            autoFlushSound.Checked = false;
            autoFlushSound.Font = new Font("Segoe UI", 10F);
            autoFlushSound.Location = new Point(170, 73);
            autoFlushSound.Name = "autoFlushSound";
            autoFlushSound.Size = new Size(120, 23);
            autoFlushSound.TabIndex = 11;
            autoFlushSound.Text = "Enable/Disable";
            autoFlushSound.CheckedChanged += autoFlushSound_CheckedChanged;
            // 
            // ROSeperator1
            // 
            ROSeperator1.Location = new Point(2, 29);
            ROSeperator1.Name = "ROSeperator1";
            ROSeperator1.Size = new Size(477, 10);
            ROSeperator1.TabIndex = 13;
            ROSeperator1.Text = "ROSeperator1";
            // 
            // saveAdvancedCfgBtn
            // 
            saveAdvancedCfgBtn.BackColor = Color.Transparent;
            saveAdvancedCfgBtn.Font = new Font("Segoe UI", 9.5F);
            saveAdvancedCfgBtn.Location = new Point(8, 161);
            saveAdvancedCfgBtn.Name = "saveAdvancedCfgBtn";
            saveAdvancedCfgBtn.Size = new Size(626, 29);
            saveAdvancedCfgBtn.TabIndex = 4;
            saveAdvancedCfgBtn.Text = "Save Settings";
            saveAdvancedCfgBtn.Click += saveAdvancedCfgBtn_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            linkLabel1.LinkColor = Color.Orange;
            linkLabel1.Location = new Point(397, 20);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(258, 17);
            linkLabel1.TabIndex = 13;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://buymeacoffee.com/rustforgedev";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(420, 5);
            label1.Name = "label1";
            label1.Size = new Size(198, 15);
            label1.TabIndex = 13;
            label1.Text = "Like what I do? Consider Donating:";
            label1.Click += label1_Click;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            linkLabel3.LinkColor = Color.Orange;
            linkLabel3.Location = new Point(446, 679);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(225, 17);
            linkLabel3.TabIndex = 15;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "https://rustoptimizer.voidtech.xyz/";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // MainFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(666, 703);
            Controls.Add(linkLabel3);
            Controls.Add(ROGroupBox5);
            Controls.Add(ROGroupBox4);
            Controls.Add(ROGroupBox3);
            Controls.Add(ROGroupBox2);
            Controls.Add(ROGroupBox1);
            Controls.Add(linkLabel2);
            Controls.Add(menuStrip1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainFrm";
            Text = "Rust Optimizer";
            Load += MainFrm_Load;
            ROGroupBox1.ResumeLayout(false);
            ROGroupBox1.PerformLayout();
            ROGroupBox2.ResumeLayout(false);
            ROGroupBox2.PerformLayout();
            ROGroupBox3.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ROGroupBox4.ResumeLayout(false);
            ROGroupBox5.ResumeLayout(false);
            ROTabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)autoFlushinterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GUI.ROGroupBox ROGroupBox1;
        private GUI.ROLabel ROLabel1;
        private GUI.ROButton gamePathSelectBtn;
        private GUI.ROGroupBox ROGroupBox2;
        private Label lblGPUInfo;
        private Label lblCPUInfo;
        private GUI.ROLabel ROLabel4;
        private GUI.ROLabel ROLabel3;
        private GUI.ROLabel ROLabel2;
        private Label lblRAMInfo;
        private GUI.ROGroupBox ROGroupBox3;
        private GUI.ROLabel ROLabel5;
        private GUI.ROComboBox profileDropdown;
        private GUI.ROButton optimizeBtn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private GUI.ROGroupBox ROGroupBox4;
        private GUI.ROButton restoreBackupBtn;
        private GUI.ROLabel ROLabel6;
        private GUI.ROComboBox backupDropdown;
        private GUI.ROButton saveBackupBtn;
        private ToolStripMenuItem backUpLocationToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem defaultSettingsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem1;
        private ToolStripMenuItem supportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem restoreBackupToolStripMenuItem;
        private GUI.ROButton autoDetectBtn;
        private ToolStripMenuItem exportProfileToolStripMenuItem;
        private ToolStripMenuItem importProfileToolStripMenuItem;
        private ToolStripMenuItem flushRamToolStripMenuItem;
        private GUI.ROGroupBox ROGroupBox5;
        private GUI.ROLabel ROLabel7;
        private GUI.ROComboBox autoFlushMinHour;
        private NumericUpDown autoFlushinterval;
        private GUI.ROButton saveAdvancedCfgBtn;
        private GUI.ROLabel ROLabel9;
        private GUI.ROLabel ROLabel8;
        private GUI.ROCheckBox autoFlushSound;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private Label label1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem donateToolStripMenuItem;
        public GUI.ROCheckBox autoFlushChk;
        private GUI.ROLabel ROLabel10;
        public GUI.ROCheckBox highPriority;
        private ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private GUI.ROTabControl ROTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GUI.ROSeperator ROSeperator1;
        private ToolStripMenuItem openLogFileToolStripMenuItem;
        private ToolStripMenuItem openLogFilePathToolStripMenuItem;
        private ToolStripMenuItem openSettingsFilePathToolStripMenuItem;
        public GUI.ROTextBox gamePathString;
        private GUI.LaunchButton launchButton1;
        private LinkLabel linkLabel3;
    }
}
