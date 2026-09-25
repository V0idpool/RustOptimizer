using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RustOptimizer
{
    partial class About
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(About));
            lblAppName = new Label();
            lblVersion = new Label();
            lblDescription = new Label();
            linkDocs = new GUI.AboutLinkTile();
            linkGitHub = new GUI.AboutLinkTile();
            linkDiscord = new GUI.AboutLinkTile();
            linkDonate = new GUI.AboutLinkTile();
            cardPanel = new Panel();
            linkWebsite = new GUI.AboutLinkTile();
            cardPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblAppName.ForeColor = Color.FromArgb(237, 232, 228);
            lblAppName.Location = new Point(14, 14);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(176, 30);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "Rust Optimizer";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVersion.ForeColor = Color.FromArgb(224, 83, 40);
            lblVersion.Location = new Point(200, 26);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(41, 15);
            lblVersion.TabIndex = 1;
            lblVersion.Text = "v2.0.0";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.ForeColor = Color.FromArgb(160, 150, 140);
            lblDescription.Location = new Point(16, 52);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(428, 64);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "A lightweight, open-source Optimization Tool designed specifically to fix Rusts memory leaks and config issues so you can focus on the wipe.\r\n\r\nMaximize your FPS!";
            // 
            // linkDocs
            // 
            linkDocs.Icon = Properties.Resources.logo;
            linkDocs.Location = new Point(20, 223);
            linkDocs.Name = "linkDocs";
            linkDocs.Size = new Size(224, 62);
            linkDocs.TabIndex = 2;
            linkDocs.Title = "Documentation";
            linkDocs.Url = "https://rustoptimizer.voidtech.xyz/docs";
            // 
            // linkGitHub
            // 
            linkGitHub.Icon = Properties.Resources.github;
            linkGitHub.Location = new Point(256, 223);
            linkGitHub.Name = "linkGitHub";
            linkGitHub.Size = new Size(224, 62);
            linkGitHub.TabIndex = 3;
            linkGitHub.Title = "GitHub Source";
            linkGitHub.Url = "https://github.com/V0idpool/RustOptimizer";
            // 
            // linkDiscord
            // 
            linkDiscord.Icon = Properties.Resources.discord;
            linkDiscord.Location = new Point(20, 291);
            linkDiscord.Name = "linkDiscord";
            linkDiscord.Size = new Size(224, 62);
            linkDiscord.TabIndex = 4;
            linkDiscord.Title = "Discord Support";
            linkDiscord.Url = "https://rustoptimizer.voidtech.xyz/invite";
            // 
            // linkDonate
            // 
            linkDonate.Icon = Properties.Resources.cup;
            linkDonate.Location = new Point(256, 291);
            linkDonate.Name = "linkDonate";
            linkDonate.Size = new Size(224, 62);
            linkDonate.TabIndex = 5;
            linkDonate.Title = "Donations";
            linkDonate.Url = "https://buymeacoffee.com/rustforgedev";
            // 
            // cardPanel
            // 
            cardPanel.BackColor = Color.FromArgb(30, 30, 30);
            cardPanel.Controls.Add(lblAppName);
            cardPanel.Controls.Add(lblVersion);
            cardPanel.Controls.Add(lblDescription);
            cardPanel.Location = new Point(20, 9);
            cardPanel.Name = "cardPanel";
            cardPanel.Padding = new Padding(16);
            cardPanel.Size = new Size(460, 130);
            cardPanel.TabIndex = 1;
            // 
            // linkWebsite
            // 
            linkWebsite.Icon = Properties.Resources.link;
            linkWebsite.Location = new Point(20, 149);
            linkWebsite.Name = "linkWebsite";
            linkWebsite.Size = new Size(460, 62);
            linkWebsite.TabIndex = 6;
            linkWebsite.Title = "Official Website";
            linkWebsite.Url = "https://rustoptimizer.voidtech.xyz/";
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(500, 367);
            Controls.Add(linkWebsite);
            Controls.Add(linkDonate);
            Controls.Add(linkDiscord);
            Controls.Add(linkGitHub);
            Controls.Add(linkDocs);
            Controls.Add(cardPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "About";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About Rust Optimizer";
            Load += About_Load;
            cardPanel.ResumeLayout(false);
            cardPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel cardPanel;
        private Label lblAppName;
        private Label lblVersion;
        private Label lblDescription;
        private GUI.AboutLinkTile linkDocs;
        private GUI.AboutLinkTile linkGitHub;
        private GUI.AboutLinkTile linkDiscord;
        private GUI.AboutLinkTile linkDonate;
        private GUI.AboutLinkTile linkWebsite;
    }
}