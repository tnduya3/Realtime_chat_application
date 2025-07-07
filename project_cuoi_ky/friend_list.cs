using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project_cuoi_ky.Services;
using project_cuoi_ky.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace project_cuoi_ky
{
    public partial class friend_list : Form
    {
        private const string ApiBaseUrl = "https://localhost:7092/api/";
        private const string SignalRHubUrl = "https://localhost:7092/chathub";
        
        private FriendService _friendService;
        private SignalRService _signalRService;
        private int _currentUserId;
        private List<project_cuoi_ky.Models.ApiUserInfo> _allUsers;
        private List<project_cuoi_ky.Models.FriendInfo> _friends;
        private List<project_cuoi_ky.Models.ApiUserInfo> _pendingRequests;
        private List<project_cuoi_ky.Models.OnlineUser> _onlineUsers;
        private string _searchText = "";
        private Dictionary<int, project_cuoi_ky.Models.FriendshipStatus> _statusCache;
        private Dictionary<int, string> _rawStatusCache;
        private bool _isLoadingStatuses = false;
        
        public friend_list()
        {
            InitializeComponent();
            
            LoadUserInfoFromSettings();
            
            // Initialize services
            _friendService = new FriendService(ApiBaseUrl);
            _signalRService = new SignalRService(SignalRHubUrl);
            _allUsers = new List<ApiUserInfo>();
            _friends = new List<FriendInfo>();
            _pendingRequests = new List<ApiUserInfo>();
            _onlineUsers = new List<OnlineUser>();
            _statusCache = new Dictionary<int, FriendshipStatus>();
            _rawStatusCache = new Dictionary<int, string>();
            
            // Setup SignalR events
            SetupSignalREvents();
        }
        
        private void LoadUserInfoFromSettings()
        {
            try
            {
                if (Properties.Settings.Default.IsLoggedIn && 
                    !string.IsNullOrEmpty(Properties.Settings.Default.UserId))
                {
                    if (int.TryParse(Properties.Settings.Default.UserId, out int userId))
                    {
                        _currentUserId = userId;
                    }
                    else
                    {
                        _currentUserId = 1; // Fallback
                    }
                }
                else
                {
                    MessageBox.Show("Please login first.", "Not logged in", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user info: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _currentUserId = 1; // Fallback
            }
        }
        
        private async void friend_list_Load(object sender, EventArgs e)
        {
            await InitializeSignalR();
            await LoadAllData();
        }
        
        private async Task InitializeSignalR()
        {
            try
            {
                await _signalRService.InitializeAsync();
                await _signalRService.RegisterUserAsync(_currentUserId);
            }
            catch (Exception ex)
            {
                lblOnlineUsersStatus.Text = $"SignalR connection failed: {ex.Message}";
                lblOnlineUsersStatus.ForeColor = Color.Red;
            }
        }
        
        private void SetupSignalREvents()
        {
            _signalRService.ConnectionStatusChanged += OnSignalRStatusChanged;
        }
        
        private void OnSignalRStatusChanged(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { OnSignalRStatusChanged(status); });
                return;
            }
            
            lblOnlineUsersStatus.Text = status;
            lblOnlineUsersStatus.ForeColor = status.Contains("Connected") ? Color.Green : Color.Orange;
        }
        
        private async Task LoadAllData()
        {
            // Clear cache when refreshing
            _statusCache.Clear();
            _rawStatusCache.Clear();
            
            await LoadAllUsers();
            await LoadFriends();
            await LoadPendingRequests();
            await LoadOnlineUsers();
            
            // Load statuses for all users in background
            await LoadFriendshipStatuses();
        }
        
        private async Task LoadAllUsers()
        {
            try
            {
                lblAllUsersStatus.Text = "Loading users...";
                lblAllUsersStatus.ForeColor = Color.Blue;
                
                var response = await _friendService.GetAllUsers();
                  
                if (response?.Users?.Count > 0)
                {
                    _allUsers = response.Users;
                    
                    // Filter out current user
                    _allUsers = _allUsers.Where(u => u.userId != _currentUserId).ToList();
                    
                    await DisplayAllUsers();
                    lblAllUsersStatus.Text = $"Found {_allUsers.Count} users";
                    lblAllUsersStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblAllUsersStatus.Text = "No users found";
                    lblAllUsersStatus.ForeColor = Color.Orange;
                }
            }
            catch (Exception ex)
            {
                lblAllUsersStatus.Text = $"Error loading users: {ex.Message}";
                lblAllUsersStatus.ForeColor = Color.Red;
            }
        }
        
        private async Task LoadFriends()
        {
            try
            {
                lblFriendsStatus.Text = "Loading friends...";
                lblFriendsStatus.ForeColor = Color.Blue;
                
                var response = await _friendService.GetUserFriends(_currentUserId);
                
                if (response?.Friends?.Count > 0)
                {
                    _friends = response.Friends;
                    DisplayFriends();
                    lblFriendsStatus.Text = $"You have {_friends.Count} friends";
                    lblFriendsStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblFriendsStatus.Text = "No friends found";
                    lblFriendsStatus.ForeColor = Color.Orange;
                }
            }
            catch (Exception ex)
            {
                lblFriendsStatus.Text = $"Error loading friends: {ex.Message}";
                lblFriendsStatus.ForeColor = Color.Red;
            }
        }
        
        private async Task LoadPendingRequests()
        {
            try
            {
                lblRequestsStatus.Text = "Loading requests...";
                lblRequestsStatus.ForeColor = Color.Blue;
                
                var response = await _friendService.GetPendingRequests(_currentUserId);                
                if (response?.Requests?.Count > 0)
                {
                    _pendingRequests = response.Requests;
                    DisplayPendingRequests();
                    lblRequestsStatus.Text = $"You have {_pendingRequests.Count} pending requests";
                    lblRequestsStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblRequestsStatus.Text = "No pending requests";
                    lblRequestsStatus.ForeColor = Color.Orange;
                }
            }
            catch (Exception ex)
            {
                lblRequestsStatus.Text = $"Error loading requests: {ex.Message}";
                lblRequestsStatus.ForeColor = Color.Red;
            }
        }        
        
        private async Task DisplayAllUsers()
        {
            pnlAllUsers.Controls.Clear();
            
            var filteredUsers = _allUsers;
            
            // Apply search filter
            if (!string.IsNullOrEmpty(_searchText) && _searchText != "Search users...")
            {
                filteredUsers = _allUsers.Where(u => 
                    u.userName.ToLower().Contains(_searchText.ToLower()) ||
                    u.email.ToLower().Contains(_searchText.ToLower())).ToList();
            }
            
            foreach (var user in filteredUsers)
            {
                var userCard = new UserCard();
                var statusResult = await GetFriendshipStatus(user.userId);
                userCard.SetUserInfo(user, statusResult.status, statusResult.rawStatus);
                userCard.ActionClicked += OnUserCardActionClicked;
                userCard.Width = pnlAllUsers.Width - 25;
                
                pnlAllUsers.Controls.Add(userCard);
            }
        }
        
        private void DisplayFriends()
        {
            pnlFriends.Controls.Clear();
            
            foreach (var friend in _friends)
            {
                var userInfo = new ApiUserInfo 
                { 
                    userId = friend.userId, 
                    userName = friend.userName, 
                    email = friend.email 
                };
                
                var userCard = new UserCard();
                userCard.SetUserInfo(userInfo, FriendshipStatus.Friend);
                userCard.ActionClicked += OnUserCardActionClicked;
                userCard.Width = pnlFriends.Width - 25;
                
                pnlFriends.Controls.Add(userCard);
            }
        }        
        
        private void DisplayPendingRequests()
        {
            pnlRequests.Controls.Clear();
            
            foreach (var userInfo in _pendingRequests)
            {
                var userCard = new UserCard();
                userCard.SetUserInfo(userInfo, FriendshipStatus.RequestReceived);
                userCard.ActionClicked += OnUserCardActionClicked;
                userCard.Width = pnlRequests.Width - 25;
                
                pnlRequests.Controls.Add(userCard);
            }
        }
        
        private async Task LoadOnlineUsers()
        {
            try
            {
                lblOnlineUsersStatus.Text = "Loading online users...";
                lblOnlineUsersStatus.ForeColor = Color.Blue;
                
                if (!_signalRService.IsConnected)
                {
                    lblOnlineUsersStatus.Text = "SignalR not connected";
                    lblOnlineUsersStatus.ForeColor = Color.Orange;
                    return;
                }
                
                // Setup event handler for online users response
                var tcs = new TaskCompletionSource<bool>();
                Action<List<OnlineUser>, int, DateTime> onlineUsersHandler = null;
                
                onlineUsersHandler = (users, totalCount, timestamp) =>
                {
                    _onlineUsers = users ?? new List<OnlineUser>();
                    this.Invoke((MethodInvoker)delegate
                    {
                        DisplayOnlineUsers();
                        lblOnlineUsersStatus.Text = $"Found {_onlineUsers.Count} users online";
                        lblOnlineUsersStatus.ForeColor = Color.Green;
                    });
                    
                    // Remove event handler
                    _signalRService.OnlineUsersReceived -= onlineUsersHandler;
                    tcs.SetResult(true);
                };
                
                _signalRService.OnlineUsersReceived += onlineUsersHandler;
                
                // Request online users from SignalR Hub
                await _signalRService.GetAllOnlineUsersAsync();
                
                // Wait for response with timeout
                var timeoutTask = Task.Delay(5000);
                var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);
                
                if (completedTask == timeoutTask)
                {
                    _signalRService.OnlineUsersReceived -= onlineUsersHandler;
                    lblOnlineUsersStatus.Text = "Timeout loading online users";
                    lblOnlineUsersStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblOnlineUsersStatus.Text = $"Error loading online users: {ex.Message}";
                lblOnlineUsersStatus.ForeColor = Color.Red;
            }
        }
        
        private void DisplayOnlineUsers()
        {
            pnlOnlineUsers.Controls.Clear();
            
            if (_onlineUsers == null || _onlineUsers.Count == 0)
            {
                var noUsersLabel = new Label
                {
                    Text = "No users currently online",
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 10),
                    Padding = new Padding(10)
                };
                pnlOnlineUsers.Controls.Add(noUsersLabel);
                return;
            }
            
            // Filter out current user
            var filteredUsers = _onlineUsers.Where(u => u.userId != _currentUserId).ToList();
            
            // Apply search filter if any
            if (!string.IsNullOrEmpty(_searchText) && _searchText != "Search users...")
            {
                filteredUsers = filteredUsers.Where(u => 
                    u.username.ToLower().Contains(_searchText.ToLower())).ToList();
            }
            
            foreach (var onlineUser in filteredUsers)
            {
                // Convert OnlineUser to ApiUserInfo for UserCard
                var userInfo = new ApiUserInfo
                {
                    userId = onlineUser.userId,
                    userName = onlineUser.username,
                    email = "", // Online users might not have email info
                    isOnline = true
                };
                
                var userCard = new UserCard();
                userCard.SetOnlineUserInfo(onlineUser, userInfo);
                userCard.ActionClicked += OnUserCardActionClicked;
                userCard.Width = pnlOnlineUsers.Width - 25;
                
                pnlOnlineUsers.Controls.Add(userCard);
            }
        }
          
        private async Task<(FriendshipStatus status, string rawStatus)> GetFriendshipStatus(int userId)
        {
            // Check cache first
            if (_statusCache.ContainsKey(userId))
            {
                var cachedStatus = _statusCache[userId];
                var cachedRawStatus = _rawStatusCache.ContainsKey(userId) ? _rawStatusCache[userId] : "";
                return (cachedStatus, cachedRawStatus);
            }
              // Check local data for quick response
            if (_friends.Any(f => f.userId == userId))
                return (FriendshipStatus.Friend, "friend");
            
            if (_pendingRequests.Any(r => r.userId == userId))
                return (FriendshipStatus.RequestReceived, "pending");
            
            // If not in cache and not loading statuses, return None for now
            if (!_isLoadingStatuses)
                return (FriendshipStatus.None, "none");
            
            // Try to get from API
            try
            {
                var result = await _friendService.GetFriendshipStatus(_currentUserId, userId);
                _statusCache[userId] = result.status;
                _rawStatusCache[userId] = result.rawStatus;
                return result;
            }
            catch
            {
                return (FriendshipStatus.None, "error");
            }
        }
        
        private async Task LoadFriendshipStatuses()
        {
            if (_allUsers.Count == 0) return;
            
            _isLoadingStatuses = true;
            lblAllUsersStatus.Text = "Loading friendship statuses...";
            lblAllUsersStatus.ForeColor = Color.Blue;
            
            try
            {
                // Load statuses in small batches to avoid overwhelming the API
                var batchSize = 5;
                for (int i = 0; i < _allUsers.Count; i += batchSize)
                {
                    var batch = _allUsers.Skip(i).Take(batchSize);                    
                    var tasks = batch.Select(async user =>
                    {
                        try
                        {
                            var result = await _friendService.GetFriendshipStatus(_currentUserId, user.userId);
                            _statusCache[user.userId] = result.status;
                            _rawStatusCache[user.userId] = result.rawStatus;
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error loading status for user {user.userId}: {ex.Message}");
                        }
                    });
                    
                    await Task.WhenAll(tasks);
                    if (tabControl.SelectedIndex == 0) // All Users tab
                    {
                        // Run DisplayAllUsers on UI thread (fire and forget)
                        this.BeginInvoke(new Action(async () => await DisplayAllUsers()));
                    }
                    
                    await Task.Delay(200);
                }
                
                lblAllUsersStatus.Text = $"Found {_allUsers.Count} users";
                lblAllUsersStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblAllUsersStatus.Text = $"Error loading statuses: {ex.Message}";
                lblAllUsersStatus.ForeColor = Color.Red;
            }
            finally
            {
                _isLoadingStatuses = false;
            }
        }
          
        private async void OnUserCardActionClicked(object sender, (int friendId, string action) args)
        {
            try
            {
                bool success = false;
                
                switch (args.action)
                {
                    case "send_request":
                        success = await _friendService.SendFriendRequest(_currentUserId, args.friendId);
                        if (success)
                        {
                            // Update cache immediately
                            _statusCache[args.friendId] = FriendshipStatus.RequestSent;
                            
                            MessageBox.Show("Friend request sent successfully!", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadAllData(); // Refresh data
                        }
                        break;
                          
                    case "accept_request":
                        success = await _friendService.AcceptFriendRequest(_currentUserId, args.friendId);
                        if (success)
                        {
                            // Update cache immediately
                            _statusCache[args.friendId] = FriendshipStatus.Friend;
                            
                            MessageBox.Show("Friend request accepted!", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadAllData(); // Refresh data
                        }
                        else
                        {
                            MessageBox.Show("Failed to accept friend request. Please check your connection and try again.", "Accept Failed", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                        
                    case "unfriend":
                        var result = MessageBox.Show("Are you sure you want to unfriend this user?", 
                            "Confirm Unfriend", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            success = await _friendService.UnfriendUser(_currentUserId, args.friendId);
                            if (success)
                            {
                                // Update cache immediately
                                _statusCache[args.friendId] = FriendshipStatus.None;
                                
                                MessageBox.Show("User unfriended successfully!", "Success", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadAllData(); // Refresh data
                            }
                        }
                        break;
                        
                    case "reject_request":
                        success = await _friendService.RejectFriendRequest(_currentUserId, args.friendId);
                        if (success)
                        {
                            // Update cache immediately
                            _statusCache[args.friendId] = FriendshipStatus.None;
                            
                            MessageBox.Show("Friend request rejected!", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadAllData(); // Refresh data
                        }
                        break;
                        
                    case "start_chat":
                        await HandleStartChat(args.friendId);
                        break;
                }
                  if (!success && args.action != "unfriend" && args.action != "accept_request" && args.action != "start_chat")
                {
                    MessageBox.Show("Operation failed. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAllData();
        }
        
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search users...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search users...";
                txtSearch.ForeColor = Color.Gray;
            }
        }
          
        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _searchText = txtSearch.Text;
            
            // Filter based on current tab
            switch (tabControl.SelectedIndex)
            {
                case 0: // All Users tab
                    await DisplayAllUsers();
                    break;
                case 3: // Online Users tab
                    DisplayOnlineUsers();
                    break;
            }
        }
          
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _friendService?.Dispose();
            _ = Task.Run(async () => await _signalRService?.DisconnectAsync());
            base.OnFormClosing(e);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task HandleStartChat(int friendId)
        {
            try
            {
                // Find friend info
                var friendInfo = _allUsers.FirstOrDefault(u => u.userId == friendId);
                if (friendInfo == null)
                {
                    // Try to find in friends list
                    var friend = _friends.FirstOrDefault(f => f.userId == friendId);
                    if (friend != null)
                    {
                        friendInfo = new ApiUserInfo
                        {
                            userId = friend.userId,
                            userName = friend.userName,
                            email = friend.email
                        };
                    }
                }
                
                if (friendInfo == null)
                {
                    MessageBox.Show("Friend information not found.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Open start chat form
                using (var startChatForm = new startchat(friendInfo))
                {
                    if (startChatForm.ShowDialog() == DialogResult.OK && startChatForm.MessageSent)
                    {
                        var initialMessage = startChatForm.InitialMessage;
                        
                        // Call API to start chat
                        bool success = await _friendService.StartChatWithFriend(_currentUserId, friendId, initialMessage);
                        
                        if (success)
                        {
                            MessageBox.Show($"Chat started successfully with {friendInfo.userName}!", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // Close friend list and return to home form
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to start chat. Please try again.", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting chat: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
