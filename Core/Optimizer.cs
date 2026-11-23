using Microsoft.Win32;
using RustOptimizer.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RustOptimizer.Core
{
    public sealed class PingTestProgress
    {
        public int Completed { get; init; }
        public int Total { get; init; }
        public double AvgPingMs { get; init; }
        public double MinPingMs { get; init; }
        public double MaxPingMs { get; init; }
        public double JitterMs { get; init; }
        public double PacketLossPercent { get; init; }
        public int Sent { get; init; }
        public int Lost { get; init; }
        public string ProbeMode { get; init; } = "ICMP";
    }

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

        [DllImport("ntdll.dll")]
        private static extern int NtQuerySystemInformation(int SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength, out int ReturnLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetLogicalProcessorInformation(IntPtr Buffer, ref uint ReturnLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("powrprof.dll")]
        private static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, out IntPtr ActivePolicyGuid);

        [DllImport("powrprof.dll")]
        private static extern uint PowerSetActiveScheme(IntPtr UserRootPowerKey, ref Guid SchemeGuid);

        private const int RelationProcessorCore = 0;
        private const int SystemMemoryListInformation = 80;

        private static readonly Guid HighPerformancePlanGuid = new Guid("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");
        private static readonly Guid UltimatePerformancePlanGuid = new Guid("e9a42b02-d5df-448d-aa00-03f14749eb61");

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

        [StructLayout(LayoutKind.Sequential)]
        private struct MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_MEMORY_LIST_INFORMATION
        {
            public UIntPtr ZeroPageCount;
            public UIntPtr FreePageCount;
            public UIntPtr ModifiedPageCount;
            public UIntPtr ModifiedNoWritePageCount;
            public UIntPtr BadPageCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public UIntPtr[] PageCountByPriority;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public UIntPtr[] RepurposedPagesByPriority;
            public UIntPtr ModifiedPageCountPageFile;
        }

        private static System.Timers.Timer? ProcessWatchdog;
        private static readonly object WatchdogLock = new object();

        private static bool WantsHighPriority = false;
        private static bool WantsPCoresOnly = false;
        private static bool WantsAutoPowerPlan = false;
        private static bool WantsThrottleBackgroundApps = false;

        private static long CachedPhysicalCoreMask = 0;
        private static Guid? PreviousPowerPlanGuid = null;
        private static DateTime LastSmartFlushUtc = DateTime.MinValue;

        private static readonly Dictionary<int, ProcessPriorityClass> ThrottledProcesses = new Dictionary<int, ProcessPriorityClass>();
        // TODO: Add a more comprehensive list, or allow users to supply a list of apps to throttle.
        private static readonly string[] BackgroundAppsToThrottle =
        {
            "chrome", "msedge", "firefox", "brave", "opera", "opera_gx",
            "steamwebhelper", "epicgameslauncher"
        };

        public static Dictionary<string, string> GetOptimalSettings(string profile)
        {
            var settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            double totalRamGb = HardwareDetector.GetTotalMemoryInGB();
            string gcBuffer = totalRamGb >= 24.0 ? "4096" : (totalRamGb >= 12.0 ? "2048" : "1024");

            // Universal baseline settings
            settings["graphics.vsync"] = "False";
            settings["client.headbob"] = "False";
            settings["effects.sharpen"] = "False";
            settings["effects.motionblur"] = "False";
            settings["effects.antialiasing"] = "0";
            settings["effects.maxgibs"] = "0";
            settings["global.showblood"] = "True";
            settings["gc.buffer"] = gcBuffer;
            settings["gc.incremental_milliseconds"] = "1";
            settings["client.clampscreenshake"] = "True";
            settings["effects.showoutlines"] = "False";
            settings["graphics.impostorshadows"] = "False";
            settings["console.erroroverlay"] = "False";
            settings["accessibility.treemarkercolor"] = "0";
            settings["audio.musicvolume"] = "0";
            settings["audio.musicvolumemenu"] = "0";
            settings["graphics.screenmode"] = UserConfigs.ScreenMode.ToString();
            settings["fps.limit"] = UserConfigs.FpsLimit.ToString();
            settings["fps.limitinmenu"] = UserConfigs.FpsLimitInMenu.ToString();

            string gpuName = HardwareDetector.GetGpuName();
            if (IsNvidiaGpu(gpuName))
            {
                settings["graphics.reflex"] = "2";
            }

            switch (profile)
            {
                case "Competitive (Max FPS)":
                    settings["graphics.drawdistance"] = "500";
                    settings["graphics.lodbias"] = "1";
                    settings["graphics.af"] = "1";
                    settings["graphics.shadowquality"] = "0";
                    settings["graphicssettings.shadowcascades"] = "0";
                    settings["tree.meshes"] = "0";
                    settings["effects.ao"] = "False";
                    settings["graphics.dof"] = "False";
                    settings["effects.bloom"] = "False";
                    settings["effects.shafts"] = "False";
                    settings["effects.vignet"] = "False";
                    settings["grass.quality"] = "0";
                    settings["particle.quality"] = "0";
                    settings["sss.enabled"] = "False";
                    settings["water.quality"] = "0";
                    settings["water.reflections"] = "0";
                    break;

                case "Balanced (Good Performance & Looks)":
                    settings["graphics.drawdistance"] = "1000";
                    settings["graphics.lodbias"] = "1.5";
                    settings["graphics.af"] = "2";
                    settings["graphics.shadowquality"] = "1";
                    settings["graphicssettings.shadowcascades"] = "1";
                    settings["tree.meshes"] = "30";
                    settings["effects.ao"] = "True";
                    settings["graphics.dof"] = "False";
                    settings["effects.bloom"] = "True";
                    settings["effects.shafts"] = "True";
                    settings["effects.vignet"] = "False";
                    settings["grass.quality"] = "25";
                    settings["particle.quality"] = "25";
                    settings["sss.enabled"] = "True";
                    settings["water.quality"] = "1";
                    settings["water.reflections"] = "1";
                    break;

                case "Recommended (Optimized)":
                    settings["graphics.drawdistance"] = "1500";
                    settings["graphics.lodbias"] = "2.0";
                    settings["graphics.af"] = "4";
                    settings["graphics.shadowquality"] = "1";
                    settings["graphicssettings.shadowcascades"] = "1";
                    settings["tree.meshes"] = "50";
                    settings["effects.ao"] = "True";
                    settings["graphics.dof"] = "False";
                    settings["effects.bloom"] = "True";
                    settings["effects.shafts"] = "True";
                    settings["effects.vignet"] = "False";
                    settings["grass.quality"] = "50";
                    settings["particle.quality"] = "50";
                    settings["sss.enabled"] = "True";
                    settings["water.quality"] = "1";
                    settings["water.reflections"] = "1";
                    break;

                case "Ultra (Maximum Visuals)":
                    //  Maximum visual fidelity
                    settings["graphics.drawdistance"] = "2500";
                    settings["graphics.lodbias"] = "5";
                    settings["graphics.af"] = "16";
                    settings["graphics.shadowquality"] = "3";
                    settings["graphicssettings.shadowcascades"] = "2";
                    settings["graphics.impostorshadows"] = "True";
                    settings["effects.antialiasing"] = "2";
                    settings["tree.meshes"] = "100";
                    settings["effects.ao"] = "True";
                    settings["graphics.dof"] = "False";
                    settings["effects.bloom"] = "True";
                    settings["effects.shafts"] = "True";
                    settings["effects.vignet"] = "False";
                    settings["grass.quality"] = "100";
                    settings["particle.quality"] = "100";
                    settings["sss.enabled"] = "True";
                    settings["water.quality"] = "2";
                    settings["water.reflections"] = "2";
                    break;

                default:
                    goto case "Recommended (Optimized)";
            }

            return settings;
        }

        public static bool IsNvidiaGpu(string gpuName)
        {
            if (string.IsNullOrWhiteSpace(gpuName)) return false;
            return gpuName.Contains("NVIDIA", StringComparison.OrdinalIgnoreCase)
                || gpuName.Contains("GeForce", StringComparison.OrdinalIgnoreCase)
                || gpuName.Contains("RTX", StringComparison.OrdinalIgnoreCase)
                || gpuName.Contains("GTX", StringComparison.OrdinalIgnoreCase);
        }

        public static (int DeletedFiles, double FreedMB, int SkippedFiles) ClearShaderCaches()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string[] cacheDirs =
            {
                Path.Combine(localAppData, "D3DSCache"),
                Path.Combine(localAppData, "NVIDIA", "DXCache"),
                Path.Combine(localAppData, "NVIDIA", "GLCache"),
                Path.Combine(localAppData, "AMD", "DxCache"),
                Path.Combine(localAppData, "AMD", "GLCache")
            };

            long freedBytes = 0;
            int deletedFiles = 0;
            int skippedFiles = 0;

            foreach (string dirPath in cacheDirs)
            {
                if (!Directory.Exists(dirPath)) continue;

                DirectoryInfo di = new DirectoryInfo(dirPath);
                foreach (FileInfo file in di.EnumerateFiles("*", SearchOption.AllDirectories))
                {
                    try
                    {
                        long len = file.Length;
                        file.Delete();
                        freedBytes += len;
                        deletedFiles++;
                    }
                    catch (IOException)
                    {
                        skippedFiles++;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        skippedFiles++;
                    }
                }
            }

            return (deletedFiles, freedBytes / 1024.0 / 1024.0, skippedFiles);
        }

        private static void PlayFlushSound()
        {
            try
            {
                if (!UserConfigs.AutoFlushSfx) return;

                string tempSoundPath = Path.Combine(Path.GetTempPath(), "RustOptimizerTemp", "RustOptimizer_flush.mp3");

                if (!File.Exists(tempSoundPath))
                {
                    string targetDirectory = Path.GetDirectoryName(tempSoundPath);
                    if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);
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
                ExceptionHandler.LogError(ex);
            }
        }

        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static bool TryGetMemoryStats(out double freeGb, out double standbyGb, out double availPhysGb)
        {
            freeGb = 0;
            standbyGb = 0;
            availPhysGb = 0;

            var memStatus = new MEMORYSTATUSEX { dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>() };
            if (!GlobalMemoryStatusEx(ref memStatus))
                return false;

            availPhysGb = memStatus.ullAvailPhys / (1024.0 * 1024.0 * 1024.0);

            int structSize = Marshal.SizeOf<SYSTEM_MEMORY_LIST_INFORMATION>();
            IntPtr buffer = Marshal.AllocHGlobal(structSize);
            try
            {
                if (NtQuerySystemInformation(SystemMemoryListInformation, buffer, structSize, out _) == 0)
                {
                    var listInfo = Marshal.PtrToStructure<SYSTEM_MEMORY_LIST_INFORMATION>(buffer);
                    double pageSize = Environment.SystemPageSize;
                    ulong freePages = listInfo.ZeroPageCount.ToUInt64() + listInfo.FreePageCount.ToUInt64();
                    ulong standbyPages = 0;
                    if (listInfo.PageCountByPriority != null)
                    {
                        for (int i = 0; i < listInfo.PageCountByPriority.Length; i++)
                            standbyPages += listInfo.PageCountByPriority[i].ToUInt64();
                    }

                    freeGb = (freePages * pageSize) / (1024.0 * 1024.0 * 1024.0);
                    standbyGb = (standbyPages * pageSize) / (1024.0 * 1024.0 * 1024.0);
                    return true;
                }
            }
            catch { }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            freeGb = availPhysGb;
            standbyGb = 0;
            return true;
        }

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
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        UserConfigs.AutoFlushEnabled = true;
                        MainFrm.Instance.autoFlushChk.Checked = true;
                        RestartAsAdministrator();
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
                try
                {
                    Marshal.WriteInt32(pCommand, command);
                    NtSetSystemInformation(SystemMemoryListInformation, pCommand, Marshal.SizeOf(command));
                }
                finally
                {
                    Marshal.FreeHGlobal(pCommand);
                }

                PlayFlushSound();

                if (!isAutoFlush)
                {
                    MessageBox.Show("RAM standby list flush complete!", "Rust Optimizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
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

            if (!UserConfigs.AutoFlushEnabled)
                return;

            int intervalMs;
            if (UserConfigs.SmartFlushEnabled)
            {
                intervalMs = 30 * 1000;
            }
            else if (UserConfigs.AutoFlushInterval > 0)
            {
                intervalMs = UserConfigs.AutoFlushUnit?.Equals("Hours", StringComparison.OrdinalIgnoreCase) == true ? UserConfigs.AutoFlushInterval * 60 * 60 * 1000  : UserConfigs.AutoFlushInterval * 60 * 1000;
            }
            else
            {
                return;
            }

            MainFrm.Instance.autoFlushTimer = new System.Windows.Forms.Timer { Interval = intervalMs };
            MainFrm.Instance.autoFlushTimer.Tick += AutoFlushTimer_Tick;
            MainFrm.Instance.autoFlushTimer.Start();
        }

        private static void AutoFlushTimer_Tick(object sender, EventArgs e)
        {
            if (!UserConfigs.SmartFlushEnabled)
            {
                FlushStandbyList(isAutoFlush: true);
                return;
            }

            if ((DateTime.UtcNow - LastSmartFlushUtc).TotalSeconds < 60)
                return;

            if (TryGetMemoryStats(out double freeGb, out double standbyGb, out double availPhysGb))
            {
                double thresholdGb = Math.Max(1, UserConfigs.SmartFlushThresholdGB);
                bool lowFreeWithHighStandby = freeGb < thresholdGb && standbyGb >= 0.5;
                bool lowAvailFallback = standbyGb == 0 && availPhysGb < thresholdGb;

                if (lowFreeWithHighStandby || lowAvailFallback)
                {
                    LastSmartFlushUtc = DateTime.UtcNow;
                    FlushStandbyList(isAutoFlush: true);
                }
            }
        }

        public static void RestartAsAdministrator()
        {
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
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("Administrator elevation was canceled.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

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

            return 0;
        }

        public static void SetPriority(bool high)
        {
            WantsHighPriority = high;
            ApplyIfeoPriority(high);
            EnsureWatchdogState();
            TriggerImmediateCheck();
        }

        public static void SetCpuAffinity(bool physicalOnly)
        {
            WantsPCoresOnly = physicalOnly;
            EnsureWatchdogState();
            TriggerImmediateCheck();
        }

        public static void SetAutoPowerPlan(bool enabled)
        {
            WantsAutoPowerPlan = enabled;
            if (!enabled) RestorePreviousPowerPlan();
            EnsureWatchdogState();
            TriggerImmediateCheck();
        }

        public static void SetBackgroundAppThrottling(bool enabled)
        {
            WantsThrottleBackgroundApps = enabled;
            if (!enabled) RestoreThrottledProcesses();
            EnsureWatchdogState();
            TriggerImmediateCheck();
        }

        public static void ApplyIfeoPriority(bool high)
        {
            if (!IsAdministrator()) return;

            const string perfKeyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\RustClient.exe\PerfOptions";
            try
            {
                if (high)
                {
                    using RegistryKey key = Registry.LocalMachine.CreateSubKey(perfKeyPath, true);
                    key?.SetValue("CpuPriorityClass", 3, RegistryValueKind.DWord);
                }
                else
                {
                    using RegistryKey key = Registry.LocalMachine.OpenSubKey(perfKeyPath, true);
                    key?.DeleteValue("CpuPriorityClass", false);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }
        }

        private static void EnsureWatchdogState()
        {
            bool needsMonitoring = WantsHighPriority || WantsPCoresOnly || WantsAutoPowerPlan || WantsThrottleBackgroundApps;

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

        private static void TriggerImmediateCheck() => Task.Run(ProcessWatchdog_Tick);

        private static void ProcessWatchdog_Tick()
        {
            lock (WatchdogLock)
            {
                try
                {
                    Process[] rustProcesses = Process.GetProcessesByName("RustClient");
                    bool rustRunning = rustProcesses.Length > 0;

                    if (rustRunning)
                    {
                        if (WantsAutoPowerPlan) ActivateGamingPowerPlan();
                        if (WantsThrottleBackgroundApps) ThrottleBackgroundProcesses();

                        foreach (Process p in rustProcesses)
                        {
                            try
                            {
                                ApplyProcessPriority(p);
                                ApplyProcessAffinity(p);
                            }
                            catch (System.ComponentModel.Win32Exception) { /* EAC blocks handle edits after init */ }
                            catch (InvalidOperationException) { }
                            finally
                            {
                                p.Dispose();
                            }
                        }
                    }
                    else
                    {
                        RestorePreviousPowerPlan();
                        RestoreThrottledProcesses();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogError(ex);
                }
            }
        }

        private static void ApplyProcessPriority(Process p)
        {
            ProcessPriorityClass targetPriority = WantsHighPriority
                ? ProcessPriorityClass.High
                : ProcessPriorityClass.Normal;

            if (p.PriorityClass != targetPriority)
                p.PriorityClass = targetPriority;
        }

        private static void ApplyProcessAffinity(Process p)
        {
            long targetMask = WantsPCoresOnly ? GetPhysicalCoreMask() : 0;
            if (targetMask == 0)
                targetMask = Environment.ProcessorCount >= 64 ? -1L : (1L << Environment.ProcessorCount) - 1;

            if ((long)p.ProcessorAffinity != targetMask)
                p.ProcessorAffinity = (IntPtr)targetMask;
        }

        private static void ActivateGamingPowerPlan()
        {
            if (PreviousPowerPlanGuid.HasValue) return;

            try
            {
                if (PowerGetActiveScheme(IntPtr.Zero, out IntPtr activeGuidPtr) == 0 && activeGuidPtr != IntPtr.Zero)
                {
                    Guid current = Marshal.PtrToStructure<Guid>(activeGuidPtr);
                    LocalFree(activeGuidPtr);

                    if (current == UltimatePerformancePlanGuid || current == HighPerformancePlanGuid)
                        return;

                    Guid target = UltimatePerformancePlanGuid;
                    if (PowerSetActiveScheme(IntPtr.Zero, ref target) != 0)
                    {
                        target = HighPerformancePlanGuid;
                        if (PowerSetActiveScheme(IntPtr.Zero, ref target) != 0)
                            return;
                    }

                    PreviousPowerPlanGuid = current;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }
        }

        private static void RestorePreviousPowerPlan()
        {
            if (!PreviousPowerPlanGuid.HasValue) return;

            try
            {
                Guid restore = PreviousPowerPlanGuid.Value;
                PowerSetActiveScheme(IntPtr.Zero, ref restore);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }
            finally
            {
                PreviousPowerPlanGuid = null;
            }
        }

        private static void ThrottleBackgroundProcesses()
        {
            foreach (string procName in BackgroundAppsToThrottle)
            {
                Process[] procs = Process.GetProcessesByName(procName);
                foreach (Process p in procs)
                {
                    try
                    {
                        if (!ThrottledProcesses.ContainsKey(p.Id))
                        {
                            ProcessPriorityClass current = p.PriorityClass;
                            if (current == ProcessPriorityClass.Normal || current == ProcessPriorityClass.AboveNormal)
                            {
                                ThrottledProcesses[p.Id] = current;
                                p.PriorityClass = ProcessPriorityClass.BelowNormal;
                            }
                        }
                    }
                    catch { }
                    finally { p.Dispose(); }
                }
            }
        }

        private static void RestoreThrottledProcesses()
        {
            if (ThrottledProcesses.Count == 0) return;

            foreach (var kvp in ThrottledProcesses.ToList())
            {
                try
                {
                    using Process p = Process.GetProcessById(kvp.Key);
                    if (!p.HasExited && p.PriorityClass == ProcessPriorityClass.BelowNormal)
                    {
                        p.PriorityClass = kvp.Value;
                    }
                }
                catch { }
            }

            ThrottledProcesses.Clear();
        }

        public static void RestoreAllRuntimeStates()
        {
            lock (WatchdogLock)
            {
                RestorePreviousPowerPlan();
                RestoreThrottledProcesses();
            }
        }

        public static void ApplyFSOState(bool disableFSO, string gamePath)
        {
            if (Environment.OSVersion.Version.Major >= 10)
            {
                string keyPath = @"System\GameConfigStore";
                try
                {
                    using RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true);
                    if (key != null)
                    {
                        int targetValue = disableFSO ? 2 : 0;
                        key.SetValue("GameDVR_FSEBehaviorMode", targetValue, RegistryValueKind.DWord);
                        key.SetValue("GameDVR_HonorUserFSEBehaviorMode", disableFSO ? 1 : 0, RegistryValueKind.DWord);
                        key.SetValue("GameDVR_FSEBehavior", targetValue, RegistryValueKind.DWord);
                        key.SetValue("GameDVR_DXGIHonorFSEWindowsCompatible", disableFSO ? 1 : 0, RegistryValueKind.DWord);
                    }
                }
                catch (Exception ex) { ExceptionHandler.LogError(ex); }
            }
            else
            {
                if (string.IsNullOrEmpty(gamePath) || !Directory.Exists(gamePath)) return;

                string rustClientExe = Path.Combine(gamePath, "RustClient.exe");
                string layersPath = @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";

                try
                {
                    using RegistryKey key = Registry.CurrentUser.OpenSubKey(layersPath, true) ?? Registry.CurrentUser.CreateSubKey(layersPath);
                    if (key != null)
                    {
                        const string fsoFlag = "DISABLEDXMAXIMIZEDWINDOWEDMODE";
                        string currentDb = (key.GetValue(rustClientExe) as string) ?? string.Empty;
                        var flags = currentDb.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        if (disableFSO)
                        {
                            if (!flags.Contains("~")) flags.Insert(0, "~");
                            if (!flags.Contains(fsoFlag)) flags.Add(fsoFlag);
                        }
                        else
                        {
                            flags.Remove(fsoFlag);
                            if (flags.Count == 1 && flags[0] == "~") flags.Clear();
                        }

                        if (flags.Count > 0) key.SetValue(rustClientExe, string.Join(" ", flags));
                        else key.DeleteValue(rustClientExe, false);
                    }
                }
                catch (Exception ex) { ExceptionHandler.LogError(ex); }
            }
        }

        public static void ApplyGpuPreference(bool highPerformance, string gamePath)
        {
            if (string.IsNullOrWhiteSpace(gamePath) || !Directory.Exists(gamePath)) return;

            string rustClientExe = Path.Combine(gamePath, "RustClient.exe");
            const string gpuPrefPath = @"Software\Microsoft\DirectX\UserGpuPreferences";

            try
            {
                using RegistryKey key = Registry.CurrentUser.CreateSubKey(gpuPrefPath, true);
                if (key == null) return;

                if (highPerformance)
                    key.SetValue(rustClientExe, "GpuPreference=2;", RegistryValueKind.String);
                else
                    key.DeleteValue(rustClientExe, false);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }
        }

        public static void ApplyGameModeAndDvr(bool optimize)
        {
            try
            {
                using (RegistryKey gameBarKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\GameBar", true))
                {
                    if (optimize)
                        gameBarKey?.SetValue("AutoGameModeEnabled", 1, RegistryValueKind.DWord);
                }

                using (RegistryKey dvrKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\GameDVR", true))
                {
                    dvrKey?.SetValue("AppCaptureEnabled", optimize ? 0 : 1, RegistryValueKind.DWord);
                }

                using (RegistryKey storeKey = Registry.CurrentUser.CreateSubKey(@"System\GameConfigStore", true))
                {
                    storeKey?.SetValue("GameDVR_Enabled", optimize ? 0 : 1, RegistryValueKind.DWord);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }
        }

        public static int ApplyNicPowerTweaks(bool disablePowerSaving)
        {
            if (!IsAdministrator())
                throw new UnauthorizedAccessException("Administrator privileges are required to modify network adapter power settings.");

            var activeEthernetIds = NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                           && nic.OperationalStatus == OperationalStatus.Up)
                .Select(nic => nic.Id)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (activeEthernetIds.Count == 0) return 0;

            const string nicClassPath = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}";
            string[] eeeValueNames =
            {
                "*EEE", "EEE", "EEELinkAdvertisement", "EnableGreenEthernet",
                "PowerSavingMode", "AdvancedEEE", "GigaLite", "ULPMode"
            };

            int updatedAdapters = 0;

            using RegistryKey classKey = Registry.LocalMachine.OpenSubKey(nicClassPath, true);
            if (classKey == null) return 0;

            foreach (string subKeyName in classKey.GetSubKeyNames())
            {
                if (!int.TryParse(subKeyName, out _)) continue;

                using RegistryKey adapterKey = classKey.OpenSubKey(subKeyName, true);
                if (adapterKey == null) continue;

                string instanceId = adapterKey.GetValue("NetCfgInstanceId") as string;
                if (string.IsNullOrEmpty(instanceId) || !activeEthernetIds.Contains(instanceId)) continue;

                adapterKey.SetValue("PnPCapabilities", disablePowerSaving ? 24 : 0, RegistryValueKind.DWord);

                foreach (string valName in eeeValueNames)
                {
                    object existing = adapterKey.GetValue(valName);
                    if (existing != null)
                    {
                        if (existing is int)
                            adapterKey.SetValue(valName, disablePowerSaving ? 0 : 1, RegistryValueKind.DWord);
                        else
                            adapterKey.SetValue(valName, disablePowerSaving ? "0" : "1", RegistryValueKind.String);
                    }
                }

                updatedAdapters++;
            }

            return updatedAdapters;
        }

        public static void ApplyQosDscpPolicy(bool enable)
        {
            if (!IsAdministrator())
                throw new UnauthorizedAccessException("Administrator privileges are required to modify Windows QoS policies.");

            const string qosRoot = @"SOFTWARE\Policies\Microsoft\Windows\QoS";
            const string policyName = "RustOptimizer_RustClient";

            if (enable)
            {
                using RegistryKey key = Registry.LocalMachine.CreateSubKey($@"{qosRoot}\{policyName}", true);
                if (key == null) return;

                key.SetValue("Version", "1.0", RegistryValueKind.String);
                key.SetValue("Application Name", "RustClient.exe", RegistryValueKind.String);
                key.SetValue("Protocol", "UDP", RegistryValueKind.String);
                key.SetValue("Local Port", "*", RegistryValueKind.String);
                key.SetValue("Local IP", "*", RegistryValueKind.String);
                key.SetValue("Local IP Prefix Length", "*", RegistryValueKind.String);
                key.SetValue("Remote Port", "*", RegistryValueKind.String);
                key.SetValue("Remote IP", "*", RegistryValueKind.String);
                key.SetValue("Remote IP Prefix Length", "*", RegistryValueKind.String);
                key.SetValue("DSCP Value", "46", RegistryValueKind.String);
                key.SetValue("Throttle Rate", "-1", RegistryValueKind.String);
            }
            else
            {
                using RegistryKey rootKey = Registry.LocalMachine.OpenSubKey(qosRoot, true);
                rootKey?.DeleteSubKeyTree(policyName, false);
            }
        }

        public static bool FlushDnsCache(out string output)
        {
            output = string.Empty;
            try
            {
                using Process proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ipconfig",
                        Arguments = "/flushdns",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                output = proc.StandardOutput.ReadToEnd().Trim();
                proc.WaitForExit(5000);
                return proc.ExitCode == 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
                output = ex.Message;
                return false;
            }
        }

        private static readonly byte[] A2SInfoPayload =
{
    0xFF, 0xFF, 0xFF, 0xFF, 0x54,
    (byte)'S', (byte)'o', (byte)'u', (byte)'r', (byte)'c', (byte)'e', (byte)' ',
    (byte)'E', (byte)'n', (byte)'g', (byte)'i', (byte)'n', (byte)'e', (byte)' ',
    (byte)'Q', (byte)'u', (byte)'e', (byte)'r', (byte)'y', 0x00
};

        private static (string Host, int? Port) ParseHost(string rawInput)
        {
            string cleaned = rawInput.Trim();
            if (cleaned.StartsWith("client.connect ", StringComparison.OrdinalIgnoreCase))
                cleaned = cleaned.Substring(15).Trim();
            else if (cleaned.StartsWith("connect ", StringComparison.OrdinalIgnoreCase))
                cleaned = cleaned.Substring(8).Trim();

            int colonIdx = cleaned.LastIndexOf(':');
            if (colonIdx > 0 && int.TryParse(cleaned.Substring(colonIdx + 1), out int parsedPort) && parsedPort is >= 1 and <= 65535)
            {
                return (cleaned.Substring(0, colonIdx).Trim(), parsedPort);
            }

            return (cleaned, null);
        }

        private static async Task<double?> SendProbe(IPAddress ip, int port, int timeoutMs, CancellationToken ct)
        {
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            timeoutCts.CancelAfter(timeoutMs);

            try
            {
                using var udp = new UdpClient(ip.AddressFamily);
                udp.Connect(ip, port);

                var sw = Stopwatch.StartNew();
                await udp.SendAsync(A2SInfoPayload, A2SInfoPayload.Length);

                UdpReceiveResult res = await udp.ReceiveAsync(timeoutCts.Token);
                sw.Stop();

                if (res.Buffer.Length >= 5 &&
                    res.Buffer[0] == 0xFF && res.Buffer[1] == 0xFF &&
                    res.Buffer[2] == 0xFF && res.Buffer[3] == 0xFF &&
                    (res.Buffer[4] == 0x49 || res.Buffer[4] == 0x41 || res.Buffer[4] == 0x6D))
                {
                    return sw.Elapsed.TotalMilliseconds;
                }
            }
            catch
            {
                // Timeout, cancellation, or port unreachable
            }

            return null;
        }

        private static async Task<int?> DiscoverPort(IPAddress ip, int? specifiedPort, CancellationToken ct)
        {
            var candidates = new List<int>();
            if (specifiedPort.HasValue)
            {
                int p = specifiedPort.Value;
                candidates.Add(p);
                candidates.Add(p + 2);
                candidates.Add(p + 1);
                candidates.Add(p - 1);
            }
            else
            {
                candidates.Add(28017);
                candidates.Add(28015);
            }
            candidates.Add(27015);

            var uniquePorts = candidates.Where(p => p is >= 1 and <= 65535).Distinct().ToList();

            using var discoveryCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var tasks = uniquePorts.Select(async port =>
            {
                double? rtt = await SendProbe(ip, port, 1200, discoveryCts.Token);
                return rtt.HasValue ? (int?)port : null;
            }).ToList();

            while (tasks.Count > 0)
            {
                Task<int?> finished = await Task.WhenAny(tasks);
                tasks.Remove(finished);

                int? foundPort = await finished;
                if (foundPort.HasValue)
                {
                    discoveryCts.Cancel();
                    return foundPort.Value;
                }
            }

            return null;
        }

        public static async Task<PingTestProgress> RunPingTestAsync(
            string rawHost,
            int count,
            IProgress<PingTestProgress> progress,
            CancellationToken ct)
        {
            var (host, specifiedPort) = ParseHost(rawHost);

            IPAddress targetIp;
            if (!IPAddress.TryParse(host, out targetIp))
            {
                IPAddress[] addresses = await Dns.GetHostAddressesAsync(host, ct);
                targetIp = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
                           ?? addresses.FirstOrDefault()
                           ?? throw new Exception($"Could not resolve host '{host}'.");
            }

            int? a2sPort = null;
            if (specifiedPort.HasValue)
            {
                a2sPort = await DiscoverPort(targetIp, specifiedPort, ct);
            }
            else
            {
                bool icmpReachable = false;
                try
                {
                    using var quickPing = new Ping();
                    PingReply quickReply = await quickPing.SendPingAsync(targetIp, 800);
                    icmpReachable = quickReply.Status == IPStatus.Success;
                }
                catch { }

                if (!icmpReachable)
                    a2sPort = await DiscoverPort(targetIp, null, ct);
            }

            string mode = a2sPort.HasValue ? $"UDP A2S :{a2sPort.Value}" : "ICMP";
            var rtts = new List<double>(count);
            int sent = 0;
            int lost = 0;

            using var ping = a2sPort.HasValue ? null : new Ping();
            PingTestProgress latest = new PingTestProgress { Total = count, ProbeMode = mode };

            for (int i = 1; i <= count; i++)
            {
                ct.ThrowIfCancellationRequested();
                sent++;

                if (a2sPort.HasValue)
                {
                    double? rtt = await SendProbe(targetIp, a2sPort.Value, 2000, ct);
                    if (rtt.HasValue) rtts.Add(rtt.Value);
                    else lost++;
                }
                else
                {
                    try
                    {
                        PingReply reply = await ping.SendPingAsync(targetIp, 2000);
                        if (reply.Status == IPStatus.Success) rtts.Add(reply.RoundtripTime);
                        else lost++;
                    }
                    catch
                    {
                        lost++;
                    }
                }

                double avg = rtts.Count > 0 ? rtts.Average() : 0;
                double min = rtts.Count > 0 ? rtts.Min() : 0;
                double max = rtts.Count > 0 ? rtts.Max() : 0;
                double jitter = 0;

                if (rtts.Count >= 2)
                {
                    double diffSum = 0;
                    for (int j = 1; j < rtts.Count; j++)
                        diffSum += Math.Abs(rtts[j] - rtts[j - 1]);
                    jitter = diffSum / (rtts.Count - 1);
                }

                double lossPct = (lost * 100.0) / sent;

                latest = new PingTestProgress
                {
                    Completed = i,
                    Total = count,
                    AvgPingMs = avg,
                    MinPingMs = min,
                    MaxPingMs = max,
                    JitterMs = jitter,
                    PacketLossPercent = lossPct,
                    Sent = sent,
                    Lost = lost,
                    ProbeMode = mode
                };

                progress?.Report(latest);

                if (i < count)
                {
                    await Task.Delay(a2sPort.HasValue ? 500 : 250, ct);
                }
            }

            return latest;
        }
    }
}