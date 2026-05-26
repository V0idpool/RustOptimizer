using RustOptimizer.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RustOptimizer.Core
{
    public static class Optimizer
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, uint BufferLength, IntPtr PreviousState, IntPtr ReturnLength);
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, System.Text.StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        [DllImport("ntdll.dll")]
        private static extern int NtSetSystemInformation(int SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength);

        [StructLayout(LayoutKind.Sequential)]
        private struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TOKEN_PRIVILEGES
        {
            public uint PrivilegeCount;
            public LUID Luid;
            public uint Attributes;
        }

        /// <summary>
        /// This method gets the right settings for a specific profile, like Competitive or Ultra.
        /// It's the main logic for applying all the tweaks.
        /// </summary>
        public static Dictionary<string, string> GetOptimalSettings(string profile)
        {
            var settings = new Dictionary<string, string>();

            // Universal Settings for all profiles
            settings.Add("graphics.vsync", "False");
            settings.Add("client.headbob", "False");
            settings.Add("effects.sharpen", "False");
            settings.Add("effects.motionblur", "False");
            settings.Add("effects.antialiasing", "0");
            settings.Add("effects.maxgibs", "0");
            settings.Add("global.showblood", "True");
            settings.Add("gc.buffer", "4096");
            settings.Add("client.clampscreenshake", "True");
            settings.Add("effects.showoutlines", "False");
            settings.Add("graphics.impostorshadows", "False");
            settings.Add("console.erroroverlay", "False");
            settings.Add("accessibility.treemarkercolor", "0");
            settings.Add("audio.musicvolume", "0");
            settings.Add("audio.musicvolumemenu", "0");

            switch (profile)
            {
                case "Competitive (Max FPS)":
                    // Pure performance focus
                    settings.Add("graphics.drawdistance", "500");
                    settings.Add("graphics.lodbias", "1");
                    settings.Add("graphics.af", "1");
                    settings.Add("graphics.shadowquality", "0");
                    settings.Add("graphicssettings.shadowcascades", "0");
                    settings.Add("tree.meshes", "0");
                    settings.Add("effects.ao", "False");
                    settings.Add("graphics.dof", "False");
                    settings.Add("effects.bloom", "False");
                    settings.Add("effects.shafts", "False");
                    settings.Add("effects.vignet", "False");
                    settings.Add("grass.quality", "0");
                    settings.Add("particle.quality", "0");
                    settings.Add("sss.enabled", "False");
                    settings.Add("water.quality", "0");
                    settings.Add("water.reflections", "0");
                    break;

                case "Recommended (Optimized)":
                    // Balances performance with visual fidelity to reduce pop-in.
                    settings.Add("graphics.drawdistance", "1500");
                    settings.Add("graphics.lodbias", "2.0");
                    settings.Add("graphics.af", "4");
                    settings.Add("graphics.shadowquality", "1");
                    settings.Add("graphicssettings.shadowcascades", "1");
                    settings.Add("tree.meshes", "50");
                    settings.Add("effects.ao", "True");
                    settings.Add("graphics.dof", "False");
                    settings.Add("effects.bloom", "True");
                    settings.Add("effects.shafts", "True");
                    settings.Add("effects.vignet", "False");
                    settings.Add("grass.quality", "50");
                    settings.Add("particle.quality", "50");
                    settings.Add("sss.enabled", "True");
                    settings.Add("water.quality", "1");
                    settings.Add("water.reflections", "1");
                    break;

                case "Balanced (Good-looking & Fast)":
                    // A middle ground that provides a good experience on most systems.
                    settings.Add("graphics.drawdistance", "1000");
                    settings.Add("graphics.lodbias", "1.5");
                    settings.Add("graphics.af", "2");
                    settings.Add("graphics.shadowquality", "1");
                    settings.Add("graphicssettings.shadowcascades", "1");
                    settings.Add("tree.meshes", "30");
                    settings.Add("effects.ao", "True");
                    settings.Add("graphics.dof", "False");
                    settings.Add("effects.bloom", "True");
                    settings.Add("effects.shafts", "True");
                    settings.Add("effects.vignet", "False");
                    settings.Add("grass.quality", "25");
                    settings.Add("particle.quality", "25");
                    settings.Add("sss.enabled", "True");
                    settings.Add("water.quality", "1");
                    settings.Add("water.reflections", "1");
                    break;

                case "Ultra (Maximum Visuals)":
                    // High-quality settings
                    settings.Add("graphics.drawdistance", "2500");
                    settings.Add("graphics.lodbias", "5");
                    settings.Add("graphics.af", "16");
                    settings.Add("graphics.shadowquality", "0");
                    settings.Add("graphicssettings.shadowcascades", "1");
                    settings.Add("tree.meshes", "100");
                    settings.Add("effects.ao", "False");
                    settings.Add("graphics.dof", "False");
                    settings.Add("effects.bloom", "False");
                    settings.Add("effects.shafts", "True");
                    settings.Add("effects.vignet", "False");
                    settings.Add("grass.quality", "0");
                    settings.Add("particle.quality", "0");
                    settings.Add("sss.enabled", "True");
                    settings.Add("water.quality", "0");
                    settings.Add("water.reflections", "0");
                    break;

                default:
                    // Fallback to Recommended (Optimized) as the default
                    goto case "Recommended (Optimized)";
            }
            return settings;
        }
        /// <summary>
        /// Plays a Toilet Flush sound
        /// </summary>
        private static void PlayFlushSound()
        {
            try
            {
                var ini = new RustOptimizer.Helpers.inisettings { Path = MainFrm.ConfigPath };
                string soundStr = ini.ReadValue("AppSettings", "FlushSound");
                bool playSound = !string.IsNullOrEmpty(soundStr) && soundStr.Equals("True", StringComparison.OrdinalIgnoreCase);

                if (!playSound) return;

                string tempSoundPath = Path.Combine(Path.GetTempPath(), "RustOptimizerTemp", "RustOptimizer_flush.mp3");

                if (!File.Exists(tempSoundPath))
                {
                    string targetDirectory = Path.GetDirectoryName(tempSoundPath);
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }
                    EmbedResources.SaveToDisk("flush.mp3", tempSoundPath);
                }

                if (File.Exists(tempSoundPath))
                {
                    mciSendString("close ramflush", null, 0, IntPtr.Zero);
                    mciSendString($"open \"{tempSoundPath}\" type mpegvideo alias ramflush", null, 0, IntPtr.Zero);
                    mciSendString("play ramflush from 0", null, 0, IntPtr.Zero);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }
        }
        /// <summary>
        /// Checks if a user is running the app as Administrator (Vital for Flushing RAM & Such).
        /// </summary>
        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        /// <summary>
        /// Flushes the Windows Standby List to free up physical RAM. 
        /// The application MUST be running as Administrator for this to work.
        /// </summary>
        public static void FlushStandbyList(bool isAutoFlush = false)
        {
            var ini = new RustOptimizer.Helpers.inisettings { Path = MainFrm.ConfigPath };
            if (!IsAdministrator())
            {
                if (!isAutoFlush)
                {
                    MainFrm.Instance.autoFlushChk.Checked = false;

                    ini.WriteValue("AppSettings", "AutoFlush", "False", ini.Path);

                    DialogResult result = MessageBox.Show(
                 "The Flush RAM feature requires Rust Optimizer to be run as Administrator.\n\nWould you like to restart the application as Administrator now?",
                 "Administrator Required",
                 MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Warning
             );

                    if (result == DialogResult.OK)
                    {
                        ini.WriteValue("AppSettings", "AutoFlush", "True", ini.Path);
                        MainFrm.Instance.autoFlushChk.Checked = true;
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
                            ini.WriteValue("AppSettings", "AutoFlush", "False", ini.Path);
                            MainFrm.Instance.autoFlushChk.Checked = false;
                            MessageBox.Show("Restart canceled. Rust Optimizer will run in standard user mode without Auto Flush features.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }


                }
                return;
            }
            try
            {
                if (!OpenProcessToken(Process.GetCurrentProcess().Handle, 0x0020 | 0x0008, out IntPtr tokenHandle))
                    return;

                if (!LookupPrivilegeValue(null, "SeProfileSingleProcessPrivilege", out LUID luid))
                    return;

                TOKEN_PRIVILEGES tp = new TOKEN_PRIVILEGES
                {
                    PrivilegeCount = 1,
                    Luid = luid,
                    Attributes = 0x00000002
                };

                AdjustTokenPrivileges(tokenHandle, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);

                int command = 4;
                IntPtr pCommand = Marshal.AllocHGlobal(sizeof(int));
                Marshal.WriteInt32(pCommand, command);

                NtSetSystemInformation(80, pCommand, Marshal.SizeOf(command));

                Marshal.FreeHGlobal(pCommand);

                PlayFlushSound();

                if (!isAutoFlush)
                {
                    MessageBox.Show("Ram flush complete!", "RustOptimizer");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing Standby List: {ex.Message}");
            }
        }
        public static void InitializeAutoFlushTimer()
        {
            if (MainFrm.Instance.autoFlushTimer != null)
            {
                MainFrm.Instance.autoFlushTimer.Stop();
                MainFrm.Instance.autoFlushTimer.Dispose();
                MainFrm.Instance.autoFlushTimer = null;
            }

            var ini = new RustOptimizer.Helpers.inisettings { Path = MainFrm.ConfigPath };

            string autoFlushStr = ini.ReadValue("AppSettings", "AutoFlush");
            bool isAutoFlushEnabled = !string.IsNullOrEmpty(autoFlushStr) && autoFlushStr.Equals("True", StringComparison.OrdinalIgnoreCase);

            if (!isAutoFlushEnabled)
            {
                return;
            }

            string intervalStr = ini.ReadValue("AppSettings", "FlushInterval");
            string unitStr = ini.ReadValue("AppSettings", "FlushUnit");

            if (int.TryParse(intervalStr, out int intervalVal) && intervalVal > 0)
            {
                int intervalMs;

                if (unitStr?.Equals("Hours", StringComparison.OrdinalIgnoreCase) == true)
                {
                    intervalMs = intervalVal * 60 * 60 * 1000;
                }
                else
                {
                    intervalMs = intervalVal * 60 * 1000;
                }

                MainFrm.Instance.autoFlushTimer = new System.Windows.Forms.Timer();
                MainFrm.Instance.autoFlushTimer.Interval = intervalMs;
                MainFrm.Instance.autoFlushTimer.Tick += AutoFlushTimer_Tick;
                MainFrm.Instance.autoFlushTimer.Start();
            }
        }
        private static void AutoFlushTimer_Tick(object sender, EventArgs e)
        {
           FlushStandbyList(isAutoFlush: true);
        }
    }
}