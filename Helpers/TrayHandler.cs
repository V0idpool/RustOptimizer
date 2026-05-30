using RustOptimizer.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustOptimizer.Helpers
{
    public class TrayHandler
    {
        public static void HideFormToTray()
        {
            MainFrm.Instance.Hide();
            MainFrm.Instance.ShowInTaskbar = false;
            MainFrm.Instance.sysTrayIcon.Visible = true;
            MainFrm.Instance.sysTrayIcon.BalloonTipTitle = "Rust Optimizer";
            MainFrm.Instance.sysTrayIcon.BalloonTipText = "Running in the background...";
            MainFrm.Instance.sysTrayIcon.BalloonTipIcon = ToolTipIcon.Info;

            MainFrm.Instance.sysTrayIcon.ShowBalloonTip(3000);
        }
        public static void LaunchRust()
        {
            if (Process.GetProcessesByName("RustClient").Length > 0 || Process.GetProcessesByName("Rust").Length > 0)
            {
                MessageBox.Show("Rust is already running. Please close the game before launching it through Rust Optimizer.", "Game Already Running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rustPath = UserConfigs.GamePath;
            if (string.IsNullOrWhiteSpace(rustPath))
            {
                MessageBox.Show("Rust path is not set. Please set your game path in the settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string rustExe = Path.Combine(rustPath, "Rust.exe");
            if (!File.Exists(rustExe))
            {
                MessageBox.Show($"Rust.exe could not be found at: {rustExe}\n\nPlease verify your game path.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = rustExe,
                    WorkingDirectory = rustPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch Rust: {ex.Message}\n\nCheck your log file for full details.", "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExceptionHandler.LogError(ex);
            }
        }

        public static void ShowFormFromTray()
        {
            MainFrm.Instance.Show();
            MainFrm.Instance.WindowState = FormWindowState.Normal;
            MainFrm.Instance.ShowInTaskbar = true;
            MainFrm.Instance.sysTrayIcon.Visible = false;
            MainFrm.Instance.BringToFront();
        }
    }
}
