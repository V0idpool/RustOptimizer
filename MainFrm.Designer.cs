using RustOptimizer.GUI;

namespace RustOptimizer
{
    partial class MainFrm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            roThemeContainer1 = new ROThemeContainer();
            pnlContent = new Panel();
            pnlProfiles = new Panel();
            grpProfiles = new ROGroupBox();
            cardProfCompetitive = new ROActionCard();
            cardProfBalanced = new ROActionCard();
            cardProfRecommended = new ROActionCard();
            cardProfUltra = new ROActionCard();
            lblScreenMode = new Label();
            screenModeDropdown = new ROComboBox();
            lblFpsLimit = new Label();
            fpsLimitDropdown = new ROComboBox();
            lblMenuFpsLimit = new Label();
            menuFpsLimitDropdown = new ROComboBox();
            profileDropdown = new ROComboBox();
            autoDetectBtn = new ROModernButton();
            optimizeBtn = new ROModernButton();
            grpBackups = new ROGroupBox();
            lblBackupCaption = new Label();
            backupDropdown = new ROComboBox();
            saveBackupBtn = new ROModernButton();
            restoreBackupBtn = new ROModernButton();
            pnlNetwork = new Panel();
            grpNetworkTweaks = new ROGroupBox();
            lblNicPowerTitle = new Label();
            lblNicPowerDesc = new Label();
            disableNicPowerToggle = new ROToggle();
            sepNet1 = new ROSeparator();
            lblQosTitle = new Label();
            lblQosDesc = new Label();
            qosDscpToggle = new ROToggle();
            btnApplyNetworkTweaks = new ROModernButton();
            btnFlushDns = new ROModernButton();
            grpPingMonitor = new ROGroupBox();
            txtServerIp = new ROTextBox();
            pingCountNumeric = new RONumericUpDown();
            btnRunPingTest = new ROModernButton();
            lblPingAvg = new ROStatPill();
            lblPingJitter = new ROStatPill();
            lblPacketLoss = new ROStatPill();
            pnlAutoFlush = new Panel();
            grpAutoFlush = new ROGroupBox();
            lblAutoFlushTitle = new Label();
            lblAutoFlushDesc = new Label();
            autoFlushChk = new ROToggle();
            sepFlush1 = new ROSeparator();
            lblSmartFlushTitle = new Label();
            lblSmartFlushDesc = new Label();
            smartFlushToggle = new ROToggle();
            sepFlush2 = new ROSeparator();
            lblSmartThresholdTitle = new Label();
            lblSmartThresholdDesc = new Label();
            smartFlushThreshold = new RONumericUpDown();
            sepFlush3 = new ROSeparator();
            lblIntervalTitle = new Label();
            lblIntervalDesc = new Label();
            autoFlushinterval = new RONumericUpDown();
            autoFlushMinHour = new ROComboBox();
            sepFlush4 = new ROSeparator();
            lblFlushSfxTitle = new Label();
            lblFlushSfxDesc = new Label();
            autoFlushSound = new ROToggle();
            btnSaveFlush = new ROModernButton();
            btnFlushNow = new ROModernButton();
            pnlTools = new Panel();
            grpSystemTweaks = new ROGroupBox();
            lblPriorityTitle = new Label();
            lblPriorityDesc = new Label();
            highPriority = new ROToggle();
            sepTool1 = new ROSeparator();
            lblPCoresTitle = new Label();
            lblPCoresDesc = new Label();
            pCoresToggle = new ROToggle();
            sepTool2 = new ROSeparator();
            lblFsoTitle = new Label();
            lblFsoDesc = new Label();
            disabledFSO = new ROToggle();
            sepTool3 = new ROSeparator();
            lblPowerPlanTitle = new Label();
            lblPowerPlanDesc = new Label();
            autoPowerPlanToggle = new ROToggle();
            sepTool4 = new ROSeparator();
            lblGpuPrefTitle = new Label();
            lblGpuPrefDesc = new Label();
            highPerfGpuToggle = new ROToggle();
            sepTool5 = new ROSeparator();
            lblGameModeTitle = new Label();
            lblGameModeDesc = new Label();
            gameModeDvrToggle = new ROToggle();
            sepTool6 = new ROSeparator();
            lblThrottleTitle = new Label();
            lblThrottleDesc = new Label();
            throttleAppsToggle = new ROToggle();
            grpCacheCleaners = new ROGroupBox();
            clearSkinCacheBtn = new ROModernButton();
            clearShaderCacheBtn = new ROModernButton();
            saveAdvancedCfgBtn = new ROModernButton();
            pnlDashboard = new Panel();
            grpGamePath = new ROGroupBox();
            gamePathString = new ROTextBox();
            gamePathSelectBtn = new ROModernButton();
            grpHardware = new ROGroupBox();
            lblCPUInfo = new ROStatPill();
            lblGPUInfo = new ROStatPill();
            lblRAMInfo = new ROStatPill();
            grpQuickActions = new ROGroupBox();
            linkDonate = new AboutLinkTile();
            cardQuickFlush = new ROActionCard();
            cardQuickCache = new ROActionCard();
            btnDashTray = new ROModernButton();
            pnlAbout = new Panel();
            grpLinks = new ROGroupBox();
            cardPanel = new Panel();
            roLabel2 = new ROLabel();
            lblDescription = new Label();
            linkWebsite = new AboutLinkTile();
            aboutLinkTile1 = new AboutLinkTile();
            linkDocs = new AboutLinkTile();
            linkDiscord = new AboutLinkTile();
            linkGitHub = new AboutLinkTile();
            grpDiagnostics = new ROGroupBox();
            btnOpenLog = new ROModernButton();
            btnOpenLogDir = new ROModernButton();
            btnOpenSettingsDir = new ROModernButton();
            pnlSidebar = new Panel();
            label1 = new Label();
            updateToggle = new ROToggle();
            _lnkWebsite = new LinkLabel();
            roLabel1 = new ROLabel();
            navDashboard = new ROSidebarItem();
            navProfiles = new ROSidebarItem();
            navTools = new ROSidebarItem();
            navAutoFlush = new ROSidebarItem();
            navNetwork = new ROSidebarItem();
            navAbout = new ROSidebarItem();
            sepSidebar = new ROSeparator();
            launchButton1 = new ROLaunchButton();
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
            openLogFileToolStripMenuItem = new ToolStripMenuItem();
            openLogFilePathToolStripMenuItem = new ToolStripMenuItem();
            openSettingsFilePathToolStripMenuItem = new ToolStripMenuItem();
            supportToolStripMenuItem = new ToolStripMenuItem();
            flushRamToolStripMenuItem = new ToolStripMenuItem();
            minimizeToTrayToolStripMenuItem = new ToolStripMenuItem();
            roThemeContainer1.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlProfiles.SuspendLayout();
            grpProfiles.SuspendLayout();
            grpBackups.SuspendLayout();
            pnlNetwork.SuspendLayout();
            grpNetworkTweaks.SuspendLayout();
            grpPingMonitor.SuspendLayout();
            pnlAutoFlush.SuspendLayout();
            grpAutoFlush.SuspendLayout();
            pnlTools.SuspendLayout();
            grpSystemTweaks.SuspendLayout();
            grpCacheCleaners.SuspendLayout();
            pnlDashboard.SuspendLayout();
            grpGamePath.SuspendLayout();
            grpHardware.SuspendLayout();
            grpQuickActions.SuspendLayout();
            pnlAbout.SuspendLayout();
            grpLinks.SuspendLayout();
            cardPanel.SuspendLayout();
            grpDiagnostics.SuspendLayout();
            pnlSidebar.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // roThemeContainer1
            // 
            roThemeContainer1.BackColor = Color.FromArgb(14, 13, 12);
            roThemeContainer1.BorderStyle = FormBorderStyle.None;
            roThemeContainer1.Controls.Add(pnlContent);
            roThemeContainer1.Controls.Add(pnlSidebar);
            roThemeContainer1.Controls.Add(menuStrip1);
            roThemeContainer1.Dock = DockStyle.Fill;
            roThemeContainer1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            roThemeContainer1.Image = Properties.Resources.ROIcon;
            roThemeContainer1.Location = new Point(0, 0);
            roThemeContainer1.Movable = true;
            roThemeContainer1.Name = "roThemeContainer1";
            roThemeContainer1.Sizable = false;
            roThemeContainer1.Size = new Size(920, 600);
            roThemeContainer1.SmartBounds = true;
            roThemeContainer1.StartPosition = FormStartPosition.CenterScreen;
            roThemeContainer1.SubHeader = "v";
            roThemeContainer1.TabIndex = 0;
            roThemeContainer1.Text = "RUST OPTIMIZER";
            roThemeContainer1.TransparencyKey = Color.Empty;
            roThemeContainer1.Transparent = false;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(14, 13, 12);
            pnlContent.Controls.Add(pnlDashboard);
            pnlContent.Controls.Add(pnlAbout);
            pnlContent.Controls.Add(pnlProfiles);
            pnlContent.Controls.Add(pnlNetwork);
            pnlContent.Controls.Add(pnlAutoFlush);
            pnlContent.Controls.Add(pnlTools);
            pnlContent.Location = new Point(214, 74);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(705, 525);
            pnlContent.TabIndex = 2;
            // 
            // pnlProfiles
            // 
            pnlProfiles.BackColor = Color.FromArgb(14, 13, 12);
            pnlProfiles.Controls.Add(grpProfiles);
            pnlProfiles.Controls.Add(grpBackups);
            pnlProfiles.Dock = DockStyle.Fill;
            pnlProfiles.Location = new Point(0, 0);
            pnlProfiles.Name = "pnlProfiles";
            pnlProfiles.Size = new Size(705, 525);
            pnlProfiles.TabIndex = 1;
            pnlProfiles.Visible = false;
            // 
            // grpProfiles
            // 
            grpProfiles.BackColor = Color.Transparent;
            grpProfiles.Controls.Add(cardProfCompetitive);
            grpProfiles.Controls.Add(cardProfBalanced);
            grpProfiles.Controls.Add(cardProfRecommended);
            grpProfiles.Controls.Add(cardProfUltra);
            grpProfiles.Controls.Add(lblScreenMode);
            grpProfiles.Controls.Add(screenModeDropdown);
            grpProfiles.Controls.Add(lblFpsLimit);
            grpProfiles.Controls.Add(fpsLimitDropdown);
            grpProfiles.Controls.Add(lblMenuFpsLimit);
            grpProfiles.Controls.Add(menuFpsLimitDropdown);
            grpProfiles.Controls.Add(profileDropdown);
            grpProfiles.Controls.Add(autoDetectBtn);
            grpProfiles.Controls.Add(optimizeBtn);
            grpProfiles.Font = new Font("Segoe UI", 9.5F);
            grpProfiles.Location = new Point(16, 14);
            grpProfiles.Name = "grpProfiles";
            grpProfiles.Padding = new Padding(14, 44, 14, 14);
            grpProfiles.Size = new Size(673, 290);
            grpProfiles.SubTitle = "Auto-scales gc.buffer & NVIDIA Reflex from hardware";
            grpProfiles.TabIndex = 0;
            grpProfiles.Title = "Optimization Profiles";
            // 
            // cardProfCompetitive
            // 
            cardProfCompetitive.BackColor = Color.Transparent;
            cardProfCompetitive.BadgeCharacter = "⚡";
            cardProfCompetitive.Location = new Point(14, 46);
            cardProfCompetitive.Name = "cardProfCompetitive";
            cardProfCompetitive.Selected = false;
            cardProfCompetitive.Size = new Size(316, 62);
            cardProfCompetitive.Subtitle = "Stripped visuals for maximum FPS & lowest latency";
            cardProfCompetitive.TabIndex = 0;
            cardProfCompetitive.Title = "Competitive (Max FPS)";
            // 
            // cardProfBalanced
            // 
            cardProfBalanced.BackColor = Color.Transparent;
            cardProfBalanced.BadgeCharacter = "◈";
            cardProfBalanced.Location = new Point(342, 46);
            cardProfBalanced.Name = "cardProfBalanced";
            cardProfBalanced.Selected = false;
            cardProfBalanced.Size = new Size(317, 62);
            cardProfBalanced.Subtitle = "Clean visuals balanced with smooth frame pacing";
            cardProfBalanced.TabIndex = 1;
            cardProfBalanced.Title = "Balanced";
            // 
            // cardProfRecommended
            // 
            cardProfRecommended.BackColor = Color.Transparent;
            cardProfRecommended.BadgeCharacter = "★";
            cardProfRecommended.Location = new Point(14, 114);
            cardProfRecommended.Name = "cardProfRecommended";
            cardProfRecommended.Selected = false;
            cardProfRecommended.Size = new Size(316, 62);
            cardProfRecommended.Subtitle = "Best all-around Rust performance configuration";
            cardProfRecommended.TabIndex = 2;
            cardProfRecommended.Title = "Recommended (Optimized)";
            // 
            // cardProfUltra
            // 
            cardProfUltra.BackColor = Color.Transparent;
            cardProfUltra.BadgeCharacter = "✦";
            cardProfUltra.Location = new Point(342, 114);
            cardProfUltra.Name = "cardProfUltra";
            cardProfUltra.Selected = false;
            cardProfUltra.Size = new Size(317, 62);
            cardProfUltra.Subtitle = "True max shadows, water, grass & draw distance";
            cardProfUltra.TabIndex = 3;
            cardProfUltra.Title = "Ultra (Maximum Visuals)";
            // 
            // lblScreenMode
            // 
            lblScreenMode.AutoSize = true;
            lblScreenMode.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblScreenMode.ForeColor = Color.FromArgb(165, 156, 148);
            lblScreenMode.Location = new Point(14, 183);
            lblScreenMode.Name = "lblScreenMode";
            lblScreenMode.Size = new Size(199, 13);
            lblScreenMode.TabIndex = 4;
            lblScreenMode.Text = "Screen Mode (graphics.screenmode):";
            // 
            // screenModeDropdown
            // 
            screenModeDropdown.BackColor = Color.FromArgb(28, 25, 23);
            screenModeDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            screenModeDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            screenModeDropdown.Font = new Font("Segoe UI", 9F);
            screenModeDropdown.ForeColor = Color.FromArgb(242, 238, 234);
            screenModeDropdown.ItemHeight = 24;
            screenModeDropdown.Items.AddRange(new object[] { "Exclusive Fullscreen (1)", "Borderless Windowed (0)" });
            screenModeDropdown.Location = new Point(14, 200);
            screenModeDropdown.Name = "screenModeDropdown";
            screenModeDropdown.Size = new Size(222, 30);
            screenModeDropdown.TabIndex = 5;
            // 
            // lblFpsLimit
            // 
            lblFpsLimit.AutoSize = true;
            lblFpsLimit.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblFpsLimit.ForeColor = Color.FromArgb(165, 156, 148);
            lblFpsLimit.Location = new Point(248, 183);
            lblFpsLimit.Name = "lblFpsLimit";
            lblFpsLimit.Size = new Size(152, 13);
            lblFpsLimit.TabIndex = 6;
            lblFpsLimit.Text = "In-Game FPS Cap (fps.limit):";
            // 
            // fpsLimitDropdown
            // 
            fpsLimitDropdown.BackColor = Color.FromArgb(28, 25, 23);
            fpsLimitDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            fpsLimitDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            fpsLimitDropdown.Font = new Font("Segoe UI", 9F);
            fpsLimitDropdown.ForeColor = Color.FromArgb(242, 238, 234);
            fpsLimitDropdown.ItemHeight = 24;
            fpsLimitDropdown.Items.AddRange(new object[] { "Unlimited (0)", "60", "90", "120", "144", "165", "180", "200", "240", "300" });
            fpsLimitDropdown.Location = new Point(248, 200);
            fpsLimitDropdown.Name = "fpsLimitDropdown";
            fpsLimitDropdown.Size = new Size(198, 30);
            fpsLimitDropdown.TabIndex = 7;
            // 
            // lblMenuFpsLimit
            // 
            lblMenuFpsLimit.AutoSize = true;
            lblMenuFpsLimit.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblMenuFpsLimit.ForeColor = Color.FromArgb(165, 156, 148);
            lblMenuFpsLimit.Location = new Point(458, 183);
            lblMenuFpsLimit.Name = "lblMenuFpsLimit";
            lblMenuFpsLimit.Size = new Size(179, 13);
            lblMenuFpsLimit.TabIndex = 8;
            lblMenuFpsLimit.Text = "Menu FPS Cap (fps.limitinmenu):";
            // 
            // menuFpsLimitDropdown
            // 
            menuFpsLimitDropdown.BackColor = Color.FromArgb(28, 25, 23);
            menuFpsLimitDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            menuFpsLimitDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            menuFpsLimitDropdown.Font = new Font("Segoe UI", 9F);
            menuFpsLimitDropdown.ForeColor = Color.FromArgb(242, 238, 234);
            menuFpsLimitDropdown.ItemHeight = 24;
            menuFpsLimitDropdown.Items.AddRange(new object[] { "Unlimited (0)", "30", "60", "90", "120" });
            menuFpsLimitDropdown.Location = new Point(458, 200);
            menuFpsLimitDropdown.Name = "menuFpsLimitDropdown";
            menuFpsLimitDropdown.Size = new Size(201, 30);
            menuFpsLimitDropdown.TabIndex = 9;
            // 
            // profileDropdown
            // 
            profileDropdown.BackColor = Color.FromArgb(28, 25, 23);
            profileDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            profileDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            profileDropdown.Font = new Font("Segoe UI", 9.25F);
            profileDropdown.ForeColor = Color.FromArgb(242, 238, 234);
            profileDropdown.ItemHeight = 26;
            profileDropdown.Items.AddRange(new object[] { "Competitive (Max FPS)", "Balanced (Good Performance & Looks)", "Recommended (Optimized)", "Ultra (Maximum Visuals)" });
            profileDropdown.Location = new Point(14, 244);
            profileDropdown.Name = "profileDropdown";
            profileDropdown.Size = new Size(358, 32);
            profileDropdown.TabIndex = 10;
            // 
            // autoDetectBtn
            // 
            autoDetectBtn.AccentColor = Color.FromArgb(224, 83, 40);
            autoDetectBtn.BackColor = Color.Transparent;
            autoDetectBtn.CornerRadius = 8;
            autoDetectBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            autoDetectBtn.ForeColor = Color.FromArgb(242, 238, 234);
            autoDetectBtn.IsPrimaryAction = false;
            autoDetectBtn.Location = new Point(382, 244);
            autoDetectBtn.Name = "autoDetectBtn";
            autoDetectBtn.Size = new Size(126, 32);
            autoDetectBtn.TabIndex = 11;
            autoDetectBtn.Text = "Auto-Detect";
            autoDetectBtn.Click += autoDetectBtn_Click;
            // 
            // optimizeBtn
            // 
            optimizeBtn.AccentColor = Color.FromArgb(224, 83, 40);
            optimizeBtn.BackColor = Color.Transparent;
            optimizeBtn.CornerRadius = 8;
            optimizeBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            optimizeBtn.ForeColor = Color.FromArgb(242, 238, 234);
            optimizeBtn.IsPrimaryAction = true;
            optimizeBtn.Location = new Point(516, 244);
            optimizeBtn.Name = "optimizeBtn";
            optimizeBtn.Size = new Size(143, 32);
            optimizeBtn.TabIndex = 12;
            optimizeBtn.Text = "Apply Profile";
            optimizeBtn.Click += optimizeBtn_Click;
            // 
            // grpBackups
            // 
            grpBackups.BackColor = Color.Transparent;
            grpBackups.Controls.Add(lblBackupCaption);
            grpBackups.Controls.Add(backupDropdown);
            grpBackups.Controls.Add(saveBackupBtn);
            grpBackups.Controls.Add(restoreBackupBtn);
            grpBackups.Font = new Font("Segoe UI", 9.5F);
            grpBackups.Location = new Point(16, 314);
            grpBackups.Name = "grpBackups";
            grpBackups.Padding = new Padding(14, 44, 14, 14);
            grpBackups.Size = new Size(673, 186);
            grpBackups.SubTitle = "Snapshot and recover your client.cfg";
            grpBackups.TabIndex = 1;
            grpBackups.Title = "Backup & Restore";
            // 
            // lblBackupCaption
            // 
            lblBackupCaption.AutoSize = true;
            lblBackupCaption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblBackupCaption.ForeColor = Color.FromArgb(165, 156, 148);
            lblBackupCaption.Location = new Point(14, 52);
            lblBackupCaption.Name = "lblBackupCaption";
            lblBackupCaption.Size = new Size(172, 15);
            lblBackupCaption.TabIndex = 0;
            lblBackupCaption.Text = "Saved Configuration Backups:";
            // 
            // backupDropdown
            // 
            backupDropdown.BackColor = Color.FromArgb(28, 25, 23);
            backupDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            backupDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            backupDropdown.Font = new Font("Segoe UI", 9.25F);
            backupDropdown.ForeColor = Color.FromArgb(242, 238, 234);
            backupDropdown.ItemHeight = 26;
            backupDropdown.Location = new Point(14, 74);
            backupDropdown.Name = "backupDropdown";
            backupDropdown.Size = new Size(645, 32);
            backupDropdown.TabIndex = 1;
            // 
            // saveBackupBtn
            // 
            saveBackupBtn.AccentColor = Color.FromArgb(224, 83, 40);
            saveBackupBtn.BackColor = Color.Transparent;
            saveBackupBtn.CornerRadius = 8;
            saveBackupBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            saveBackupBtn.ForeColor = Color.FromArgb(242, 238, 234);
            saveBackupBtn.IsPrimaryAction = false;
            saveBackupBtn.Location = new Point(14, 126);
            saveBackupBtn.Name = "saveBackupBtn";
            saveBackupBtn.Size = new Size(316, 36);
            saveBackupBtn.TabIndex = 2;
            saveBackupBtn.Text = "Save New Backup";
            saveBackupBtn.Click += saveBackupBtn_Click;
            // 
            // restoreBackupBtn
            // 
            restoreBackupBtn.AccentColor = Color.FromArgb(224, 83, 40);
            restoreBackupBtn.BackColor = Color.Transparent;
            restoreBackupBtn.CornerRadius = 8;
            restoreBackupBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            restoreBackupBtn.ForeColor = Color.FromArgb(242, 238, 234);
            restoreBackupBtn.IsPrimaryAction = true;
            restoreBackupBtn.Location = new Point(342, 126);
            restoreBackupBtn.Name = "restoreBackupBtn";
            restoreBackupBtn.Size = new Size(317, 36);
            restoreBackupBtn.TabIndex = 3;
            restoreBackupBtn.Text = "Restore Selected Backup";
            restoreBackupBtn.Click += restoreBackupBtn_Click;
            // 
            // pnlNetwork
            // 
            pnlNetwork.BackColor = Color.FromArgb(14, 13, 12);
            pnlNetwork.Controls.Add(grpNetworkTweaks);
            pnlNetwork.Controls.Add(btnApplyNetworkTweaks);
            pnlNetwork.Controls.Add(btnFlushDns);
            pnlNetwork.Controls.Add(grpPingMonitor);
            pnlNetwork.Dock = DockStyle.Fill;
            pnlNetwork.Location = new Point(0, 0);
            pnlNetwork.Name = "pnlNetwork";
            pnlNetwork.Size = new Size(705, 525);
            pnlNetwork.TabIndex = 4;
            pnlNetwork.Visible = false;
            // 
            // grpNetworkTweaks
            // 
            grpNetworkTweaks.BackColor = Color.Transparent;
            grpNetworkTweaks.Controls.Add(lblNicPowerTitle);
            grpNetworkTweaks.Controls.Add(lblNicPowerDesc);
            grpNetworkTweaks.Controls.Add(disableNicPowerToggle);
            grpNetworkTweaks.Controls.Add(sepNet1);
            grpNetworkTweaks.Controls.Add(lblQosTitle);
            grpNetworkTweaks.Controls.Add(lblQosDesc);
            grpNetworkTweaks.Controls.Add(qosDscpToggle);
            grpNetworkTweaks.Font = new Font("Segoe UI", 9.5F);
            grpNetworkTweaks.Location = new Point(16, 14);
            grpNetworkTweaks.Name = "grpNetworkTweaks";
            grpNetworkTweaks.Padding = new Padding(14, 44, 14, 14);
            grpNetworkTweaks.Size = new Size(673, 188);
            grpNetworkTweaks.SubTitle = "Requires Administrator privileges";
            grpNetworkTweaks.TabIndex = 0;
            grpNetworkTweaks.Title = "Network Adapter & QoS Tweaks";
            // 
            // lblNicPowerTitle
            // 
            lblNicPowerTitle.AutoSize = true;
            lblNicPowerTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNicPowerTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblNicPowerTitle.Location = new Point(16, 50);
            lblNicPowerTitle.Name = "lblNicPowerTitle";
            lblNicPowerTitle.Size = new Size(370, 17);
            lblNicPowerTitle.TabIndex = 0;
            lblNicPowerTitle.Text = "Disable NIC Power Saving / Energy-Efficient Ethernet (EEE)";
            // 
            // lblNicPowerDesc
            // 
            lblNicPowerDesc.AutoSize = true;
            lblNicPowerDesc.Font = new Font("Segoe UI", 8.25F);
            lblNicPowerDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblNicPowerDesc.Location = new Point(16, 70);
            lblNicPowerDesc.Name = "lblNicPowerDesc";
            lblNicPowerDesc.Size = new Size(506, 13);
            lblNicPowerDesc.TabIndex = 1;
            lblNicPowerDesc.Text = "Disables EEE, Green Ethernet and power sleep on active Ethernet NICs to stop random lag spikes.";
            // 
            // disableNicPowerToggle
            // 
            disableNicPowerToggle.BackColor = Color.Transparent;
            disableNicPowerToggle.Checked = false;
            disableNicPowerToggle.Location = new Point(612, 57);
            disableNicPowerToggle.Name = "disableNicPowerToggle";
            disableNicPowerToggle.Size = new Size(44, 22);
            disableNicPowerToggle.TabIndex = 2;
            // 
            // sepNet1
            // 
            sepNet1.BackColor = Color.Transparent;
            sepNet1.Location = new Point(16, 102);
            sepNet1.Name = "sepNet1";
            sepNet1.Size = new Size(641, 8);
            sepNet1.TabIndex = 3;
            // 
            // lblQosTitle
            // 
            lblQosTitle.AutoSize = true;
            lblQosTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblQosTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblQosTitle.Location = new Point(16, 118);
            lblQosTitle.Name = "lblQosTitle";
            lblQosTitle.Size = new Size(266, 17);
            lblQosTitle.TabIndex = 4;
            lblQosTitle.Text = "QoS Expedited Tagging for RustClient.exe";
            // 
            // lblQosDesc
            // 
            lblQosDesc.AutoSize = true;
            lblQosDesc.Font = new Font("Segoe UI", 8.25F);
            lblQosDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblQosDesc.Location = new Point(16, 138);
            lblQosDesc.Name = "lblQosDesc";
            lblQosDesc.Size = new Size(486, 13);
            lblQosDesc.TabIndex = 5;
            lblQosDesc.Text = "Tags RustClient.exe UDP packets with DSCP 46 (Expedited Forwarding) for QoS-aware routers.";
            // 
            // qosDscpToggle
            // 
            qosDscpToggle.BackColor = Color.Transparent;
            qosDscpToggle.Checked = false;
            qosDscpToggle.Location = new Point(612, 125);
            qosDscpToggle.Name = "qosDscpToggle";
            qosDscpToggle.Size = new Size(44, 22);
            qosDscpToggle.TabIndex = 6;
            // 
            // btnApplyNetworkTweaks
            // 
            btnApplyNetworkTweaks.AccentColor = Color.FromArgb(224, 83, 40);
            btnApplyNetworkTweaks.BackColor = Color.Transparent;
            btnApplyNetworkTweaks.CornerRadius = 8;
            btnApplyNetworkTweaks.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnApplyNetworkTweaks.ForeColor = Color.FromArgb(242, 238, 234);
            btnApplyNetworkTweaks.IsPrimaryAction = true;
            btnApplyNetworkTweaks.Location = new Point(16, 214);
            btnApplyNetworkTweaks.Name = "btnApplyNetworkTweaks";
            btnApplyNetworkTweaks.Size = new Size(444, 38);
            btnApplyNetworkTweaks.TabIndex = 1;
            btnApplyNetworkTweaks.Text = "Apply Network Tweaks";
            btnApplyNetworkTweaks.Click += btnApplyNetworkTweaks_Click;
            // 
            // btnFlushDns
            // 
            btnFlushDns.AccentColor = Color.FromArgb(224, 83, 40);
            btnFlushDns.BackColor = Color.Transparent;
            btnFlushDns.CornerRadius = 8;
            btnFlushDns.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFlushDns.ForeColor = Color.FromArgb(242, 238, 234);
            btnFlushDns.IsPrimaryAction = false;
            btnFlushDns.Location = new Point(472, 214);
            btnFlushDns.Name = "btnFlushDns";
            btnFlushDns.Size = new Size(217, 38);
            btnFlushDns.TabIndex = 2;
            btnFlushDns.Text = "Flush DNS Cache";
            btnFlushDns.Click += btnFlushDns_Click;
            // 
            // grpPingMonitor
            // 
            grpPingMonitor.BackColor = Color.Transparent;
            grpPingMonitor.Controls.Add(txtServerIp);
            grpPingMonitor.Controls.Add(pingCountNumeric);
            grpPingMonitor.Controls.Add(btnRunPingTest);
            grpPingMonitor.Controls.Add(lblPingAvg);
            grpPingMonitor.Controls.Add(lblPingJitter);
            grpPingMonitor.Controls.Add(lblPacketLoss);
            grpPingMonitor.Font = new Font("Segoe UI", 9.5F);
            grpPingMonitor.Location = new Point(16, 264);
            grpPingMonitor.Name = "grpPingMonitor";
            grpPingMonitor.Padding = new Padding(14, 44, 14, 14);
            grpPingMonitor.Size = new Size(673, 244);
            grpPingMonitor.SubTitle = "Enter server IP and port, or hostname and port and ping attempts";
            grpPingMonitor.TabIndex = 3;
            grpPingMonitor.Title = "Server Jitter & Packet Loss Monitor";
            // 
            // txtServerIp
            // 
            txtServerIp.BackColor = Color.Transparent;
            txtServerIp.Location = new Point(14, 50);
            txtServerIp.MaxLength = 256;
            txtServerIp.Multiline = false;
            txtServerIp.Name = "txtServerIp";
            txtServerIp.ReadOnly = false;
            txtServerIp.Size = new Size(406, 32);
            txtServerIp.TabIndex = 0;
            txtServerIp.Text = "1.1.1.1";
            txtServerIp.TextAlign = HorizontalAlignment.Left;
            txtServerIp.UseSystemPasswordChar = false;
            // 
            // pingCountNumeric
            // 
            pingCountNumeric.BackColor = Color.Transparent;
            pingCountNumeric.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pingCountNumeric.Location = new Point(430, 50);
            pingCountNumeric.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            pingCountNumeric.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            pingCountNumeric.Name = "pingCountNumeric";
            pingCountNumeric.Size = new Size(96, 34);
            pingCountNumeric.TabIndex = 1;
            pingCountNumeric.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // btnRunPingTest
            // 
            btnRunPingTest.AccentColor = Color.FromArgb(224, 83, 40);
            btnRunPingTest.BackColor = Color.Transparent;
            btnRunPingTest.CornerRadius = 8;
            btnRunPingTest.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRunPingTest.ForeColor = Color.FromArgb(242, 238, 234);
            btnRunPingTest.IsPrimaryAction = true;
            btnRunPingTest.Location = new Point(536, 50);
            btnRunPingTest.Name = "btnRunPingTest";
            btnRunPingTest.Size = new Size(123, 34);
            btnRunPingTest.TabIndex = 2;
            btnRunPingTest.Text = "Run Test";
            btnRunPingTest.Click += btnRunPingTest_Click;
            // 
            // lblPingAvg
            // 
            lblPingAvg.BackColor = Color.Transparent;
            lblPingAvg.DotColor = Color.FromArgb(224, 83, 40);
            lblPingAvg.Location = new Point(14, 100);
            lblPingAvg.Name = "lblPingAvg";
            lblPingAvg.Prefix = "AVG PING:";
            lblPingAvg.Size = new Size(645, 32);
            lblPingAvg.TabIndex = 3;
            lblPingAvg.Text = "Idle | enter a server IP or website url (e.g. 1.1.1.1, google.com, or Game server IP:port) and click Run Test";
            // 
            // lblPingJitter
            // 
            lblPingJitter.BackColor = Color.Transparent;
            lblPingJitter.DotColor = Color.FromArgb(235, 145, 45);
            lblPingJitter.Location = new Point(14, 142);
            lblPingJitter.Name = "lblPingJitter";
            lblPingJitter.Prefix = "JITTER:";
            lblPingJitter.Size = new Size(645, 32);
            lblPingJitter.TabIndex = 4;
            lblPingJitter.Text = "--";
            // 
            // lblPacketLoss
            // 
            lblPacketLoss.BackColor = Color.Transparent;
            lblPacketLoss.DotColor = Color.FromArgb(38, 175, 96);
            lblPacketLoss.Location = new Point(14, 184);
            lblPacketLoss.Name = "lblPacketLoss";
            lblPacketLoss.Prefix = "PACKET LOSS:";
            lblPacketLoss.Size = new Size(645, 32);
            lblPacketLoss.TabIndex = 5;
            lblPacketLoss.Text = "--";
            // 
            // pnlAutoFlush
            // 
            pnlAutoFlush.BackColor = Color.FromArgb(14, 13, 12);
            pnlAutoFlush.Controls.Add(grpAutoFlush);
            pnlAutoFlush.Controls.Add(btnSaveFlush);
            pnlAutoFlush.Controls.Add(btnFlushNow);
            pnlAutoFlush.Dock = DockStyle.Fill;
            pnlAutoFlush.Location = new Point(0, 0);
            pnlAutoFlush.Name = "pnlAutoFlush";
            pnlAutoFlush.Size = new Size(705, 525);
            pnlAutoFlush.TabIndex = 3;
            pnlAutoFlush.Visible = false;
            // 
            // grpAutoFlush
            // 
            grpAutoFlush.BackColor = Color.Transparent;
            grpAutoFlush.Controls.Add(lblAutoFlushTitle);
            grpAutoFlush.Controls.Add(lblAutoFlushDesc);
            grpAutoFlush.Controls.Add(autoFlushChk);
            grpAutoFlush.Controls.Add(sepFlush1);
            grpAutoFlush.Controls.Add(lblSmartFlushTitle);
            grpAutoFlush.Controls.Add(lblSmartFlushDesc);
            grpAutoFlush.Controls.Add(smartFlushToggle);
            grpAutoFlush.Controls.Add(sepFlush2);
            grpAutoFlush.Controls.Add(lblSmartThresholdTitle);
            grpAutoFlush.Controls.Add(lblSmartThresholdDesc);
            grpAutoFlush.Controls.Add(smartFlushThreshold);
            grpAutoFlush.Controls.Add(sepFlush3);
            grpAutoFlush.Controls.Add(lblIntervalTitle);
            grpAutoFlush.Controls.Add(lblIntervalDesc);
            grpAutoFlush.Controls.Add(autoFlushinterval);
            grpAutoFlush.Controls.Add(autoFlushMinHour);
            grpAutoFlush.Controls.Add(sepFlush4);
            grpAutoFlush.Controls.Add(lblFlushSfxTitle);
            grpAutoFlush.Controls.Add(lblFlushSfxDesc);
            grpAutoFlush.Controls.Add(autoFlushSound);
            grpAutoFlush.Font = new Font("Segoe UI", 9.5F);
            grpAutoFlush.Location = new Point(16, 14);
            grpAutoFlush.Name = "grpAutoFlush";
            grpAutoFlush.Padding = new Padding(14, 44, 14, 14);
            grpAutoFlush.Size = new Size(673, 388);
            grpAutoFlush.SubTitle = "Requires Administrator privileges";
            grpAutoFlush.TabIndex = 0;
            grpAutoFlush.Title = "Standby Memory Auto-Flush";
            // 
            // lblAutoFlushTitle
            // 
            lblAutoFlushTitle.AutoSize = true;
            lblAutoFlushTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblAutoFlushTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblAutoFlushTitle.Location = new Point(16, 48);
            lblAutoFlushTitle.Name = "lblAutoFlushTitle";
            lblAutoFlushTitle.Size = new Size(121, 17);
            lblAutoFlushTitle.TabIndex = 0;
            lblAutoFlushTitle.Text = "Enable Auto-Flush";
            // 
            // lblAutoFlushDesc
            // 
            lblAutoFlushDesc.AutoSize = true;
            lblAutoFlushDesc.Font = new Font("Segoe UI", 8.25F);
            lblAutoFlushDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblAutoFlushDesc.Location = new Point(16, 68);
            lblAutoFlushDesc.Name = "lblAutoFlushDesc";
            lblAutoFlushDesc.Size = new Size(444, 13);
            lblAutoFlushDesc.TabIndex = 1;
            lblAutoFlushDesc.Text = "Clears Windows standby RAM automatically to prevent long game session stuttering.";
            // 
            // autoFlushChk
            // 
            autoFlushChk.BackColor = Color.Transparent;
            autoFlushChk.Checked = false;
            autoFlushChk.Location = new Point(612, 55);
            autoFlushChk.Name = "autoFlushChk";
            autoFlushChk.Size = new Size(44, 22);
            autoFlushChk.TabIndex = 2;
            autoFlushChk.CheckedChanged += autoFlushChk_CheckedChanged;
            // 
            // sepFlush1
            // 
            sepFlush1.BackColor = Color.Transparent;
            sepFlush1.Location = new Point(16, 94);
            sepFlush1.Name = "sepFlush1";
            sepFlush1.Size = new Size(641, 8);
            sepFlush1.TabIndex = 3;
            // 
            // lblSmartFlushTitle
            // 
            lblSmartFlushTitle.AutoSize = true;
            lblSmartFlushTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSmartFlushTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblSmartFlushTitle.Location = new Point(16, 108);
            lblSmartFlushTitle.Name = "lblSmartFlushTitle";
            lblSmartFlushTitle.Size = new Size(254, 17);
            lblSmartFlushTitle.TabIndex = 4;
            lblSmartFlushTitle.Text = "Smart Threshold Mode (Recommended)";
            // 
            // lblSmartFlushDesc
            // 
            lblSmartFlushDesc.AutoSize = true;
            lblSmartFlushDesc.Font = new Font("Segoe UI", 8.25F);
            lblSmartFlushDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblSmartFlushDesc.Location = new Point(16, 128);
            lblSmartFlushDesc.Name = "lblSmartFlushDesc";
            lblSmartFlushDesc.Size = new Size(440, 13);
            lblSmartFlushDesc.TabIndex = 5;
            lblSmartFlushDesc.Text = "Only flushes when free RAM drops below your threshold and standby cache is high.";
            // 
            // smartFlushToggle
            // 
            smartFlushToggle.BackColor = Color.Transparent;
            smartFlushToggle.Checked = false;
            smartFlushToggle.Location = new Point(612, 115);
            smartFlushToggle.Name = "smartFlushToggle";
            smartFlushToggle.Size = new Size(44, 22);
            smartFlushToggle.TabIndex = 6;
            // 
            // sepFlush2
            // 
            sepFlush2.BackColor = Color.Transparent;
            sepFlush2.Location = new Point(16, 154);
            sepFlush2.Name = "sepFlush2";
            sepFlush2.Size = new Size(641, 8);
            sepFlush2.TabIndex = 7;
            // 
            // lblSmartThresholdTitle
            // 
            lblSmartThresholdTitle.AutoSize = true;
            lblSmartThresholdTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSmartThresholdTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblSmartThresholdTitle.Location = new Point(16, 168);
            lblSmartThresholdTitle.Name = "lblSmartThresholdTitle";
            lblSmartThresholdTitle.Size = new Size(212, 17);
            lblSmartThresholdTitle.TabIndex = 8;
            lblSmartThresholdTitle.Text = "Free RAM Trigger Threshold (GB)";
            // 
            // lblSmartThresholdDesc
            // 
            lblSmartThresholdDesc.AutoSize = true;
            lblSmartThresholdDesc.Font = new Font("Segoe UI", 8.25F);
            lblSmartThresholdDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblSmartThresholdDesc.Location = new Point(16, 188);
            lblSmartThresholdDesc.Name = "lblSmartThresholdDesc";
            lblSmartThresholdDesc.Size = new Size(448, 13);
            lblSmartThresholdDesc.TabIndex = 9;
            lblSmartThresholdDesc.Text = "Triggers Smart Flush when free physical RAM drops below this amount (default: 2 GB).";
            // 
            // smartFlushThreshold
            // 
            smartFlushThreshold.BackColor = Color.Transparent;
            smartFlushThreshold.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            smartFlushThreshold.Location = new Point(548, 172);
            smartFlushThreshold.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            smartFlushThreshold.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            smartFlushThreshold.Name = "smartFlushThreshold";
            smartFlushThreshold.Size = new Size(108, 32);
            smartFlushThreshold.TabIndex = 10;
            smartFlushThreshold.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // sepFlush3
            // 
            sepFlush3.BackColor = Color.Transparent;
            sepFlush3.Location = new Point(16, 218);
            sepFlush3.Name = "sepFlush3";
            sepFlush3.Size = new Size(641, 8);
            sepFlush3.TabIndex = 11;
            // 
            // lblIntervalTitle
            // 
            lblIntervalTitle.AutoSize = true;
            lblIntervalTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblIntervalTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblIntervalTitle.Location = new Point(16, 232);
            lblIntervalTitle.Name = "lblIntervalTitle";
            lblIntervalTitle.Size = new Size(304, 17);
            lblIntervalTitle.TabIndex = 12;
            lblIntervalTitle.Text = "Fixed Timer Interval (When Smart Mode is OFF)";
            // 
            // lblIntervalDesc
            // 
            lblIntervalDesc.AutoSize = true;
            lblIntervalDesc.Font = new Font("Segoe UI", 8.25F);
            lblIntervalDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblIntervalDesc.Location = new Point(16, 252);
            lblIntervalDesc.Name = "lblIntervalDesc";
            lblIntervalDesc.Size = new Size(379, 13);
            lblIntervalDesc.TabIndex = 13;
            lblIntervalDesc.Text = "Fixed RAM flush interval, if you prefer timed over Smart Threshold Mode.";
            // 
            // autoFlushinterval
            // 
            autoFlushinterval.BackColor = Color.Transparent;
            autoFlushinterval.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            autoFlushinterval.Location = new Point(443, 236);
            autoFlushinterval.Maximum = new decimal(new int[] { 240, 0, 0, 0 });
            autoFlushinterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            autoFlushinterval.Name = "autoFlushinterval";
            autoFlushinterval.Size = new Size(99, 32);
            autoFlushinterval.TabIndex = 14;
            autoFlushinterval.Value = new decimal(new int[] { 15, 0, 0, 0 });
            autoFlushinterval.ValueChanged += autoFlushinterval_ValueChanged;
            // 
            // autoFlushMinHour
            // 
            autoFlushMinHour.BackColor = Color.FromArgb(28, 25, 23);
            autoFlushMinHour.DrawMode = DrawMode.OwnerDrawFixed;
            autoFlushMinHour.DropDownStyle = ComboBoxStyle.DropDownList;
            autoFlushMinHour.Font = new Font("Segoe UI", 9.25F);
            autoFlushMinHour.ForeColor = Color.FromArgb(242, 238, 234);
            autoFlushMinHour.ItemHeight = 26;
            autoFlushMinHour.Items.AddRange(new object[] { "Minutes", "Hours" });
            autoFlushMinHour.Location = new Point(548, 236);
            autoFlushMinHour.Name = "autoFlushMinHour";
            autoFlushMinHour.Size = new Size(108, 32);
            autoFlushMinHour.TabIndex = 15;
            // 
            // sepFlush4
            // 
            sepFlush4.BackColor = Color.Transparent;
            sepFlush4.Location = new Point(16, 282);
            sepFlush4.Name = "sepFlush4";
            sepFlush4.Size = new Size(641, 8);
            sepFlush4.TabIndex = 16;
            // 
            // lblFlushSfxTitle
            // 
            lblFlushSfxTitle.AutoSize = true;
            lblFlushSfxTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFlushSfxTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblFlushSfxTitle.Location = new Point(16, 296);
            lblFlushSfxTitle.Name = "lblFlushSfxTitle";
            lblFlushSfxTitle.Size = new Size(193, 17);
            lblFlushSfxTitle.TabIndex = 17;
            lblFlushSfxTitle.Text = "RAM Flush Sound Effect (SFX)";
            // 
            // lblFlushSfxDesc
            // 
            lblFlushSfxDesc.AutoSize = true;
            lblFlushSfxDesc.Font = new Font("Segoe UI", 8.25F);
            lblFlushSfxDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblFlushSfxDesc.Location = new Point(16, 316);
            lblFlushSfxDesc.Name = "lblFlushSfxDesc";
            lblFlushSfxDesc.Size = new Size(315, 26);
            lblFlushSfxDesc.TabIndex = 18;
            lblFlushSfxDesc.Text = "Play a confirmation tone each time an auto-flush completes.\r\n[You're welcome, Sal! :P]";
            // 
            // autoFlushSound
            // 
            autoFlushSound.BackColor = Color.Transparent;
            autoFlushSound.Checked = false;
            autoFlushSound.Location = new Point(612, 308);
            autoFlushSound.Name = "autoFlushSound";
            autoFlushSound.Size = new Size(44, 22);
            autoFlushSound.TabIndex = 19;
            autoFlushSound.CheckedChanged += autoFlushSound_CheckedChanged;
            // 
            // btnSaveFlush
            // 
            btnSaveFlush.AccentColor = Color.FromArgb(224, 83, 40);
            btnSaveFlush.BackColor = Color.Transparent;
            btnSaveFlush.CornerRadius = 8;
            btnSaveFlush.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSaveFlush.ForeColor = Color.FromArgb(242, 238, 234);
            btnSaveFlush.IsPrimaryAction = true;
            btnSaveFlush.Location = new Point(16, 414);
            btnSaveFlush.Name = "btnSaveFlush";
            btnSaveFlush.Size = new Size(444, 40);
            btnSaveFlush.TabIndex = 1;
            btnSaveFlush.Text = "Save Auto-Flush Settings";
            btnSaveFlush.Click += saveAdvancedCfgBtn_Click;
            // 
            // btnFlushNow
            // 
            btnFlushNow.AccentColor = Color.FromArgb(224, 83, 40);
            btnFlushNow.BackColor = Color.Transparent;
            btnFlushNow.CornerRadius = 8;
            btnFlushNow.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFlushNow.ForeColor = Color.FromArgb(242, 238, 234);
            btnFlushNow.IsPrimaryAction = false;
            btnFlushNow.Location = new Point(472, 414);
            btnFlushNow.Name = "btnFlushNow";
            btnFlushNow.Size = new Size(217, 40);
            btnFlushNow.TabIndex = 2;
            btnFlushNow.Text = "Flush RAM Now";
            btnFlushNow.Click += flushRamToolStripMenuItem_Click;
            // 
            // pnlTools
            // 
            pnlTools.BackColor = Color.FromArgb(14, 13, 12);
            pnlTools.Controls.Add(grpSystemTweaks);
            pnlTools.Controls.Add(grpCacheCleaners);
            pnlTools.Controls.Add(saveAdvancedCfgBtn);
            pnlTools.Dock = DockStyle.Fill;
            pnlTools.Location = new Point(0, 0);
            pnlTools.Name = "pnlTools";
            pnlTools.Size = new Size(705, 525);
            pnlTools.TabIndex = 2;
            pnlTools.Visible = false;
            // 
            // grpSystemTweaks
            // 
            grpSystemTweaks.BackColor = Color.Transparent;
            grpSystemTweaks.Controls.Add(lblPriorityTitle);
            grpSystemTweaks.Controls.Add(lblPriorityDesc);
            grpSystemTweaks.Controls.Add(highPriority);
            grpSystemTweaks.Controls.Add(sepTool1);
            grpSystemTweaks.Controls.Add(lblPCoresTitle);
            grpSystemTweaks.Controls.Add(lblPCoresDesc);
            grpSystemTweaks.Controls.Add(pCoresToggle);
            grpSystemTweaks.Controls.Add(sepTool2);
            grpSystemTweaks.Controls.Add(lblFsoTitle);
            grpSystemTweaks.Controls.Add(lblFsoDesc);
            grpSystemTweaks.Controls.Add(disabledFSO);
            grpSystemTweaks.Controls.Add(sepTool3);
            grpSystemTweaks.Controls.Add(lblPowerPlanTitle);
            grpSystemTweaks.Controls.Add(lblPowerPlanDesc);
            grpSystemTweaks.Controls.Add(autoPowerPlanToggle);
            grpSystemTweaks.Controls.Add(sepTool4);
            grpSystemTweaks.Controls.Add(lblGpuPrefTitle);
            grpSystemTweaks.Controls.Add(lblGpuPrefDesc);
            grpSystemTweaks.Controls.Add(highPerfGpuToggle);
            grpSystemTweaks.Controls.Add(sepTool5);
            grpSystemTweaks.Controls.Add(lblGameModeTitle);
            grpSystemTweaks.Controls.Add(lblGameModeDesc);
            grpSystemTweaks.Controls.Add(gameModeDvrToggle);
            grpSystemTweaks.Controls.Add(sepTool6);
            grpSystemTweaks.Controls.Add(lblThrottleTitle);
            grpSystemTweaks.Controls.Add(lblThrottleDesc);
            grpSystemTweaks.Controls.Add(throttleAppsToggle);
            grpSystemTweaks.Font = new Font("Segoe UI", 9.5F);
            grpSystemTweaks.Location = new Point(16, 12);
            grpSystemTweaks.Name = "grpSystemTweaks";
            grpSystemTweaks.Padding = new Padding(14, 44, 14, 14);
            grpSystemTweaks.Size = new Size(673, 350);
            grpSystemTweaks.SubTitle = "Scheduling, power plan, & background throttling";
            grpSystemTweaks.TabIndex = 0;
            grpSystemTweaks.Title = "System & Engine Tweaks";
            // 
            // lblPriorityTitle
            // 
            lblPriorityTitle.AutoSize = true;
            lblPriorityTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPriorityTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblPriorityTitle.Location = new Point(16, 44);
            lblPriorityTitle.Name = "lblPriorityTitle";
            lblPriorityTitle.Size = new Size(167, 15);
            lblPriorityTitle.TabIndex = 0;
            lblPriorityTitle.Text = "Rust CPU High Priority (IFEO)";
            // 
            // lblPriorityDesc
            // 
            lblPriorityDesc.AutoSize = true;
            lblPriorityDesc.Font = new Font("Segoe UI", 8F);
            lblPriorityDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblPriorityDesc.Location = new Point(16, 61);
            lblPriorityDesc.Name = "lblPriorityDesc";
            lblPriorityDesc.Size = new Size(440, 13);
            lblPriorityDesc.TabIndex = 1;
            lblPriorityDesc.Text = "Sets Windows IFEO High Priority on RustClient.exe before EAC locks handle (Admin).";
            // 
            // highPriority
            // 
            highPriority.BackColor = Color.Transparent;
            highPriority.Checked = false;
            highPriority.Location = new Point(612, 50);
            highPriority.Name = "highPriority";
            highPriority.Size = new Size(44, 22);
            highPriority.TabIndex = 2;
            // 
            // sepTool1
            // 
            sepTool1.BackColor = Color.Transparent;
            sepTool1.Location = new Point(16, 80);
            sepTool1.Name = "sepTool1";
            sepTool1.Size = new Size(641, 6);
            sepTool1.TabIndex = 3;
            // 
            // lblPCoresTitle
            // 
            lblPCoresTitle.AutoSize = true;
            lblPCoresTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPCoresTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblPCoresTitle.Location = new Point(16, 88);
            lblPCoresTitle.Name = "lblPCoresTitle";
            lblPCoresTitle.Size = new Size(112, 15);
            lblPCoresTitle.TabIndex = 4;
            lblPCoresTitle.Text = "Physical Cores Only";
            // 
            // lblPCoresDesc
            // 
            lblPCoresDesc.AutoSize = true;
            lblPCoresDesc.Font = new Font("Segoe UI", 8F);
            lblPCoresDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblPCoresDesc.Location = new Point(16, 105);
            lblPCoresDesc.Name = "lblPCoresDesc";
            lblPCoresDesc.Size = new Size(358, 13);
            lblPCoresDesc.TabIndex = 5;
            lblPCoresDesc.Text = "Pins CPU affinity to physical cores, avoiding SMT/hyperthread issues.";
            // 
            // pCoresToggle
            // 
            pCoresToggle.BackColor = Color.Transparent;
            pCoresToggle.Checked = false;
            pCoresToggle.Location = new Point(612, 94);
            pCoresToggle.Name = "pCoresToggle";
            pCoresToggle.Size = new Size(44, 22);
            pCoresToggle.TabIndex = 6;
            // 
            // sepTool2
            // 
            sepTool2.BackColor = Color.Transparent;
            sepTool2.Location = new Point(16, 124);
            sepTool2.Name = "sepTool2";
            sepTool2.Size = new Size(641, 6);
            sepTool2.TabIndex = 7;
            // 
            // lblFsoTitle
            // 
            lblFsoTitle.AutoSize = true;
            lblFsoTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFsoTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblFsoTitle.Location = new Point(16, 132);
            lblFsoTitle.Name = "lblFsoTitle";
            lblFsoTitle.Size = new Size(219, 15);
            lblFsoTitle.TabIndex = 8;
            lblFsoTitle.Text = "Disable Fullscreen Optimizations (FSO)";
            // 
            // lblFsoDesc
            // 
            lblFsoDesc.AutoSize = true;
            lblFsoDesc.Font = new Font("Segoe UI", 8F);
            lblFsoDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblFsoDesc.Location = new Point(16, 149);
            lblFsoDesc.Name = "lblFsoDesc";
            lblFsoDesc.Size = new Size(383, 13);
            lblFsoDesc.TabIndex = 9;
            lblFsoDesc.Text = "Applies Windows FSO override to prevent DWM fullscreen latency issues.";
            // 
            // disabledFSO
            // 
            disabledFSO.BackColor = Color.Transparent;
            disabledFSO.Checked = false;
            disabledFSO.Location = new Point(612, 138);
            disabledFSO.Name = "disabledFSO";
            disabledFSO.Size = new Size(44, 22);
            disabledFSO.TabIndex = 10;
            disabledFSO.CheckedChanged += disabledFSO_CheckedChanged;
            // 
            // sepTool3
            // 
            sepTool3.BackColor = Color.Transparent;
            sepTool3.Location = new Point(16, 168);
            sepTool3.Name = "sepTool3";
            sepTool3.Size = new Size(641, 6);
            sepTool3.TabIndex = 11;
            // 
            // lblPowerPlanTitle
            // 
            lblPowerPlanTitle.AutoSize = true;
            lblPowerPlanTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPowerPlanTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblPowerPlanTitle.Location = new Point(16, 176);
            lblPowerPlanTitle.Name = "lblPowerPlanTitle";
            lblPowerPlanTitle.Size = new Size(188, 15);
            lblPowerPlanTitle.TabIndex = 12;
            lblPowerPlanTitle.Text = "Auto High / Ultimate Power Plan";
            // 
            // lblPowerPlanDesc
            // 
            lblPowerPlanDesc.AutoSize = true;
            lblPowerPlanDesc.Font = new Font("Segoe UI", 8F);
            lblPowerPlanDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblPowerPlanDesc.Location = new Point(16, 193);
            lblPowerPlanDesc.Name = "lblPowerPlanDesc";
            lblPowerPlanDesc.Size = new Size(498, 13);
            lblPowerPlanDesc.TabIndex = 13;
            lblPowerPlanDesc.Text = "Switches to Ultimate/High Performance while Rust runs and restores your previous plan on exit.";
            // 
            // autoPowerPlanToggle
            // 
            autoPowerPlanToggle.BackColor = Color.Transparent;
            autoPowerPlanToggle.Checked = false;
            autoPowerPlanToggle.Location = new Point(612, 182);
            autoPowerPlanToggle.Name = "autoPowerPlanToggle";
            autoPowerPlanToggle.Size = new Size(44, 22);
            autoPowerPlanToggle.TabIndex = 14;
            // 
            // sepTool4
            // 
            sepTool4.BackColor = Color.Transparent;
            sepTool4.Location = new Point(16, 212);
            sepTool4.Name = "sepTool4";
            sepTool4.Size = new Size(641, 6);
            sepTool4.TabIndex = 15;
            // 
            // lblGpuPrefTitle
            // 
            lblGpuPrefTitle.AutoSize = true;
            lblGpuPrefTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGpuPrefTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblGpuPrefTitle.Location = new Point(16, 220);
            lblGpuPrefTitle.Name = "lblGpuPrefTitle";
            lblGpuPrefTitle.Size = new Size(184, 15);
            lblGpuPrefTitle.TabIndex = 16;
            lblGpuPrefTitle.Text = "High-Performance GPU Binding";
            // 
            // lblGpuPrefDesc
            // 
            lblGpuPrefDesc.AutoSize = true;
            lblGpuPrefDesc.Font = new Font("Segoe UI", 8F);
            lblGpuPrefDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblGpuPrefDesc.Location = new Point(16, 237);
            lblGpuPrefDesc.Name = "lblGpuPrefDesc";
            lblGpuPrefDesc.Size = new Size(367, 13);
            lblGpuPrefDesc.TabIndex = 17;
            lblGpuPrefDesc.Text = "Forces Windows DXGI to always schedule RustClient.exe on your GPU.";
            // 
            // highPerfGpuToggle
            // 
            highPerfGpuToggle.BackColor = Color.Transparent;
            highPerfGpuToggle.Checked = false;
            highPerfGpuToggle.Location = new Point(612, 226);
            highPerfGpuToggle.Name = "highPerfGpuToggle";
            highPerfGpuToggle.Size = new Size(44, 22);
            highPerfGpuToggle.TabIndex = 18;
            // 
            // sepTool5
            // 
            sepTool5.BackColor = Color.Transparent;
            sepTool5.Location = new Point(16, 256);
            sepTool5.Name = "sepTool5";
            sepTool5.Size = new Size(641, 6);
            sepTool5.TabIndex = 19;
            // 
            // lblGameModeTitle
            // 
            lblGameModeTitle.AutoSize = true;
            lblGameModeTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGameModeTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblGameModeTitle.Location = new Point(16, 264);
            lblGameModeTitle.Name = "lblGameModeTitle";
            lblGameModeTitle.Size = new Size(216, 15);
            lblGameModeTitle.TabIndex = 20;
            lblGameModeTitle.Text = "Game Mode ON & Disable Game DVR";
            lblGameModeTitle.UseMnemonic = false;
            // 
            // lblGameModeDesc
            // 
            lblGameModeDesc.AutoSize = true;
            lblGameModeDesc.Font = new Font("Segoe UI", 8F);
            lblGameModeDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblGameModeDesc.Location = new Point(16, 281);
            lblGameModeDesc.Name = "lblGameModeDesc";
            lblGameModeDesc.Size = new Size(528, 13);
            lblGameModeDesc.TabIndex = 21;
            lblGameModeDesc.Text = "Enables Windows Game Mode (essential for Ryzen X3D) and disables background Game DVR capture.";
            // 
            // gameModeDvrToggle
            // 
            gameModeDvrToggle.BackColor = Color.Transparent;
            gameModeDvrToggle.Checked = false;
            gameModeDvrToggle.Location = new Point(612, 270);
            gameModeDvrToggle.Name = "gameModeDvrToggle";
            gameModeDvrToggle.Size = new Size(44, 22);
            gameModeDvrToggle.TabIndex = 22;
            // 
            // sepTool6
            // 
            sepTool6.BackColor = Color.Transparent;
            sepTool6.Location = new Point(16, 300);
            sepTool6.Name = "sepTool6";
            sepTool6.Size = new Size(641, 6);
            sepTool6.TabIndex = 23;
            // 
            // lblThrottleTitle
            // 
            lblThrottleTitle.AutoSize = true;
            lblThrottleTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblThrottleTitle.ForeColor = Color.FromArgb(242, 238, 234);
            lblThrottleTitle.Location = new Point(16, 308);
            lblThrottleTitle.Name = "lblThrottleTitle";
            lblThrottleTitle.Size = new Size(215, 15);
            lblThrottleTitle.TabIndex = 24;
            lblThrottleTitle.Text = "Throttle Background Apps (EAC-Safe)";
            // 
            // lblThrottleDesc
            // 
            lblThrottleDesc.AutoSize = true;
            lblThrottleDesc.Font = new Font("Segoe UI", 8F);
            lblThrottleDesc.ForeColor = Color.FromArgb(165, 156, 148);
            lblThrottleDesc.Location = new Point(16, 325);
            lblThrottleDesc.Name = "lblThrottleDesc";
            lblThrottleDesc.Size = new Size(492, 13);
            lblThrottleDesc.TabIndex = 25;
            lblThrottleDesc.Text = "Lowers browsers and launchers to Below Normal priority while Rust runs, and restores on exit.";
            // 
            // throttleAppsToggle
            // 
            throttleAppsToggle.BackColor = Color.Transparent;
            throttleAppsToggle.Checked = false;
            throttleAppsToggle.Location = new Point(612, 314);
            throttleAppsToggle.Name = "throttleAppsToggle";
            throttleAppsToggle.Size = new Size(44, 22);
            throttleAppsToggle.TabIndex = 26;
            // 
            // grpCacheCleaners
            // 
            grpCacheCleaners.BackColor = Color.Transparent;
            grpCacheCleaners.Controls.Add(clearSkinCacheBtn);
            grpCacheCleaners.Controls.Add(clearShaderCacheBtn);
            grpCacheCleaners.Font = new Font("Segoe UI", 9.5F);
            grpCacheCleaners.Location = new Point(16, 370);
            grpCacheCleaners.Name = "grpCacheCleaners";
            grpCacheCleaners.Padding = new Padding(14, 44, 14, 14);
            grpCacheCleaners.Size = new Size(673, 96);
            grpCacheCleaners.SubTitle = "Note: Shaders rebuild during first 5 or so minutes in-game";
            grpCacheCleaners.TabIndex = 1;
            grpCacheCleaners.Title = "Cache Cleaners";
            // 
            // clearSkinCacheBtn
            // 
            clearSkinCacheBtn.AccentColor = Color.FromArgb(224, 83, 40);
            clearSkinCacheBtn.BackColor = Color.Transparent;
            clearSkinCacheBtn.CornerRadius = 8;
            clearSkinCacheBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            clearSkinCacheBtn.ForeColor = Color.FromArgb(242, 238, 234);
            clearSkinCacheBtn.IsPrimaryAction = false;
            clearSkinCacheBtn.Location = new Point(14, 48);
            clearSkinCacheBtn.Name = "clearSkinCacheBtn";
            clearSkinCacheBtn.Size = new Size(316, 36);
            clearSkinCacheBtn.TabIndex = 0;
            clearSkinCacheBtn.Text = "Clear Workshop Skin Cache";
            clearSkinCacheBtn.Click += clearSkinCacheBtn_Click;
            // 
            // clearShaderCacheBtn
            // 
            clearShaderCacheBtn.AccentColor = Color.FromArgb(224, 83, 40);
            clearShaderCacheBtn.BackColor = Color.Transparent;
            clearShaderCacheBtn.CornerRadius = 8;
            clearShaderCacheBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            clearShaderCacheBtn.ForeColor = Color.FromArgb(242, 238, 234);
            clearShaderCacheBtn.IsPrimaryAction = false;
            clearShaderCacheBtn.Location = new Point(342, 48);
            clearShaderCacheBtn.Name = "clearShaderCacheBtn";
            clearShaderCacheBtn.Size = new Size(317, 36);
            clearShaderCacheBtn.TabIndex = 1;
            clearShaderCacheBtn.Text = "Clear DirectX / GPU Shader Cache";
            clearShaderCacheBtn.Click += clearShaderCacheBtn_Click;
            // 
            // saveAdvancedCfgBtn
            // 
            saveAdvancedCfgBtn.AccentColor = Color.FromArgb(224, 83, 40);
            saveAdvancedCfgBtn.BackColor = Color.Transparent;
            saveAdvancedCfgBtn.CornerRadius = 8;
            saveAdvancedCfgBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            saveAdvancedCfgBtn.ForeColor = Color.FromArgb(242, 238, 234);
            saveAdvancedCfgBtn.IsPrimaryAction = true;
            saveAdvancedCfgBtn.Location = new Point(16, 474);
            saveAdvancedCfgBtn.Name = "saveAdvancedCfgBtn";
            saveAdvancedCfgBtn.Size = new Size(673, 38);
            saveAdvancedCfgBtn.TabIndex = 2;
            saveAdvancedCfgBtn.Text = "Save & Apply Settings";
            saveAdvancedCfgBtn.Click += saveAdvancedCfgBtn_Click;
            // 
            // pnlDashboard
            // 
            pnlDashboard.BackColor = Color.FromArgb(14, 13, 12);
            pnlDashboard.Controls.Add(grpGamePath);
            pnlDashboard.Controls.Add(grpHardware);
            pnlDashboard.Controls.Add(grpQuickActions);
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.Location = new Point(0, 0);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Size = new Size(705, 525);
            pnlDashboard.TabIndex = 0;
            // 
            // grpGamePath
            // 
            grpGamePath.BackColor = Color.Transparent;
            grpGamePath.Controls.Add(gamePathString);
            grpGamePath.Controls.Add(gamePathSelectBtn);
            grpGamePath.Font = new Font("Segoe UI", 9.5F);
            grpGamePath.Location = new Point(16, 14);
            grpGamePath.Name = "grpGamePath";
            grpGamePath.Padding = new Padding(14, 44, 14, 14);
            grpGamePath.Size = new Size(673, 108);
            grpGamePath.SubTitle = "Select your Rust installation directory";
            grpGamePath.TabIndex = 0;
            grpGamePath.Title = "Game Install Location";
            // 
            // gamePathString
            // 
            gamePathString.BackColor = Color.Transparent;
            gamePathString.Location = new Point(14, 52);
            gamePathString.MaxLength = 32767;
            gamePathString.Multiline = false;
            gamePathString.Name = "gamePathString";
            gamePathString.ReadOnly = false;
            gamePathString.Size = new Size(536, 32);
            gamePathString.TabIndex = 0;
            gamePathString.TextAlign = HorizontalAlignment.Left;
            gamePathString.UseSystemPasswordChar = false;
            // 
            // gamePathSelectBtn
            // 
            gamePathSelectBtn.AccentColor = Color.FromArgb(224, 83, 40);
            gamePathSelectBtn.BackColor = Color.Transparent;
            gamePathSelectBtn.CornerRadius = 8;
            gamePathSelectBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gamePathSelectBtn.ForeColor = Color.FromArgb(242, 238, 234);
            gamePathSelectBtn.IsPrimaryAction = false;
            gamePathSelectBtn.Location = new Point(558, 52);
            gamePathSelectBtn.Name = "gamePathSelectBtn";
            gamePathSelectBtn.Size = new Size(101, 34);
            gamePathSelectBtn.TabIndex = 1;
            gamePathSelectBtn.Text = "Browse...";
            gamePathSelectBtn.Click += gamePathSelectBtn_Click;
            // 
            // grpHardware
            // 
            grpHardware.BackColor = Color.Transparent;
            grpHardware.Controls.Add(lblCPUInfo);
            grpHardware.Controls.Add(lblGPUInfo);
            grpHardware.Controls.Add(lblRAMInfo);
            grpHardware.Font = new Font("Segoe UI", 9.5F);
            grpHardware.Location = new Point(16, 134);
            grpHardware.Name = "grpHardware";
            grpHardware.Padding = new Padding(14, 44, 14, 14);
            grpHardware.Size = new Size(673, 176);
            grpHardware.SubTitle = "Detected system specs";
            grpHardware.TabIndex = 1;
            grpHardware.Title = "Hardware Information";
            // 
            // lblCPUInfo
            // 
            lblCPUInfo.BackColor = Color.Transparent;
            lblCPUInfo.DotColor = Color.FromArgb(224, 83, 40);
            lblCPUInfo.Location = new Point(14, 50);
            lblCPUInfo.Name = "lblCPUInfo";
            lblCPUInfo.Prefix = "CPU:";
            lblCPUInfo.Size = new Size(645, 32);
            lblCPUInfo.TabIndex = 0;
            lblCPUInfo.Text = "Detecting...";
            // 
            // lblGPUInfo
            // 
            lblGPUInfo.BackColor = Color.Transparent;
            lblGPUInfo.DotColor = Color.FromArgb(235, 145, 45);
            lblGPUInfo.Location = new Point(14, 90);
            lblGPUInfo.Name = "lblGPUInfo";
            lblGPUInfo.Prefix = "GPU:";
            lblGPUInfo.Size = new Size(645, 32);
            lblGPUInfo.TabIndex = 1;
            lblGPUInfo.Text = "Detecting...";
            // 
            // lblRAMInfo
            // 
            lblRAMInfo.BackColor = Color.Transparent;
            lblRAMInfo.DotColor = Color.FromArgb(38, 175, 96);
            lblRAMInfo.Location = new Point(14, 130);
            lblRAMInfo.Name = "lblRAMInfo";
            lblRAMInfo.Prefix = "RAM:";
            lblRAMInfo.Size = new Size(645, 32);
            lblRAMInfo.TabIndex = 2;
            lblRAMInfo.Text = "Detecting...";
            // 
            // grpQuickActions
            // 
            grpQuickActions.BackColor = Color.Transparent;
            grpQuickActions.Controls.Add(linkDonate);
            grpQuickActions.Controls.Add(cardQuickFlush);
            grpQuickActions.Controls.Add(cardQuickCache);
            grpQuickActions.Controls.Add(btnDashTray);
            grpQuickActions.Font = new Font("Segoe UI", 9.5F);
            grpQuickActions.Location = new Point(16, 322);
            grpQuickActions.Name = "grpQuickActions";
            grpQuickActions.Padding = new Padding(14, 44, 14, 14);
            grpQuickActions.Size = new Size(673, 186);
            grpQuickActions.SubTitle = "One-click maintenance utilities";
            grpQuickActions.TabIndex = 2;
            grpQuickActions.Title = "Quick Actions";
            // 
            // linkDonate
            // 
            linkDonate.BackColor = Color.FromArgb(20, 18, 17);
            linkDonate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            linkDonate.Icon = Properties.Resources.cup;
            linkDonate.Location = new Point(14, 136);
            linkDonate.Name = "linkDonate";
            linkDonate.Size = new Size(316, 34);
            linkDonate.TabIndex = 0;
            linkDonate.Title = "Support Development (Buy Me a Coffee)";
            linkDonate.Url = "https://buymeacoffee.com/rustforgedev";
            // 
            // cardQuickFlush
            // 
            cardQuickFlush.BackColor = Color.Transparent;
            cardQuickFlush.BadgeCharacter = "↻";
            cardQuickFlush.Location = new Point(14, 52);
            cardQuickFlush.Name = "cardQuickFlush";
            cardQuickFlush.Selected = false;
            cardQuickFlush.Size = new Size(316, 70);
            cardQuickFlush.Subtitle = "Purge Windows standby memory list now";
            cardQuickFlush.TabIndex = 1;
            cardQuickFlush.Title = "Flush Standby RAM";
            cardQuickFlush.Click += flushRamToolStripMenuItem_Click;
            // 
            // cardQuickCache
            // 
            cardQuickCache.BackColor = Color.Transparent;
            cardQuickCache.BadgeCharacter = "✕";
            cardQuickCache.Location = new Point(342, 52);
            cardQuickCache.Name = "cardQuickCache";
            cardQuickCache.Selected = false;
            cardQuickCache.Size = new Size(317, 70);
            cardQuickCache.Subtitle = "Delete cached Rust workshop skin files";
            cardQuickCache.TabIndex = 2;
            cardQuickCache.Title = "Clear Skin Cache";
            cardQuickCache.Click += clearSkinCacheBtn_Click;
            // 
            // btnDashTray
            // 
            btnDashTray.AccentColor = Color.FromArgb(224, 83, 40);
            btnDashTray.BackColor = Color.Transparent;
            btnDashTray.CornerRadius = 8;
            btnDashTray.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDashTray.ForeColor = Color.FromArgb(242, 238, 234);
            btnDashTray.IsPrimaryAction = false;
            btnDashTray.Location = new Point(342, 136);
            btnDashTray.Name = "btnDashTray";
            btnDashTray.Size = new Size(317, 34);
            btnDashTray.TabIndex = 3;
            btnDashTray.Text = "Minimize to System Tray";
            btnDashTray.Click += minimizeToTrayToolStripMenuItem_Click;
            // 
            // pnlAbout
            // 
            pnlAbout.BackColor = Color.FromArgb(14, 13, 12);
            pnlAbout.Controls.Add(grpLinks);
            pnlAbout.Controls.Add(grpDiagnostics);
            pnlAbout.Dock = DockStyle.Fill;
            pnlAbout.Location = new Point(0, 0);
            pnlAbout.Name = "pnlAbout";
            pnlAbout.Size = new Size(705, 525);
            pnlAbout.TabIndex = 5;
            pnlAbout.Visible = false;
            // 
            // grpLinks
            // 
            grpLinks.BackColor = Color.Transparent;
            grpLinks.Controls.Add(cardPanel);
            grpLinks.Controls.Add(linkWebsite);
            grpLinks.Controls.Add(aboutLinkTile1);
            grpLinks.Controls.Add(linkDocs);
            grpLinks.Controls.Add(linkDiscord);
            grpLinks.Controls.Add(linkGitHub);
            grpLinks.Font = new Font("Segoe UI", 9.5F);
            grpLinks.Location = new Point(16, 14);
            grpLinks.Name = "grpLinks";
            grpLinks.Padding = new Padding(14, 44, 14, 14);
            grpLinks.Size = new Size(673, 380);
            grpLinks.SubTitle = "About Rust Optimizer";
            grpLinks.TabIndex = 0;
            grpLinks.Title = "About";
            // 
            // cardPanel
            // 
            cardPanel.BackColor = Color.FromArgb(30, 30, 30);
            cardPanel.Controls.Add(roLabel2);
            cardPanel.Controls.Add(lblDescription);
            cardPanel.Location = new Point(20, 41);
            cardPanel.Name = "cardPanel";
            cardPanel.Padding = new Padding(16);
            cardPanel.Size = new Size(636, 112);
            cardPanel.TabIndex = 0;
            // 
            // roLabel2
            // 
            roLabel2.BackColor = Color.Transparent;
            roLabel2.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            roLabel2.Location = new Point(18, 5);
            roLabel2.Name = "roLabel2";
            roLabel2.Size = new Size(140, 24);
            roLabel2.TabIndex = 0;
            roLabel2.Value1 = "Rust";
            roLabel2.Value2 = "Optimizer";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.ForeColor = Color.FromArgb(160, 150, 140);
            lblDescription.Location = new Point(18, 31);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(601, 76);
            lblDescription.TabIndex = 1;
            lblDescription.Text = resources.GetString("lblDescription.Text");
            // 
            // linkWebsite
            // 
            linkWebsite.BackColor = Color.FromArgb(20, 18, 17);
            linkWebsite.Icon = Properties.Resources.ROIcon;
            linkWebsite.Location = new Point(20, 159);
            linkWebsite.Name = "linkWebsite";
            linkWebsite.Size = new Size(636, 62);
            linkWebsite.TabIndex = 1;
            linkWebsite.Title = "Rust Optimizer Official Website";
            linkWebsite.Url = "https://rustoptimizer.us/";
            // 
            // aboutLinkTile1
            // 
            aboutLinkTile1.BackColor = Color.FromArgb(20, 18, 17);
            aboutLinkTile1.Icon = Properties.Resources.cup;
            aboutLinkTile1.Location = new Point(342, 301);
            aboutLinkTile1.Name = "aboutLinkTile1";
            aboutLinkTile1.Size = new Size(315, 62);
            aboutLinkTile1.TabIndex = 2;
            aboutLinkTile1.Title = "Donations";
            aboutLinkTile1.Url = "https://buymeacoffee.com/rustforgedev";
            // 
            // linkDocs
            // 
            linkDocs.BackColor = Color.FromArgb(20, 18, 17);
            linkDocs.Icon = Properties.Resources.logo;
            linkDocs.Location = new Point(20, 233);
            linkDocs.Name = "linkDocs";
            linkDocs.Size = new Size(315, 62);
            linkDocs.TabIndex = 3;
            linkDocs.Title = "Documentation";
            linkDocs.Url = "https://rustoptimizer.us/docs";
            // 
            // linkDiscord
            // 
            linkDiscord.BackColor = Color.FromArgb(20, 18, 17);
            linkDiscord.Icon = Properties.Resources.discord;
            linkDiscord.Location = new Point(20, 301);
            linkDiscord.Name = "linkDiscord";
            linkDiscord.Size = new Size(315, 62);
            linkDiscord.TabIndex = 4;
            linkDiscord.Title = "Discord Support";
            linkDiscord.Url = "https://rustoptimizer.us/invite";
            // 
            // linkGitHub
            // 
            linkGitHub.BackColor = Color.FromArgb(20, 18, 17);
            linkGitHub.Icon = Properties.Resources.github;
            linkGitHub.Location = new Point(342, 233);
            linkGitHub.Name = "linkGitHub";
            linkGitHub.Size = new Size(315, 62);
            linkGitHub.TabIndex = 5;
            linkGitHub.Title = "GitHub Source";
            linkGitHub.Url = "https://github.com/V0idpool/RustOptimizer";
            // 
            // grpDiagnostics
            // 
            grpDiagnostics.BackColor = Color.Transparent;
            grpDiagnostics.Controls.Add(btnOpenLog);
            grpDiagnostics.Controls.Add(btnOpenLogDir);
            grpDiagnostics.Controls.Add(btnOpenSettingsDir);
            grpDiagnostics.Font = new Font("Segoe UI", 9.5F);
            grpDiagnostics.Location = new Point(16, 400);
            grpDiagnostics.Name = "grpDiagnostics";
            grpDiagnostics.Padding = new Padding(14, 44, 14, 14);
            grpDiagnostics.Size = new Size(673, 114);
            grpDiagnostics.SubTitle = "Quick access to logs and UserCFG.ini";
            grpDiagnostics.TabIndex = 1;
            grpDiagnostics.Title = "Diagnostics & Config Files";
            // 
            // btnOpenLog
            // 
            btnOpenLog.AccentColor = Color.FromArgb(224, 83, 40);
            btnOpenLog.BackColor = Color.Transparent;
            btnOpenLog.CornerRadius = 8;
            btnOpenLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOpenLog.ForeColor = Color.FromArgb(242, 238, 234);
            btnOpenLog.IsPrimaryAction = false;
            btnOpenLog.Location = new Point(14, 54);
            btnOpenLog.Name = "btnOpenLog";
            btnOpenLog.Size = new Size(206, 38);
            btnOpenLog.TabIndex = 0;
            btnOpenLog.Text = "Open Log File";
            btnOpenLog.Click += openLogFileToolStripMenuItem_Click;
            // 
            // btnOpenLogDir
            // 
            btnOpenLogDir.AccentColor = Color.FromArgb(224, 83, 40);
            btnOpenLogDir.BackColor = Color.Transparent;
            btnOpenLogDir.CornerRadius = 8;
            btnOpenLogDir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOpenLogDir.ForeColor = Color.FromArgb(242, 238, 234);
            btnOpenLogDir.IsPrimaryAction = false;
            btnOpenLogDir.Location = new Point(232, 54);
            btnOpenLogDir.Name = "btnOpenLogDir";
            btnOpenLogDir.Size = new Size(206, 38);
            btnOpenLogDir.TabIndex = 1;
            btnOpenLogDir.Text = "Open Logs Folder";
            btnOpenLogDir.Click += openLogFilePathToolStripMenuItem_Click;
            // 
            // btnOpenSettingsDir
            // 
            btnOpenSettingsDir.AccentColor = Color.FromArgb(224, 83, 40);
            btnOpenSettingsDir.BackColor = Color.Transparent;
            btnOpenSettingsDir.CornerRadius = 8;
            btnOpenSettingsDir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOpenSettingsDir.ForeColor = Color.FromArgb(242, 238, 234);
            btnOpenSettingsDir.IsPrimaryAction = false;
            btnOpenSettingsDir.Location = new Point(450, 54);
            btnOpenSettingsDir.Name = "btnOpenSettingsDir";
            btnOpenSettingsDir.Size = new Size(209, 38);
            btnOpenSettingsDir.TabIndex = 2;
            btnOpenSettingsDir.Text = "Open Settings Folder";
            btnOpenSettingsDir.Click += openSettingsFilePathToolStripMenuItem_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(20, 18, 17);
            pnlSidebar.Controls.Add(label1);
            pnlSidebar.Controls.Add(updateToggle);
            pnlSidebar.Controls.Add(_lnkWebsite);
            pnlSidebar.Controls.Add(roLabel1);
            pnlSidebar.Controls.Add(navDashboard);
            pnlSidebar.Controls.Add(navProfiles);
            pnlSidebar.Controls.Add(navTools);
            pnlSidebar.Controls.Add(navAutoFlush);
            pnlSidebar.Controls.Add(navNetwork);
            pnlSidebar.Controls.Add(navAbout);
            pnlSidebar.Controls.Add(sepSidebar);
            pnlSidebar.Controls.Add(launchButton1);
            pnlSidebar.Location = new Point(1, 74);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(212, 525);
            pnlSidebar.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8.25F);
            label1.ForeColor = Color.FromArgb(165, 156, 148);
            label1.Location = new Point(14, 366);
            label1.Name = "label1";
            label1.Size = new Size(107, 13);
            label1.TabIndex = 11;
            label1.Text = "Check For Updates:";
            // 
            // updateToggle
            // 
            updateToggle.BackColor = Color.Transparent;
            updateToggle.Checked = true;
            updateToggle.Location = new Point(156, 361);
            updateToggle.Name = "updateToggle";
            updateToggle.Size = new Size(44, 22);
            updateToggle.TabIndex = 10;
            updateToggle.Text = "roToggle1";
            updateToggle.CheckedChanged += updateToggle_CheckedChanged;
            // 
            // _lnkWebsite
            // 
            _lnkWebsite.ActiveLinkColor = Color.White;
            _lnkWebsite.AutoSize = true;
            _lnkWebsite.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _lnkWebsite.LinkBehavior = LinkBehavior.HoverUnderline;
            _lnkWebsite.LinkColor = Color.FromArgb(224, 83, 40);
            _lnkWebsite.Location = new Point(45, 428);
            _lnkWebsite.Name = "_lnkWebsite";
            _lnkWebsite.Size = new Size(123, 15);
            _lnkWebsite.TabIndex = 0;
            _lnkWebsite.TabStop = true;
            _lnkWebsite.Text = "voidtechstudios.us ↗";
            _lnkWebsite.VisitedLinkColor = Color.FromArgb(224, 83, 40);
            _lnkWebsite.LinkClicked += _lnkWebsite_LinkClicked;
            // 
            // roLabel1
            // 
            roLabel1.BackColor = Color.Transparent;
            roLabel1.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            roLabel1.Location = new Point(29, 402);
            roLabel1.Name = "roLabel1";
            roLabel1.Size = new Size(154, 24);
            roLabel1.TabIndex = 1;
            roLabel1.Value1 = "VOID TECH";
            roLabel1.Value2 = " STUDIOS";
            // 
            // navDashboard
            // 
            navDashboard.BackColor = Color.Transparent;
            navDashboard.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            navDashboard.LinkedPanel = pnlDashboard;
            navDashboard.Location = new Point(12, 16);
            navDashboard.Name = "navDashboard";
            navDashboard.Selected = true;
            navDashboard.Size = new Size(188, 38);
            navDashboard.TabIndex = 2;
            navDashboard.Text = "Dashboard";
            navDashboard.TextCharacter = "⌂";
            // 
            // navProfiles
            // 
            navProfiles.BackColor = Color.Transparent;
            navProfiles.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            navProfiles.LinkedPanel = pnlProfiles;
            navProfiles.Location = new Point(12, 60);
            navProfiles.Name = "navProfiles";
            navProfiles.Selected = false;
            navProfiles.Size = new Size(188, 38);
            navProfiles.TabIndex = 3;
            navProfiles.Text = "Profiles & Backups";
            navProfiles.TextCharacter = "⚡";
            // 
            // navTools
            // 
            navTools.BackColor = Color.Transparent;
            navTools.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            navTools.LinkedPanel = pnlTools;
            navTools.Location = new Point(12, 104);
            navTools.Name = "navTools";
            navTools.Selected = false;
            navTools.Size = new Size(188, 38);
            navTools.TabIndex = 4;
            navTools.Text = "System Tweaks";
            navTools.TextCharacter = "⚙";
            // 
            // navAutoFlush
            // 
            navAutoFlush.BackColor = Color.Transparent;
            navAutoFlush.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            navAutoFlush.LinkedPanel = pnlAutoFlush;
            navAutoFlush.Location = new Point(12, 148);
            navAutoFlush.Name = "navAutoFlush";
            navAutoFlush.Selected = false;
            navAutoFlush.Size = new Size(188, 38);
            navAutoFlush.TabIndex = 5;
            navAutoFlush.Text = "RAM Auto-Flush";
            navAutoFlush.TextCharacter = "↻";
            // 
            // navNetwork
            // 
            navNetwork.BackColor = Color.Transparent;
            navNetwork.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            navNetwork.LinkedPanel = pnlNetwork;
            navNetwork.Location = new Point(12, 192);
            navNetwork.Name = "navNetwork";
            navNetwork.Selected = false;
            navNetwork.Size = new Size(188, 38);
            navNetwork.TabIndex = 6;
            navNetwork.Text = "Network & Ping";
            navNetwork.TextCharacter = "⇄";
            // 
            // navAbout
            // 
            navAbout.BackColor = Color.Transparent;
            navAbout.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            navAbout.LinkedPanel = pnlAbout;
            navAbout.Location = new Point(12, 236);
            navAbout.Name = "navAbout";
            navAbout.Selected = false;
            navAbout.Size = new Size(188, 38);
            navAbout.TabIndex = 7;
            navAbout.Text = "Logs & About";
            navAbout.TextCharacter = "★";
            // 
            // sepSidebar
            // 
            sepSidebar.BackColor = Color.Transparent;
            sepSidebar.Location = new Point(12, 446);
            sepSidebar.Name = "sepSidebar";
            sepSidebar.Size = new Size(188, 8);
            sepSidebar.TabIndex = 8;
            // 
            // launchButton1
            // 
            launchButton1.BackColor = Color.Transparent;
            launchButton1.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            launchButton1.ForeColor = Color.White;
            launchButton1.Location = new Point(12, 460);
            launchButton1.Name = "launchButton1";
            launchButton1.Size = new Size(188, 54);
            launchButton1.SubText = "";
            launchButton1.TabIndex = 9;
            launchButton1.Text = "LAUNCH RUST";
            launchButton1.Click += launchButton1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.BackColor = Color.FromArgb(28, 25, 23);
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Font = new Font("Segoe UI", 9F);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem, supportToolStripMenuItem, flushRamToolStripMenuItem, minimizeToTrayToolStripMenuItem });
            menuStrip1.Location = new Point(1, 46);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.Size = new Size(918, 28);
            menuStrip1.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backUpLocationToolStripMenuItem, exitToolStripMenuItem, restoreBackupToolStripMenuItem, defaultSettingsToolStripMenuItem, exportProfileToolStripMenuItem, importProfileToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem1 });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // backUpLocationToolStripMenuItem
            // 
            backUpLocationToolStripMenuItem.Name = "backUpLocationToolStripMenuItem";
            backUpLocationToolStripMenuItem.Size = new Size(164, 22);
            backUpLocationToolStripMenuItem.Text = "Back-ups Path...";
            backUpLocationToolStripMenuItem.Click += backUpLocationToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(164, 22);
            exitToolStripMenuItem.Text = "Save Back-up...";
            exitToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // restoreBackupToolStripMenuItem
            // 
            restoreBackupToolStripMenuItem.Name = "restoreBackupToolStripMenuItem";
            restoreBackupToolStripMenuItem.Size = new Size(164, 22);
            restoreBackupToolStripMenuItem.Text = "Restore Backup...";
            restoreBackupToolStripMenuItem.Click += restoreBackupToolStripMenuItem_Click;
            // 
            // defaultSettingsToolStripMenuItem
            // 
            defaultSettingsToolStripMenuItem.Name = "defaultSettingsToolStripMenuItem";
            defaultSettingsToolStripMenuItem.Size = new Size(164, 22);
            defaultSettingsToolStripMenuItem.Text = "Load Defaults...";
            defaultSettingsToolStripMenuItem.Click += defaultSettingsToolStripMenuItem_Click;
            // 
            // exportProfileToolStripMenuItem
            // 
            exportProfileToolStripMenuItem.Name = "exportProfileToolStripMenuItem";
            exportProfileToolStripMenuItem.Size = new Size(164, 22);
            exportProfileToolStripMenuItem.Text = "Export Profile...";
            exportProfileToolStripMenuItem.Click += exportProfileToolStripMenuItem_Click;
            // 
            // importProfileToolStripMenuItem
            // 
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
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(164, 22);
            exitToolStripMenuItem1.Text = "Exit";
            exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openLogFileToolStripMenuItem, openLogFilePathToolStripMenuItem, openSettingsFilePathToolStripMenuItem });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(44, 24);
            aboutToolStripMenuItem.Text = "Help";
            // 
            // openLogFileToolStripMenuItem
            // 
            openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            openLogFileToolStripMenuItem.Size = new Size(205, 22);
            openLogFileToolStripMenuItem.Text = "Open Log File...";
            openLogFileToolStripMenuItem.Click += openLogFileToolStripMenuItem_Click;
            // 
            // openLogFilePathToolStripMenuItem
            // 
            openLogFilePathToolStripMenuItem.Name = "openLogFilePathToolStripMenuItem";
            openLogFilePathToolStripMenuItem.Size = new Size(205, 22);
            openLogFilePathToolStripMenuItem.Text = "Open Log File Path...";
            openLogFilePathToolStripMenuItem.Click += openLogFilePathToolStripMenuItem_Click;
            // 
            // openSettingsFilePathToolStripMenuItem
            // 
            openSettingsFilePathToolStripMenuItem.Name = "openSettingsFilePathToolStripMenuItem";
            openSettingsFilePathToolStripMenuItem.Size = new Size(205, 22);
            openSettingsFilePathToolStripMenuItem.Text = "Open Settings File Path...";
            openSettingsFilePathToolStripMenuItem.Click += openSettingsFilePathToolStripMenuItem_Click;
            // 
            // supportToolStripMenuItem
            // 
            supportToolStripMenuItem.Name = "supportToolStripMenuItem";
            supportToolStripMenuItem.Size = new Size(61, 24);
            supportToolStripMenuItem.Text = "Support";
            supportToolStripMenuItem.Click += supportToolStripMenuItem_Click;
            // 
            // flushRamToolStripMenuItem
            // 
            flushRamToolStripMenuItem.Name = "flushRamToolStripMenuItem";
            flushRamToolStripMenuItem.Size = new Size(76, 24);
            flushRamToolStripMenuItem.Text = "Flush RAM";
            flushRamToolStripMenuItem.Click += flushRamToolStripMenuItem_Click;
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            minimizeToTrayToolStripMenuItem.Size = new Size(107, 24);
            minimizeToTrayToolStripMenuItem.Text = "Minimize to Tray";
            minimizeToTrayToolStripMenuItem.Click += minimizeToTrayToolStripMenuItem_Click;
            // 
            // MainFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 13, 12);
            ClientSize = new Size(920, 600);
            Controls.Add(roThemeContainer1);
            ForeColor = Color.FromArgb(242, 238, 234);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rust Optimizer";
            FormClosing += MainFrm_FormClosing;
            Load += MainFrm_Load;
            roThemeContainer1.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlProfiles.ResumeLayout(false);
            grpProfiles.ResumeLayout(false);
            grpProfiles.PerformLayout();
            grpBackups.ResumeLayout(false);
            grpBackups.PerformLayout();
            pnlNetwork.ResumeLayout(false);
            grpNetworkTweaks.ResumeLayout(false);
            grpNetworkTweaks.PerformLayout();
            grpPingMonitor.ResumeLayout(false);
            pnlAutoFlush.ResumeLayout(false);
            grpAutoFlush.ResumeLayout(false);
            grpAutoFlush.PerformLayout();
            pnlTools.ResumeLayout(false);
            grpSystemTweaks.ResumeLayout(false);
            grpSystemTweaks.PerformLayout();
            grpCacheCleaners.ResumeLayout(false);
            pnlDashboard.ResumeLayout(false);
            grpGamePath.ResumeLayout(false);
            grpHardware.ResumeLayout(false);
            grpQuickActions.ResumeLayout(false);
            pnlAbout.ResumeLayout(false);
            grpLinks.ResumeLayout(false);
            cardPanel.ResumeLayout(false);
            grpDiagnostics.ResumeLayout(false);
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ROThemeContainer roThemeContainer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem backUpLocationToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem restoreBackupToolStripMenuItem;
        private ToolStripMenuItem defaultSettingsToolStripMenuItem;
        private ToolStripMenuItem exportProfileToolStripMenuItem;
        private ToolStripMenuItem importProfileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem openLogFileToolStripMenuItem;
        private ToolStripMenuItem openLogFilePathToolStripMenuItem;
        private ToolStripMenuItem openSettingsFilePathToolStripMenuItem;
        private ToolStripMenuItem supportToolStripMenuItem;
        private ToolStripMenuItem flushRamToolStripMenuItem;
        private ToolStripMenuItem minimizeToTrayToolStripMenuItem;

        private Panel pnlSidebar;
        private ROSidebarItem navDashboard;
        private ROSidebarItem navProfiles;
        private ROSidebarItem navTools;
        private ROSidebarItem navAutoFlush;
        private ROSidebarItem navNetwork;
        private ROSidebarItem navAbout;
        private ROSeparator sepSidebar;
        private ROLaunchButton launchButton1;
        private ROLabel roLabel1;
        private LinkLabel _lnkWebsite;

        private Panel pnlContent;

        // Dashboard
        private Panel pnlDashboard;
        private ROGroupBox grpGamePath;
        public ROTextBox gamePathString;
        private ROModernButton gamePathSelectBtn;
        private ROGroupBox grpHardware;
        private ROStatPill lblCPUInfo;
        private ROStatPill lblGPUInfo;
        private ROStatPill lblRAMInfo;
        private ROGroupBox grpQuickActions;
        private AboutLinkTile linkDonate;
        private ROActionCard cardQuickFlush;
        private ROActionCard cardQuickCache;
        private ROModernButton btnDashTray;

        // Profiles & Backups
        private Panel pnlProfiles;
        private ROGroupBox grpProfiles;
        private ROActionCard cardProfCompetitive;
        private ROActionCard cardProfBalanced;
        private ROActionCard cardProfRecommended;
        private ROActionCard cardProfUltra;
        private Label lblScreenMode;
        private ROComboBox screenModeDropdown;
        private Label lblFpsLimit;
        private ROComboBox fpsLimitDropdown;
        private Label lblMenuFpsLimit;
        private ROComboBox menuFpsLimitDropdown;
        private ROComboBox profileDropdown;
        private ROModernButton autoDetectBtn;
        private ROModernButton optimizeBtn;
        private ROGroupBox grpBackups;
        private Label lblBackupCaption;
        private ROComboBox backupDropdown;
        private ROModernButton saveBackupBtn;
        private ROModernButton restoreBackupBtn;

        // System Tweaks
        private Panel pnlTools;
        private ROGroupBox grpSystemTweaks;
        private Label lblPriorityTitle;
        private Label lblPriorityDesc;
        public ROToggle highPriority;
        private ROSeparator sepTool1;
        private Label lblPCoresTitle;
        private Label lblPCoresDesc;
        public ROToggle pCoresToggle;
        private ROSeparator sepTool2;
        private Label lblFsoTitle;
        private Label lblFsoDesc;
        public ROToggle disabledFSO;
        private ROSeparator sepTool3;
        private Label lblPowerPlanTitle;
        private Label lblPowerPlanDesc;
        public ROToggle autoPowerPlanToggle;
        private ROSeparator sepTool4;
        private Label lblGpuPrefTitle;
        private Label lblGpuPrefDesc;
        public ROToggle highPerfGpuToggle;
        private ROSeparator sepTool5;
        private Label lblGameModeTitle;
        private Label lblGameModeDesc;
        public ROToggle gameModeDvrToggle;
        private ROSeparator sepTool6;
        private Label lblThrottleTitle;
        private Label lblThrottleDesc;
        public ROToggle throttleAppsToggle;
        private ROGroupBox grpCacheCleaners;
        private ROModernButton clearSkinCacheBtn;
        private ROModernButton clearShaderCacheBtn;
        private ROModernButton saveAdvancedCfgBtn;

        // RAM Auto-Flush
        private Panel pnlAutoFlush;
        private ROGroupBox grpAutoFlush;
        private Label lblAutoFlushTitle;
        private Label lblAutoFlushDesc;
        public ROToggle autoFlushChk;
        private ROSeparator sepFlush1;
        private Label lblSmartFlushTitle;
        private Label lblSmartFlushDesc;
        public ROToggle smartFlushToggle;
        private ROSeparator sepFlush2;
        private Label lblSmartThresholdTitle;
        private Label lblSmartThresholdDesc;
        private RONumericUpDown smartFlushThreshold;
        private ROSeparator sepFlush3;
        private Label lblIntervalTitle;
        private Label lblIntervalDesc;
        private RONumericUpDown autoFlushinterval;
        private ROComboBox autoFlushMinHour;
        private ROSeparator sepFlush4;
        private Label lblFlushSfxTitle;
        private Label lblFlushSfxDesc;
        private ROToggle autoFlushSound;
        private ROModernButton btnSaveFlush;
        private ROModernButton btnFlushNow;

        // Network & Ping
        private Panel pnlNetwork;
        private ROGroupBox grpNetworkTweaks;
        private Label lblNicPowerTitle;
        private Label lblNicPowerDesc;
        public ROToggle disableNicPowerToggle;
        private ROSeparator sepNet1;
        private Label lblQosTitle;
        private Label lblQosDesc;
        public ROToggle qosDscpToggle;
        private ROModernButton btnApplyNetworkTweaks;
        private ROModernButton btnFlushDns;
        private ROGroupBox grpPingMonitor;
        private ROTextBox txtServerIp;
        private RONumericUpDown pingCountNumeric;
        private ROModernButton btnRunPingTest;
        private ROStatPill lblPingAvg;
        private ROStatPill lblPingJitter;
        private ROStatPill lblPacketLoss;

        // About & Links
        private Panel pnlAbout;
        private ROGroupBox grpLinks;
        private Panel cardPanel;
        private ROLabel roLabel2;
        private Label lblDescription;
        private AboutLinkTile linkWebsite;
        private AboutLinkTile aboutLinkTile1;
        private AboutLinkTile linkDocs;
        private AboutLinkTile linkDiscord;
        private AboutLinkTile linkGitHub;
        private ROGroupBox grpDiagnostics;
        private ROModernButton btnOpenLog;
        private ROModernButton btnOpenLogDir;
        private ROModernButton btnOpenSettingsDir;
        private Label label1;
        private ROToggle updateToggle;
    }
}