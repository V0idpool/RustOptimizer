using Microsoft.Win32;
using RustOptimizer.Core;
using RustOptimizer.Helpers;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace RustOptimizer
{
    public partial class MainFrm : Form
    {

        public static MainFrm Instance { get; private set; }
        private RustConfig rustConfig = new RustConfig();
        public System.Windows.Forms.Timer autoFlushTimer;
        public NotifyIcon sysTrayIcon;
        private CancellationTokenSource _pingCts;

        public MainFrm()
        {
            InitializeComponent();
            Instance = this;
            // Fixes the buggy colors of the menustrip :(
            menuStrip1.Renderer = new GUI.ROMenuRenderer();
            menuStrip1.ForeColor = GUI.ROPalette.TextPrimary;
            menuStrip1.BackColor = GUI.ROPalette.SurfaceCard;
            cardProfCompetitive.Click += (s, e) => profileDropdown.SelectedIndex = 0;
            cardProfBalanced.Click += (s, e) => profileDropdown.SelectedIndex = 1;
            cardProfRecommended.Click += (s, e) => profileDropdown.SelectedIndex = 2;
            cardProfUltra.Click += (s, e) => profileDropdown.SelectedIndex = 3;
            profileDropdown.SelectedIndexChanged += (s, e) => SyncProfileCards();
        }

        private async void MainFrm_Load(object sender, EventArgs e)
        {
            System.Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string versionString = currentVersion.Major.ToString() + "." + currentVersion.Minor.ToString();
            Text = $"Rust Optimizer v{versionString}";
            roThemeContainer1.SubHeader = $"v{versionString}";

            string directory = Path.GetDirectoryName(UserConfigs.ConfigPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(UserConfigs.ConfigPath))
            {
                Helpers.EmbedResources.SaveToDisk("UserCFG.ini", UserConfigs.ConfigPath);
            }
            UserConfigs.Refresh();
            if (UserConfigs.CheckForUpdates)
            {
                _ = Updates.CheckForUpdates();
            }

            string gamePath = UserConfigs.GamePath;
            if (!UserConfigs.IsValidRustDirectory(gamePath))
            {
                string? detectedPath = UserConfigs.TryAutoDetectRustPath();
                if (!string.IsNullOrEmpty(detectedPath))
                {
                    UserConfigs.SetGamePath(detectedPath);
                    var ini = new inisettings { Path = UserConfigs.ConfigPath };
                    ini.WriteValue("AppSettings", "GamePath", UserConfigs.GamePath, ini.Path);
                    gamePath = UserConfigs.GamePath;
                }
            }

            if (!string.IsNullOrEmpty(gamePath))
            {
                gamePathString.Text = gamePath;
            }

            string backupsPath = UserConfigs.BackupsPath;
            if (string.IsNullOrEmpty(backupsPath))
            {
                UserConfigs.BackupsPath = Path.Combine(Application.StartupPath, "backups");
            }
            string savedProfile = UserConfigs.SavedProfile;
            if (!string.IsNullOrEmpty(savedProfile) && profileDropdown.Items.Contains(savedProfile))
            {
                profileDropdown.SelectedItem = savedProfile;
            }
            else
            {
                string recommendedProfile = RecommendProfile();
                profileDropdown.SelectedItem = recommendedProfile;
                MessageBox.Show($"Based on your hardware, we recommend the '{recommendedProfile}' profile for the best experience.", "Profile Recommendation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int interval = UserConfigs.AutoFlushInterval;
            string unitStr = UserConfigs.AutoFlushUnit;
            autoFlushinterval.Value = interval;

            if (!string.IsNullOrEmpty(unitStr) && (unitStr.Equals("Minutes", StringComparison.OrdinalIgnoreCase) || unitStr.Equals("Hours", StringComparison.OrdinalIgnoreCase)))
            {
                autoFlushMinHour.Text = unitStr;
            }
            else
            {
                autoFlushMinHour.Text = "Minutes";
            }
            pCoresToggle.Checked = UserConfigs.PCoresOnly;
            Optimizer.SetCpuAffinity(UserConfigs.PCoresOnly);
            autoFlushChk.Checked = UserConfigs.AutoFlushEnabled;
            highPriority.Checked = UserConfigs.CPUHighPriority;
            Optimizer.SetPriority(UserConfigs.CPUHighPriority);

            disabledFSO.CheckedChanged -= disabledFSO_CheckedChanged;
            disabledFSO.Checked = UserConfigs.DisableFSO;
            disabledFSO.CheckedChanged += disabledFSO_CheckedChanged;
            try
            {
                Optimizer.ApplyFSOState(UserConfigs.DisableFSO, UserConfigs.GamePath);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }

            autoFlushSound.Checked = UserConfigs.AutoFlushSfx;
            if (autoFlushChk.Checked && !Optimizer.IsAdministrator())
            {
                autoFlushChk.Checked = false;
                UserConfigs.AutoFlushEnabled = false;

                DialogResult result = MessageBox.Show(
                "Auto Flush requires Rust Optimizer to be run as Administrator.\n\nWould you like to restart the application as Administrator now?",
                "Administrator Required",
                 MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
                );

                if (result == DialogResult.OK)
                {
                    UserConfigs.AutoFlushEnabled = true;
                    autoFlushChk.Checked = true;
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        WorkingDirectory = Application.StartupPath,
                        FileName = Application.ExecutablePath,
                        Verb = "runas"
                    };

                    try
                    {
                        Process.Start(startInfo);
                        Application.Exit();
                        return;
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        UserConfigs.AutoFlushEnabled = false;
                        autoFlushChk.Checked = false;
                        MessageBox.Show("Restart canceled. Rust Optimizer will run in standard user mode without Auto Flush features.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            Optimizer.InitializeAutoFlushTimer();
            lblCPUInfo.Text = string.Format("{0} ({1} GHz)", Helpers.HardwareDetector.GetCpuName(), Helpers.HardwareDetector.GetCpuSpeedInGhz());
            lblGPUInfo.Text = $"{HardwareDetector.GetGpuName()} ({HardwareDetector.GetGpuVramInGB():F0}GB VRAM)";
            lblRAMInfo.Text = $"{HardwareDetector.GetRamInfo()} ({HardwareDetector.GetTotalMemoryInGB():F0} GB)";
            LoadBackups();
            updateToggle.Checked = UserConfigs.CheckForUpdates;
            screenModeDropdown.SelectedIndex = UserConfigs.ScreenMode == 0 ? 1 : 0;
            SelectDropdownByNumericValue(fpsLimitDropdown, UserConfigs.FpsLimit, 0);
            SelectDropdownByNumericValue(menuFpsLimitDropdown, UserConfigs.FpsLimitInMenu, 2);
            smartFlushToggle.Checked = UserConfigs.SmartFlushEnabled;
            smartFlushThreshold.Value = Math.Clamp(UserConfigs.SmartFlushThresholdGB, 1, 16);
            autoPowerPlanToggle.Checked = UserConfigs.AutoPowerPlan;
            highPerfGpuToggle.Checked = UserConfigs.HighPerfGpuBinding;
            gameModeDvrToggle.Checked = UserConfigs.GameModeAndDisableDvr;
            throttleAppsToggle.Checked = UserConfigs.ThrottleBackgroundApps;
            Optimizer.SetAutoPowerPlan(UserConfigs.AutoPowerPlan);
            Optimizer.SetBackgroundAppThrottling(UserConfigs.ThrottleBackgroundApps);

            try
            {
                if (UserConfigs.HighPerfGpuBinding)
                    Optimizer.ApplyGpuPreference(true, UserConfigs.GamePath);
                if (UserConfigs.GameModeAndDisableDvr)
                    Optimizer.ApplyGameModeAndDvr(true);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }

            // Network & Ping UI
            disableNicPowerToggle.Checked = UserConfigs.DisableNicPowerSaving;
            qosDscpToggle.Checked = UserConfigs.QosDscpEnabled;
            txtServerIp.Text = UserConfigs.LastPingServer;
            ContextMenuStrip trayMenu = new ContextMenuStrip
            {
                Renderer = new GUI.ROMenuRenderer()
            };
            trayMenu.Items.Add("Launch Rust...", null, (s, args) => TrayHandler.LaunchRust());
            trayMenu.Items.Add("Open", null, (s, args) => TrayHandler.ShowFormFromTray());
            trayMenu.Items.Add("Exit", null, (s, args) =>
            {
                sysTrayIcon.Visible = false;
                Application.Exit();
            });

            sysTrayIcon = new NotifyIcon
            {
                Icon = this.Icon,
                Text = "Rust Optimizer",
                ContextMenuStrip = trayMenu,
                Visible = false
            };
            navDashboard.Selected = true;
            sysTrayIcon.DoubleClick += (s, args) => TrayHandler.ShowFormFromTray();
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _pingCts?.Cancel();
            Optimizer.RestoreAllRuntimeStates();
        }

        /// <summary>
        /// Grabs all the backup folders and adds them to the dropdown menu.
        /// </summary>
        private void LoadBackups()
        {
            backupDropdown.Items.Clear();
            if (Directory.Exists(UserConfigs.BackupsPath))
            {
                string[] backupDirectories = Directory.GetDirectories(UserConfigs.BackupsPath, "Backup-*");
                foreach (string dir in backupDirectories)
                {
                    backupDropdown.Items.Add(Path.GetFileName(dir));
                }
            }
        }
        /// <summary>
        /// Checks the user's hardware specs and suggests an optimization profile.
        /// </summary>
        public string RecommendProfile()
        {
            double ramInGB = HardwareDetector.GetTotalMemoryInGB();
            double vramInGB = HardwareDetector.GetGpuVramInGB();
            double cpuSpeedInGhz = HardwareDetector.GetCpuSpeedInGhz();

            if (vramInGB >= 12 && ramInGB >= 32 && cpuSpeedInGhz >= 4.0)
            {
                return "Ultra (Maximum Visuals)";
            }

            if (vramInGB >= 8 && ramInGB >= 16 && cpuSpeedInGhz >= 3.0)
            {
                return "Recommended (Optimized)";
            }

            if (vramInGB >= 4 && ramInGB >= 8 && cpuSpeedInGhz >= 2.5)
            {
                return "Balanced (Good Performance & Looks)";
            }

            return "Competitive (Max FPS)";
        }
        /// <summary>
        /// This button lets the user select their game's path using a folder dialog.
        /// </summary>
        private void gamePathSelectBtn_Click(object sender, EventArgs e)
        {
            var ini = new inisettings { Path = UserConfigs.ConfigPath };
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    UserConfigs.SetGamePath(fbd.SelectedPath);
                    gamePathString.Text = fbd.SelectedPath;
                    ini.WriteValue("AppSettings", "GamePath", fbd.SelectedPath, ini.Path);
                    UserConfigs.Refresh();
                }
            }
        }

        /// <summary>
        /// Just a simple function to close the application.
        /// </summary>
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SyncProfileCards()
        {
            cardProfCompetitive.Selected = profileDropdown.SelectedIndex == 0;
            cardProfBalanced.Selected = profileDropdown.SelectedIndex == 1;
            cardProfRecommended.Selected = profileDropdown.SelectedIndex == 2;
            cardProfUltra.Selected = profileDropdown.SelectedIndex == 3;
        }

        private static void SelectDropdownByNumericValue(ComboBox combo, int value, int fallbackIndex)
        {
            string target = value == 0 ? "Unlimited (0)" : value.ToString(CultureInfo.InvariantCulture);
            int idx = combo.Items.IndexOf(target);
            combo.SelectedIndex = idx >= 0 ? idx : Math.Min(fallbackIndex, combo.Items.Count - 1);
        }

        private static int ParseDropdownNumericValue(ComboBox combo, int fallback)
        {
            string text = combo.SelectedItem?.ToString() ?? string.Empty;
            if (text.StartsWith("Unlimited", StringComparison.OrdinalIgnoreCase)) return 0;
            return int.TryParse(text, out int parsed) ? parsed : fallback;
        }

        /// <summary>
        /// The main button that applies the optimization profile to the game's client.cfg file.
        /// It first loads the current settings, updates them with the new profile, and saves the file.
        /// </summary>
        private void optimizeBtn_Click(object sender, EventArgs e)
        {
            UserConfigs.SetGamePath(gamePathString.Text);
            gamePathString.Text = UserConfigs.GamePath;
            if (string.IsNullOrEmpty(gamePathString.Text) || !Directory.Exists(gamePathString.Text))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string profile = profileDropdown.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(profile))
            {
                MessageBox.Show("Please select an optimization profile first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int screenMode = screenModeDropdown.SelectedIndex == 1 ? 0 : 1;
            int fpsLimit = ParseDropdownNumericValue(fpsLimitDropdown, 0);
            int menuFpsLimit = ParseDropdownNumericValue(menuFpsLimitDropdown, 60);

            var ini = new inisettings { Path = UserConfigs.ConfigPath };
            ini.WriteValue("AppSettings", "Profile", profile, ini.Path);
            ini.WriteValue("AppSettings", "ScreenMode", screenMode.ToString(CultureInfo.InvariantCulture), ini.Path);
            ini.WriteValue("AppSettings", "FpsLimit", fpsLimit.ToString(CultureInfo.InvariantCulture), ini.Path);
            ini.WriteValue("AppSettings", "FpsLimitInMenu", menuFpsLimit.ToString(CultureInfo.InvariantCulture), ini.Path);
            UserConfigs.Refresh();

            rustConfig.LoadSettings(UserConfigs.GameConfigPath);
            var optimalSettings = Optimizer.GetOptimalSettings(profile);
            foreach (var setting in optimalSettings)
            {
                rustConfig.SetSetting(setting.Key, setting.Value);
            }

            rustConfig.SaveSettings(UserConfigs.GameConfigPath);
            MessageBox.Show($"'{profile}' profile applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
 
        /// <summary>
        /// Creates a backup of the current client.cfg file, named with a timestamp, and adds it to the list.
        /// </summary>
        private void saveBackupBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gamePathString.Text) || !File.Exists(UserConfigs.GameConfigPath))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string backupFolder = "Backup-" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
            string newBackupPath = Path.Combine(UserConfigs.BackupsPath, backupFolder);

            Directory.CreateDirectory(newBackupPath);
            rustConfig.LoadSettings(UserConfigs.GameConfigPath);
            rustConfig.SaveSettings(Path.Combine(newBackupPath, "client.cfg"));
            MessageBox.Show("Settings backup saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadBackups();
        }
        /// <summary>
        /// Restores a selected backup from the dropdown menu, copying it to the game's config folder.
        /// </summary>
        private void restoreBackupBtn_Click(object sender, EventArgs e)
        {
            if (backupDropdown.SelectedItem == null)
            {
                MessageBox.Show("Please select a backup to restore.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(gamePathString.Text) || !Directory.Exists(gamePathString.Text))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedBackupFolder = backupDropdown.SelectedItem.ToString();
            string backupConfigPath = Path.Combine(UserConfigs.BackupsPath, selectedBackupFolder, "client.cfg");

            if (!File.Exists(backupConfigPath))
            {
                MessageBox.Show("Selected backup file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UserConfigs.EnsureGameConfigDirectory();
            File.Copy(backupConfigPath, UserConfigs.GameConfigPath, true);
            MessageBox.Show("Settings restored successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Restores a backup by letting the user pick a folder manually.
        /// </summary>
        private void restoreBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(gamePathString.Text) || !Directory.Exists(gamePathString.Text))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                string initialPath = UserConfigs.BackupsPath;

                if (Directory.Exists(UserConfigs.BackupsPath))
                {
                    fbd.SelectedPath = initialPath;
                }
                else
                {
                    fbd.SelectedPath = Application.StartupPath;
                }

                DialogResult result = fbd.ShowDialog();
                fbd.Description = "Select a backup folder to restore from:";
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string backupConfigPath = Path.Combine(fbd.SelectedPath, "client.cfg");

                    if (!File.Exists(backupConfigPath))
                    {
                        MessageBox.Show("The selected folder does not contain a client.cfg backup.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        UserConfigs.EnsureGameConfigDirectory();

                        File.Copy(backupConfigPath, UserConfigs.GameConfigPath, true);
                        MessageBox.Show("Settings restored successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.LogError(ex);
                        MessageBox.Show($"An error occurred while restoring the backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        /// <summary>
        /// This creates a backup of the current client.cfg file and puts it in a timestamped folder.
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gamePathString.Text) || !File.Exists(UserConfigs.GameConfigPath))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string backupFolder = "Backup-" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
            string newBackupPath = Path.Combine(UserConfigs.BackupsPath, backupFolder);
            Directory.CreateDirectory(newBackupPath);
            File.Copy(UserConfigs.GameConfigPath, Path.Combine(newBackupPath, "client.cfg"));
            MessageBox.Show("Settings backup saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadBackups();
        }
        /// <summary>
        /// Lets the user change where the backups are stored and saves the new path to the INI file.
        /// </summary>
        private void backUpLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (Directory.Exists(UserConfigs.BackupsPath))
                {
                    fbd.SelectedPath = UserConfigs.BackupsPath;
                }

                fbd.Description = "Select a new location for your backups:";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    var ini = new inisettings { Path = UserConfigs.ConfigPath };
                    ini.WriteValue("AppSettings", "BackupPath", fbd.SelectedPath, ini.Path);
                    UserConfigs.BackupsPath = fbd.SelectedPath;
                    UserConfigs.Refresh();
                    LoadBackups();

                    MessageBox.Show("Backup path updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// This is a quick way to restore the game's default settings from the backup we have in the app.
        /// It just replaces your current client.cfg with a fresh one.
        /// </summary>
        private void defaultSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(gamePathString.Text) || !Directory.Exists(gamePathString.Text))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Helpers.EmbedResources.SaveToDisk("client.cfg", UserConfigs.GameConfigPath);
                MessageBox.Show("Default settings restored successfully! Restart your game for changes to take effect.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
                MessageBox.Show($"An error occurred while restoring default settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Opens a Discord invite link to join the support server.
        /// </summary>
        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string supportURL = "https://discord.gg/tfwf9Qr7rG";

            DialogResult result = MessageBox.Show(
                "To receive direct support and connect with our community, you can join our Discord server. Click OK to open the invitation link.",
                "RustForge Discord",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = supportURL,
                    UseShellExecute = true
                });
            }
        }

        /// <summary>
        /// Analyzes the user's hardware (CPU, GPU, and RAM) and recommends the most suitable optimization profile from the dropdown menu for the best performance.
        /// </summary>
        private void autoDetectBtn_Click(object sender, EventArgs e)
        {
            string recommendedProfile = RecommendProfile();
            profileDropdown.SelectedItem = recommendedProfile;
            MessageBox.Show($"Based on your hardware, we recommend the '{recommendedProfile}' profile for the best experience.", "Profile Recommendation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Save your current game settings to a file that can be shared. The file gets saved as a .rop file, making it easy to send to your friends.
        /// </summary>
        private void exportProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gamePathString.Text) || !File.Exists(UserConfigs.GameConfigPath))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Rust Optimizer Profile (*.rop)|*.rop";
                sfd.Title = "Export a Rust Optimizer Profile";
                sfd.FileName = "my_custom_profile.rop";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(UserConfigs.GameConfigPath, sfd.FileName, true);
                        MessageBox.Show("Profile exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.LogError(ex);
                        MessageBox.Show($"An error occurred while exporting the profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        /// <summary>
        /// Import someone else's .rop profile settings into your game, and offers the option to save it as a backup.
        /// </summary>
        private void importProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gamePathString.Text) || !Directory.Exists(gamePathString.Text))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Rust Optimizer Profile (*.rop)|*.rop";
                ofd.Title = "Import a Rust Optimizer Profile";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        UserConfigs.EnsureGameConfigDirectory();
                        File.Copy(ofd.FileName, UserConfigs.GameConfigPath, true);
                        MessageBox.Show("Profile imported successfully! Restart your game for changes to take effect.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        var result = MessageBox.Show("Would you like to save this imported profile as a new backup?", "Save as Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            saveBackupBtn_Click(sender, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.LogError(ex);
                        MessageBox.Show($"An error occurred while importing the profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void flushRamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Optimizer.FlushStandbyList(false);
        }

        private void saveAdvancedCfgBtn_Click(object sender, EventArgs e)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = UserConfigs.ConfigPath };
            UserConfigs.SetGamePath(gamePathString.Text);
            gamePathString.Text = UserConfigs.GamePath;

            ini.WriteValue("AppSettings", "GamePath", gamePathString.Text, ini.Path);
            ini.WriteValue("AppSettings", "Profile", profileDropdown.Text, ini.Path);
            ini.WriteValue("AppSettings", "AutoFlush", autoFlushChk.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "SmartFlush", smartFlushToggle.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "SmartFlushThresholdGB", smartFlushThreshold.Value.ToString(CultureInfo.InvariantCulture), ini.Path);
            ini.WriteValue("AppSettings", "FlushInterval", autoFlushinterval.Value.ToString(CultureInfo.InvariantCulture), ini.Path);
            ini.WriteValue("AppSettings", "FlushUnit", autoFlushMinHour.Text, ini.Path);
            ini.WriteValue("AppSettings", "FlushSound", autoFlushSound.Checked.ToString(CultureInfo.InvariantCulture), ini.Path);
            ini.WriteValue("AppSettings", "CPUHighPriority", highPriority.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "PhysicalCoresOnly", pCoresToggle.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "DisableFSO", disabledFSO.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "AutoPowerPlan", autoPowerPlanToggle.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "HighPerfGpuBinding", highPerfGpuToggle.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "GameModeAndDisableDvr", gameModeDvrToggle.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "ThrottleBackgroundApps", throttleAppsToggle.Checked.ToString(), ini.Path);
            UserConfigs.Refresh();
            Optimizer.SetCpuAffinity(pCoresToggle.Checked);
            Optimizer.SetPriority(highPriority.Checked);
            Optimizer.SetAutoPowerPlan(autoPowerPlanToggle.Checked);
            Optimizer.SetBackgroundAppThrottling(throttleAppsToggle.Checked);
            Optimizer.InitializeAutoFlushTimer();

            try
            {
                Optimizer.ApplyFSOState(disabledFSO.Checked, UserConfigs.GamePath);
                Optimizer.ApplyGpuPreference(highPerfGpuToggle.Checked, UserConfigs.GamePath);
                Optimizer.ApplyGameModeAndDvr(gameModeDvrToggle.Checked);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Some tweaks require Administrator privileges. Try running Rust Optimizer as Administrator.", "Registry Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }

            MessageBox.Show("Settings Saved!", "Rust Optimizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string site = "https://voidtechstudios.us/";
            Process.Start(new ProcessStartInfo
            {
                FileName = site,
                UseShellExecute = true
            });
        }

        private void autoFlushSound_CheckedChanged(object sender, EventArgs e)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = UserConfigs.ConfigPath };
            ini.WriteValue("AppSettings", "FlushSound", autoFlushSound.Checked.ToString(CultureInfo.InvariantCulture), ini.Path);
            UserConfigs.Refresh();
        }

        private void autoFlushChk_CheckedChanged(object sender, EventArgs e)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = UserConfigs.ConfigPath };
            ini.WriteValue("AppSettings", "AutoFlush", autoFlushChk.Checked.ToString(), ini.Path);
            UserConfigs.Refresh();
        }

        private void autoFlushinterval_ValueChanged(object sender, EventArgs e)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = UserConfigs.ConfigPath };
            ini.WriteValue("AppSettings", "FlushInterval", autoFlushinterval.Value.ToString(CultureInfo.InvariantCulture), ini.Path);
            UserConfigs.Refresh();
        }

        private void minimizeToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrayHandler.HideFormToTray();
        }

        private void launchRustBtn_Click(object sender, EventArgs e)
        {
            TrayHandler.LaunchRust();
        }

        private void openLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string logFilePath = ExceptionHandler.GetLogFilePath();

                if (System.IO.File.Exists(logFilePath))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(logFilePath)
                    {
                        UseShellExecute = true
                    });
                }
                else
                {
                    string logDirectory = System.IO.Path.GetDirectoryName(logFilePath);
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(logDirectory)
                    {
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogMessage($"Could not open log file: {ex.Message}");
                MessageBox.Show($"Could not open log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openLogFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string logFilePath = ExceptionHandler.GetLogFilePath();
                string logDirectory = System.IO.Path.GetDirectoryName(logFilePath);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(logDirectory)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogMessage($"Could not open log file path: {ex.Message}");
                MessageBox.Show($"Could not open log file path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openSettingsFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string settingsFilePath = UserConfigs.ConfigPath;
                string settingsDirectory = System.IO.Path.GetDirectoryName(settingsFilePath);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(settingsDirectory)
                {
                    UseShellExecute = true
                });
            }

            catch (Exception ex)
            {
                ExceptionHandler.LogMessage($"Could not open settings file path: {ex.Message}");
                MessageBox.Show($"Could not open settings file path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void launchButton1_Click(object sender, EventArgs e)
        {
            TrayHandler.LaunchRust();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string site = "https://rustoptimizer.us/";
            Process.Start(new ProcessStartInfo
            {
                FileName = site,
                UseShellExecute = true
            });
        }

        private void disabledFSO_CheckedChanged(object sender, EventArgs e)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = UserConfigs.ConfigPath };
            ini.WriteValue("AppSettings", "DisableFSO", disabledFSO.Checked.ToString(), ini.Path);
            UserConfigs.Refresh();
        }

        private void clearSkinCacheBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserConfigs.GamePath) || !Directory.Exists(UserConfigs.GamePath))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DirectoryInfo rustDir = new DirectoryInfo(UserConfigs.GamePath);
                string steamAppsDir = rustDir.Parent.Parent.FullName;
                string workshopPath = Path.Combine(steamAppsDir, "workshop", "content", "252490");

                if (!Directory.Exists(workshopPath))
                {
                    MessageBox.Show("Skin cache is already empty or path is non-standard.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                long freedBytes = 0;
                int deletedFiles = 0;
                DirectoryInfo di = new DirectoryInfo(workshopPath);

                foreach (FileInfo file in di.EnumerateFiles("*", SearchOption.AllDirectories))
                {
                    freedBytes += file.Length;
                    file.Delete();
                    deletedFiles++;
                }

                foreach (DirectoryInfo dir in di.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    dir.Delete(true);
                }

                double freedMB = freedBytes / 1024.0 / 1024.0;
                MessageBox.Show($"Successfully cleared {deletedFiles} cached skins.\nFreed {freedMB:F2} MB of disk space!", "Cache Nuked", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException)
            {
                MessageBox.Show("Could not clear skin cache. Make sure Rust is fully closed so the files aren't in use.", "File in Use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearShaderCacheBtn_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "This clears DirectX, NVIDIA, and AMD GPU shader caches.\n\n" +
                "Note: Your first 5 minutes or so in-game after clearing will rebuild fresh shaders (brief initial loading stutter, smoother gameplay afterward).\n\nProceed?",
                "Clear Shader Cache", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var (deletedFiles, freedMB, skippedFiles) = Optimizer.ClearShaderCaches();
            MessageBox.Show(
                $"Shader cache cleanup complete!\n\n" +
                $"• Files deleted: {deletedFiles}\n" +
                $"• Space freed: {freedMB:F2} MB\n" +
                $"• Active/locked files skipped: {skippedFiles}",
                "Shader Cache Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnApplyNetworkTweaks_Click(object sender, EventArgs e)
        {
            if (!Optimizer.IsAdministrator())
            {
                DialogResult res = MessageBox.Show(
                    "Modifying NIC power settings and Windows QoS DSCP policies requires Administrator privileges.\n\nRestart as Administrator now?",
                    "Administrator Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (res == DialogResult.OK)
                    Optimizer.RestartAsAdministrator();
                return;
            }

            try
            {
                int adaptersUpdated = Optimizer.ApplyNicPowerTweaks(disableNicPowerToggle.Checked);
                Optimizer.ApplyQosDscpPolicy(qosDscpToggle.Checked);

                var ini = new inisettings { Path = UserConfigs.ConfigPath };
                ini.WriteValue("AppSettings", "DisableNicPowerSaving", disableNicPowerToggle.Checked.ToString(), ini.Path);
                ini.WriteValue("AppSettings", "QosDscpEnabled", qosDscpToggle.Checked.ToString(), ini.Path);
                UserConfigs.Refresh();

                MessageBox.Show(
                    $"Network tweaks applied!\n\n" +
                    $"• Active Ethernet NICs updated: {adaptersUpdated} (NIC Power Saving / EEE {(disableNicPowerToggle.Checked ? "Disabled" : "Restored")})\n" +
                    $"• QoS DSCP Policy (RustClient.exe UDP): {(qosDscpToggle.Checked ? "Enabled" : "Removed")}\n\n" +
                    "Tip: Reboot or disconnect/reconnect your Ethernet adapter to make sure the driver reloads power settings.",
                    "Network Tweaks Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
                MessageBox.Show($"Failed to apply network tweaks: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFlushDns_Click(object sender, EventArgs e)
        {
            bool ok = Optimizer.FlushDnsCache(out string output);
            MessageBox.Show(string.IsNullOrWhiteSpace(output) ? (ok ? "DNS resolver cache flushed." : "DNS flush failed.") : output,
                "Flush DNS", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private async void btnRunPingTest_Click(object sender, EventArgs e)
        {
            if (_pingCts != null)
            {
                _pingCts.Cancel();
                return;
            }

            string target = txtServerIp.Text.Trim();
            if (string.IsNullOrWhiteSpace(target))
            {
                MessageBox.Show("Please enter a server IP address or hostname first.", "Ping Monitor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ini = new inisettings { Path = UserConfigs.ConfigPath };
            ini.WriteValue("AppSettings", "LastPingServer", target, ini.Path);
            UserConfigs.LastPingServer = target;

            int count = (int)pingCountNumeric.Value;
            _pingCts = new CancellationTokenSource();
            btnRunPingTest.Text = "Stop";
            lblPingAvg.Text = $"Testing {target} (0/{count})...";
            lblPingJitter.Text = "Measuring...";
            lblPacketLoss.Text = "Measuring...";

            var progress = new Progress<PingTestProgress>(p =>
            {
                lblPingAvg.Text = p.Sent > p.Lost
                    ? $"{p.AvgPingMs:F1} ms  (Min: {p.MinPingMs:F0} | Max: {p.MaxPingMs:F0} ms)  [{p.ProbeMode}]  {p.Completed}/{p.Total}"
                    : $"No reply yet [{p.ProbeMode}]  {p.Completed}/{p.Total}";

                lblPingJitter.Text = p.Sent - p.Lost >= 2
                    ? $"{p.JitterMs:F1} ms  ({(p.JitterMs < 5 ? "Rock Solid" : p.JitterMs < 15 ? "Good" : "Spiky / Unstable")})"
                    : "Calculating...";

                lblPacketLoss.Text = $"{p.PacketLossPercent:F1}%  ({p.Lost} lost of {p.Sent} sent)";
                lblPacketLoss.DotColor = p.PacketLossPercent == 0 ? GUI.ROPalette.HudGreen : GUI.ROPalette.AccentCrimson;
            });

            try
            {
                await Optimizer.RunPingTestAsync(target, count, progress, _pingCts.Token);
            }
            catch (OperationCanceledException)
            {
                lblPingAvg.Text += "  (Stopped)";
            }
            catch (Exception ex)
            {
                lblPingAvg.Text = $"Error: {ex.Message}";
            }
            finally
            {
                _pingCts.Dispose();
                _pingCts = null;
                btnRunPingTest.Text = "Run Test";
            }
        }

        private void _lnkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://voidtechstudios.us/") { UseShellExecute = true });
        }

        private void updateToggle_CheckedChanged(object sender, EventArgs e)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = UserConfigs.ConfigPath };
            ini.WriteValue("AppSettings", "CheckForUpdates", updateToggle.Checked.ToString(), ini.Path);
            UserConfigs.CheckForUpdates = updateToggle.Checked;

            if (updateToggle.Checked)
            {
                _ = Updates.CheckForUpdates();
            }
        }
    }
}