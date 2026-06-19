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
        public static bool PvpGuideShortcuts { get; set; } = false;
        public static string GameConfigPath => GetGameConfigPath(GamePath);
        public static string KeysConfigPath => GetKeysConfigPath(GamePath);

        public static string GetGameConfigPath(string gamePath)
        {
            return Path.Combine(gamePath ?? string.Empty, "cfg", "client.cfg");
        }

        public static string GetKeysConfigPath(string gamePath)
        {
            return Path.Combine(gamePath ?? string.Empty, "cfg", "keys.cfg");
        }

        /// <summary>
        /// Loads, and refreshes Global Settings.
        /// It first loads the current settings, updates them with the new profile, and saves the file.
        /// </summary>
        public static void Refresh()
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = ConfigPath };

            BackupsPath = ini.ReadValue("AppSettings", "BackupPath");
            GamePath = ini.ReadValue("AppSettings", "GamePath");
            SavedProfile = ini.ReadValue("AppSettings", "Profile");
            AutoFlushEnabled = ini.GetBoolean("AppSettings", "AutoFlush", false);
            AutoFlushSfx = ini.GetBoolean("AppSettings", "FlushSound", false);
            AutoFlushInterval = ini.GetInteger("AppSettings", "FlushInterval", 15);
            AutoFlushUnit = ini.ReadValue("AppSettings", "FlushUnit", "Minutes");
            CPUHighPriority = ini.GetBoolean("AppSettings", "CPUHighPriority", false);
            PvpGuideShortcuts = ini.GetBoolean("AppSettings", "PvpGuideShortcuts", false);

        }
    }
}
