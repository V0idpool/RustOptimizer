using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;
namespace RustOptimizer.GUI
{

    // IMPORTANT:
    // Please leave these comments in place as they help protect intellectual rights and allow
    // developers to determine the version of the theme they are using. The preffered method
    // of distributing this theme is through the Nimoru Software home page at nimoru.com.

    // Name: Rust Optimizer V2 Theme
    // Created: 5/30/2026
    // Version: 2.0
    // Site: https://voidtech.xyz/

    // This work is licensed under a Creative Commons Attribution 3.0 Unported License.
    // To view a copy of this license, please visit http://creativecommons.org/licenses/by/3.0/

    // Copyright © 2026 VoidTech Studios

    abstract public class ThemeModule : ContainerControl
    {

        internal static Graphics G;



        public static Bitmap TextBitmap;
        public static Graphics TextGraphics;
        public ThemeModule()
        {
            TextBitmap = new Bitmap(1, 1);
            TextGraphics = Graphics.FromImage(TextBitmap);
        }

        internal static SizeF MeasureString(string text, Font font)
        {
            return TextGraphics.MeasureString(text, font);
        }

        internal static SizeF MeasureString(string text, Font font, int width)
        {
            return TextGraphics.MeasureString(text, font, width, StringFormat.GenericTypographic);
        }

        public static GraphicsPath CreateRoundPath;
        public static Rectangle CreateRoundRectangle;

        internal static GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
        {
            CreateRoundRectangle = new Rectangle(x, y, width, height);
            return CreateRound(CreateRoundRectangle, slope);
        }

        internal static GraphicsPath CreateRound(Rectangle r, int slope)
        {
            CreateRoundPath = new GraphicsPath(FillMode.Winding);
            CreateRoundPath.AddArc(r.X, r.Y, slope, slope, 180.0f, 90.0f);
            CreateRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270.0f, 90.0f);
            CreateRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0.0f, 90.0f);
            CreateRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90.0f, 90.0f);
            CreateRoundPath.CloseFigure();
            return CreateRoundPath;
        }

    }

    // Fixes the buggy menustrip colors
    public class ColorTable : ProfessionalColorTable
    {
        private Color BackColor = Color.FromArgb(40, 40, 40);
        private Color HoverColor = Color.FromArgb(60, 60, 60);
        private Color BorderColor = Color.FromArgb(70, 70, 70);
        public override Color ToolStripDropDownBackground => BackColor;
        public override Color ImageMarginGradientBegin => BackColor;
        public override Color ImageMarginGradientMiddle => BackColor;
        public override Color ImageMarginGradientEnd => BackColor;
        public override Color MenuItemSelected => HoverColor;
        public override Color MenuItemBorder => HoverColor;
        public override Color MenuItemPressedGradientBegin => BackColor;
        public override Color MenuItemPressedGradientEnd => BackColor;
        public override Color MenuItemPressedGradientMiddle => BackColor;
        public override Color MenuBorder => BorderColor;
        public override Color SeparatorDark => BorderColor;
        public override Color SeparatorLight => BackColor;
    }
    [DefaultEvent("TextChanged")]
    public class ROTextBox : Control
    {
        public TextBox Base;
        private int borderRadius = 4;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private const int WM_VSCROLL = 0x115;
        private const int SB_BOTTOM = 7;

        protected override Size DefaultSize => new Size(150, 30);

        public ROTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;

            Base = new TextBox
            {
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 40),
                Location = new Point(6, 6)
            };

            Controls.Add(Base);

            Base.TextChanged += (s, e) => { Text = Base.Text; };
            Base.KeyDown += OnBaseKeyDown;
            Base.Enter += (s, e) => Invalidate();
            Base.Leave += (s, e) => Invalidate();

            UpdateBaseSize();
        }

        public void ScrollToBottom()
        {
            if (Base.IsHandleCreated) SendMessage(Base.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);
        }

        public void AppendText(string text) => Base.AppendText(text);

        public HorizontalAlignment TextAlign
        {
            get => Base.TextAlign;
            set => Base.TextAlign = value;
        }

        public int MaxLength
        {
            get => Base.MaxLength;
            set => Base.MaxLength = value;
        }

        public bool ReadOnly
        {
            get => Base.ReadOnly;
            set => Base.ReadOnly = value;
        }

        public bool UseSystemPasswordChar
        {
            get => Base.UseSystemPasswordChar;
            set => Base.UseSystemPasswordChar = value;
        }

        public bool Multiline
        {
            get => Base.Multiline;
            set
            {
                Base.Multiline = value;
                Base.WordWrap = value;
                Base.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None;
                UpdateBaseSize();
            }
        }

        public override string Text
        {
            get => base.Text;
            set { base.Text = value; Base.Text = value; }
        }

        public override Font Font
        {
            get => base.Font;
            set { base.Font = value; Base.Font = value; UpdateBaseSize(); }
        }

        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                Base.SelectAll();
                e.SuppressKeyPress = true;
            }
        }

        private void UpdateBaseSize()
        {
            if (Base == null) return;
            Base.Width = Width - 12;

            if (!Multiline)
            {
                Height = Base.Height + 12;
            }
            else
            {
                Base.Height = Height - 12;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateBaseSize();
            base.OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Base.Focus();
            base.OnMouseDown(e);
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int curveSize = radius * 2;
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle mainRect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (GraphicsPath path = GetRoundedPath(mainRect, borderRadius))
            {
                using (SolidBrush bgBrush = new SolidBrush(Base.BackColor))
                {
                    e.Graphics.FillPath(bgBrush, path);
                }

                Color borderColor = Base.Focused ? Color.FromArgb(90, 90, 90) : Color.FromArgb(70, 70, 70);
                using (Pen borderPen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }
            }
        }
    }
    public class ROButton : Control
    {
        private int borderRadius = 4;
        private bool isHovered = false;
        private bool isPressed = false;

        protected override Size DefaultSize => new Size(120, 35);

        public ROButton()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Hand;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isPressed = true;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int curveSize = radius * 2;
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle mainRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (GraphicsPath path = GetRoundedPath(mainRect, borderRadius))
            {
                Color topColor = Color.FromArgb(75, 75, 75);
                Color bottomColor = Color.FromArgb(65, 65, 65);

                if (!Enabled)
                {
                    topColor = Color.FromArgb(45, 45, 45);
                    bottomColor = Color.FromArgb(40, 40, 40);
                }
                else if (isHovered && !isPressed)
                {
                    topColor = Color.FromArgb(85, 85, 85);
                    bottomColor = Color.FromArgb(75, 75, 75);
                }
                else if (isPressed)
                {
                    topColor = Color.FromArgb(50, 50, 50);
                    bottomColor = Color.FromArgb(50, 50, 50);
                }

                using (LinearGradientBrush brush = new LinearGradientBrush(mainRect, topColor, bottomColor, 90F))
                {
                    e.Graphics.FillPath(brush, path);
                }

                if (Enabled && !isPressed)
                {
                    Rectangle innerRect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
                    using (GraphicsPath innerPath = GetRoundedPath(innerRect, borderRadius - 1))
                    using (Pen innerPen = new Pen(Color.FromArgb(15, 255, 255, 255), 1))
                    {
                        e.Graphics.DrawPath(innerPen, innerPath);
                    }
                }

                using (Pen borderPen = new Pen(Color.FromArgb(35, 35, 35), 1))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }
            }

            Color textColor = Enabled ? Color.White : Color.FromArgb(120, 120, 120);
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, mainRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
    public class ROGroupBox : ContainerControl
    {
        public string Title { get; set; } = "Title";
        public string SubTitle { get; set; } = "Details";
        protected override Size DefaultSize
        {
            get { return new Size(250, 125); }
        }
        public ROGroupBox()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            this.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            this.Padding = new Padding(10, 45, 10, 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle mainRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            int radius = 6;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(mainRect.X, mainRect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(mainRect.Right - radius * 2, mainRect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(mainRect.Right - radius * 2, mainRect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(mainRect.X, mainRect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(40, 40, 40)))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen borderPen = new Pen(Color.FromArgb(60, 60, 60), 1))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }
            }

            using (Font titleFont = new Font("Segoe UI", 12f, FontStyle.Bold))
            {
                e.Graphics.DrawString(Title, titleFont, Brushes.White, new PointF(12, 10));
            }

            using (Font subFont = new Font("Segoe UI", 8.5f, FontStyle.Regular))
            {
                using (SolidBrush subBrush = new SolidBrush(Color.FromArgb(150, 150, 150)))
                {
                    e.Graphics.DrawString(SubTitle, subFont, subBrush, new PointF(13, 30));
                }
            }
        }
    }
    public class ROComboBox : ComboBox
    {
        public ROComboBox()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.BackColor = Color.FromArgb(40, 40, 40);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            this.ItemHeight = 22;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle mainRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(35, 35, 35)))
            {
                e.Graphics.FillRectangle(bgBrush, mainRect);
            }

            using (Pen borderPen = new Pen(Color.FromArgb(70, 70, 70), 1))
            {
                e.Graphics.DrawRectangle(borderPen, mainRect);
            }

            if (this.SelectedIndex != -1)
            {
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString(this.Text, this.Font, textBrush, new PointF(6, (this.Height - this.Font.Height) / 2));
                }
            }

            using (Pen arrowPen = new Pen(Color.FromArgb(150, 150, 150), 2f))
            {
                int arrowX = this.Width - 16;
                int arrowY = this.Height / 2 - 2;
                e.Graphics.DrawLine(arrowPen, arrowX, arrowY, arrowX + 4, arrowY + 4);
                e.Graphics.DrawLine(arrowPen, arrowX + 4, arrowY + 4, arrowX + 8, arrowY);
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            bool isHovered = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color bgColor = isHovered ? Color.FromArgb(60, 60, 60) : Color.FromArgb(40, 40, 40);

            using (SolidBrush bgBrush = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
            }

            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                e.Graphics.DrawString(this.Items[e.Index].ToString(), this.Font, textBrush, new PointF(e.Bounds.X + 6, e.Bounds.Y + 3));
            }
        }
    }
    public class ROCheckBox : Control
    {
        public event EventHandler CheckedChanged;
        private bool _checked;
        public bool Checked
        {
            get => _checked;
            set { _checked = value; CheckedChanged?.Invoke(this, EventArgs.Empty); Invalidate(); }
        }

        public ROCheckBox()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            this.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            this.Size = new Size(150, 24);
            this.Cursor = Cursors.Hand;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Checked = !Checked;
            base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle boxRect = new Rectangle(0, (this.Height - 18) / 2, 18, 18);

            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(30, 30, 30)))
            {
                e.Graphics.FillRectangle(bgBrush, boxRect);
            }

            if (Checked)
            {
                using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(90, 100, 110)))
                {
                    e.Graphics.FillRectangle(accentBrush, new Rectangle(boxRect.X + 2, boxRect.Y + 2, boxRect.Width - 4, boxRect.Height - 4));
                }

                using (Pen checkPen = new Pen(Color.White, 2f))
                {
                    e.Graphics.DrawLine(checkPen, boxRect.X + 4, boxRect.Y + 9, boxRect.X + 8, boxRect.Bottom - 5);
                    e.Graphics.DrawLine(checkPen, boxRect.X + 8, boxRect.Bottom - 5, boxRect.Right - 4, boxRect.Y + 5);
                }
            }

            using (Pen borderPen = new Pen(Color.FromArgb(70, 70, 70), 1))
            {
                e.Graphics.DrawRectangle(borderPen, boxRect);
            }

            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(220, 220, 220)))
            {
                e.Graphics.DrawString(this.Text, this.Font, textBrush, new PointF(26, (this.Height - e.Graphics.MeasureString(this.Text, this.Font).Height) / 2));
            }
        }
    }
    public class LaunchButton : Control
    {
        private int borderRadius = 6;
        private bool isHovered = false;
        private bool isPressed = false;

        public LaunchButton()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint |  ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            this.DoubleBuffered = true;
            this.Size = new Size(240, 60);
            this.Cursor = Cursors.Hand;
            this.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.Text = "LAUNCH RUST";
            this.BackColor = Color.Transparent;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isPressed = true;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int curveSize = radius * 2;
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle mainRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (GraphicsPath path = GetRoundedPath(mainRect, borderRadius))
            {
                Color topColor = Color.FromArgb(60, 60, 60);
                Color bottomColor = Color.FromArgb(50, 50, 50);

                if (isHovered && !isPressed)
                {
                    topColor = Color.FromArgb(70, 70, 70);
                    bottomColor = Color.FromArgb(60, 60, 60);
                }
                else if (isPressed)
                {
                    topColor = Color.FromArgb(45, 45, 45);
                    bottomColor = Color.FromArgb(45, 45, 45);
                }

                using (LinearGradientBrush brush = new LinearGradientBrush(mainRect, topColor, bottomColor, 90F))
                {
                    e.Graphics.FillPath(brush, path);
                }

                Rectangle innerRect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
                using (GraphicsPath innerPath = GetRoundedPath(innerRect, borderRadius - 1))
                {
                    using (Pen innerPen = new Pen(Color.FromArgb(20, 255, 255, 255), 1))
                    {
                        e.Graphics.DrawPath(innerPen, innerPath);
                    }
                }

                using (Pen borderPen = new Pen(Color.FromArgb(35, 35, 35), 1))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }
            }

            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, mainRect, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
    public class ROProgressBar : Control
    {

        public int _Minimum;
        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                _Minimum = value;
                if (value > _Value)
                    _Value = value;
                if (value > _Maximum)
                    _Maximum = value;
                Invalidate();
            }
        }

        public int _Maximum = 100;
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                _Maximum = value;
                if (value < _Value)
                    _Value = value;
                if (value < _Minimum)
                    _Minimum = value;
                Invalidate();
            }
        }

        public int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value > _Maximum || value < _Minimum)
                {
                    throw new Exception("Property value is not valid.");
                }

                _Value = value;
                Invalidate();
            }
        }

        public void Increment(int amount)
        {
            Value += amount;
        }

        public ROProgressBar()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            P1 = new Pen(Color.FromArgb(35, 35, 35));
            P2 = new Pen(Color.FromArgb(55, 55, 55));
            B1 = new SolidBrush(Color.FromArgb(200, 160, 0));
        }

        public GraphicsPath GP1, GP2, GP3;

        public Rectangle R1, R2;

        public Pen P1, P2;
        public SolidBrush B1;
        public LinearGradientBrush GB1, GB2;

        public int I1;

        protected override void OnPaint(PaintEventArgs e)
        {
            ThemeModule.G = e.Graphics;

            ThemeModule.G.Clear(BackColor);
            ThemeModule.G.SmoothingMode = SmoothingMode.AntiAlias;

            GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            R1 = new Rectangle(0, 2, Width - 1, Height - 1);
            GB1 = new LinearGradientBrush(R1, Color.FromArgb(45, 45, 45), Color.FromArgb(50, 50, 50), 90.0f);

            ThemeModule.G.SetClip(GP1);
            ThemeModule.G.FillRectangle(GB1, R1);

            I1 = (int)Math.Round((_Value - _Minimum) / (double)(_Maximum - _Minimum) * (Width - 3));

            if (I1 > 1)
            {
                GP3 = ThemeModule.CreateRound(1, 1, I1, Height - 3, 7);

                R2 = new Rectangle(1, 1, I1, Height - 3);
                GB2 = new LinearGradientBrush(R2, Color.FromArgb(205, 150, 0), Color.FromArgb(150, 110, 0), 90.0f);

                ThemeModule.G.FillPath(GB2, GP3);
                ThemeModule.G.DrawPath(P1, GP3);

                ThemeModule.G.SetClip(GP3);
                ThemeModule.G.SmoothingMode = SmoothingMode.None;

                ThemeModule.G.FillRectangle(B1, R2.X, R2.Y + 1, R2.Width, R2.Height / 2);

                ThemeModule.G.SmoothingMode = SmoothingMode.AntiAlias;
                ThemeModule.G.ResetClip();
            }

            ThemeModule.G.DrawPath(P2, GP1);
            ThemeModule.G.DrawPath(P1, GP2);
        }

    }

    public class ROLabel : Control
    {

        public ROLabel()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);

            B1 = new SolidBrush(Color.Orange);
        }

        public string _Value1 = "NET";
        public string Value1
        {
            get
            {
                return _Value1;
            }
            set
            {
                _Value1 = value;
                Invalidate();
            }
        }

        public string _Value2 = "SEAL";
        public string Value2
        {
            get
            {
                return _Value2;
            }
            set
            {
                _Value2 = value;
                Invalidate();
            }
        }

        public SolidBrush B1;

        public PointF PT1, PT2;
        public SizeF SZ1, SZ2;

        protected override void OnPaint(PaintEventArgs e)
        {
            ThemeModule.G = e.Graphics;
            ThemeModule.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            ThemeModule.G.Clear(BackColor);

            SZ1 = ThemeModule.G.MeasureString(Value1, Font, Width, StringFormat.GenericTypographic);
            SZ2 = ThemeModule.G.MeasureString(Value2, Font, Width, StringFormat.GenericTypographic);

            PT1 = new PointF(0f, Height / 2 - SZ1.Height / 2f);
            PT2 = new PointF(SZ1.Width + 1f, Height / 2 - SZ1.Height / 2f);

            ThemeModule.G.DrawString(Value1, Font, Brushes.Black, PT1.X + 1f, PT1.Y + 1f);
            ThemeModule.G.DrawString(Value1, Font, Brushes.White, PT1);

            ThemeModule.G.DrawString(Value2, Font, Brushes.Black, PT2.X + 1f, PT2.Y + 1f);
            ThemeModule.G.DrawString(Value2, Font, B1, PT2);
        }

    }
    public class ROTabControl : TabControl
    {

        public ROTabControl()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            SizeMode = TabSizeMode.Fixed;
            Alignment = TabAlignment.Left;
            ItemSize = new Size(28, 115);

            DrawMode = TabDrawMode.OwnerDrawFixed;

            P1 = new Pen(Color.FromArgb(55, 55, 55));
            P2 = new Pen(Color.FromArgb(35, 35, 35));
            P3 = new Pen(Color.FromArgb(45, 45, 45), 2f);

            B1 = new SolidBrush(Color.FromArgb(50, 50, 50));
            B2 = new SolidBrush(Color.FromArgb(35, 35, 35));
            B3 = new SolidBrush(Color.FromArgb(205, 150, 0));
            B4 = new SolidBrush(Color.FromArgb(65, 65, 65));

            SF1 = new StringFormat();
            SF1.LineAlignment = StringAlignment.Center;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is TabPage)
            {
                e.Control.BackColor = Color.FromArgb(40, 40, 40);
            }

            base.OnControlAdded(e);
        }

        public GraphicsPath GP1, GP2, GP3, GP4;

        public Rectangle R1, R2;

        public Pen P1, P2, P3;
        public SolidBrush B1, B2, B3, B4;

        public PathGradientBrush PB1;

        public TabPage TP1;
        public StringFormat SF1;

        public int Offset;
        public int ItemHeight;

        protected override void OnPaint(PaintEventArgs e)
        {
            ThemeModule.G = e.Graphics;
            ThemeModule.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            ThemeModule.G.Clear(Color.FromArgb(50, 50, 50));
            ThemeModule.G.SmoothingMode = SmoothingMode.AntiAlias;

            ItemHeight = ItemSize.Height + 2;

            GP1 = ThemeModule.CreateRound(0, 0, ItemHeight + 3, Height - 1, 7);
            GP2 = ThemeModule.CreateRound(1, 1, ItemHeight + 3, Height - 3, 7);

            PB1 = new PathGradientBrush(GP1);
            PB1.CenterColor = Color.FromArgb(50, 50, 50);
            PB1.SurroundColors = new[] { Color.FromArgb(45, 45, 45) };
            PB1.FocusScales = new PointF(0.8f, 0.95f);

            ThemeModule.G.FillPath(PB1, GP1);

            ThemeModule.G.DrawPath(P1, GP1);
            ThemeModule.G.DrawPath(P2, GP2);

            for (int I = 0, loopTo = TabCount - 1; I <= loopTo; I++)
            {
                R1 = GetTabRect(I);
                R1.Y += 2;
                R1.Height -= 3;
                R1.Width += 1;
                R1.X -= 1;

                TP1 = TabPages[I];
                Offset = 0;

                if (SelectedIndex == I)
                {
                    ThemeModule.G.FillRectangle(B1, R1);

                    for (int J = 0; J <= 1; J++)
                    {
                        ThemeModule.G.FillRectangle(B2, R1.X + 5 + J * 5, R1.Y + 6, 2, R1.Height - 9);

                        ThemeModule.G.SmoothingMode = SmoothingMode.None;
                        ThemeModule.G.FillRectangle(B3, R1.X + 5 + J * 5, R1.Y + 5, 2, R1.Height - 9);
                        ThemeModule.G.SmoothingMode = SmoothingMode.AntiAlias;

                        Offset += 5;
                    }

                    ThemeModule.G.DrawRectangle(P3, R1.X + 1, R1.Y - 1, R1.Width, R1.Height + 2);
                    ThemeModule.G.DrawRectangle(P1, R1.X + 1, R1.Y + 1, R1.Width - 2, R1.Height - 2);
                    ThemeModule.G.DrawRectangle(P2, R1);
                }
                else
                {
                    for (int J = 0; J <= 1; J++)
                    {
                        ThemeModule.G.FillRectangle(B2, R1.X + 5 + J * 5, R1.Y + 6, 2, R1.Height - 9);

                        ThemeModule.G.SmoothingMode = SmoothingMode.None;
                        ThemeModule.G.FillRectangle(B4, R1.X + 5 + J * 5, R1.Y + 5, 2, R1.Height - 9);
                        ThemeModule.G.SmoothingMode = SmoothingMode.AntiAlias;

                        Offset += 5;
                    }
                }

                R1.X += 5 + Offset;

                R2 = R1;
                R2.Y += 1;
                R2.X += 1;

                ThemeModule.G.DrawString(TP1.Text, Font, Brushes.Black, R2, SF1);
                ThemeModule.G.DrawString(TP1.Text, Font, Brushes.White, R1, SF1);
            }

            GP3 = ThemeModule.CreateRound(ItemHeight, 0, Width - ItemHeight - 1, Height - 1, 7);
            GP4 = ThemeModule.CreateRound(ItemHeight + 1, 1, Width - ItemHeight - 3, Height - 3, 7);

            ThemeModule.G.DrawPath(P2, GP3);
            ThemeModule.G.DrawPath(P1, GP4);
        }

    }

    [DefaultEvent("CheckedChanged")]
    public class ROOnOffBox : Control
    {

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender);

        public ROOnOffBox()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            P1 = new Pen(Color.FromArgb(55, 55, 55));
            P2 = new Pen(Color.FromArgb(35, 35, 35));
            P3 = new Pen(Color.FromArgb(65, 65, 65));

            B1 = new SolidBrush(Color.FromArgb(35, 35, 35));
            B2 = new SolidBrush(Color.FromArgb(85, 85, 85));
            B3 = new SolidBrush(Color.FromArgb(65, 65, 65));
            B4 = new SolidBrush(Color.FromArgb(205, 150, 0));
            B5 = new SolidBrush(Color.FromArgb(40, 40, 40));

            SF1 = new StringFormat();
            SF1.LineAlignment = StringAlignment.Center;
            SF1.Alignment = StringAlignment.Near;

            SF2 = new StringFormat();
            SF2.LineAlignment = StringAlignment.Center;
            SF2.Alignment = StringAlignment.Far;

            Size = new Size(80, 30);
            MinimumSize = Size;
            MaximumSize = Size;
        }

        public bool _Checked;
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                CheckedChanged?.Invoke(this);

                Invalidate();
            }
        }

        public GraphicsPath GP1, GP2, GP3, GP4;

        public Pen P1, P2, P3;
        public SolidBrush B1, B2, B3, B4, B5;

        public PathGradientBrush PB1;
        public LinearGradientBrush GB1;

        public Rectangle R1, R2, R3;
        public StringFormat SF1, SF2;

        public int Offset;

        protected override void OnPaint(PaintEventArgs e)
        {
            ThemeModule.G = e.Graphics;
            ThemeModule.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            ThemeModule.G.Clear(BackColor);
            ThemeModule.G.SmoothingMode = SmoothingMode.AntiAlias;

            GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            PB1 = new PathGradientBrush(GP1);
            PB1.CenterColor = Color.FromArgb(50, 50, 50);
            PB1.SurroundColors = new[] { Color.FromArgb(45, 45, 45) };
            PB1.FocusScales = new PointF(0.3f, 0.3f);

            ThemeModule.G.FillPath(PB1, GP1);
            ThemeModule.G.DrawPath(P1, GP1);
            ThemeModule.G.DrawPath(P2, GP2);

            R1 = new Rectangle(5, 0, Width - 10, Height + 2);
            R2 = new Rectangle(6, 1, Width - 10, Height + 2);

            R3 = new Rectangle(1, 1, Width / 2 - 1, Height - 3);

            if (_Checked)
            {
                ThemeModule.G.DrawString("\u2713", Font, Brushes.Black, R2, SF1);
                ThemeModule.G.DrawString("\u2713", Font, Brushes.White, R1, SF1);

                R3.X += Width / 2 - 1;
            }
            else
            {
                ThemeModule.G.DrawString("\u2717", Font, B1, R2, SF2);
                ThemeModule.G.DrawString("\u2717", Font, B2, R1, SF2);
            }

            GP3 = ThemeModule.CreateRound(R3, 7);
            GP4 = ThemeModule.CreateRound(R3.X + 1, R3.Y + 1, R3.Width - 2, R3.Height - 2, 7);

            GB1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90.0f);

            ThemeModule.G.FillPath(GB1, GP3);
            ThemeModule.G.DrawPath(P2, GP3);
            ThemeModule.G.DrawPath(P3, GP4);

            Offset = R3.X + R3.Width / 2 - 3;

            for (int I = 0; I <= 1; I++)
            {
                if (_Checked)
                {
                    ThemeModule.G.FillRectangle(B1, Offset + I * 5, 7, 2, Height - 14);
                }
                else
                {
                    ThemeModule.G.FillRectangle(B3, Offset + I * 5, 7, 2, Height - 14);
                }

                ThemeModule.G.SmoothingMode = SmoothingMode.None;

                if (_Checked)
                {
                    ThemeModule.G.FillRectangle(B4, Offset + I * 5, 7, 2, Height - 14);
                }
                else
                {
                    ThemeModule.G.FillRectangle(B5, Offset + I * 5, 7, 2, Height - 14);
                }

                ThemeModule.G.SmoothingMode = SmoothingMode.AntiAlias;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Checked = !Checked;
            base.OnMouseDown(e);
        }

    }
    public class ROSeperator : Control
    {

        public ROSeperator()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Height = 10;

            P1 = new Pen(Color.FromArgb(35, 35, 35));
            P2 = new Pen(Color.FromArgb(55, 55, 55));
        }

        public Pen P1, P2;

        protected override void OnPaint(PaintEventArgs e)
        {
            ThemeModule.G = e.Graphics;
            ThemeModule.G.Clear(BackColor);

            ThemeModule.G.DrawLine(P1, 0, 5, Width, 5);
            ThemeModule.G.DrawLine(P2, 0, 6, Width, 6);
        }

    }

   
    public class ROKeyboard : Control
    {

        public Bitmap TextBitmap;
        public Graphics TextGraphics;

        public const string LowerKeys = @"1234567890-=qwertyuiop[]asdfghjkl\;'zxcvbnm,./`";
        public const string UpperKeys = "!@#$%^&*()_+QWERTYUIOP{}ASDFGHJKL|:\"ZXCVBNM<>?~";

        public ROKeyboard()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Font = new Font("Verdana", 8.25f);

            TextBitmap = new Bitmap(1, 1);
            TextGraphics = Graphics.FromImage(TextBitmap);

            MinimumSize = new Size(386, 162);
            MaximumSize = new Size(386, 162);

            Lower = LowerKeys.ToCharArray();
            Upper = UpperKeys.ToCharArray();

            PrepareCache();

            P1 = new Pen(Color.FromArgb(45, 45, 45));
            P2 = new Pen(Color.FromArgb(65, 65, 65));
            P3 = new Pen(Color.FromArgb(35, 35, 35));

            B1 = new SolidBrush(Color.FromArgb(100, 100, 100));
        }

        public Control _Target;
        public Control Target
        {
            get
            {
                return _Target;
            }
            set
            {
                _Target = value;
            }
        }

        public bool Shift;

        public int Pressed = -1;
        public Rectangle[] Buttons;

        public char[] Lower;
        public char[] Upper;
        public string[] Other = new[] { "Shift", "Space", "Back" };

        public PointF[] UpperCache;
        public PointF[] LowerCache;

        public void PrepareCache()
        {
            Buttons = new Rectangle[51];
            UpperCache = new PointF[Upper.Length];
            LowerCache = new PointF[Lower.Length];

            int I;

            SizeF S;
            Rectangle R;

            for (int Y = 0; Y <= 3; Y++)
            {
                for (int X = 0; X <= 11; X++)
                {
                    I = Y * 12 + X;
                    R = new Rectangle(X * 32, Y * 32, 32, 32);

                    Buttons[I] = R;

                    if (!(I == 47) && !char.IsLetter(Upper[I]))
                    {
                        S = TextGraphics.MeasureString(Conversions.ToString(Upper[I]), Font);
                        UpperCache[I] = new PointF(R.X + (R.Width / 2 - S.Width / 2f), R.Y + R.Height - S.Height - 2f);

                        S = TextGraphics.MeasureString(Conversions.ToString(Lower[I]), Font);
                        LowerCache[I] = new PointF(R.X + (R.Width / 2 - S.Width / 2f), R.Y + R.Height - S.Height - 2f);
                    }
                }
            }

            Buttons[48] = new Rectangle(0, 4 * 32, 2 * 32, 32);
            Buttons[49] = new Rectangle(Buttons[48].Right, 4 * 32, 8 * 32, 32);
            Buttons[50] = new Rectangle(Buttons[49].Right, 4 * 32, 2 * 32, 32);
        }

        public GraphicsPath GP1;

        public SizeF SZ1;
        public PointF PT1;

        public Pen P1, P2, P3;
        public SolidBrush B1;

        public PathGradientBrush PB1;
        public LinearGradientBrush GB1;

        protected override void OnPaint(PaintEventArgs e)
        {
            ThemeModule.G = e.Graphics;
            ThemeModule.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            ThemeModule.G.Clear(BackColor);

            Rectangle R;

            int Offset;
            ThemeModule.G.DrawRectangle(P1, 0, 0, 12 * 32 + 1, 5 * 32 + 1);

            for (int I = 0, loopTo = Buttons.Length - 1; I <= loopTo; I++)
            {
                R = Buttons[I];

                Offset = 0;
                if (I == Pressed)
                {
                    Offset = 1;

                    GP1 = new GraphicsPath();
                    GP1.AddRectangle(R);

                    PB1 = new PathGradientBrush(GP1);
                    PB1.CenterColor = Color.FromArgb(60, 60, 60);
                    PB1.SurroundColors = new[] { Color.FromArgb(55, 55, 55) };
                    PB1.FocusScales = new PointF(0.8f, 0.5f);

                    ThemeModule.G.FillPath(PB1, GP1);
                }
                else
                {
                    GB1 = new LinearGradientBrush(R, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90.0f);
                    ThemeModule.G.FillRectangle(GB1, R);
                }

                switch (I)
                {
                    case 48:
                    case 49:
                    case 50:
                        {
                            SZ1 = ThemeModule.G.MeasureString(Other[I - 48], Font);
                            ThemeModule.G.DrawString(Other[I - 48], Font, Brushes.Black, R.X + (R.Width / 2 - SZ1.Width / 2f) + Offset + 1f, R.Y + (R.Height / 2 - SZ1.Height / 2f) + Offset + 1f);
                            ThemeModule.G.DrawString(Other[I - 48], Font, Brushes.White, R.X + (R.Width / 2 - SZ1.Width / 2f) + Offset, R.Y + (R.Height / 2 - SZ1.Height / 2f) + Offset);
                            break;
                        }
                    case 47:
                        {
                            DrawArrow(Color.Black, R.X + Offset + 1, R.Y + Offset + 1);
                            DrawArrow(Color.White, R.X + Offset, R.Y + Offset);
                            break;
                        }

                    default:
                        {
                            if (Shift)
                            {
                                ThemeModule.G.DrawString(Conversions.ToString(Upper[I]), Font, Brushes.Black, R.X + 3 + Offset + 1, R.Y + 2 + Offset + 1);
                                ThemeModule.G.DrawString(Conversions.ToString(Upper[I]), Font, Brushes.White, R.X + 3 + Offset, R.Y + 2 + Offset);

                                if (!char.IsLetter(Lower[I]))
                                {
                                    PT1 = LowerCache[I];
                                    ThemeModule.G.DrawString(Conversions.ToString(Lower[I]), Font, B1, PT1.X + Offset, PT1.Y + Offset);
                                }
                            }
                            else
                            {
                                ThemeModule.G.DrawString(Conversions.ToString(Lower[I]), Font, Brushes.Black, R.X + 3 + Offset + 1, R.Y + 2 + Offset + 1);
                                ThemeModule.G.DrawString(Conversions.ToString(Lower[I]), Font, Brushes.White, R.X + 3 + Offset, R.Y + 2 + Offset);

                                if (!char.IsLetter(Upper[I]))
                                {
                                    PT1 = UpperCache[I];
                                    ThemeModule.G.DrawString(Conversions.ToString(Upper[I]), Font, B1, PT1.X + Offset, PT1.Y + Offset);
                                }
                            }

                            break;
                        }
                }

                ThemeModule.G.DrawRectangle(P2, R.X + 1 + Offset, R.Y + 1 + Offset, R.Width - 2, R.Height - 2);
                ThemeModule.G.DrawRectangle(P3, R.X + Offset, R.Y + Offset, R.Width, R.Height);

                if (I == Pressed)
                {
                    ThemeModule.G.DrawLine(P1, R.X, R.Y, R.Right, R.Y);
                    ThemeModule.G.DrawLine(P1, R.X, R.Y, R.X, R.Bottom);
                }
            }
        }

        public void DrawArrow(Color color, int rx, int ry)
        {
            var R = new Rectangle(rx + 8, ry + 8, 16, 16);
            ThemeModule.G.SmoothingMode = SmoothingMode.AntiAlias;

            var P = new Pen(color, 1f);
            var C = new AdjustableArrowCap(3f, 2f);
            P.CustomEndCap = C;

            ThemeModule.G.DrawArc(P, R, 0.0f, 290.0f);

            P.Dispose();
            C.Dispose();
            ThemeModule.G.SmoothingMode = SmoothingMode.None;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int Index = e.Y / 32 * 12 + e.X / 32;

            if (Index > 47)
            {
                for (int I = 48, loopTo = Buttons.Length - 1; I <= loopTo; I++)
                {
                    if (Buttons[I].Contains(e.X, e.Y))
                    {
                        Pressed = I;
                        break;
                    }
                }
            }
            else
            {
                Pressed = Index;
            }

            HandleKey();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Pressed = -1;
            Invalidate();
        }

        public void HandleKey()
        {
            if (_Target is null)
                return;
            if (Pressed == -1)
                return;

            switch (Pressed)
            {
                case 47:
                    {
                        _Target.Text = string.Empty;
                        break;
                    }
                case 48:
                    {
                        Shift = !Shift;
                        break;
                    }
                case 49:
                    {
                        _Target.Text += " ";
                        break;
                    }
                case 50:
                    {
                        if (!(_Target.Text.Length == 0))
                        {
                            _Target.Text = _Target.Text.Remove(_Target.Text.Length - 1);
                        }

                        break;
                    }

                default:
                    {
                        if (Shift)
                        {
                            _Target.Text += Conversions.ToString(Upper[Pressed]);
                        }
                        else
                        {
                            _Target.Text += Conversions.ToString(Lower[Pressed]);
                        }

                        break;
                    }
            }
        }

    }
}