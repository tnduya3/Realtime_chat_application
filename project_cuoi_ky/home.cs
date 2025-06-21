using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using project_cuoi_ky.Services;

namespace project_cuoi_ky
{
    public partial class home : Form
    {
        private const string SignalRHubUrl = "https://localhost:7092/chathub";
        private const string ApiBaseUrl = "https://localhost:7092/api/";

        // Services
        private SignalRService _signalRService;
        private ApiService _apiService;
        private ChatroomManager _chatroomManager;
        private MessageManager _messageManager;
        private ToastManager _toastManager;

        // User ID và Chatroom ID hiện tại
        private int _currentUserId = 1;
        private int _currentChatroomId;
        
        public home()
        {
            InitializeComponent();
            
            // Load thông tin user từ Properties.Settings
            LoadUserInfoFromSettings();
            
            // Initialize services
            InitializeServices();
              // Đặt các giá trị vào UI
            UpdateUserDisplayInfo();
            this.FormClosing += MainForm_FormClosing;

            // Add tooltips
            AddTooltips();
            
            // Add test event handler for toast notification
            chatTitleLabel.DoubleClick += chatTitleLabel_DoubleClick;

            // Load chatrooms
            _ = LoadChatrooms();
        }        
        
        private void InitializeServices()
        {
            // Initialize API service
            _apiService = new ApiService(ApiBaseUrl);
            
            // Initialize SignalR service
            _signalRService = new SignalRService(SignalRHubUrl);
            _signalRService.MessageReceived += OnSignalRMessageReceived;
            _signalRService.NotificationReceived += OnSignalRNotificationReceived;
            _signalRService.UserJoinedChatroom += OnUserJoinedChatroom;
            _signalRService.UserLeftChatroom += OnUserLeftChatroom;
            _signalRService.ConnectionStatusChanged += OnConnectionStatusChanged;
            
            // Initialize chatroom manager
            _chatroomManager = new ChatroomManager(pnlChatroomList);
            _chatroomManager.ChatroomSelected += OnChatroomSelected;
            _chatroomManager.StatusChanged += OnChatroomManagerStatusChanged;
            
            // Initialize message manager
            _messageManager = new MessageManager(pnlChatMessages, _currentUserId);
            _messageManager.StatusChanged += OnMessageManagerStatusChanged;
            
            // Initialize toast manager
            _toastManager = new ToastManager(this);
            _toastManager.ToastClicked += OnToastClicked;
            
            // Start SignalR connection
            _ = Task.Run(async () => await _signalRService.InitializeAsync());
        }

        // SignalR Event Handlers
        private void OnSignalRMessageReceived(MessageDisplayData messageData)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Add message to display
                _messageManager.AddMessageToDisplay(messageData.SenderName, messageData.content, messageData.createdAt, messageData.senderId != _currentUserId);
                  // If this message is from someone else, handle notifications
                if (messageData.senderId != _currentUserId)
                {
                    // If app is not focused or message is from different chatroom, show notification
                    bool showNotification = !this.Focused || messageData.chatRoomId != _currentChatroomId;
                    
                    if (showNotification)
                    {
                        var shortContent = messageData.content.Length > 50 ? 
                            messageData.content.Substring(0, 50) + "..." : 
                            messageData.content;
                            
                        // Get chatroom name for notification
                        var chatroom = _chatroomManager.GetChatroomById(messageData.chatRoomId);
                        var chatroomName = chatroom?.name ?? $"Chatroom {messageData.chatRoomId}";
                        
                        // Show toast notification
                        _toastManager.ShowToast(
                            $"New message from {messageData.SenderName}",
                            $"[{chatroomName}] {shortContent}",
                            messageData.chatRoomId.ToString()
                        );
                        
                        _messageManager.AddSystemMessage($"📱 [{chatroomName}] {messageData.SenderName}: {shortContent}");
                        FlashWindow();
                    }
                }
            });
        }
        
        private void OnSignalRNotificationReceived(string type, string title, string body)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Check if this is a message notification and if we're not in the current chatroom
                if (type == "new_message" || type == "chatroom_message")
                {
                    // Show a subtle notification in the message area instead of popup
                    _messageManager.AddSystemMessage($"📢 {title}: {body}");
                    
                    // Flash the window title if app is not focused
                    if (!this.Focused)
                    {
                        FlashWindow();
                    }
                }
                else
                {
                    // For other notification types, still show MessageBox
                    MessageBox.Show(body, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
        }

        private void OnUserJoinedChatroom(string userId, string username, string chatroomId, string joinedAt)
        {
            this.Invoke((MethodInvoker)delegate
            {
                _messageManager.AddSystemMessage($"User joined chatroom: {username} (User ID: {userId}, Chatroom ID: {chatroomId}, Joined At: {joinedAt})");
            });
        }

        private void OnUserLeftChatroom(string userId, string username, string chatroomId, string leftAt)
        {
            this.Invoke((MethodInvoker)delegate
            {
                _messageManager.AddSystemMessage($"User left chatroom: {username}");
            });
        }

        private void OnConnectionStatusChanged(string status)
        {
            this.Invoke((MethodInvoker)delegate
            {
                _messageManager.AddSystemMessage(status);
                
                // Auto register user and join chatroom when connected
                if (status.Contains("Connected to SignalR Hub"))
                {
                    _ = Task.Run(async () =>
                    {
                        await _signalRService.RegisterUserAsync(_currentUserId);
                        if (_currentChatroomId > 0)
                        {
                            await _signalRService.JoinChatroomAsync(_currentChatroomId, _currentUserId);
                        }
                    });
                }
            });
        }
        
        private void OnToastClicked(object sender, string chatroomId)
        {
            // When toast is clicked, switch to the relevant chatroom
            if (int.TryParse(chatroomId, out int chatId))
            {
                // Bring window to front
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
                this.Activate();
                
                // Select the chatroom
                _chatroomManager.SelectChatroom(chatId);
            }
        }
        
        // Chatroom Manager Event Handlers
        private void OnChatroomSelected(int chatroomId)
        {
            _currentChatroomId = chatroomId;
            
            var selectedChatroom = _chatroomManager.GetChatroomById(chatroomId);
            if (selectedChatroom != null)
            {
                chatTitleLabel.Text = selectedChatroom.name;
                // Show edit button when a chatroom is selected
                btnEditChatroom.Visible = true;
            }
            else
            {
                // Hide edit button when no chatroom is selected
                btnEditChatroom.Visible = false;
            }
            
            // Clear messages and load new ones
            _messageManager.ClearMessages();
            
            // Join chatroom via SignalR
            _ = Task.Run(async () => await _signalRService.JoinChatroomAsync(_currentChatroomId, _currentUserId));
            
            // Load messages
            LoadMessagesForChatroom(_currentChatroomId);
        }

        private void OnChatroomManagerStatusChanged(string status)
        {
            _messageManager.AddSystemMessage(status);
        }

        private void OnMessageManagerStatusChanged(string status)
        {
            // Could log to console or show in status bar
            Console.WriteLine($"MessageManager: {status}");
        }
        
        // Enhanced event handler for sending messages with better validation
        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                string messageContent = txtMessageInput.Text.Trim();
                if (string.IsNullOrEmpty(messageContent))
                {
                    MessageBox.Show("Please enter a message.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate message length
                if (messageContent.Length > 1000)
                {
                    MessageBox.Show("Message is too long. Please keep it under 1000 characters.", "Message Too Long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate chatroom selection
                if (_currentChatroomId <= 0)
                {
                    MessageBox.Show("Please select a chatroom first.", "No Chatroom Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // Try SignalR first
                    await _signalRService.SendMessageAsync(_currentUserId, _currentChatroomId, messageContent);
                    
                    // Send notification to all chatroom members after successful message
                    try
                    {
                        var currentChatroom = _chatroomManager.GetChatroomById(_currentChatroomId);
                        var senderName = Properties.Settings.Default.DisplayName ?? Properties.Settings.Default.UserEmail ?? "User";
                        
                        // Send chatroom notification (broadcast to all members except sender)
                        await _signalRService.SendChatroomNotificationAsync(
                            _currentChatroomId, 
                            _currentUserId, 
                            senderName, 
                            messageContent, 
                            0 // messageId - we don't have this from SignalR, using 0 as placeholder
                        );
                    }
                    catch (Exception notifyEx)
                    {
                        // Log notification error but don't fail the message sending
                        _messageManager.AddSystemMessage($"Message sent but notification failed: {notifyEx.Message}");
                    }
                    
                    txtMessageInput.Clear();
                }
                catch (Exception)
                {
                    // Fallback to API
                    _messageManager.AddSystemMessage("SignalR failed, trying API fallback...");
                    bool success = await _apiService.SendMessage(_currentUserId, _currentChatroomId, messageContent);
                    
                    if (success)
                    {
                        // Send notification after successful API message
                        try
                        {
                            var senderName = Properties.Settings.Default.DisplayName ?? Properties.Settings.Default.UserEmail ?? "User";
                            
                            // Send chatroom notification via SignalR if connection is available
                            if (_signalRService.IsConnected)
                            {
                                await _signalRService.SendChatroomNotificationAsync(
                                    _currentChatroomId, 
                                    _currentUserId, 
                                    senderName, 
                                    messageContent, 
                                    0 // messageId - placeholder
                                );
                            }
                        }
                        catch (Exception notifyEx)
                        {
                            _messageManager.AddSystemMessage($"Message sent but notification failed: {notifyEx.Message}");
                        }
                        
                        txtMessageInput.Clear();
                        _messageManager.AddSystemMessage("Message sent via API successfully.");
                    }
                    else
                    {
                        _messageManager.AddSystemMessage("Failed to send message via API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        
        // Xử lý khi Form đóng để ngắt kết nối SignalR
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _toastManager?.ClearAllToasts();
                await _signalRService?.DisconnectAsync();
                _apiService?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during form closing: {ex.Message}");
            }
        }

        // Enhanced Form load with improved API integration
        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Load initial chatroom if selected
                if (_currentChatroomId > 0)
                {
                    var chatroomInfo = await _apiService.GetChatroomInfo(_currentChatroomId);
                    if (chatroomInfo != null)
                    {
                        this.Text = $"Chat - {chatroomInfo.name}";
                    }

                    // Load participants, stats, and messages
                    await _apiService.LoadChatroomParticipants(_currentChatroomId);
                    await _apiService.LoadChatroomStats(_currentChatroomId);
                    
                    var messages = await _apiService.GetChatroomMessages(_currentChatroomId, 1, 20);
                    _messageManager.LoadMessages(messages);
                    
                    // Join chatroom
                    // await _apiService.JoinChatroom(_currentChatroomId, _currentUserId);
                }
            }
            catch (Exception ex)
            {
                _messageManager.AddSystemMessage($"Error during form load: {ex.Message}");
            }
        }        
        
        private async Task LoadChatrooms()
        {
            try
            {
                // Show loading status
                userStatusLabel.Text = "Loading chatrooms...";
                userStatusLabel.ForeColor = System.Drawing.Color.Blue;
                                
                var chatroomsResponse = await _apiService.GetUserChatrooms(_currentUserId);
                
                if (chatroomsResponse?.Chatrooms?.Count > 0)
                {
                    _chatroomManager.LoadChatrooms(chatroomsResponse.Chatrooms);
                    userStatusLabel.Text = $"{chatroomsResponse.Chatrooms.Count} chatrooms loaded";
                    userStatusLabel.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // Show no chatrooms message instead of test data
                    _chatroomManager.ShowNoChatroomsMessage();
                    _messageManager.AddSystemMessage($"API call successful but returned no chatrooms for user {_currentUserId}");
                    userStatusLabel.Text = "No chatrooms found";
                    userStatusLabel.ForeColor = System.Drawing.Color.Orange;
                }
            }
            catch (Exception ex)
            {
                _chatroomManager.ShowNoChatroomsMessage();
                _messageManager.AddSystemMessage($"Error loading chatrooms: {ex.Message}. Please check your connection.");
                userStatusLabel.Text = "Error loading chatrooms";
                userStatusLabel.ForeColor = System.Drawing.Color.Red;
            }
        }   
        
        // Load messages for a specific chatroom
        private async void LoadMessagesForChatroom(int chatroomId)
        {
            try
            {
                var messages = await _apiService.GetChatroomMessages(chatroomId);
                
                if (messages?.Count > 0)
                {
                    _messageManager.LoadMessages(messages);
                }
                else
                {
                    _messageManager.AddSystemMessage($"No messages found for chatroom {chatroomId}. Start the conversation!");
                }
            }
            catch (Exception ex)
            {
                _messageManager.AddSystemMessage($"Error loading messages: {ex.Message}");
            }
        }

        // Event handlers cho search textbox
        private void txtSearchChatrooms_Enter(object sender, EventArgs e)
        {
            if (txtSearchChatrooms.Text == "Search chatrooms...")
            {
                txtSearchChatrooms.Text = "";
                txtSearchChatrooms.ForeColor = Color.Black;
            }
        }

        private void txtSearchChatrooms_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchChatrooms.Text))
            {
                txtSearchChatrooms.Text = "Search chatrooms...";
                txtSearchChatrooms.ForeColor = Color.Gray;
            }
        }

        private void txtSearchChatrooms_TextChanged(object sender, EventArgs e)
        {
            _chatroomManager.FilterChatrooms(txtSearchChatrooms.Text);
        }

        // Load thông tin user từ Properties.Settings
        private void LoadUserInfoFromSettings()
        {
            try
            {
                // Kiểm tra xem user đã đăng nhập chưa
                if (Properties.Settings.Default.IsLoggedIn && 
                    !string.IsNullOrEmpty(Properties.Settings.Default.UserId))
                {
                    // Parse userId từ string sang int
                    if (int.TryParse(Properties.Settings.Default.UserId, out int userId))
                    {
                        _currentUserId = userId;
                    }
                    else
                    {
                        // Fallback về default nếu parse thất bại
                        _currentUserId = 1;
                    }
                }
                else
                {
                    // User chưa đăng nhập, redirect về login
                    MessageBox.Show("Bạn chưa đăng nhập. Vui lòng đăng nhập lại.", "Chưa đăng nhập", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    // Đóng form hiện tại và mở login form
                    this.Hide();
                    login loginForm = new login();
                    loginForm.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thông tin user: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _currentUserId = 1; // Fallback
            }
        }

        // Cập nhật thông tin hiển thị của user
        private void UpdateUserDisplayInfo()
        {
            try
            {
                string displayText = "";
                
                // Lấy displayName từ Settings
                string displayName = Properties.Settings.Default.DisplayName;
                string email = Properties.Settings.Default.UserEmail;
                
                // Nếu displayName trống, fallback về email
                if (!string.IsNullOrEmpty(displayName))
                {
                    displayText = displayName;
                }
                else if (!string.IsNullOrEmpty(email))
                {
                    displayText = email;
                }
                else
                {
                    displayText = $"User {_currentUserId}"; // Fallback cuối cùng
                }
                
                // Cập nhật txtCurrentUserId thành hiển thị displayName
                txtCurrentUserId.Text = displayText;
                
                // Cập nhật title của form để hiển thị thông tin user
                this.Text = $"Chat - {displayText}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin hiển thị: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentUserId.Text = $"User {_currentUserId}";
            }
        }

        // Method to refresh chatrooms
        private async void RefreshChatrooms()
        {
            await LoadChatrooms();
        }

        // Add this to handle double-click on status label to refresh
        private void userStatusLabel_DoubleClick(object sender, EventArgs e)
        {
            if (userStatusLabel.Text.Contains("Error") || userStatusLabel.Text.Contains("No chatrooms"))
            {
                RefreshChatrooms();
            }
        }

        // Test method để debug API call
        private async void TestApiCall()
        {
            try
            {
                userStatusLabel.Text = "Testing API...";
                userStatusLabel.ForeColor = System.Drawing.Color.Blue;
                
                var url = $"https://localhost:7092/api/Chatrooms/user/{_currentUserId}";
                _messageManager.AddSystemMessage($"Testing API call to: {url}");
                _messageManager.AddSystemMessage($"Current User ID: {_currentUserId}");
                
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    _messageManager.AddSystemMessage($"Response Status: {response.StatusCode}");
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        _messageManager.AddSystemMessage($"Response Content: {content}");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        _messageManager.AddSystemMessage($"Error Content: {errorContent}");
                    }
                }
                
                userStatusLabel.Text = "API test completed";
                userStatusLabel.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                _messageManager.AddSystemMessage($"Test API Error: {ex.Message}");
                userStatusLabel.Text = "API test failed";
                userStatusLabel.ForeColor = System.Drawing.Color.Red;
            }
        }        
        
        // Status click now only refreshes chatrooms, remove test API call
        private void userStatusLabel_Click(object sender, EventArgs e)
        {
            RefreshChatrooms();
        }
        
        private void txtMessageInput_Enter(object sender, EventArgs e)
        {
            // This method is for when textbox gets focus, not for Enter key
        }

        // Handle Enter key press to send message
        private void txtMessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.Handled = true; // Prevent the newline
                btnSendMessage_Click(sender, e);
            }
        }

        // Logout functionality
        private void LogoutUser()
        {
            try
            {
                // Clear all user settings
                Properties.Settings.Default.AccessToken = "";
                Properties.Settings.Default.RefreshToken = "";
                Properties.Settings.Default.UserId = "";
                Properties.Settings.Default.UserEmail = "";
                Properties.Settings.Default.DisplayName = "";
                Properties.Settings.Default.IsLoggedIn = false;
                Properties.Settings.Default.Save();
                
                // Disconnect services
                _ = Task.Run(async () =>
                {
                    await _signalRService?.DisconnectAsync();
                });
                
                // Show login form and close current form
                this.Hide();
                login loginForm = new login();
                loginForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during logout: {ex.Message}", "Logout Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handle user profile picture double-click for logout
        private void userProfilePicture_DoubleClick(object sender, EventArgs e)
        {
            var result = new logout_form().ShowDialog();

            if (result == DialogResult.OK)
            {
                LogoutUser();
            }
        }        
        
        // Add tooltips for better UX
        private void AddTooltips()
        {
            var tooltip = new ToolTip();
            tooltip.SetToolTip(userProfilePicture, "Double-click to logout");
            tooltip.SetToolTip(userStatusLabel, "Click to refresh chatrooms\nDouble-click to retry if error");
            tooltip.SetToolTip(txtSearchChatrooms, "Search your chatrooms");
            tooltip.SetToolTip(btnSendMessage, "Send message (or press Enter)");
            tooltip.SetToolTip(btnEditChatroom, "Edit chatroom name and description");
            tooltip.SetToolTip(chatTitleLabel, "Double-click to view chatroom members");
        }
        
        private void btnFriend_Click(object sender, EventArgs e)
        {
            try
            {
                friend_list friendForm = new friend_list();
                friendForm.ShowDialog(); // Show as modal dialog
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening friend manager: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditChatroom_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentChatroomId <= 0)
                {
                    MessageBox.Show("Please select a chatroom first.", "No Chatroom Selected", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Get current chatroom info
                var currentChatroom = _chatroomManager.GetChatroomById(_currentChatroomId);
                if (currentChatroom == null)
                {
                    MessageBox.Show("Chatroom information not found.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Open edit form
                using (var editForm = new edit_chatroom(_currentChatroomId, currentChatroom.name, currentChatroom.description ?? ""))
                {
                    if (editForm.ShowDialog() == DialogResult.OK && editForm.ChangesSaved)
                    {
                        var newName = editForm.ChatroomName;
                        var newDescription = editForm.Description;
                        
                        // Call API to update chatroom
                        bool success = await _apiService.UpdateChatroom(_currentChatroomId, newName, newDescription, _currentUserId);
                        
                        if (success)
                        {
                            // Update UI
                            chatTitleLabel.Text = newName;
                            
                            // Refresh chatrooms list
                            await LoadChatrooms();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update chatroom. Please try again.", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing chatroom: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Flash window to notify user of new messages
        private void FlashWindow()
        {
            try
            {
                // Simple way to flash the window - change title temporarily
                string originalTitle = this.Text;
                this.Text = "💬 New Message - " + originalTitle;
                
                // Reset title after 2 seconds
                var timer = new Timer();
                timer.Interval = 2000;
                timer.Tick += (s, e) =>
                {
                    this.Text = originalTitle;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
                
                // Also flash the taskbar if possible
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                // If flashing fails, just log it
                Console.WriteLine($"Error flashing window: {ex.Message}");
            }
        }        
        
        // Show participants when double-clicking chatroom title
        private async void chatTitleLabel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (_currentChatroomId <= 0)
                {
                    MessageBox.Show("Please select a chatroom first.", "No Chatroom Selected", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                var currentChatroom = _chatroomManager.GetChatroomById(_currentChatroomId);
                if (currentChatroom == null)
                {
                    MessageBox.Show("Chatroom information not found.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Show loading cursor
                this.Cursor = Cursors.WaitCursor;
                
                // Get participants from API
                var participants = await _apiService.GetChatroomParticipants(_currentChatroomId);
                
                // Reset cursor
                this.Cursor = Cursors.Default;
                  // Show participants form
                using (var participantsForm = new ParticipantsListForm(currentChatroom.name, participants, _currentChatroomId, _currentUserId))
                {
                    // Handle refresh request from participants form
                    participantsForm.RefreshRequested += async (s, args) =>
                    {
                        try
                        {
                            participantsForm.Cursor = Cursors.WaitCursor;
                            var refreshedParticipants = await _apiService.GetChatroomParticipants(_currentChatroomId);
                            participantsForm.UpdateParticipants(refreshedParticipants);
                            participantsForm.Cursor = Cursors.Default;
                        }
                        catch (Exception ex)
                        {
                            participantsForm.Cursor = Cursors.Default;
                            MessageBox.Show($"Error refreshing participants: {ex.Message}", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    
                    // Handle add user request from participants form
                    participantsForm.AddUserRequested += async (s, args) =>
                    {
                        try
                        {
                            participantsForm.Cursor = Cursors.WaitCursor;
                            
                            bool success = await _apiService.AddUserToChatroom(
                                args.ChatroomId, 
                                args.UserId, 
                                args.AddedBy, 
                                "member" // Default role
                            );
                            
                            participantsForm.Cursor = Cursors.Default;
                            
                            if (success)
                            {
                                MessageBox.Show($"User {args.UserId} has been added to the chatroom successfully!", "Success", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // Clear the input and refresh the list
                                participantsForm.ClearAddUserInput();
                                
                                // Auto refresh participants list
                                var refreshedParticipants = await _apiService.GetChatroomParticipants(_currentChatroomId);
                                participantsForm.UpdateParticipants(refreshedParticipants);
                            }
                            else
                            {
                                MessageBox.Show("Failed to add user to chatroom. Please check if the User ID exists and try again.", "Error", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            participantsForm.Cursor = Cursors.Default;
                            MessageBox.Show($"Error adding user: {ex.Message}", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    
                    participantsForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Error loading participants: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Test method for toast notification (for development - can be called from other events)
        private void TestToastNotification()
        {
            _toastManager.ShowToast(
                "Test Notification", 
                "This is a test message to verify toast notifications are working correctly!",
                _currentChatroomId.ToString()
            );
        }
    }
}
