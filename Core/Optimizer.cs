using RustOptimizer.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetLogicalProcessorInformation(IntPtr Buffer, ref uint ReturnLength);

        private const int RelationProcessorCore = 0;

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION
        {
            public UIntPtr ProcessorMask;
            public int Relationship;
            public long UnionPad1;
            public long UnionPad2;
        }

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
        private static System.Timers.Timer ProcessWatchdog;
        private static bool WantsHighPriority = false;
        private static bool WantsPCoresOnly = false;
        private static long CachedPhysicalCoreMask = 0;

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
                bool playSound = UserConfigs.AutoFlushSfx;

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
                ExceptionHandler.LogError(ex);
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
            if (!IsAdministrator())
            {
                if (!isAutoFlush)
                {
                    MainFrm.Instance.autoFlushChk.Checked = false;

                    UserConfigs.AutoFlushEnabled = false;

                    DialogResult result = MessageBox.Show(
                 "The Flush RAM feature requires Rust Optimizer to be run as Administrator.\n\nWould you like to restart the application as Administrator now?",
                 "Administrator Required",
                 MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Warning
             );

                    if (result == DialogResult.OK)
                    {
                        UserConfigs.AutoFlushEnabled = true;
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
                            UserConfigs.AutoFlushEnabled= false;
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
                ExceptionHandler.LogError(ex);
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

            
            bool isAutoFlushEnabled = UserConfigs.AutoFlushEnabled;

            if (!isAutoFlushEnabled)
            {
                return;
            }

            if (UserConfigs.AutoFlushInterval > 0)
            {
                int intervalMs;

                if (UserConfigs.AutoFlushUnit?.Equals("Hours", StringComparison.OrdinalIgnoreCase) == true)
                {
                    intervalMs = UserConfigs.AutoFlushInterval * 60 * 60 * 1000;
                }
                else
                {
                    intervalMs = UserConfigs.AutoFlushInterval * 60 * 1000;
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

        /// <summary>
        /// Asks the Windows Kernel for the true CPU cores.
        /// Extracts exactly one logical thread for every physical core.
        /// </summary>
        private static long GetPhysicalCoreMask()
        {
            if (CachedPhysicalCoreMask != 0) return CachedPhysicalCoreMask;

            uint returnLength = 0;
            GetLogicalProcessorInformation(IntPtr.Zero, ref returnLength);
            if (returnLength == 0) return 0;

            IntPtr buffer = Marshal.AllocHGlobal((int)returnLength);
            try
            {
                if (GetLogicalProcessorInformation(buffer, ref returnLength))
                {
                    int size = Marshal.SizeOf(typeof(SYSTEM_LOGICAL_PROCESSOR_INFORMATION));
                    int count = (int)returnLength / size;
                    long physicalMask = 0;

                    for (int i = 0; i < count; i++)
                    {
                        IntPtr itemAddr = new IntPtr(buffer.ToInt64() + (i * size));
                        var info = (SYSTEM_LOGICAL_PROCESSOR_INFORMATION)Marshal.PtrToStructure(itemAddr, typeof(SYSTEM_LOGICAL_PROCESSOR_INFORMATION));

                        if (info.Relationship == RelationProcessorCore)
                        {
                            long coreMask = (long)info.ProcessorMask;
                            physicalMask |= (coreMask & -coreMask);
                        }
                    }
                    CachedPhysicalCoreMask = physicalMask;
                    return physicalMask;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return 0; // Fallback
        }

        /// <summary>
        /// Enables or disables high process priority monitoring for Rust.
        /// </summary>
        public static void SetPriority(bool high)
        {
            WantsHighPriority = high;
            EnsureWatchdogState();
            TriggerImmediateCheck();
        }

        /// <summary>
        /// Restricts RustClient.exe execution to physical CPU cores, bypassing SMT/Hyper-Threading.
        /// </summary>
        public static void SetCpuAffinity(bool physicalOnly)
        {
            WantsPCoresOnly = physicalOnly;
            EnsureWatchdogState();
            TriggerImmediateCheck();
        }
        /// <summary>
        /// Starts the background monitor if any optimization feature is enabled,
        /// or stops it to save background resources when all are disabled.
        /// </summary>
        private static void EnsureWatchdogState()
        {
            bool needsMonitoring = WantsHighPriority || WantsPCoresOnly;

            if (needsMonitoring)
            {
                if (ProcessWatchdog == null)
                {
                    ProcessWatchdog = new System.Timers.Timer(5000);
                    ProcessWatchdog.Elapsed += (s, e) => ProcessWatchdog_Tick();
                    ProcessWatchdog.AutoReset = true;
                    ProcessWatchdog.Start();
                }
            }
            else if (ProcessWatchdog != null)
            {
                ProcessWatchdog.Stop();
                ProcessWatchdog.Dispose();
                ProcessWatchdog = null;
            }
        }

        private static void TriggerImmediateCheck()
        {
            Task.Run(() => ProcessWatchdog_Tick());
        }

        /// <summary>
        /// Shared monitor polling loop. Iterates game instances once and dispatches to modular workers.
        /// </summary>
        private static void ProcessWatchdog_Tick()
        {
            try
            {
                Process[] rustProcesses = Process.GetProcessesByName("RustClient");
                if (rustProcesses.Length == 0) return;

                foreach (Process p in rustProcesses)
                {
                    ApplyProcessPriority(p);
                    ApplyProcessAffinity(p);
                }
            }
            catch (System.ComponentModel.Win32Exception) {  /* Catch "Access Denied" errors silently, this stops the background thread from crashing or spamming your log file. */}
            catch (InvalidOperationException) { /* Silently catch if the game closes while the loop is checking it. */ }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }
        }
        /// <summary>
        /// Applies the requested ProcessPriorityClass.
        /// </summary>
        private static void ApplyProcessPriority(Process p)
        {
            ProcessPriorityClass targetPriority = WantsHighPriority
                ? ProcessPriorityClass.High
                : ProcessPriorityClass.Normal;

            if (p.PriorityClass != targetPriority)
            {
                p.PriorityClass = targetPriority;
            }
        }

        /// <summary>
        /// Calculates and applies the processor affinity mask.
        /// </summary>
        private static void ApplyProcessAffinity(Process p)
        {
            long targetMask = 0;

            if (WantsPCoresOnly)
            {
                targetMask = GetPhysicalCoreMask();
            }

            if (targetMask == 0)
            {
                targetMask = Environment.ProcessorCount >= 64 ? -1L : (1L << Environment.ProcessorCount) - 1;
            }

            if ((long)p.ProcessorAffinity != targetMask)
            {
                p.ProcessorAffinity = (IntPtr)targetMask;
            }
        }
    }
}