using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustOptimizer.Helpers
{
    public class Updates
    {
        private const string VersionUrl = "https://raw.githubusercontent.com/V0idpool/RustOptimizer/refs/heads/main/version.txt";
        private const string NexusModsUrl = "https://www.nexusmods.com/rust/mods/5?tab=files";

        public static async Task CheckForUpdates()
        {
            try
            {
                Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "RustOptimizer-UpdateClient");
                    string fetchedVersionStr = await client.GetStringAsync(VersionUrl);
                    fetchedVersionStr = fetchedVersionStr.Trim();

                    if (Version.TryParse(fetchedVersionStr, out Version onlineVersion))
                    {
                        if (onlineVersion > currentVersion)
                        {
                            DialogResult result = MessageBox.Show(
                                $"A new update for Rust Optimizer is available!\n\nCurrent Version: {currentVersion}\nLatest Version: {onlineVersion}\n\nWould you like to open NexusMods to download it?",
                                "Update Available",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information
                            );

                            if (result == DialogResult.OK)
                            {
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = NexusModsUrl,
                                    UseShellExecute = true
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update check failed: {ex.Message}");
                ExceptionHandler.LogError(ex);
            }
        }
    }
}
