using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using project_cuoi_ky.Services;
using project_cuoi_ky.Models;
using project_cuoi_ky.Utils;

namespace project_cuoi_ky
{
    public partial class ParticipantsListForm : Form
    {
        private List<ChatroomParticipant> _participants;
        private string _chatroomName;
        private int _chatroomId;
        private int _currentUserId;
        private FriendService _friendService;
        private List<FriendInfo> _friendsList;
        private bool _isUpdatingComboBox = false;
          
        public ParticipantsListForm()
        {
            InitializeComponent();
            _friendService = new FriendService("https://localhost:7092/api/");
            _friendsList = new List<FriendInfo>();
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
            tooltip.SetToolTip(cmbFriends, "Select a friend to add to this chatroom");
            tooltip.SetToolTip(btnAdd, "Add selected friend to this chatroom");
            tooltip.SetToolTip(btnRefresh, "Refresh the members list");
            
            // Setup ComboBox
            SetupFriendsComboBox();
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
                
                // Load friends list for ComboBox
                LoadFriendsList();
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
            try
            {
                // Check if a friend is selected
                if (cmbFriends.SelectedItem == null || !(cmbFriends.SelectedItem is FriendInfo selectedFriend))
                {
                    MessageBox.Show("Please select a friend to add to the chatroom.", "No Friend Selected", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbFriends.Focus();
                    return;
                }
                
                // Validate selected friend
                if (selectedFriend.userId <= 0)
                {
                    MessageBox.Show("Invalid friend selection. Please try again.", "Invalid Selection", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbFriends.Focus();
                    return;
                }
                
                // Check if user is already a member (double-check)
                if (_participants?.Any(p => p.userId == selectedFriend.userId) == true)
                {
                    MessageBox.Show($"{selectedFriend.userName} is already a member of this chatroom.", "User Already Exists", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbFriends.Focus();
                    return;
                }
                
                // Raise event to parent form
                AddUserRequested?.Invoke(this, new AddUserEventArgs 
                { 
                    UserId = selectedFriend.userId, 
                    ChatroomId = _chatroomId, 
                    AddedBy = _currentUserId 
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
          
        private void SetupFriendsComboBox()
        {
            cmbFriends.Text = "Type friend name...";
            cmbFriends.ForeColor = Color.Gray;
            cmbFriends.DisplayMember = "userName";
            cmbFriends.ValueMember = "userId";
            
            // Add events
            cmbFriends.TextChanged += cmbFriends_TextChanged;
            cmbFriends.Leave += cmbFriends_Leave;
        }
          
        private async void LoadFriendsList()
        {
            try
            {
                if (_currentUserId <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Cannot load friends: Invalid current user ID");
                    return;
                }
                
                var friendsResponse = await _friendService.GetUserFriends(_currentUserId);
                if (friendsResponse?.Friends != null)
                {
                    _friendsList = friendsResponse.Friends.ToList();
                    UpdateFriendsComboBox();
                }
                else
                {
                    _friendsList = new List<FriendInfo>();
                    cmbFriends.Text = "Failed to load friends";
                    cmbFriends.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading friends: {ex.Message}");
                _friendsList = new List<FriendInfo>();
                cmbFriends.Text = "Error loading friends";
                cmbFriends.ForeColor = Color.Red;
            }
        }
        
        private void UpdateFriendsComboBox()
        {
            if (_isUpdatingComboBox) return; // Prevent recursion
            
            try
            {
                _isUpdatingComboBox = true;
                
                // Clear current items safely
                cmbFriends.BeginUpdate();
                cmbFriends.DataSource = null;
                cmbFriends.Items.Clear();
                
                if (_friendsList?.Count > 0)
                {
                    // Filter out friends who are already participants
                    var availableFriends = _friendsList.Where(f => 
                        !_participants.Any(p => p.userId == f.userId)).ToList();
                    
                    if (availableFriends.Count > 0)
                    {
                        cmbFriends.DataSource = availableFriends;
                        cmbFriends.SelectedIndex = -1; // No selection initially
                        cmbFriends.Text = "Type friend name...";
                        cmbFriends.ForeColor = Color.Gray;
                    }
                    else
                    {
                        cmbFriends.Text = "All friends are already members";
                        cmbFriends.ForeColor = Color.Gray;
                    }
                }
                else
                {
                    cmbFriends.Text = "No friends available";
                    cmbFriends.ForeColor = Color.Gray;
                }
                
                cmbFriends.EndUpdate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating ComboBox: {ex.Message}");
                cmbFriends.EndUpdate();
            }
            finally
            {
                _isUpdatingComboBox = false;
            }
        }        // ComboBox event handlers
        private void cmbFriends_Enter(object sender, EventArgs e)
        {
            try
            {
                // Load friends list if not loaded yet
                if (_friendsList == null || _friendsList.Count == 0)
                {
                    LoadFriendsList();
                }
                
                // Clear placeholder text and prepare for typing
                if (cmbFriends.Text == "Type friend name..." || cmbFriends.Text == "Select a friend..." || 
                    cmbFriends.Text == "No friends available" || 
                    cmbFriends.Text == "Failed to load friends" || cmbFriends.Text == "Error loading friends" ||
                    cmbFriends.Text == "All friends are already members")
                {
                    _isUpdatingComboBox = true;
                    cmbFriends.Text = "";
                    cmbFriends.ForeColor = Color.Black;
                    _isUpdatingComboBox = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Enter event: {ex.Message}");
            }
        }
        
        private void cmbFriends_Leave(object sender, EventArgs e)
        {
            // If text is empty, restore placeholder
            if (string.IsNullOrWhiteSpace(cmbFriends.Text))
            {
                _isUpdatingComboBox = true;
                cmbFriends.Text = "Type friend name...";
                cmbFriends.ForeColor = Color.Gray;
                _isUpdatingComboBox = false;
            }
        }
        
        private void cmbFriends_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle Enter key
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnAddUser_Click(sender, e);
            }
        }
        
        // Legacy TextBox event handlers (keeping for compatibility, but hidden)
        
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
          
        // Clear the add user input
        public void ClearAddUserInput()
        {
            _isUpdatingComboBox = true;
            cmbFriends.SelectedIndex = -1;
            cmbFriends.Text = "Type friend name...";
            cmbFriends.ForeColor = Color.Gray;
            _isUpdatingComboBox = false;
            cmbFriends.Focus();
            
            // Refresh friends list to update available friends
            UpdateFriendsComboBox();
        }

        private void cmbFriends_TextChanged(object sender, EventArgs e)
        {
            // Skip if we're updating programmatically or no friends loaded
            if (_isUpdatingComboBox || _friendsList?.Count == 0)
                return;
                
            // Skip if text is placeholder
            var currentText = cmbFriends.Text;
            if (string.IsNullOrWhiteSpace(currentText) || 
                currentText == "Type friend name..." || 
                currentText == "Select a friend..." ||
                currentText == "No friends available" ||
                currentText == "All friends are already members")
                return;
                
            try
            {
                _isUpdatingComboBox = true;
                
                // Filter friends based on search term
                var availableFriends = _friendsList.Where(f => 
                    !_participants.Any(p => p.userId == f.userId)).ToList();
                
                var filteredFriends = FriendSearchHelper.SearchFriends(availableFriends, currentText);
                
                // Update ComboBox items safely
                cmbFriends.BeginUpdate();
                
                // Clear DataSource first, then clear items
                var currentSelection = cmbFriends.Text;
                cmbFriends.DataSource = null;
                cmbFriends.Items.Clear();
                
                if (filteredFriends.Count > 0)
                {
                    cmbFriends.DataSource = filteredFriends;
                    cmbFriends.DroppedDown = true; // Show dropdown with results
                    cmbFriends.Text = currentSelection; // Restore user's text
                    cmbFriends.SelectionStart = currentSelection.Length; // Cursor at end
                    cmbFriends.ForeColor = Color.Black;
                }
                else
                {
                    // No results found
                    cmbFriends.Text = currentSelection;
                    cmbFriends.ForeColor = Color.Red;
                }
                
                cmbFriends.EndUpdate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in TextChanged: {ex.Message}");
                // Reset ComboBox on error
                try
                {
                    cmbFriends.DataSource = null;
                    cmbFriends.Items.Clear();
                    UpdateFriendsComboBox();
                }
                catch
                {
                    // Ignore errors in recovery
                }
            }
            finally
            {
                _isUpdatingComboBox = false;
            }
        }
    }
}
