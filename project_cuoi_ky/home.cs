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

        // User ID và Chatroom ID hiện tại
        private int _currentUserId = 1;
        private int _currentChatroomId = 1;
        
        public home()
        {
            InitializeComponent();
            
            // Load thông tin user từ Properties.Settings
            LoadUserInfoFromSettings();
            
            // Initialize services
            InitializeServices();
            
            // Đặt các giá trị vào UI
            UpdateUserDisplayInfo();
            txtCurrentChatroomId.Text = _currentChatroomId.ToString();
            this.FormClosing += MainForm_FormClosing;

            // Load chatrooms
            LoadChatrooms();
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
            
            // Start SignalR connection
            _ = Task.Run(async () => await _signalRService.InitializeAsync());
        }

        // SignalR Event Handlers
        private void OnSignalRMessageReceived(MessageDisplayData messageData)
        {
            this.Invoke((MethodInvoker)delegate
            {
                _messageManager.AddMessageToDisplay(messageData.SenderName, messageData.content, messageData.createdAt, messageData.senderId != _currentUserId);
            });
        }

        private void OnSignalRNotificationReceived(string type, string title, string body)
        {
            this.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show(body, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Chatroom Manager Event Handlers
        private void OnChatroomSelected(int chatroomId)
        {
            _currentChatroomId = chatroomId;
            txtCurrentChatroomId.Text = _currentChatroomId.ToString();
            
            var selectedChatroom = _chatroomManager.GetChatroomById(chatroomId);
            if (selectedChatroom != null)
            {
                chatTitleLabel.Text = selectedChatroom.Name;
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
        }// Enhanced event handler for sending messages with better validation
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
                    txtMessageInput.Clear();
                    _messageManager.AddSystemMessage("Message sent via SignalR");
                }
                catch (Exception)
                {
                    // Fallback to API
                    _messageManager.AddSystemMessage("SignalR failed, trying API fallback...");
                    bool success = await _apiService.SendMessage(_currentUserId, _currentChatroomId, messageContent);
                    
                    if (success)
                    {
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
        }        // Xử lý khi Form đóng để ngắt kết nối SignalR
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
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
                        this.Text = $"Chat - {chatroomInfo.Name}";
                    }

                    // Load participants, stats, and messages
                    await _apiService.LoadChatroomParticipants(_currentChatroomId);
                    await _apiService.LoadChatroomStats(_currentChatroomId);
                    
                    var messages = await _apiService.GetChatroomMessages(_currentChatroomId, 1, 20);
                    _messageManager.LoadMessages(messages);
                    
                    // Join chatroom
                    await _apiService.JoinChatroom(_currentChatroomId, _currentUserId);
                }
            }
            catch (Exception ex)
            {
                _messageManager.AddSystemMessage($"Error during form load: {ex.Message}");
            }        // Load chatrooms từ API
        }
        private async void LoadChatrooms()
        {
            try
            {
                var chatroomsResponse = await _apiService.GetUserChatrooms(_currentUserId);
                
                if (chatroomsResponse.Chatrooms?.Count > 0)
                {
                    _chatroomManager.LoadChatrooms(chatroomsResponse.Chatrooms);
                }
                else
                {
                    // Fallback: load test data
                    _chatroomManager.LoadTestData();
                }
            }
            catch (Exception ex)
            {
                _chatroomManager.LoadTestData();
                _messageManager.AddSystemMessage($"Error loading chatrooms: {ex.Message}. Using test data.");
            }
        }

        // Load messages for a specific chatroom
        private async void LoadMessagesForChatroom(int chatroomId)
        {
            try
            {
                var messages = await _apiService.GetChatroomMessages(chatroomId);
                _messageManager.LoadMessages(messages);
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
    }
}
