using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RustOptimizer.Helpers
{
    public class Updates
    {
        public static readonly Version? currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

        private const string VersionUrl = "https://rustoptimizer.us/download/official/version.txt";
        private const string DownloadUrl = "https://rustoptimizer.us/downloads";

        private static readonly HttpClient SharedHttpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        private static readonly TimeSpan CheckCooldown = TimeSpan.FromSeconds(60);
        private static long _lastCheckTicks = 0;
        private static int _isChecking = 0;

        public static async Task CheckForUpdates()
        {
            if (Interlocked.CompareExchange(ref _isChecking, 1, 0) != 0)
                return;

            try
            {
                long nowTicks = DateTime.UtcNow.Ticks;
                long lastTicks = Interlocked.Read(ref _lastCheckTicks);

                if (lastTicks > 0 && (nowTicks - lastTicks) < CheckCooldown.Ticks)
                {
                    double remainingSeconds = Math.Ceiling((CheckCooldown.Ticks - (nowTicks - lastTicks)) / (double)TimeSpan.TicksPerSecond);
                    ExceptionHandler.LogMessage($"[UPDATE] Throttled update check ({remainingSeconds}s cooldown remaining).");
                    return;
                }

                Interlocked.Exchange(ref _lastCheckTicks, nowTicks);

                using HttpResponseMessage response = await SharedHttpClient.GetAsync(VersionUrl);

                if (response.IsSuccessStatusCode)
                {
                    string rawContent = (await response.Content.ReadAsStringAsync()).Trim();
                    if (string.IsNullOrWhiteSpace(rawContent))
                        return;

                    string[] lines = rawContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    string latestVersionTxt = lines[0].Trim().TrimStart('v', 'V');
                    string releaseNotes = lines.Length > 1
                        ? string.Join(Environment.NewLine + Environment.NewLine, lines[1..]).Trim()
                        : string.Empty;

                    if (currentVersion is not null && Version.TryParse(latestVersionTxt, out Version? onlineVersion))
                    {
                        if (onlineVersion > currentVersion)
                        {
                            ExceptionHandler.LogMessage($"[UPDATE] New version v{onlineVersion} is available. (Installed: v{currentVersion}).");
                            ShowUpdateAvailableDialog(currentVersion.ToString(), onlineVersion.ToString(), releaseNotes);
                        }
                    }
                }
                else
                {
                    ExceptionHandler.LogMessage($"Update Check: Server returned {response.StatusCode}. Continuing offline.");
                }
            }
            catch (HttpRequestException)
            {
                ExceptionHandler.LogMessage("Update Check: No internet connection detected. Continuing offline.");
            }
            catch (TaskCanceledException)
            {
                ExceptionHandler.LogMessage("Update Check: Server timed out. Continuing offline.");
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }
            finally
            {
                Interlocked.Exchange(ref _isChecking, 0);
            }
        }

        private static void ShowUpdateAvailableDialog(string currentVer, string latestVer, string releaseNotes)
        {
            void OpenDialog()
            {
                using var dlg = new UpdateDlg(currentVer, latestVer, releaseNotes);
                DialogResult result = MainFrm.Instance is not null
                    ? dlg.ShowDialog(MainFrm.Instance)
                    : dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Process.Start(new ProcessStartInfo(DownloadUrl) { UseShellExecute = true });
                }
            }

            if (MainFrm.Instance is not null && MainFrm.Instance.InvokeRequired)
            {
                MainFrm.Instance.BeginInvoke((Action)OpenDialog);
            }
            else
            {
                OpenDialog();
            }
        }
    }
}