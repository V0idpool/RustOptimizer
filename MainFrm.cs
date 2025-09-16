using RustOptimizer.Core;
using RustOptimizer.Helpers;
using System.Diagnostics;
using System.Globalization;

namespace RustOptimizer
{
    public partial class MainFrm : Form
    {
        public static string ConfigPath { get; private set; } = Path.Combine(Application.StartupPath, "User", "UserCFG.ini");
        private string BackupsPath { get; set; } = Path.Combine(Application.StartupPath, "backups");
        private string GameConfigPath => Path.Combine(gamePathString.Text, "cfg", "client.cfg");
        private RustConfig rustConfig = new RustConfig();
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = ConfigPath };
            string directory = Path.GetDirectoryName(ConfigPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(ConfigPath))
            {
                Helpers.EmbedResources.SaveToDisk("UserCFG.ini", ConfigPath);
            }

            string gamePath = ini.ReadValue("AppSettings", "GamePath");
            if (!string.IsNullOrEmpty(gamePath))
            {
                gamePathString.Text = gamePath;
            }

            BackupsPath = ini.ReadValue("AppSettings", "BackupPath");
            if (string.IsNullOrEmpty(BackupsPath))
            {
                BackupsPath = Path.Combine(Application.StartupPath, "backups");
                ini.WriteValue("AppSettings", "BackupPath", BackupsPath, ini.Path);
            }
            string savedProfile = ini.ReadValue("AppSettings", "Profile");
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
            lblCPUInfo.Text = string.Format("{0} ({1} GHz)", Helpers.HardwareDetector.GetCpuName(), Helpers.HardwareDetector.GetCpuSpeedInGhz());
            lblGPUInfo.Text = $"{HardwareDetector.GetGpuName()} ({HardwareDetector.GetGpuVramInGB():F0}GB VRAM)";
            lblRAMInfo.Text = $"{HardwareDetector.GetRamInfo()} ({HardwareDetector.GetTotalMemoryInGB():F0} GB)";
            LoadBackups();
        }
        /// <summary>
        /// Grabs all the backup folders and adds them to the dropdown menu.
        /// </summary>
        private void LoadBackups()
        {
            backupDropdown.Items.Clear();
            if (Directory.Exists(BackupsPath))
            {
                string[] backupDirectories = Directory.GetDirectories(BackupsPath, "Backup-*");
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
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    gamePathString.Text = fbd.SelectedPath;
                }
            }
        }
        /// <summary>
        /// This button saves the user's settings, like the game path and profile, to the INI file.
        /// </summary>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = ConfigPath };
            ini.WriteValue("AppSettings", "GamePath", gamePathString.Text, ini.Path);
            ini.WriteValue("AppSettings", "Profile", profileDropdown.Text, ini.Path);
            MessageBox.Show("Settings Saved!");
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

            rustConfig.LoadSettings(GameConfigPath);

            var optimalSettings = Optimizer.GetOptimalSettings(profile);

            foreach (var setting in optimalSettings)
            {
                rustConfig.SetSetting(setting.Key, setting.Value);
            }

            rustConfig.SaveSettings(GameConfigPath);
            MessageBox.Show($"'{profile}' profile applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Creates a backup of the current client.cfg file, named with a timestamp, and adds it to the list.
        /// </summary>
        private void saveBackupBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gamePathString.Text) || !File.Exists(GameConfigPath))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string backupFolder = "Backup-" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
            string newBackupPath = Path.Combine(BackupsPath, backupFolder);

            Directory.CreateDirectory(newBackupPath);
            rustConfig.LoadSettings(GameConfigPath);
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
            string backupConfigPath = Path.Combine(BackupsPath, selectedBackupFolder, "client.cfg");

            if (!File.Exists(backupConfigPath))
            {
                MessageBox.Show("Selected backup file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            File.Copy(backupConfigPath, GameConfigPath, true);
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
                string initialPath = BackupsPath;

                if (Directory.Exists(BackupsPath))
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
                        File.Copy(backupConfigPath, GameConfigPath, true);
                        MessageBox.Show("Settings restored successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
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
            if (string.IsNullOrEmpty(gamePathString.Text) || !File.Exists(GameConfigPath))
            {
                MessageBox.Show("Please select a valid game path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string backupFolder = "Backup-" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
            string newBackupPath = Path.Combine(BackupsPath, backupFolder);
            Directory.CreateDirectory(newBackupPath);
            File.Copy(GameConfigPath, Path.Combine(newBackupPath, "client.cfg"));
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
                if (Directory.Exists(BackupsPath))
                {
                    fbd.SelectedPath = BackupsPath;
                }

                fbd.Description = "Select a new location for your backups:";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    var ini = new inisettings { Path = ConfigPath };
                    ini.WriteValue("AppSettings", "BackupPath", fbd.SelectedPath, ini.Path);
                    BackupsPath = fbd.SelectedPath;
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
                Helpers.EmbedResources.SaveToDisk("client.cfg", GameConfigPath);
                MessageBox.Show("Default settings restored successfully! Restart your game for changes to take effect.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while restoring default settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Opens a Discord invite link to join the support server.
        /// </summary>
        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string supportURL = "https://discord.gg/R4bR9JwAfv";

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
                @"\b\fs24 • Download:\b0\par " +
                @"\fs20\hlink https://github.com/V0idpool/RustOptimizer\par\par " +
                @"\b\fs24 • Donations:\b0\par " +
                @"\fs20\hlink https://buymeacoffee.com/rustforgedev\par\par " +
                @"\b\fs24 • Support:\b0\par " +
                @"\fs20\hlink https://discord.gg/R4bR9JwAfv\par\par " +
                @"}";

            using (About aboutForm = new About(rtfMessage))
            {
                aboutForm.nsGroupBox1.Title = titleAbout;
                aboutForm.ShowDialog();
            }
        }
    }
}
