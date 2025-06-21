using System;
using System.Drawing;
using System.Windows.Forms;
using project_cuoi_ky.Models;

namespace project_cuoi_ky
{
    public partial class StartChatForm : Form
    {
        private Label lblTitle;
        private Label lblUserName;
        private Label lblEmail;
        private Label lblMessageLabel;
        private RichTextBox rtbMessage;
        private Button btnSend;
        private Button btnCancel;
        private Panel pnlMain;
        
        public string InitialMessage => rtbMessage.Text;
        public bool MessageSent { get; private set; } = false;
        
        private ApiUserInfo _friendInfo;
        
        public StartChatForm(ApiUserInfo friendInfo)
        {
            _friendInfo = friendInfo;
            InitializeComponent();
            SetupLayout();
            LoadFriendInfo();
        }
        
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartChatForm));
            this.SuspendLayout();
            // 
            // StartChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(386, 303);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Start Conversation";
            this.ResumeLayout(false);

        }
        
        private void SetupLayout()
        {
            // Main panel
            pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };
            
            // Title
            lblTitle = new Label
            {
                Text = "Start a conversation",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            // User name
            lblUserName = new Label
            {
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(20, 60),
                Size = new Size(400, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            // Email
            lblEmail = new Label
            {
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(20, 85),
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            // Message label
            lblMessageLabel = new Label
            {
                Text = "Initial message:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(20, 120),
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            // Message text box
            rtbMessage = new RichTextBox
            {
                Location = new Point(20, 145),
                Size = new Size(410, 120),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Text = "Hi! How are you?"
            };
            
            // Send button
            btnSend = new Button
            {
                Text = "Send Message",
                Location = new Point(250, 280),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            
            // Cancel button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(360, 280),
                Size = new Size(70, 35),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(200, 200, 200),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            
            btnSend.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.BorderSize = 0;
            
            // Event handlers
            btnSend.Click += BtnSend_Click;
            btnCancel.Click += BtnCancel_Click;
            rtbMessage.KeyDown += RtbMessage_KeyDown;
            
            // Add controls to panel
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Controls.Add(lblUserName);
            pnlMain.Controls.Add(lblEmail);
            pnlMain.Controls.Add(lblMessageLabel);
            pnlMain.Controls.Add(rtbMessage);
            pnlMain.Controls.Add(btnSend);
            pnlMain.Controls.Add(btnCancel);
            
            this.Controls.Add(pnlMain);
        }
        
        private void LoadFriendInfo()
        {
            if (_friendInfo != null)
            {
                lblUserName.Text = $"To: {_friendInfo.userName}";
                lblEmail.Text = _friendInfo.email;
            }
        }
        
        private void RtbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            // Send message with Ctrl+Enter
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                BtnSend_Click(sender, e);
                e.Handled = true;
            }
        }
        
        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbMessage.Text))
            {
                MessageBox.Show("Please enter a message.", "Empty Message", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbMessage.Focus();
                return;
            }
            
            MessageSent = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
