using System;
using System.Drawing;
using System.Windows.Forms;

namespace project_cuoi_ky
{    public partial class ToastNotification : UserControl
    {
        private Timer _fadeTimer;
        private Timer _displayTimer;
        private Timer _slideTimer;
        private double _opacity = 1.0;
        private bool _isClosing = false;
        private string _chatroomId;
        private int _targetX;
        private int _startX;
        
        public event EventHandler<string> ToastClicked;
        
        public ToastNotification()
        {
            InitializeComponent();
            SetupToast();
        }
        
        public ToastNotification(string title, string message, string chatroomId = null) : this()
        {
            SetContent(title, message, chatroomId);
        }        private void SetupToast()
        {
            // Set up the control
            this.Size = new Size(350, 80);
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;
            
            // Add drop shadow effect
            this.Paint += ToastNotification_Paint;
            
            // Setup custom icon painting
            picIcon.Paint += PicIcon_Paint;
            
            // Setup timers
            _displayTimer = new Timer();
            _displayTimer.Interval = 4000; // Display for 4 seconds
            _displayTimer.Tick += DisplayTimer_Tick;
            
            _fadeTimer = new Timer();
            _fadeTimer.Interval = 50; // Fade out smoothly
            _fadeTimer.Tick += FadeTimer_Tick;
            
            _slideTimer = new Timer();
            _slideTimer.Interval = 20; // Slide animation
            _slideTimer.Tick += SlideTimer_Tick;
            
            // Click event
            this.Click += ToastNotification_Click;
            foreach (Control control in this.Controls)
            {
                control.Click += ToastNotification_Click;
            }
        }
        
        public void SetContent(string title, string message, string chatroomId = null)
        {
            lblTitle.Text = title;
            lblMessage.Text = message;
            _chatroomId = chatroomId;
        }
          public void ShowToast()
        {
            this.Visible = true;
            
            // Setup slide-in animation
            if (this.Parent != null)
            {
                _startX = this.Parent.Width; // Start from right edge
                _targetX = this.Left; // Target position
                this.Left = _startX; // Position off-screen initially
                _slideTimer.Start();
            }
            
            _displayTimer.Start();
        }
        
        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            int currentX = this.Left;
            int step = (currentX - _targetX) / 5; // Smooth deceleration
            
            if (step <= 1)
            {
                this.Left = _targetX;
                _slideTimer.Stop();
            }
            else
            {
                this.Left = currentX - step;
            }
        }
        
        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            _displayTimer.Stop();
            StartFadeOut();
        }
        
        private void StartFadeOut()
        {
            if (!_isClosing)
            {
                _isClosing = true;
                _fadeTimer.Start();
            }
        }
        
        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            _opacity -= 0.05;
            if (_opacity <= 0)
            {
                _fadeTimer.Stop();
                this.Visible = false;
                this.Parent?.Controls.Remove(this);
                this.Dispose();
            }
            else
            {
                this.Invalidate(); // Trigger repaint for fade effect
            }
        }        private void ToastNotification_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Draw drop shadow
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                g.FillRectangle(shadowBrush, 3, 3, this.Width - 3, this.Height - 3);
            }
            
            // Draw main background with rounded corners effect
            using (SolidBrush bgBrush = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(bgBrush, 0, 0, this.Width - 3, this.Height - 3);
            }
            
            // Draw border
            using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
            {
                g.DrawRectangle(pen, 0, 0, this.Width - 4, this.Height - 4);
            }
            
            // Apply opacity effect
            if (_opacity < 1.0)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)(255 * (1 - _opacity)), this.BackColor)))
                {
                    g.FillRectangle(brush, 0, 0, this.Width - 3, this.Height - 3);
                }
            }
        }
        
        private void PicIcon_Paint(object sender, PaintEventArgs e)
        {
            // Draw a simple message icon
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Background circle
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 122, 255)))
            {
                g.FillEllipse(brush, 2, 2, 28, 28);
            }
            
            // Message bubble icon
            using (Pen pen = new Pen(Color.White, 2))
            {
                // Outer bubble
                g.DrawRectangle(pen, 8, 10, 16, 10);
                // Inner lines (message lines)
                g.DrawLine(pen, 10, 13, 18, 13);
                g.DrawLine(pen, 10, 16, 16, 16);
                // Bubble tail
                Point[] tail = { new Point(12, 20), new Point(15, 23), new Point(18, 20) };
                g.DrawLines(pen, tail);
            }
        }
        
        private void ToastNotification_Click(object sender, EventArgs e)
        {
            ToastClicked?.Invoke(this, _chatroomId);
            StartFadeOut();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            StartFadeOut();
        }
          protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _displayTimer?.Dispose();
                _fadeTimer?.Dispose();
                _slideTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
