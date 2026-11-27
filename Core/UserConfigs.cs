using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RustOptimizer.Core
{
    public static class UserConfigs
    {
        public static string ConfigPath { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Rust Optimizer", "User", "UserCFG.ini");
        public static string BackupsPath { get; set; } = Path.Combine(Application.StartupPath, "backups");
        public static string GamePath { get; set; } = string.Empty;
        public static string GameConfigPath { get; private set; } = Path.Combine("cfg", "client.cfg");
        public static string? SavedProfile { get; set; } = null;
        public static int ScreenMode { get; set; } = 1;
        public static int FpsLimit { get; set; } = 0;
        public static int FpsLimitInMenu { get; set; } = 60;
        public static bool AutoFlushEnabled { get; set; } = false;
        public static bool AutoFlushSfx { get; set; } = false;
        public static int AutoFlushInterval { get; set; } = 15;
        public static string AutoFlushUnit { get; set; } = "Minutes";
        public static bool SmartFlushEnabled { get; set; } = false;
        public static int SmartFlushThresholdGB { get; set; } = 2;
        public static bool CPUHighPriority { get; set; } = false;
        public static bool PCoresOnly { get; set; } = false;
        public static bool DisableFSO { get; set; } = false;
        public static bool AutoPowerPlan { get; set; } = false;
        public static bool HighPerfGpuBinding { get; set; } = false;
        public static bool GameModeAndDisableDvr { get; set; } = false;
        public static bool ThrottleBackgroundApps { get; set; } = false;
        public static bool DisableNicPowerSaving { get; set; } = false;
        public static bool QosDscpEnabled { get; set; } = false;
        public static string LastPingServer { get; set; } = "1.1.1.1";
        public static bool CheckForUpdates { get; set; } = true;

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

        public static void Refresh()
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = ConfigPath };

            BackupsPath = ini.ReadValue("AppSettings", "BackupPath");
            SetGamePath(ini.ReadValue("AppSettings", "GamePath"));
            SavedProfile = ini.ReadValue("AppSettings", "Profile");
            ScreenMode = ini.GetInteger("AppSettings", "ScreenMode", 1);
            FpsLimit = ini.GetInteger("AppSettings", "FpsLimit", 0);
            FpsLimitInMenu = ini.GetInteger("AppSettings", "FpsLimitInMenu", 60);
            AutoFlushEnabled = ini.GetBoolean("AppSettings", "AutoFlush", false);
            AutoFlushSfx = ini.GetBoolean("AppSettings", "FlushSound", false);
            AutoFlushInterval = ini.GetInteger("AppSettings", "FlushInterval", 15);
            AutoFlushUnit = ini.ReadValue("AppSettings", "FlushUnit", "Minutes");
            SmartFlushEnabled = ini.GetBoolean("AppSettings", "SmartFlush", false);
            SmartFlushThresholdGB = ini.GetInteger("AppSettings", "SmartFlushThresholdGB", 2);
            CPUHighPriority = ini.GetBoolean("AppSettings", "CPUHighPriority", false);
            PCoresOnly = ini.GetBoolean("AppSettings", "PhysicalCoresOnly", false);
            DisableFSO = ini.GetBoolean("AppSettings", "DisableFSO", false);
            AutoPowerPlan = ini.GetBoolean("AppSettings", "AutoPowerPlan", false);
            HighPerfGpuBinding = ini.GetBoolean("AppSettings", "HighPerfGpuBinding", false);
            GameModeAndDisableDvr = ini.GetBoolean("AppSettings", "GameModeAndDisableDvr", false);
            ThrottleBackgroundApps = ini.GetBoolean("AppSettings", "ThrottleBackgroundApps", false);
            DisableNicPowerSaving = ini.GetBoolean("AppSettings", "DisableNicPowerSaving", false);
            QosDscpEnabled = ini.GetBoolean("AppSettings", "QosDscpEnabled", false);
            string savedServer = ini.ReadValue("AppSettings", "LastPingServer", "1.1.1.1");
            LastPingServer = string.IsNullOrWhiteSpace(savedServer) ? "1.1.1.1" : savedServer;
            CheckForUpdates = ini.GetBoolean("AppSettings", "CheckForUpdates", true);
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

        public static bool IsValidRustDirectory(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                return false;

            return File.Exists(Path.Combine(path, "RustClient.exe"))
                || File.Exists(Path.Combine(path, "Rust.exe"));
        }

        public static string? TryAutoDetectRustPath()
        {
            try
            {
                string? steamPath = GetSteamInstallPath();
                if (!string.IsNullOrWhiteSpace(steamPath) && Directory.Exists(steamPath))
                {
                    var libraryRoots = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { steamPath };

                    string[] vdfCandidates =
                    {
                        Path.Combine(steamPath, "config", "libraryfolders.vdf"),
                        Path.Combine(steamPath, "steamapps", "libraryfolders.vdf")
                    };

                    foreach (string vdfPath in vdfCandidates)
                    {
                        if (!File.Exists(vdfPath)) continue;

                        string content = File.ReadAllText(vdfPath);
                        foreach (Match match in Regex.Matches(content, "\"path\"\\s+\"([^\"]+)\"", RegexOptions.IgnoreCase))
                        {
                            string libPath = match.Groups[1].Value.Replace(@"\\", @"\").Trim();
                            if (Directory.Exists(libPath))
                                libraryRoots.Add(libPath);
                        }
                    }

                    foreach (string libRoot in libraryRoots)
                    {
                        string steamApps = Path.Combine(libRoot, "steamapps");
                        if (!Directory.Exists(steamApps)) continue;

                        string acfPath = Path.Combine(steamApps, "appmanifest_252490.acf");
                        if (File.Exists(acfPath))
                        {
                            string acf = File.ReadAllText(acfPath);
                            Match m = Regex.Match(acf, "\"installdir\"\\s+\"([^\"]+)\"", RegexOptions.IgnoreCase);
                            if (m.Success)
                            {
                                string candidate = Path.Combine(steamApps, "common", m.Groups[1].Value.Trim());
                                if (IsValidRustDirectory(candidate))
                                    return candidate;
                            }
                        }

                        string directCandidate = Path.Combine(steamApps, "common", "Rust");
                        if (IsValidRustDirectory(directCandidate))
                            return directCandidate;
                    }
                }

                string[] commonSubPaths =
                {
                    Path.Combine("Program Files (x86)", "Steam", "steamapps", "common", "Rust"),
                    Path.Combine("Steam", "steamapps", "common", "Rust"),
                    Path.Combine("SteamLibrary", "steamapps", "common", "Rust"),
                    Path.Combine("Games", "SteamLibrary", "steamapps", "common", "Rust")
                };

                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (!drive.IsReady || drive.DriveType != DriveType.Fixed) continue;

                    foreach (string subPath in commonSubPaths)
                    {
                        string candidate = Path.Combine(drive.RootDirectory.FullName, subPath);
                        if (IsValidRustDirectory(candidate))
                            return candidate;
                    }
                }
            }
            catch
            {
                // Fail silently and browse manually
            }

            return null;
        }

        private static string? GetSteamInstallPath()
        {
            using (RegistryKey? key64 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Valve\Steam"))
            {
                if (key64?.GetValue("InstallPath") is string path64 && !string.IsNullOrWhiteSpace(path64))
                    return path64.Trim();
            }

            using (RegistryKey? key32 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Valve\Steam"))
            {
                if (key32?.GetValue("InstallPath") is string path32 && !string.IsNullOrWhiteSpace(path32))
                    return path32.Trim();
            }

            using (RegistryKey? keyUser = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam"))
            {
                if (keyUser?.GetValue("SteamPath") is string userPath && !string.IsNullOrWhiteSpace(userPath))
                    return userPath.Replace('/', '\\').Trim();
            }

            return null;
        }
    }
}