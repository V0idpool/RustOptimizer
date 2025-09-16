using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RustOptimizer
{
    public partial class About : Form
    {
        public static About Instance { get; private set; }
        public About(string rtfText)
        {
            InitializeComponent();
            this.infoText.DetectUrls = true;
            this.infoText.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.RichTextBox_LinkClicked);
            this.infoText.Rtf = rtfText;
        }
        private void RichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.LinkText)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}
