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
        private const string VersionUrl = "https://rustoptimizer.voidtech.xyz/download/official/version.txt";
        private const string DownloadUrl = "https://rustoptimizer.voidtech.xyz/downloads";

        public static async Task CheckForUpdates()
        {
            try
            {
                Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    client.DefaultRequestHeaders.Add("User-Agent", "RustOptimizer-UpdateClient");
                    string fetchedVersionStr = await client.GetStringAsync(VersionUrl);
                    fetchedVersionStr = fetchedVersionStr.Trim();

                    if (Version.TryParse(fetchedVersionStr, out Version onlineVersion))
                    {
                        if (onlineVersion > currentVersion)
                        {
                            DialogResult result = MessageBox.Show(
                                $"A new update for Rust Optimizer is available!\n\nCurrent Version: {currentVersion}\nLatest Version: {onlineVersion}\n\nWould you like to open the website to download the latest version?",
                                "Update Available",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information
                            );

                            if (result == DialogResult.OK)
                            {
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = DownloadUrl,
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
