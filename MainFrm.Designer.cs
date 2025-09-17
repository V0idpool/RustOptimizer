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
            nsGroupBox1 = new GUI.NSGroupBox();
            nsLabel1 = new GUI.NSLabel();
            saveBtn = new GUI.NSButton();
            gamePathString = new GUI.NSTextBox();
            gamePathSelectBtn = new GUI.NSButton();
            nsGroupBox2 = new GUI.NSGroupBox();
            lblRAMInfo = new Label();
            lblGPUInfo = new Label();
            lblCPUInfo = new Label();
            nsLabel4 = new GUI.NSLabel();
            nsLabel3 = new GUI.NSLabel();
            nsLabel2 = new GUI.NSLabel();
            nsGroupBox3 = new GUI.NSGroupBox();
            autoDetectBtn = new GUI.NSButton();
            optimizeBtn = new GUI.NSButton();
            nsLabel5 = new GUI.NSLabel();
            profileDropdown = new GUI.NSComboBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            backUpLocationToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            restoreBackupToolStripMenuItem = new ToolStripMenuItem();
            defaultSettingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem1 = new ToolStripMenuItem();
            supportToolStripMenuItem = new ToolStripMenuItem();
            nsGroupBox4 = new GUI.NSGroupBox();
            saveBackupBtn = new GUI.NSButton();
            restoreBackupBtn = new GUI.NSButton();
            nsLabel6 = new GUI.NSLabel();
            backupDropdown = new GUI.NSComboBox();
            importProfileToolStripMenuItem = new ToolStripMenuItem();
            exportProfileToolStripMenuItem = new ToolStripMenuItem();
            nsGroupBox1.SuspendLayout();
            nsGroupBox2.SuspendLayout();
            nsGroupBox3.SuspendLayout();
            menuStrip1.SuspendLayout();
            nsGroupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // nsGroupBox1
            // 
            nsGroupBox1.Controls.Add(nsLabel1);
            nsGroupBox1.Controls.Add(saveBtn);
            nsGroupBox1.Controls.Add(gamePathString);
            nsGroupBox1.Controls.Add(gamePathSelectBtn);
            nsGroupBox1.DrawSeperator = false;
            nsGroupBox1.Location = new Point(12, 37);
            nsGroupBox1.Name = "nsGroupBox1";
            nsGroupBox1.Size = new Size(642, 87);
            nsGroupBox1.SubTitle = "Select the folder your game is installed in.";
            nsGroupBox1.TabIndex = 0;
            nsGroupBox1.Text = "nsGroupBox1";
            nsGroupBox1.Title = "Game Path";
            // 
            // nsLabel1
            // 
            nsLabel1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel1.Location = new Point(9, 46);
            nsLabel1.Name = "nsLabel1";
            nsLabel1.Size = new Size(98, 23);
            nsLabel1.TabIndex = 3;
            nsLabel1.Text = "nsLabel1";
            nsLabel1.Value1 = "Game ";
            nsLabel1.Value2 = " Path:";
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(551, 46);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(75, 23);
            saveBtn.TabIndex = 1;
            saveBtn.Text = "Save";
            saveBtn.Click += saveBtn_Click;
            // 
            // gamePathString
            // 
            gamePathString.Location = new Point(113, 46);
            gamePathString.MaxLength = 32767;
            gamePathString.Multiline = false;
            gamePathString.Name = "gamePathString";
            gamePathString.ReadOnly = false;
            gamePathString.Size = new Size(351, 23);
            gamePathString.TabIndex = 2;
            gamePathString.TextAlign = HorizontalAlignment.Left;
            gamePathString.UseSystemPasswordChar = false;
            // 
            // gamePathSelectBtn
            // 
            gamePathSelectBtn.Location = new Point(470, 46);
            gamePathSelectBtn.Name = "gamePathSelectBtn";
            gamePathSelectBtn.Size = new Size(75, 23);
            gamePathSelectBtn.TabIndex = 0;
            gamePathSelectBtn.Text = "Open...";
            gamePathSelectBtn.Click += gamePathSelectBtn_Click;
            // 
            // nsGroupBox2
            // 
            nsGroupBox2.Controls.Add(lblRAMInfo);
            nsGroupBox2.Controls.Add(lblGPUInfo);
            nsGroupBox2.Controls.Add(lblCPUInfo);
            nsGroupBox2.Controls.Add(nsLabel4);
            nsGroupBox2.Controls.Add(nsLabel3);
            nsGroupBox2.Controls.Add(nsLabel2);
            nsGroupBox2.DrawSeperator = false;
            nsGroupBox2.Location = new Point(12, 130);
            nsGroupBox2.Name = "nsGroupBox2";
            nsGroupBox2.Size = new Size(642, 144);
            nsGroupBox2.SubTitle = "These are your hardware specs.";
            nsGroupBox2.TabIndex = 1;
            nsGroupBox2.Text = "nsGroupBox2";
            nsGroupBox2.Title = "Hardware Specs";
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
            // nsLabel4
            // 
            nsLabel4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel4.Location = new Point(9, 107);
            nsLabel4.Name = "nsLabel4";
            nsLabel4.Size = new Size(41, 23);
            nsLabel4.TabIndex = 2;
            nsLabel4.Text = "nsLabel4";
            nsLabel4.Value1 = " ";
            nsLabel4.Value2 = "Ram:";
            // 
            // nsLabel3
            // 
            nsLabel3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel3.Location = new Point(9, 78);
            nsLabel3.Name = "nsLabel3";
            nsLabel3.Size = new Size(41, 23);
            nsLabel3.TabIndex = 1;
            nsLabel3.Text = "nsLabel3";
            nsLabel3.Value1 = " ";
            nsLabel3.Value2 = "GPU:";
            // 
            // nsLabel2
            // 
            nsLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel2.Location = new Point(9, 49);
            nsLabel2.Name = "nsLabel2";
            nsLabel2.Size = new Size(41, 23);
            nsLabel2.TabIndex = 0;
            nsLabel2.Text = "nsLabel2";
            nsLabel2.Value1 = " ";
            nsLabel2.Value2 = "CPU:";
            // 
            // nsGroupBox3
            // 
            nsGroupBox3.Controls.Add(autoDetectBtn);
            nsGroupBox3.Controls.Add(optimizeBtn);
            nsGroupBox3.Controls.Add(nsLabel5);
            nsGroupBox3.Controls.Add(profileDropdown);
            nsGroupBox3.DrawSeperator = false;
            nsGroupBox3.Location = new Point(12, 280);
            nsGroupBox3.Name = "nsGroupBox3";
            nsGroupBox3.Size = new Size(311, 144);
            nsGroupBox3.SubTitle = "Select a profile to apply a set of optimized settings.";
            nsGroupBox3.TabIndex = 2;
            nsGroupBox3.Text = "nsGroupBox3";
            nsGroupBox3.Title = "Optimization Profiles";
            // 
            // autoDetectBtn
            // 
            autoDetectBtn.Location = new Point(14, 102);
            autoDetectBtn.Name = "autoDetectBtn";
            autoDetectBtn.Size = new Size(98, 23);
            autoDetectBtn.TabIndex = 7;
            autoDetectBtn.Text = "Auto-Detect...";
            autoDetectBtn.Click += autoDetectBtn_Click;
            // 
            // optimizeBtn
            // 
            optimizeBtn.Location = new Point(149, 102);
            optimizeBtn.Name = "optimizeBtn";
            optimizeBtn.Size = new Size(146, 23);
            optimizeBtn.TabIndex = 4;
            optimizeBtn.Text = "Apply Optimizations...";
            optimizeBtn.Click += optimizeBtn_Click;
            // 
            // nsLabel5
            // 
            nsLabel5.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel5.Location = new Point(9, 54);
            nsLabel5.Name = "nsLabel5";
            nsLabel5.Size = new Size(56, 23);
            nsLabel5.TabIndex = 6;
            nsLabel5.Text = "nsLabel5";
            nsLabel5.Value1 = " ";
            nsLabel5.Value2 = "Profile:";
            // 
            // profileDropdown
            // 
            profileDropdown.BackColor = Color.FromArgb(50, 50, 50);
            profileDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            profileDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            profileDropdown.ForeColor = Color.White;
            profileDropdown.FormattingEnabled = true;
            profileDropdown.Items.AddRange(new object[] { "Competitive (Max FPS)", "Balanced (Good-looking & Fast)", "Recommended (Optimized)", "Ultra (Maximum Visuals)" });
            profileDropdown.Location = new Point(71, 54);
            profileDropdown.Name = "profileDropdown";
            profileDropdown.Size = new Size(205, 24);
            profileDropdown.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem, supportToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            menuStrip1.Size = new Size(666, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backUpLocationToolStripMenuItem, exitToolStripMenuItem, restoreBackupToolStripMenuItem, defaultSettingsToolStripMenuItem, exportProfileToolStripMenuItem, importProfileToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem1 });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // backUpLocationToolStripMenuItem
            // 
            backUpLocationToolStripMenuItem.Name = "backUpLocationToolStripMenuItem";
            backUpLocationToolStripMenuItem.Size = new Size(180, 22);
            backUpLocationToolStripMenuItem.Text = "Back-ups Path...";
            backUpLocationToolStripMenuItem.Click += backUpLocationToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "Save Back-up...";
            exitToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // restoreBackupToolStripMenuItem
            // 
            restoreBackupToolStripMenuItem.Name = "restoreBackupToolStripMenuItem";
            restoreBackupToolStripMenuItem.Size = new Size(180, 22);
            restoreBackupToolStripMenuItem.Text = "Restore Backup...";
            restoreBackupToolStripMenuItem.Click += restoreBackupToolStripMenuItem_Click;
            // 
            // defaultSettingsToolStripMenuItem
            // 
            defaultSettingsToolStripMenuItem.Name = "defaultSettingsToolStripMenuItem";
            defaultSettingsToolStripMenuItem.Size = new Size(180, 22);
            defaultSettingsToolStripMenuItem.Text = "Load Defaults...";
            defaultSettingsToolStripMenuItem.Click += defaultSettingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(180, 22);
            exitToolStripMenuItem1.Text = "Exit";
            exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem1 });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(44, 20);
            aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new Size(180, 22);
            aboutToolStripMenuItem1.Text = "About";
            aboutToolStripMenuItem1.Click += aboutToolStripMenuItem1_Click;
            // 
            // supportToolStripMenuItem
            // 
            supportToolStripMenuItem.Name = "supportToolStripMenuItem";
            supportToolStripMenuItem.Size = new Size(61, 20);
            supportToolStripMenuItem.Text = "Support";
            supportToolStripMenuItem.Click += supportToolStripMenuItem_Click;
            // 
            // nsGroupBox4
            // 
            nsGroupBox4.Controls.Add(saveBackupBtn);
            nsGroupBox4.Controls.Add(restoreBackupBtn);
            nsGroupBox4.Controls.Add(nsLabel6);
            nsGroupBox4.Controls.Add(backupDropdown);
            nsGroupBox4.DrawSeperator = false;
            nsGroupBox4.Location = new Point(329, 280);
            nsGroupBox4.Name = "nsGroupBox4";
            nsGroupBox4.Size = new Size(325, 144);
            nsGroupBox4.SubTitle = "Back-up, and Restore your game settings";
            nsGroupBox4.TabIndex = 7;
            nsGroupBox4.Text = "nsGroupBox4";
            nsGroupBox4.Title = "Back-up & Restore";
            // 
            // saveBackupBtn
            // 
            saveBackupBtn.Location = new Point(127, 102);
            saveBackupBtn.Name = "saveBackupBtn";
            saveBackupBtn.Size = new Size(88, 23);
            saveBackupBtn.TabIndex = 7;
            saveBackupBtn.Text = "Save Back-Up";
            saveBackupBtn.Click += saveBackupBtn_Click;
            // 
            // restoreBackupBtn
            // 
            restoreBackupBtn.Location = new Point(221, 102);
            restoreBackupBtn.Name = "restoreBackupBtn";
            restoreBackupBtn.Size = new Size(88, 23);
            restoreBackupBtn.TabIndex = 4;
            restoreBackupBtn.Text = "Restore...";
            restoreBackupBtn.Click += restoreBackupBtn_Click;
            // 
            // nsLabel6
            // 
            nsLabel6.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel6.Location = new Point(9, 54);
            nsLabel6.Name = "nsLabel6";
            nsLabel6.Size = new Size(77, 23);
            nsLabel6.TabIndex = 6;
            nsLabel6.Text = "nsLabel6";
            nsLabel6.Value1 = " ";
            nsLabel6.Value2 = "Back-ups:";
            // 
            // backupDropdown
            // 
            backupDropdown.BackColor = Color.FromArgb(50, 50, 50);
            backupDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            backupDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            backupDropdown.ForeColor = Color.White;
            backupDropdown.FormattingEnabled = true;
            backupDropdown.Location = new Point(92, 54);
            backupDropdown.Name = "backupDropdown";
            backupDropdown.Size = new Size(217, 24);
            backupDropdown.TabIndex = 0;
            // 
            // importProfileToolStripMenuItem
            // 
            importProfileToolStripMenuItem.Name = "importProfileToolStripMenuItem";
            importProfileToolStripMenuItem.Size = new Size(180, 22);
            importProfileToolStripMenuItem.Text = "Import Profile...";
            importProfileToolStripMenuItem.Click += importProfileToolStripMenuItem_Click;
            // 
            // exportProfileToolStripMenuItem
            // 
            exportProfileToolStripMenuItem.Name = "exportProfileToolStripMenuItem";
            exportProfileToolStripMenuItem.Size = new Size(180, 22);
            exportProfileToolStripMenuItem.Text = "Export Profile...";
            exportProfileToolStripMenuItem.Click += exportProfileToolStripMenuItem_Click;
            // 
            // MainFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(666, 435);
            Controls.Add(nsGroupBox4);
            Controls.Add(nsGroupBox3);
            Controls.Add(nsGroupBox2);
            Controls.Add(nsGroupBox1);
            Controls.Add(menuStrip1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainFrm";
            Text = "Rust Optimizer";
            Load += MainFrm_Load;
            nsGroupBox1.ResumeLayout(false);
            nsGroupBox2.ResumeLayout(false);
            nsGroupBox2.PerformLayout();
            nsGroupBox3.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            nsGroupBox4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GUI.NSGroupBox nsGroupBox1;
        private GUI.NSLabel nsLabel1;
        private GUI.NSButton saveBtn;
        private GUI.NSTextBox gamePathString;
        private GUI.NSButton gamePathSelectBtn;
        private GUI.NSGroupBox nsGroupBox2;
        private Label lblGPUInfo;
        private Label lblCPUInfo;
        private GUI.NSLabel nsLabel4;
        private GUI.NSLabel nsLabel3;
        private GUI.NSLabel nsLabel2;
        private Label lblRAMInfo;
        private GUI.NSGroupBox nsGroupBox3;
        private GUI.NSLabel nsLabel5;
        private GUI.NSComboBox profileDropdown;
        private GUI.NSButton optimizeBtn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private GUI.NSGroupBox nsGroupBox4;
        private GUI.NSButton restoreBackupBtn;
        private GUI.NSLabel nsLabel6;
        private GUI.NSComboBox backupDropdown;
        private GUI.NSButton saveBackupBtn;
        private ToolStripMenuItem backUpLocationToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem defaultSettingsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem1;
        private ToolStripMenuItem supportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem restoreBackupToolStripMenuItem;
        private GUI.NSButton autoDetectBtn;
        private ToolStripMenuItem exportProfileToolStripMenuItem;
        private ToolStripMenuItem importProfileToolStripMenuItem;
    }
}
