using System;
using System.Drawing;
using System.Windows.Forms;

namespace project_cuoi_ky
{
    public partial class ChatroomCard : UserControl
    {
        private Panel pnlMain;
        private PictureBox picAvatar;
        private Label lblChatroomName;
        private Label lblLastMessage;
        private Label lblTime;
        private Label lblUnreadCount;
          private ChatroomInfo _chatroomInfo;
        
        public event EventHandler<int> ChatroomSelected;
        
        public int ChatroomId => _chatroomInfo?.ChatRoomId ?? 0;
        
        public ChatroomCard()
        {
            InitializeComponent();
            SetupLayout();
        }
        
        private void SetupLayout()
        {
            this.Size = new Size(300, 70);
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;
            
            // Main panel
            pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 8, 10, 8),
                BackColor = Color.White
            };
            
            // Avatar picture box
            picAvatar = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(10, 10),
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            
            // Chatroom name label
            lblChatroomName = new Label
            {
                Location = new Point(70, 12),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "Chatroom Name"
            };
            
            // Last message label
            lblLastMessage = new Label
            {
                Location = new Point(70, 32),
                Size = new Size(150, 16),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.Gray,
                Text = "Last message preview..."
            };
            
            // Time label
            lblTime = new Label
            {
                Location = new Point(230, 12),
                Size = new Size(60, 16),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.TopRight,
                Text = "12:30"
            };
            
            // Unread count label
            lblUnreadCount = new Label
            {
                Location = new Point(240, 32),
                Size = new Size(20, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 120, 215), // Blue color
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                Text = "3"
            };
            
            // Add controls to main panel
            pnlMain.Controls.Add(picAvatar);
            pnlMain.Controls.Add(lblChatroomName);
            pnlMain.Controls.Add(lblLastMessage);
            pnlMain.Controls.Add(lblTime);
            pnlMain.Controls.Add(lblUnreadCount);
            
            this.Controls.Add(pnlMain);
            
            // Add hover effects
            this.MouseEnter += ChatroomCard_MouseEnter;
            this.MouseLeave += ChatroomCard_MouseLeave;
            pnlMain.MouseEnter += ChatroomCard_MouseEnter;
            pnlMain.MouseLeave += ChatroomCard_MouseLeave;
            
            // Add click events
            this.Click += ChatroomCard_Click;
            pnlMain.Click += ChatroomCard_Click;
            picAvatar.Click += ChatroomCard_Click;
            lblChatroomName.Click += ChatroomCard_Click;
            lblLastMessage.Click += ChatroomCard_Click;
        }
        
        private void ChatroomCard_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 245, 245);
            pnlMain.BackColor = Color.FromArgb(245, 245, 245);
        }
        
        private void ChatroomCard_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            pnlMain.BackColor = Color.White;
        }
        
        private void ChatroomCard_Click(object sender, EventArgs e)
        {
            if (_chatroomInfo != null)
            {
                ChatroomSelected?.Invoke(this, _chatroomInfo.ChatRoomId);
            }
        }
        
        public void SetChatroomInfo(ChatroomInfo chatroomInfo, string lastMessage = "", DateTime? lastMessageTime = null, int unreadCount = 0)
        {
            _chatroomInfo = chatroomInfo;
            
            // Set chatroom name
            lblChatroomName.Text = chatroomInfo.Name;
            
            // Set last message
            if (!string.IsNullOrEmpty(lastMessage))
            {
                lblLastMessage.Text = lastMessage.Length > 30 ? lastMessage.Substring(0, 30) + "..." : lastMessage;
            }
            else
            {
                lblLastMessage.Text = chatroomInfo.IsGroup ? "Group chat" : "Private chat";
            }
            
            // Set time
            if (lastMessageTime.HasValue)
            {
                var time = lastMessageTime.Value;
                if (time.Date == DateTime.Today)
                {
                    lblTime.Text = time.ToString("HH:mm");
                }
                else if (time.Date == DateTime.Today.AddDays(-1))
                {
                    lblTime.Text = "Yesterday";
                }
                else
                {
                    lblTime.Text = time.ToString("dd/MM");
                }
            }
            else
            {
                lblTime.Text = chatroomInfo.CreatedAt.ToString("dd/MM");
            }
            
            // Set unread count
            if (unreadCount > 0)
            {
                lblUnreadCount.Text = unreadCount > 99 ? "99+" : unreadCount.ToString();
                lblUnreadCount.Visible = true;
            }
            else
            {
                lblUnreadCount.Visible = false;
            }
            
            // Set avatar based on chatroom type
            if (chatroomInfo.IsGroup)
            {
                picAvatar.BackColor = Color.FromArgb(0, 120, 215);
                // You can set a group icon here
            }
            else
            {
                picAvatar.BackColor = Color.FromArgb(76, 175, 80);
                // You can set a user icon here
            }
        }
        
        public void SetSelected(bool selected)
        {
            if (selected)
            {
                this.BackColor = Color.FromArgb(230, 240, 250);
                pnlMain.BackColor = Color.FromArgb(230, 240, 250);
            }
            else
            {
                this.BackColor = Color.White;
                pnlMain.BackColor = Color.White;
            }
        }
    }
}
