using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustOptimizer.Helpers
{
    public static class HardwareDetector
    {
        /// <summary>
        /// Grabs the name of the CPU from your system info.
        /// </summary>
        public static string GetCpuName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                return obj["Name"].ToString().Trim();
            }
            return "N/A";
        }
        /// <summary>
        /// Gets the CPU's clock speed in GHz.
        /// </summary>
        public static double GetCpuSpeedInGhz()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                if (obj["MaxClockSpeed"] != null)
                {
                    double mhz = Convert.ToDouble(obj["MaxClockSpeed"]);
                    return Math.Round(mhz / 1000.0, 2);
                }
            }
            return 0.0;
        }
        /// <summary>
        /// Gets the name of the graphics card (GPU) and cleans it up a bit.
        /// </summary>
        public static string GetGpuName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject obj in searcher.Get())
            {
                // This cleans up the GPU name from extra info
                string name = obj["Name"].ToString();
                return Regex.Replace(name, @"\(.*\)", "").Trim();
            }
            return "N/A";
        }
        /// <summary>
        /// Gets the total amount of RAM you have installed, in gigabytes.
        /// </summary>
        public static double GetTotalMemoryInGB()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                ulong totalMemoryKB = (ulong)obj["TotalVisibleMemorySize"];
                return Math.Round(totalMemoryKB / 1024.0 / 1024.0, 1);
            }
            return 0;
        }
        /// <summary>
        /// Gets more detailed info about the RAM, like the manufacturer and type (DDR4, DDR5, etc.).
        /// </summary>
        public static string GetRamInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                string manufacturer = obj["Manufacturer"]?.ToString().Trim() ?? "Unknown";
                string partNumber = obj["PartNumber"]?.ToString().Trim() ?? "";

                // First, try to infer the RAM type directly from the part number
                string ramTypeFromPartNumber = GetRamTypeFromPartNumber(partNumber);
                if (!string.IsNullOrEmpty(ramTypeFromPartNumber))
                {
                    return $"{manufacturer} {ramTypeFromPartNumber} {partNumber}";
                }

                // If part number parsing fails, fall back to SMBIOSMemoryType
                if (obj["SMBIOSMemoryType"] != null && Convert.ToInt32(obj["SMBIOSMemoryType"]) != 0)
                {
                    int smbiosMemoryType = Convert.ToInt32(obj["SMBIOSMemoryType"]);
                    return $"{manufacturer} {GetSMBIOSRamTypeString(smbiosMemoryType)} {partNumber}";
                }

                // If SMBIOSMemoryType is unavailable, fall back to MemoryType
                if (obj["MemoryType"] != null)
                {
                    int memoryType = Convert.ToInt32(obj["MemoryType"]);
                    return $"{manufacturer} {GetWin32RamTypeString(memoryType)} {partNumber}";
                }

                return $"{manufacturer} Unknown {partNumber}";
            }
            return "N/A";
        }
        /// <summary>
        /// Helper method to parse the part number.
        /// </summary>
        private static string GetRamTypeFromPartNumber(string partNumber)
        {
            if (partNumber.Contains("DDR5", StringComparison.OrdinalIgnoreCase)) return "DDR5";
            if (partNumber.Contains("DDR4", StringComparison.OrdinalIgnoreCase) || partNumber.Contains("X4M2", StringComparison.OrdinalIgnoreCase)) return "DDR4";
            if (partNumber.Contains("DDR3", StringComparison.OrdinalIgnoreCase)) return "DDR3";
            if (partNumber.Contains("DDR2", StringComparison.OrdinalIgnoreCase)) return "DDR2";
            if (partNumber.Contains("DDR", StringComparison.OrdinalIgnoreCase)) return "DDR";
            return null;
        }
        /// <summary>
        /// This is a helper that translates SMBIOS memory codes into something readable.
        /// </summary>
        private static string GetSMBIOSRamTypeString(int smbiosMemoryType)
        {
            switch (smbiosMemoryType)
            {
                case 24: return "DDR3";
                case 25: return "DDR4";
                case 26: return "LPDDR3";
                case 27: return "LPDDR4";
                case 28: return "LPDDR5";
                case 29: return "HBM";
                case 30: return "HBM2";
                case 33: return "DDR5";
                default: return "Unknown";
            }
        }
        /// <summary>
        /// A helper to translate Win32 memory codes into a readable format.
        /// </summary>
        private static string GetWin32RamTypeString(int memoryType)
        {
            switch (memoryType)
            {
                case 1: return "Other";
                case 2: return "Unknown";
                case 3: return "DRAM";
                case 4: return "EDRAM";
                case 5: return "VRAM";
                case 6: return "SRAM";
                case 7: return "RAM";
                case 8: return "ROM";
                case 9: return "Flash";
                case 10: return "EEPROM";
                case 11: return "FEPROM";
                case 12: return "EPROM";
                case 13: return "CDRAM";
                case 14: return "3DRAM";
                case 15: return "SDRAM";
                case 16: return "SGRAM";
                case 17: return "RDRAM";
                case 18: return "DDR";
                case 19: return "DDR2";
                case 20: return "DDR2 FB-DIMM";
                case 21: return "DDR3";
                case 22: return "FBD2";
                case 23: return "DDR4";
                case 24: return "LPDDR";
                case 25: return "LPDDR2";
                case 26: return "LPDDR3";
                case 27: return "LPDDR4";
                case 28: return "LPDDR5";
                case 29: return "HBM";
                case 30: return "HBM2";
                case 31: return "DDR5";
                default: return "Unknown";
            }
        }
        /// <summary>
        /// Tries to get the amount of VRAM (video memory) from the Windows Registry.
        /// </summary>
        public static double GetGpuVramInGB()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"))
                {
                    if (key != null)
                    {
                        object o = key.GetValue("HardwareInformation.qwMemorySize");
                        if (o != null)
                        {
                            long vramBytes = (long)o;
                            return Math.Round(vramBytes / (1024.0 * 1024.0 * 1024.0), 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting GPU VRAM: {ex.Message}");
            }
            return 0;
        }
    }
}
