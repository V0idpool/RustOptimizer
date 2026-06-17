using System;
using System.Drawing;

namespace RustOptimizer.Core
{
    public static class UserConfigs
    {
        public static string ConfigPath { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Rust Optimizer", "User", "UserCFG.ini");
        public static string BackupsPath { get; set; } = Path.Combine(Application.StartupPath, "backups");
        public static string GamePath { get; set; } = string.Empty;
        public static string ?SavedProfile { get; set; } = null;
        public static bool AutoFlushEnabled { get; set; } = false;
        public static bool AutoFlushSfx { get; set; } = false;
        public static int AutoFlushInterval { get; set; } = 15;
        public static string AutoFlushUnit { get; set; } = "Minutes";
        public static bool CPUHighPriority { get; set; } = false;
        public static string GameConfigPath { get; private set; } = Path.Combine("cfg", "client.cfg");

        public static void SetGamePath(string gamePath)
        {
            GamePath = NormalizeGamePath(gamePath);
            GameConfigPath = Path.Combine(GamePath, "cfg", "client.cfg");
        }

        public static void EnsureGameConfigDirectory()
        {
            string? directory = Path.GetDirectoryName(GameConfigPath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Loads, and refreshes Global Settings.
        /// It first loads the current settings, updates them with the new profile, and saves the file.
        /// </summary>
        public static void Refresh()
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = ConfigPath };

            BackupsPath = ini.ReadValue("AppSettings", "BackupPath");
            SetGamePath(ini.ReadValue("AppSettings", "GamePath"));
            SavedProfile = ini.ReadValue("AppSettings", "Profile");
            AutoFlushEnabled = ini.GetBoolean("AppSettings", "AutoFlush", false);
            AutoFlushSfx = ini.GetBoolean("AppSettings", "FlushSound", false);
            AutoFlushInterval = ini.GetInteger("AppSettings", "FlushInterval", 15);
            AutoFlushUnit = ini.ReadValue("AppSettings", "FlushUnit", "Minutes");
            CPUHighPriority = ini.GetBoolean("AppSettings", "CPUHighPriority", false);

        }

        private static string NormalizeGamePath(string gamePath)
        {
            if (string.IsNullOrWhiteSpace(gamePath) || gamePath.Equals("Error", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            string normalizedPath = gamePath.Trim().TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            if (string.Equals(Path.GetFileName(normalizedPath), "cfg", StringComparison.OrdinalIgnoreCase))
            {
                normalizedPath = Path.GetDirectoryName(normalizedPath) ?? normalizedPath;
            }

            if (string.Equals(Path.GetFileName(normalizedPath), "Rust Optimizer", StringComparison.OrdinalIgnoreCase))
            {
                normalizedPath = Path.GetDirectoryName(normalizedPath) ?? normalizedPath;
            }

            return normalizedPath;
        }
    }
}
