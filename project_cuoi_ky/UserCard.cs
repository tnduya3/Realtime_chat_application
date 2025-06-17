using System;
using System.Drawing;
using System.Windows.Forms;
using project_cuoi_ky.Models;

namespace project_cuoi_ky
{
    public partial class UserCard : UserControl
    {        private Panel pnlMain;
        private PictureBox picAvatar;
        private Label lblUserName;
        private Label lblEmail;
        private Label lblStatus; // For showing raw API status
        private Button btnAction;
        private Button btnReject; // Additional button for reject action
        private ApiUserInfo _userInfo;
        private FriendshipStatus _friendshipStatus;
        
        public event EventHandler<(int friendId, string action)> ActionClicked;
        
        public int UserId => _userInfo?.userId ?? 0;
        
        public UserCard()
        {
            InitializeComponent();
            SetupLayout();
        }
          private void SetupLayout()
        {
            this.Size = new Size(300, 85); // Increased height for status label
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(5);
            
            // Main panel
            pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.White
            };
            
            // Avatar picture box
            picAvatar = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(10, 15),
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.CenterImage,
                BorderStyle = BorderStyle.FixedSingle
            };
            
            // User name label
            lblUserName = new Label
            {
                Location = new Point(70, 15),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "User Name"
            };
              // Email label
            lblEmail = new Label
            {
                Location = new Point(70, 35),
                Size = new Size(120, 16),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.Gray,
                Text = "user@email.com"
            };
            
            // Status label (for raw API status)
            lblStatus = new Label
            {
                Location = new Point(70, 52),
                Size = new Size(120, 14),
                Font = new Font("Segoe UI", 7.5f, FontStyle.Italic),
                ForeColor = Color.DarkBlue,
                Text = ""
            };
              // Action button
            btnAction = new Button
            {
                Location = new Point(200, 25),
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "Add Friend",
                UseVisualStyleBackColor = false
            };
            
            // Reject button (hidden by default)
            btnReject = new Button
            {
                Location = new Point(115, 25),
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                BackColor = Color.FromArgb(180, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "Reject",
                UseVisualStyleBackColor = false,
                Visible = false
            };
            
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Click += BtnAction_Click;
            
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.Click += BtnReject_Click;
            pnlMain.Controls.Add(picAvatar);
            pnlMain.Controls.Add(lblUserName);
            pnlMain.Controls.Add(lblEmail);
            pnlMain.Controls.Add(lblStatus);
            pnlMain.Controls.Add(btnAction);
            pnlMain.Controls.Add(btnReject);
            
            this.Controls.Add(pnlMain);
            
            // Add hover effects
            this.MouseEnter += UserCard_MouseEnter;
            this.MouseLeave += UserCard_MouseLeave;
            pnlMain.MouseEnter += UserCard_MouseEnter;
            pnlMain.MouseLeave += UserCard_MouseLeave;
        }
        
        private void UserCard_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 245, 245);
            pnlMain.BackColor = Color.FromArgb(245, 245, 245);
        }
        
        private void UserCard_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            pnlMain.BackColor = Color.White;
        }
          
        private void BtnAction_Click(object sender, EventArgs e)
        {
            if (_userInfo != null)
            {
                string action = GetActionFromStatus(_friendshipStatus);
                ActionClicked?.Invoke(this, (_userInfo.userId, action));
            }
        }
        
        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (_userInfo != null)
            {
                ActionClicked?.Invoke(this, (_userInfo.userId, "reject_request"));
            }
        }
          
        // Overload for UserInfo (from internal models)
        public void SetUserInfo(UserInfo userInfo, FriendshipStatus friendshipStatus = FriendshipStatus.None, string rawStatus = "")
        {
            // Convert UserInfo to ApiUserInfo
            var apiUserInfo = new ApiUserInfo
            {
                userId = userInfo.userId,
                userName = userInfo.userName,
                email = userInfo.email,
                isOnline = userInfo.isOnline,
                isActive = userInfo.isActive
            };
            
            // Call the main SetUserInfo method
            SetUserInfo(apiUserInfo, friendshipStatus, rawStatus);
        }
        
        // Original method for ApiUserInfo (from API responses)
        public void SetUserInfo(ApiUserInfo userInfo, FriendshipStatus friendshipStatus = FriendshipStatus.None, string rawStatus = "")
        {
            _userInfo = userInfo;
            _friendshipStatus = friendshipStatus;
            
            // Set user name
            lblUserName.Text = userInfo.userName ?? "Unknown User";
            
            // Set email
            lblEmail.Text = userInfo.email ?? "";
            
            // Set raw status if provided
            if (!string.IsNullOrEmpty(rawStatus))
            {
                lblStatus.Text = $"API Status: {rawStatus}";
                lblStatus.Visible = true;
            }
            else
            {
                lblStatus.Text = "";
                lblStatus.Visible = false;
            }
            
            // Set avatar (placeholder for now)
            picAvatar.Image = null; // Will add avatar logic later
            
            // Update button based on friendship status
            UpdateActionButton(friendshipStatus);
        }
          
        private void UpdateActionButton(FriendshipStatus status)
        {
            switch (status)
            {
                case FriendshipStatus.None:
                    btnAction.Text = "Add Friend";
                    btnAction.BackColor = Color.FromArgb(0, 120, 215);
                    btnAction.Enabled = true;
                    btnAction.Location = new Point(200, 25);
                    btnReject.Visible = false;
                    break;
                    
                case FriendshipStatus.RequestSent:
                    btnAction.Text = "Pending";
                    btnAction.BackColor = Color.Gray;
                    btnAction.Enabled = false;
                    btnAction.Location = new Point(200, 25);
                    btnReject.Visible = false;
                    break;
                    
                case FriendshipStatus.RequestReceived:
                    btnAction.Text = "Accept";
                    btnAction.BackColor = Color.FromArgb(0, 150, 0);
                    btnAction.Enabled = true;
                    btnAction.Location = new Point(200, 25);
                    btnAction.Size = new Size(80, 30);
                    btnReject.Visible = true;
                    break;
                    
                case FriendshipStatus.Friend:
                    btnAction.Text = "Friends";
                    btnAction.BackColor = Color.FromArgb(0, 150, 0);
                    btnAction.Enabled = true;
                    btnAction.Location = new Point(200, 25);
                    btnReject.Visible = false;
                    break;
                    
                case FriendshipStatus.Blocked:
                    btnAction.Text = "Blocked";
                    btnAction.BackColor = Color.Red;
                    btnAction.Enabled = false;
                    btnAction.Location = new Point(200, 25);
                    btnReject.Visible = false;
                    break;
            }
        }
          private string GetActionFromStatus(FriendshipStatus status)
        {
            switch (status)
            {
                case FriendshipStatus.None:
                    return "send_request";
                case FriendshipStatus.RequestReceived:
                    return "accept_request";
                case FriendshipStatus.Friend:
                    return "unfriend";
                case FriendshipStatus.RequestSent:
                    return "cancel_request"; // Could implement cancel request
                default:
                    return "send_request";
            }
        }
        
        public void UpdateFriendshipStatus(FriendshipStatus newStatus)
        {
            _friendshipStatus = newStatus;
            UpdateActionButton(newStatus);
        }
    }
}
