#pragma warning disable WFO1000

using System;
using System.Windows.Forms;

namespace RustOptimizer
{
    public partial class UpdateDlg : Form
    {
        public UpdateDlg()
        {
            InitializeComponent();
        }

        public UpdateDlg(string currentVer, string latestVer, string releaseNotes) : this()
        {
            _lblHeader.Text = $"Rust Optimizer v{latestVer} is available!";
            _lblSub.Text = $"Installed Version: v{currentVer}   |   Latest Version: v{latestVer}";
            _txtNotes.Text = string.IsNullOrWhiteSpace(releaseNotes)
                ? "A new version of Rust Optimizer is ready to download."
                : releaseNotes.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        private void BtnDownload_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnLater_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
