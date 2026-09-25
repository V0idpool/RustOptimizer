using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RustOptimizer
{
    public partial class About : Form
    {
        public About(string rtfText = null)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(20, 18, 16);
            this.StartPosition = FormStartPosition.CenterParent;
        }
        private void About_Load(object sender, EventArgs e)
        {
            System.Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string versionString = currentVersion.Major.ToString() + "." + currentVersion.Minor.ToString();
            lblVersion.Text = $"v{versionString}";
        }
    }
}
