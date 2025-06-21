using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace project_cuoi_ky
{
    public partial class ParticipantsListForm : Form
    {
        private List<ChatroomParticipant> _participants;
        private string _chatroomName;
        private int _chatroomId;
        private int _currentUserId;
        
        public ParticipantsListForm()
        {
            InitializeComponent();
            SetupForm();
        }

        // Event args for adding user to chatroom
        public class AddUserEventArgs : EventArgs
        {
            public int UserId { get; set; }
            public int ChatroomId { get; set; }
            public int AddedBy { get; set; }
        }


        public ParticipantsListForm(string chatroomName, List<ChatroomParticipant> participants, int chatroomId = 0, int currentUserId = 0) : this()
        {
            _chatroomName = chatroomName;
            _participants = participants ?? new List<ChatroomParticipant>();
            _chatroomId = chatroomId;
            _currentUserId = currentUserId;
            LoadParticipants();
        }
        
        private void SetupForm()
        {
            this.Text = "Chatroom Members";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            
            // Setup tooltips
            var tooltip = new ToolTip();
            tooltip.SetToolTip(txtUserId, "Enter the User ID of the person you want to add to this chatroom");
            tooltip.SetToolTip(btnAdd, "Add user as a member to this chatroom");
            tooltip.SetToolTip(btnRefresh, "Refresh the members list");
            
            // Setup placeholder-like behavior
            txtUserId.Text = "Enter User ID...";
            txtUserId.ForeColor = Color.Gray;
            txtUserId.Enter += txtUserId_Enter;
            txtUserId.Leave += txtUserId_Leave;
        }
        
        private void LoadParticipants()
        {
            try
            {
                // Update form title
                this.Text = $"Members - {_chatroomName}";
                
                // Update header label
                if (_participants?.Count > 0)
                {
                    lblHeader.Text = $"Members ({_participants.Count})";
                    LoadParticipantsList();
                }
                else
                {
                    lblHeader.Text = "No members found";
                    ShowNoMembersMessage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading participants: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadParticipantsList()
        {
            lstParticipants.Items.Clear();
            
            // Sort participants: owners first, then members
            var sortedParticipants = _participants
                .OrderBy(p => p.role == "owner" ? 0 : 1)
                .ThenBy(p => p.userName)
                .ToList();
            
            foreach (var participant in sortedParticipants)
            {
                var item = new ListViewItem();
                item.Text = participant.userName;
                item.SubItems.Add(GetRoleDisplayText(participant.role));
                item.Tag = participant;
                
                // Set different colors based on role
                if (participant.role.ToLower() == "owner")
                {
                    item.ForeColor = Color.DarkBlue;
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }
                else if (participant.role.ToLower() == "admin")
                {
                    item.ForeColor = Color.DarkGreen;
                }
                
                lstParticipants.Items.Add(item);
            }
        }
        
        private string GetRoleDisplayText(string role)
        {
            switch (role?.ToLower())
            {
                case "owner":
                    return "👑 Owner";
                case "admin":
                    return "⚡ Admin";
                case "member":
                default:
                    return "👤 Member";
            }
        }
        
        private void ShowNoMembersMessage()
        {
            lstParticipants.Items.Clear();
            var item = new ListViewItem("No members found");
            item.ForeColor = Color.Gray;
            item.Font = new Font(lstParticipants.Font, FontStyle.Italic);
            lstParticipants.Items.Add(item);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Raise event to parent form to refresh data
            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }
          
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Raise event to parent form to add user
            var userIdText = txtUserId.Text.Trim();
            
            // Check for placeholder text
            if (string.IsNullOrEmpty(userIdText) || userIdText == "Enter User ID...")
            {
                MessageBox.Show("Please enter a User ID.", "Input Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserId.Focus();
                return;
            }
            
            if (!int.TryParse(userIdText, out int userId))
            {
                MessageBox.Show("Please enter a valid User ID (numbers only).", "Invalid Input", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserId.Focus();
                txtUserId.SelectAll();
                return;
            }
            
            // Validate userId is positive
            if (userId <= 0)
            {
                MessageBox.Show("User ID must be a positive number.", "Invalid Input", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserId.Focus();
                txtUserId.SelectAll();
                return;
            }
            
            // Check if user is already a member
            if (_participants?.Any(p => p.userId == userId) == true)
            {
                MessageBox.Show("This user is already a member of the chatroom.", "User Already Exists", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserId.Focus();
                txtUserId.SelectAll();
                return;
            }
            
            // Raise event to parent form
            AddUserRequested?.Invoke(this, new AddUserEventArgs 
            { 
                UserId = userId, 
                ChatroomId = _chatroomId, 
                AddedBy = _currentUserId 
            });
        }
        
        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow numbers and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
            // Handle Enter key
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnAddUser_Click(sender, e);
            }
        }
        
        private void txtUserId_Enter(object sender, EventArgs e)
        {
            if (txtUserId.Text == "Enter User ID..." && txtUserId.ForeColor == Color.Gray)
            {
                txtUserId.Text = "";
                txtUserId.ForeColor = Color.Black;
            }
        }
        
        private void txtUserId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                txtUserId.Text = "Enter User ID...";
                txtUserId.ForeColor = Color.Gray;
            }
        }
        
        // Event to notify parent form to refresh participants data
        public event EventHandler RefreshRequested;
        
        // Event to notify parent form to add user
        public event EventHandler<AddUserEventArgs> AddUserRequested;
        
        // Method to update participants from parent form
        public void UpdateParticipants(List<ChatroomParticipant> participants)
        {
            _participants = participants ?? new List<ChatroomParticipant>();
            LoadParticipants();
        }
          // Clear the add user textbox
        public void ClearAddUserInput()
        {
            txtUserId.Text = "Enter User ID...";
            txtUserId.ForeColor = Color.Gray;
            txtUserId.Focus();
        }
    }
}
