namespace RustOptimizer
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            nsGroupBox1 = new GUI.NSGroupBox();
            infoText = new RichTextBox();
            nsGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // nsGroupBox1
            // 
            nsGroupBox1.Controls.Add(infoText);
            nsGroupBox1.DrawSeperator = false;
            nsGroupBox1.Location = new Point(12, 12);
            nsGroupBox1.Name = "nsGroupBox1";
            nsGroupBox1.Size = new Size(493, 329);
            nsGroupBox1.SubTitle = "";
            nsGroupBox1.TabIndex = 0;
            nsGroupBox1.Text = "nsGroupBox1";
            nsGroupBox1.Title = "About RustOptimizer";
            // 
            // infoText
            // 
            infoText.BackColor = Color.Black;
            infoText.BorderStyle = BorderStyle.None;
            infoText.Location = new Point(14, 29);
            infoText.Name = "infoText";
            infoText.Size = new Size(464, 281);
            infoText.TabIndex = 0;
            infoText.Text = "";
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(517, 353);
            Controls.Add(nsGroupBox1);
            ForeColor = Color.FromArgb(255, 128, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "About";
            Text = "About";
            Load += About_Load;
            nsGroupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private RichTextBox infoText;
        public GUI.NSGroupBox nsGroupBox1;
    }
}