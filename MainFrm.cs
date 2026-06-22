using RustOptimizer.Core;
using RustOptimizer.Helpers;
using System.Diagnostics;
using System.Globalization;

namespace RustOptimizer
{
    public partial class MainFrm : Form
    {
        public static MainFrm Instance { get; private set; }
        private RustConfig rustConfig = new RustConfig();
        public System.Windows.Forms.Timer autoFlushTimer;
        public NotifyIcon sysTrayIcon;
        public MainFrm()
        {
            InitializeComponent();
            Instance = this;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            _ = Updates.CheckForUpdates();
            System.Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string versionString = currentVersion.Major.ToString() + "." + currentVersion.Minor.ToString();
            MainFrm.Instance.Text = $"Rust Optimizer v{versionString}";
            UserConfigs.Refresh();
            string directory = Path.GetDirectoryName(UserConfigs.ConfigPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(UserConfigs.ConfigPath))
            {
                Helpers.EmbedResources.SaveToDisk("UserCFG.ini", UserConfigs.ConfigPath);
            }

            string gamePath = UserConfigs.GamePath;
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

                autoFlushChk.Checked = UserConfigs.AutoFlushEnabled;
                highPriority.Checked = UserConfigs.CPUHighPriority;
                Optimizer.SetPriority(UserConfigs.CPUHighPriority);

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
                        UserConfigs.AutoFlushEnabled=false;
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

            ContextMenuStrip trayMenu = new ContextMenuStrip();
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

            sysTrayIcon.DoubleClick += (s, args) => TrayHandler.ShowFormFromTray();
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

            return "Competitive (Maximum FPS)";
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
                    gamePathString.Text = UserConfigs.GamePath;
                    ini.WriteValue("AppSettings", "GamePath", UserConfigs.GamePath, ini.Path);
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
        /// <summary>
        /// The main button that applies the optimization profile to the game's client.cfg file.
        /// It first loads the current settings, updates them with the new profile, and saves the file.
        /// </summary>
        private void optimizeBtn_Click(object sender, EventArgs e)
        {
            UserConfigs.SetGamePath(gamePathString.Text);
            gamePathString.Text = UserConfigs.GamePath;

            if (string.IsNullOrEmpty(UserConfigs.GamePath) || !Directory.Exists(UserConfigs.GamePath))
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
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string titleAbout = "About RustOptimizer";

            string rtfMessage =
                @"{\rtf1\ansi\deff0 " +
                @"{\fonttbl{\f0 Arial;}}" +
                @"{\colortbl;\red0\green0\blue0;\red255\green128\blue0;\red255\green0\blue0;}" +
                @"\fs24 " +
                @"\pard\sa0\sl250\slmult1\cf2 " +

               @"\b\f0\fs24 RustOptimizer:\b0\par " +
        @"\fs20 RustOptimizer is a tool designed to help users quickly and easily optimize their Rust game settings. It automates the process of adjusting the game's client.cfg file to improve performance and provide an optimal gaming experience based on your system hardware.\par\par " +

                @"\b\fs24 Useful Links:\b0\par " +
                @"\fs20\cf2 " +
                @"\b\fs24 • Downloads:\b0\par " +
                @"\fs20\hlink https://github.com/V0idpool/RustOptimizer\par " +
                @"\fs20\hlink https://www.nexusmods.com/rust/mods/5\par\par " +
                @"\b\fs24 • Donations:\b0\par " +
                @"\fs20\hlink https://buymeacoffee.com/rustforgedev\par\par " +
                @"\b\fs24 • Support:\b0\par " +
                @"\fs20\hlink https://discord.gg/tfwf9Qr7rG\par\par " +
                @"}";

            using (About aboutForm = new About(rtfMessage))
            {
                aboutForm.nsGroupBox1.Title = titleAbout;
                aboutForm.ShowDialog();
            }
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

            ini.WriteValue("AppSettings", "GamePath", UserConfigs.GamePath, ini.Path);
            ini.WriteValue("AppSettings", "Profile", profileDropdown.Text, ini.Path);
            ini.WriteValue("AppSettings", "AutoFlush", autoFlushChk.Checked.ToString(), ini.Path);
            ini.WriteValue("AppSettings", "FlushInterval", autoFlushinterval.Value.ToString(CultureInfo.InvariantCulture), ini.Path);
            ini.WriteValue("AppSettings", "FlushUnit", autoFlushMinHour.Text, ini.Path);
            ini.WriteValue("AppSettings", "FlushSound", autoFlushSound.Checked.ToString(CultureInfo.InvariantCulture), ini.Path);
            ini.WriteValue("AppSettings", "CPUHighPriority", highPriority.Checked.ToString(CultureInfo.InvariantCulture), ini.Path);
            UserConfigs.Refresh();
            Optimizer.InitializeAutoFlushTimer();
            Optimizer.SetPriority(highPriority.Checked);
            MessageBox.Show("Settings Saved!", "Rust Optimizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string site = "https://buymeacoffee.com/rustforgedev";
            Process.Start(new ProcessStartInfo
            {
                FileName = site,
                UseShellExecute = true
            });
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string site = "https://rustforge.us/";
            Process.Start(new ProcessStartInfo
            {
                FileName = site,
                UseShellExecute = true
            });
        }

        private void autoFlushSound_CheckedChanged(object sender)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = UserConfigs.ConfigPath };
            ini.WriteValue("AppSettings", "FlushSound", autoFlushSound.Checked.ToString(CultureInfo.InvariantCulture), ini.Path);
            UserConfigs.Refresh();
        }

        private void autoFlushChk_CheckedChanged(object sender)
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

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string site = "https://buymeacoffee.com/rustforgedev";
            Process.Start(new ProcessStartInfo
            {
                FileName = site,
                UseShellExecute = true
            });
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
    }
}

