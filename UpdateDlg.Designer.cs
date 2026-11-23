#pragma warning disable WFO1000

using RustOptimizer.GUI;
using System.Drawing;
using System.Windows.Forms;

namespace RustOptimizer
{
    partial class UpdateDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        private ROThemeContainer _themeShell = null!;
        private Label _lblHeader = null!;
        private Label _lblSub = null!;
        private ROPatchNotesBox _txtNotes = null!;
        private ROModernButton _btnDownload = null!;
        private ROModernButton _btnLater = null!;
        private ROLabel _lblStudioBrand = null!;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _themeShell = new ROThemeContainer();
            _lblHeader = new Label();
            _lblSub = new Label();
            _txtNotes = new ROPatchNotesBox();
            _btnDownload = new ROModernButton();
            _btnLater = new ROModernButton();
            _lblStudioBrand = new ROLabel();
            _themeShell.SuspendLayout();
            SuspendLayout();
            // 
            // _themeShell
            // 
            _themeShell.BackColor = Color.FromArgb(14, 17, 22);
            _themeShell.BorderStyle = FormBorderStyle.None;
            _themeShell.Controls.Add(_lblHeader);
            _themeShell.Controls.Add(_lblSub);
            _themeShell.Controls.Add(_txtNotes);
            _themeShell.Controls.Add(_btnDownload);
            _themeShell.Controls.Add(_btnLater);
            _themeShell.Controls.Add(_lblStudioBrand);
            _themeShell.Dock = DockStyle.Fill;
            _themeShell.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _themeShell.Image = Properties.Resources.ROIcon;
            _themeShell.Location = new Point(0, 0);
            _themeShell.Movable = true;
            _themeShell.Name = "_themeShell";
            _themeShell.Padding = new Padding(14, 46, 14, 14);
            _themeShell.Sizable = false;
            _themeShell.Size = new Size(557, 373);
            _themeShell.SmartBounds = true;
            _themeShell.StartPosition = FormStartPosition.CenterParent;
            _themeShell.SubHeader = " Update Available";
            _themeShell.TabIndex = 0;
            _themeShell.Text = "Rust Optimizer";
            _themeShell.TransparencyKey = Color.Empty;
            _themeShell.Transparent = false;
            // 
            // _lblHeader
            // 
            _lblHeader.AutoSize = true;
            _lblHeader.BackColor = Color.FromArgb(14, 17, 22);
            _lblHeader.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            _lblHeader.ForeColor = Color.FromArgb(224, 83, 40);
            _lblHeader.Location = new Point(16, 48);
            _lblHeader.Name = "_lblHeader";
            _lblHeader.Size = new Size(308, 19);
            _lblHeader.TabIndex = 1;
            _lblHeader.Text = "A new version of Rust Optimizer is available!";
            // 
            // _lblSub
            // 
            _lblSub.AutoSize = true;
            _lblSub.BackColor = Color.FromArgb(14, 17, 22);
            _lblSub.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblSub.ForeColor = Color.FromArgb(148, 160, 176);
            _lblSub.Location = new Point(16, 71);
            _lblSub.Name = "_lblSub";
            _lblSub.Size = new Size(298, 15);
            _lblSub.TabIndex = 2;
            _lblSub.Text = "Installed Version: v1.0.0.0   |   Latest Version: v1.1.0.0";
            // 
            // _txtNotes
            // 
            _txtNotes.AccentColor = Color.FromArgb(224, 83, 40);
            _txtNotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _txtNotes.BackColor = Color.Transparent;
            _txtNotes.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _txtNotes.ForeColor = Color.FromArgb(240, 244, 248);
            _txtNotes.Location = new Point(16, 94);
            _txtNotes.Name = "_txtNotes";
            _txtNotes.Size = new Size(525, 218);
            _txtNotes.TabIndex = 3;
            _txtNotes.Text = "A new version of Rust Optimizer is ready to download.";
            _txtNotes.Title = "PATCH NOTES / CHANGELOG";
            // 
            // _btnDownload
            // 
            _btnDownload.AccentColor = Color.FromArgb(255, 102, 36);
            _btnDownload.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _btnDownload.BackColor = Color.Transparent;
            _btnDownload.CornerRadius = 6;
            _btnDownload.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnDownload.ForeColor = Color.FromArgb(240, 244, 248);
            _btnDownload.IsPrimaryAction = true;
            _btnDownload.Location = new Point(259, 324);
            _btnDownload.Name = "_btnDownload";
            _btnDownload.Size = new Size(190, 34);
            _btnDownload.TabIndex = 5;
            _btnDownload.Text = "Open Downloads Page ↗";
            _btnDownload.Click += BtnDownload_Click;
            // 
            // _btnLater
            // 
            _btnLater.AccentColor = Color.FromArgb(0, 180, 240);
            _btnLater.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _btnLater.BackColor = Color.Transparent;
            _btnLater.CornerRadius = 6;
            _btnLater.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnLater.ForeColor = Color.FromArgb(240, 244, 248);
            _btnLater.IsPrimaryAction = false;
            _btnLater.Location = new Point(457, 324);
            _btnLater.Name = "_btnLater";
            _btnLater.Size = new Size(84, 34);
            _btnLater.TabIndex = 6;
            _btnLater.Text = "Later";
            _btnLater.Click += BtnLater_Click;
            // 
            // _lblStudioBrand
            // 
            _lblStudioBrand.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _lblStudioBrand.BackColor = Color.Transparent;
            _lblStudioBrand.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _lblStudioBrand.Location = new Point(16, 328);
            _lblStudioBrand.Name = "_lblStudioBrand";
            _lblStudioBrand.Size = new Size(155, 24);
            _lblStudioBrand.TabIndex = 4;
            _lblStudioBrand.Value1 = "VOID TECH";
            _lblStudioBrand.Value2 = " STUDIOS";
            // 
            // UpdateDlg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 17, 22);
            ClientSize = new Size(557, 373);
            Controls.Add(_themeShell);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UpdateDlg";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Rust Optimizer Update Available";
            _themeShell.ResumeLayout(false);
            _themeShell.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}