#pragma warning disable WFO1000
#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RustOptimizer.GUI
{
    // Name: Rust Optimizer - Obsidian Rust Theme Base
    // Target: .NET 8 / .NET 10 WinForms
    // Site: https://voidtechstudios.us/
    // Copyright © 2026 Void Tech Studios

    public abstract class VTSThemeContainer : ContainerControl
    {
        protected Graphics G = null!;
        protected Bitmap? B;

        protected Rectangle CloseButtonRect;
        protected Rectangle MinimizeButtonRect;
        protected bool HoverClose;
        protected bool HoverMinimize;

        private bool _doneCreation;
        private bool _hasShown;
        private bool _isParentForm;
        private bool _backColorTransparent;
        private bool _wmLmButtonDown;
        private Rectangle _frame;
        private int _current, _previous;
        private readonly Message[] _messages = new Message[9];
        private readonly Dictionary<string, Color> _items = new();
        private readonly Bitmap _measureBitmap;
        private readonly Graphics _measureGraphics;
        private readonly GraphicsPath _drawRadialPath;

        public VTSThemeContainer()
        {
            SetStyle((ControlStyles)139270, true);
            _imageSize = Size.Empty;
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            _measureBitmap = new Bitmap(1, 1);
            _measureGraphics = Graphics.FromImage(_measureBitmap);
            _drawRadialPath = new GraphicsPath();
            InvalidateCustimization();
        }

        protected sealed override void OnHandleCreated(EventArgs e)
        {
            if (_doneCreation) InitializeMessages();
            InvalidateCustimization();
            ColorHook();

            if (_lockWidth != 0) Width = _lockWidth;
            if (_lockHeight != 0) Height = _lockHeight;
            if (!_controlMode) base.Dock = DockStyle.Fill;

            Transparent = _transparent;
            if (_transparent && _backColorTransparent) BackColor = Color.Transparent;

            base.OnHandleCreated(e);
        }

        protected sealed override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (Parent is null) return;

            _isParentForm = Parent is Form;
            if (!_controlMode)
            {
                InitializeMessages();
                if (_isParentForm && ParentForm is not null && !DesignMode)
                {
                    ParentForm.FormBorderStyle = _borderStyle;
                    if (_transparencyKey != Color.Empty)
                        ParentForm.TransparencyKey = _transparencyKey;

                    ParentForm.Shown += FormShown;
                }
                Parent.BackColor = BackColor;
            }

            OnCreation();
            _doneCreation = true;
            InvalidateTimer();
        }

        private void DoAnimation(bool i)
        {
            OnAnimation();
            if (i) Invalidate();
        }

        protected sealed override void OnPaint(PaintEventArgs e)
        {
            if (Width == 0 || Height == 0) return;

            if (_transparent && _controlMode)
            {
                PaintHook();
                if (B is not null) e.Graphics.DrawImage(B, 0, 0);
            }
            else
            {
                G = e.Graphics;
                PaintHook();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ThemeShare.RemoveAnimationCallback(DoAnimation);
            base.OnHandleDestroyed(e);
        }

        private void FormShown(object? sender, EventArgs e)
        {
            if (_controlMode || _hasShown || ParentForm is null) return;

            if (_startPosition == FormStartPosition.CenterParent || _startPosition == FormStartPosition.CenterScreen)
            {
                Rectangle sb = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
                Rectangle cb = ParentForm.Bounds;
                ParentForm.Location = new Point(sb.Width / 2 - cb.Width / 2, sb.Height / 2 - cb.Height / 2);
            }
            _hasShown = true;
        }

        protected sealed override void OnSizeChanged(EventArgs e)
        {
            if (_movable && !_controlMode)
                _frame = new Rectangle(1, 1, Math.Max(0, Width - 2), Math.Max(0, _header - 1));

            UpdateChromeButtons();
            InvalidateBitmap();
            Invalidate();
            base.OnSizeChanged(e);
        }

        protected virtual void UpdateChromeButtons()
        {
            int btnW = 36;
            int btnH = Math.Max(20, _header - 12);
            int btnY = (_header - btnH) / 2;
            CloseButtonRect = new Rectangle(Width - btnW - 8, btnY, btnW, btnH);
            MinimizeButtonRect = new Rectangle(CloseButtonRect.Left - btnW - 4, btnY, btnW, btnH);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (_lockWidth != 0) width = _lockWidth;
            if (_lockHeight != 0) height = _lockHeight;
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected MouseState State;
        private void SetState(MouseState current) { State = current; Invalidate(); }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool overClose = !_controlMode && CloseButtonRect.Contains(e.Location);
            bool overMin = !_controlMode && MinimizeButtonRect.Contains(e.Location);

            if (overClose != HoverClose || overMin != HoverMinimize)
            {
                HoverClose = overClose;
                HoverMinimize = overMin;
                Invalidate(new Rectangle(0, 0, Width, _header));
            }

            if (overClose || overMin)
            {
                Cursor = Cursors.Hand;
                base.OnMouseMove(e);
                return;
            }

            if (!(_isParentForm && ParentForm?.WindowState == FormWindowState.Maximized))
            {
                if (_sizable && !_controlMode) InvalidateMouse();
                else Cursor = Cursors.Default;
            }
            base.OnMouseMove(e);
        }

        protected override void OnEnabledChanged(EventArgs e) { SetState(Enabled ? MouseState.None : MouseState.Block); base.OnEnabledChanged(e); }
        protected override void OnMouseEnter(EventArgs e) { SetState(MouseState.Over); base.OnMouseEnter(e); }
        protected override void OnMouseUp(MouseEventArgs e) { SetState(MouseState.Over); base.OnMouseUp(e); }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (HoverClose || HoverMinimize)
            {
                HoverClose = false;
                HoverMinimize = false;
                Invalidate(new Rectangle(0, 0, Width, _header));
            }
            SetState(MouseState.None);
            Cursor = Cursors.Default;
            _previous = 0;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!_controlMode && _isParentForm && ParentForm is not null)
                {
                    if (CloseButtonRect.Contains(e.Location)) { ParentForm.Close(); return; }
                    if (MinimizeButtonRect.Contains(e.Location)) { ParentForm.WindowState = FormWindowState.Minimized; return; }
                }

                SetState(MouseState.Down);
                if (!(_isParentForm && ParentForm?.WindowState == FormWindowState.Maximized || _controlMode))
                {
                    if (_movable && _frame.Contains(e.Location))
                    {
                        Capture = false;
                        _wmLmButtonDown = true;
                        DefWndProc(ref _messages[0]);
                    }
                    else if (_sizable && _previous != 0)
                    {
                        Capture = false;
                        _wmLmButtonDown = true;
                        DefWndProc(ref _messages[_previous]);
                    }
                }
            }
            base.OnMouseDown(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (_wmLmButtonDown && m.Msg == 513)
            {
                _wmLmButtonDown = false;
                SetState(MouseState.Over);
                if (!_smartBounds || Parent is null) return;

                if (IsParentMdi && Parent.Parent is not null)
                    CorrectBounds(new Rectangle(Point.Empty, Parent.Parent.Size));
                else
                    CorrectBounds(Screen.FromControl(Parent).WorkingArea);
            }
        }

        private int GetIndex()
        {
            Point pt = PointToClient(MousePosition);
            bool b1 = pt.X < 7, b2 = pt.X > Width - 7, b3 = pt.Y < 7, b4 = pt.Y > Height - 7;
            if (b1 && b3) return 4;
            if (b1 && b4) return 7;
            if (b2 && b3) return 5;
            if (b2 && b4) return 8;
            if (b1) return 1;
            if (b2) return 2;
            if (b3) return 3;
            if (b4) return 6;
            return 0;
        }

        private void InvalidateMouse()
        {
            _current = GetIndex();
            if (_current == _previous) return;
            _previous = _current;
            Cursor = _previous switch
            {
                0 => Cursors.Default,
                1 or 2 => Cursors.SizeWE,
                3 or 6 => Cursors.SizeNS,
                4 or 8 => Cursors.SizeNWSE,
                5 or 7 => Cursors.SizeNESW,
                _ => Cursors.Default
            };
        }

        private void InitializeMessages()
        {
            if (Parent is null) return;
            _messages[0] = Message.Create(Parent.Handle, 161, new nint(2), nint.Zero);
            for (int i = 1; i <= 8; i++)
                _messages[i] = Message.Create(Parent.Handle, 161, new nint(i + 9), nint.Zero);
        }

        private void CorrectBounds(Rectangle bounds)
        {
            if (Parent is null) return;
            if (Parent.Width > bounds.Width) Parent.Width = bounds.Width;
            if (Parent.Height > bounds.Height) Parent.Height = bounds.Height;

            int x = Math.Clamp(Parent.Location.X, bounds.X, Math.Max(bounds.X, bounds.Right - Parent.Width));
            int y = Math.Clamp(Parent.Location.Y, bounds.Y, Math.Max(bounds.Y, bounds.Bottom - Parent.Height));
            Parent.Location = new Point(x, y);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override DockStyle Dock { get => base.Dock; set { if (!_controlMode) return; base.Dock = value; } }

        [Category("Misc")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (value == base.BackColor) return;
                if (!IsHandleCreated && _controlMode && value == Color.Transparent) { _backColorTransparent = true; return; }
                base.BackColor = value;
                if (Parent is not null) { if (!_controlMode) Parent.BackColor = value; ColorHook(); }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get => base.Text; set { base.Text = value; Invalidate(); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Font Font { get => base.Font; set { base.Font = value; Invalidate(); } }

        private bool _smartBounds = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool SmartBounds { get => _smartBounds; set => _smartBounds = value; }

        private bool _movable = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Movable { get => _movable; set => _movable = value; }

        private bool _sizable;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Sizable { get => _sizable; set => _sizable = value; }

        private Color _transparencyKey;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TransparencyKey
        {
            get => (_isParentForm && !_controlMode && ParentForm is not null) ? ParentForm.TransparencyKey : _transparencyKey;
            set { _transparencyKey = value; if (_isParentForm && !_controlMode && ParentForm is not null) ParentForm.TransparencyKey = value; }
        }

        private FormBorderStyle _borderStyle = FormBorderStyle.None;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FormBorderStyle BorderStyle
        {
            get => (_isParentForm && !_controlMode && ParentForm is not null) ? ParentForm.FormBorderStyle : _borderStyle;
            set { _borderStyle = value; if (_isParentForm && !_controlMode && ParentForm is not null) ParentForm.FormBorderStyle = value; }
        }

        private FormStartPosition _startPosition = FormStartPosition.CenterScreen;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FormStartPosition StartPosition
        {
            get => (_isParentForm && !_controlMode && ParentForm is not null) ? ParentForm.StartPosition : _startPosition;
            set { _startPosition = value; if (_isParentForm && !_controlMode && ParentForm is not null) ParentForm.StartPosition = value; }
        }

        private Image? _image;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? Image { get => _image; set { _imageSize = value?.Size ?? Size.Empty; _image = value; Invalidate(); } }

        private bool _transparent;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Transparent
        {
            get => _transparent;
            set
            {
                _transparent = value;
                if (!(IsHandleCreated || _controlMode)) return;
                SetStyle(ControlStyles.Opaque, !value);
                SetStyle(ControlStyles.SupportsTransparentBackColor, value);
                InvalidateBitmap();
                Invalidate();
            }
        }

        private Size _imageSize;
        protected Size ImageSize => _imageSize;
        protected bool IsParentForm => _isParentForm;
        protected bool IsParentMdi => Parent?.Parent is not null;

        private int _lockWidth;
        protected int LockWidth { get => _lockWidth; set { _lockWidth = value; if (_lockWidth != 0 && IsHandleCreated) Width = _lockWidth; } }

        private int _lockHeight;
        protected int LockHeight { get => _lockHeight; set { _lockHeight = value; if (_lockHeight != 0 && IsHandleCreated) Height = _lockHeight; } }

        private int _header = 46;
        protected int Header
        {
            get => _header;
            set
            {
                _header = value;
                if (!_controlMode)
                {
                    _frame = new Rectangle(1, 1, Math.Max(0, Width - 2), Math.Max(0, value - 1));
                    UpdateChromeButtons();
                    Invalidate();
                }
            }
        }

        private bool _controlMode;
        protected bool ControlMode { get => _controlMode; set { _controlMode = value; Invalidate(); } }

        private bool _isAnimated;
        protected bool IsAnimated { get => _isAnimated; set { _isAnimated = value; InvalidateTimer(); } }

        protected Pen GetPen(string name) => new(_items[name]);
        protected Color GetColor(string name) => _items[name];
        protected void SetColor(string name, Color value) => _items[name] = value;

        private void InvalidateBitmap()
        {
            B?.Dispose();
            if (_transparent && _controlMode && Width > 0 && Height > 0)
            {
                B = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);
                G = Graphics.FromImage(B);
            }
            else B = null;
        }

        private void InvalidateCustimization() { }

        private void InvalidateTimer()
        {
            if (DesignMode || !_doneCreation) return;
            if (_isAnimated) ThemeShare.AddAnimationCallback(DoAnimation);
            else ThemeShare.RemoveAnimationCallback(DoAnimation);
        }

        protected abstract void ColorHook();
        protected abstract void PaintHook();
        protected virtual void OnCreation() { }
        protected virtual void OnAnimation() { }

        protected void DrawBorders(Pen p1) => G.DrawRectangle(p1, 0, 0, Width - 1, Height - 1);
    }

    public static class ThemeShare
    {
        private static int _frames;
        private static bool _invalidate;
        public static readonly PrecisionTimer ThemeTimer = new();
        private const int FPS = 50;
        private const int Rate = 10;

        public delegate void AnimationDelegate(bool invalidate);
        private static readonly List<AnimationDelegate> Callbacks = new();

        private static void HandleCallbacks(nint state, bool reserve)
        {
            _invalidate = _frames >= FPS;
            if (_invalidate) _frames = 0;

            lock (Callbacks)
            {
                for (int i = 0; i < Callbacks.Count; i++)
                    Callbacks[i].Invoke(_invalidate);
            }
            _frames += Rate;
        }

        private static void InvalidateThemeTimer()
        {
            if (Callbacks.Count == 0) ThemeTimer.Delete();
            else ThemeTimer.Create(0U, Rate, HandleCallbacks);
        }

        public static void AddAnimationCallback(AnimationDelegate callback)
        {
            lock (Callbacks)
            {
                if (Callbacks.Contains(callback)) return;
                Callbacks.Add(callback);
                InvalidateThemeTimer();
            }
        }

        public static void RemoveAnimationCallback(AnimationDelegate callback)
        {
            lock (Callbacks)
            {
                if (!Callbacks.Contains(callback)) return;
                Callbacks.Remove(callback);
                InvalidateThemeTimer();
            }
        }
    }

    public enum MouseState : byte { None = 0, Over = 1, Down = 2, Block = 3 }

    public sealed class PrecisionTimer : IDisposable
    {
        private bool _enabled;
        private nint _handle;
        private TimerDelegate? _timerCallback;

        [DllImport("kernel32.dll", EntryPoint = "CreateTimerQueueTimer")]
        private static extern bool CreateTimerQueueTimer(ref nint handle, nint queue, TimerDelegate callback, nint state, uint dueTime, uint period, uint flags);

        [DllImport("kernel32.dll", EntryPoint = "DeleteTimerQueueTimer")]
        private static extern bool DeleteTimerQueueTimer(nint queue, nint handle, nint callback);

        public delegate void TimerDelegate(nint r1, bool r2);

        public void Create(uint dueTime, uint period, TimerDelegate callback)
        {
            if (_enabled) return;
            _timerCallback = callback;
            _enabled = CreateTimerQueueTimer(ref _handle, nint.Zero, _timerCallback, nint.Zero, dueTime, period, 0U);
        }

        public void Delete()
        {
            if (!_enabled) return;
            bool success = DeleteTimerQueueTimer(nint.Zero, _handle, nint.Zero);
            _enabled = !success;
        }

        public void Dispose() => Delete();
    }

    public class ROThemeContainer : VTSThemeContainer
    {
        private string _subHeader = "Version#";
        [Category("Appearance RustOptimizer")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string SubHeader { get => _subHeader; set { _subHeader = value; Invalidate(); } }

        public ROThemeContainer()
        {
            Header = 46;
            BackColor = ROPalette.BgDeep;
            Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            Text = "RUST OPTIMIZER";
            Sizable = false;

            SetColor("Background", ROPalette.BgDeep);
            SetColor("Header", ROPalette.SurfaceDark);
            SetColor("Border", ROPalette.BorderSubtle);
            SetColor("Text", ROPalette.TextPrimary);
        }

        protected override void ColorHook() { }

        protected override void PaintHook()
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            G.Clear(GetColor("Background"));

            Rectangle headerRect = new(0, 0, Width, Header);
            using (SolidBrush hdrBg = new(GetColor("Header")))
                G.FillRectangle(hdrBg, headerRect);

            using (LinearGradientBrush topRibbon = new(new Rectangle(0, 0, Math.Max(1, Width), 2), ROPalette.AccentRust, ROPalette.AccentAmber, 0f))
                G.FillRectangle(topRibbon, 0, 0, Width, 2);

            int titleX = 14;
            if (Image is not null)
            {
                int sz = 24;
                G.DrawImage(Image, new Rectangle(14, (Header - sz) / 2, sz, sz));
                titleX = 46;
            }
            else
            {
                using GraphicsPath pill = ThemeModule.GetResponsiveRoundedPath(new Rectangle(14, 12, 4, Header - 22), 2);
                using SolidBrush accent = new(ROPalette.AccentRust);
                G.FillPath(accent, pill);
                titleX = 26;
            }

            TextRenderer.DrawText(G, Text.ToUpperInvariant(), Font, new Point(titleX, 7), GetColor("Text"));
            using (Font subFont = new("Segoe UI", 7.25f, FontStyle.Bold))
                TextRenderer.DrawText(G, _subHeader, subFont, new Point(titleX, 26), ROPalette.TextMuted);

            if (HoverMinimize)
            {
                using GraphicsPath mp = ThemeModule.GetResponsiveRoundedPath(MinimizeButtonRect, 6);
                using SolidBrush mb = new(ROPalette.SurfaceHover);
                G.FillPath(mb, mp);
            }
            using (Pen minPen = new(HoverMinimize ? Color.White : ROPalette.TextSecondary, 1.6f))
            {
                int y = MinimizeButtonRect.Y + MinimizeButtonRect.Height / 2 + 2;
                G.DrawLine(minPen, MinimizeButtonRect.X + 12, y, MinimizeButtonRect.Right - 12, y);
            }

            if (HoverClose)
            {
                using GraphicsPath cp = ThemeModule.GetResponsiveRoundedPath(CloseButtonRect, 6);
                using SolidBrush cb = new(ROPalette.AccentCrimson);
                G.FillPath(cb, cp);
            }
            using (Pen closePen = new(HoverClose ? Color.White : ROPalette.TextSecondary, 1.6f))
            {
                int cx = CloseButtonRect.X + CloseButtonRect.Width / 2;
                int cy = CloseButtonRect.Y + CloseButtonRect.Height / 2;
                G.DrawLine(closePen, cx - 5, cy - 5, cx + 5, cy + 5);
                G.DrawLine(closePen, cx + 5, cy - 5, cx - 5, cy + 5);
            }

            using Pen borderPen = GetPen("Border");
            G.DrawLine(borderPen, 0, Header - 1, Width, Header - 1);
            DrawBorders(borderPen);
        }
    }
}