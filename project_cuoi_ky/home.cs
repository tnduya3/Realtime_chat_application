using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_cuoi_ky
{
    public partial class home : Form
        {
        public home()
        {
            InitializeComponent();
            InitializeSignalR();
            
            // Đặt các giá trị mặc định vào UI nếu có
            txtCurrentUserId.Text = _currentUserId.ToString();
            txtCurrentChatroomId.Text = _currentChatroomId.ToString();
        
        }

        private HubConnection _hubConnection;
        private const string SignalRHubUrl = "https://localhost:7092/chathub"; // URL của SignalR Hub trên server
        private const string ApiBaseUrl = "https://localhost:7092/api/"; // Base URL cho các API RESTful

        // User ID và Chatroom ID hiện tại (sẽ được chọn hoặc đăng nhập)
        private int _currentUserId = 1; // Giả sử User ID mặc định là 1 để test
        private int _currentChatroomId = 1; // Giả sử Chatroom ID mặc định là 1 để test

        private async void InitializeSignalR()
        {
            try
            {
                // Khởi tạo HubConnection
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(SignalRHubUrl)
                    .ConfigureLogging(logging =>
                    {
                        //// Sử dụng phương thức AddConsole để thêm console logger
                        //logging.AddConsole();
                        //logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug); // Sử dụng đầy đủ namespace để tránh lỗi CS0122
                    })
                    .Build();                // Đăng ký phương thức nhận tin nhắn từ Hub
                _hubConnection.On<int, string, string>("ReceiveMessage", (senderId, senderUsername, content) =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        AddUserMessage(senderUsername, content);
                    });
                });

                _hubConnection.Closed += async (error) =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtbMessage.AppendText("[System]: Connection closed. Reconnecting...\n");
                    });
                    await Task.Delay(5000);
                    await ConnectToSignalR();
                };

                await ConnectToSignalR();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing SignalR: {ex.Message}", "SignalR Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"SignalR initialization error: {ex}");
            }
        }

        private async Task ConnectToSignalR()
        {
            try
            {
                await _hubConnection.StartAsync();
                this.Invoke((MethodInvoker)delegate
                {
                    rtbMessage.AppendText("[System]: Connected to SignalR Hub.\n");
                    // Sau khi kết nối, tham gia chatroom ngay lập tức
                    JoinChatroom(_currentChatroomId);
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    rtbMessage.AppendText($"[System]: Could not connect to SignalR Hub: {ex.Message}\n");
                });
                Console.WriteLine($"SignalR connection error: {ex}");
            }
        }

        private async void JoinChatroom(int chatroomId)
        {
            try
            {
                // Gọi phương thức "JoinChatroom" trên server Hub
                // Server sẽ biết user ID của bạn (nếu có xác thực) hoặc bạn truyền vào
                await _hubConnection.InvokeAsync("JoinChatroom", chatroomId);
                this.Invoke((MethodInvoker)delegate
                {
                    rtbMessage.AppendText($"[System]: Joined chatroom {chatroomId}.\n");
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    rtbMessage.AppendText($"[System]: Failed to join chatroom {chatroomId}: {ex.Message}\n");
                });
                Console.WriteLine($"Error joining chatroom: {ex}");
            }
        }

        // Event handler cho nút Gửi tin nhắn
        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            // Cập nhật User ID và Chatroom ID từ UI
            if (int.TryParse(txtCurrentUserId.Text, out int userId))
            {
                _currentUserId = userId;
            }
            if (int.TryParse(txtCurrentChatroomId.Text, out int chatroomId))
            {
                _currentChatroomId = chatroomId;
            }

            string messageContent = txtMessageInput.Text.Trim();
            if (string.IsNullOrEmpty(messageContent))
            {
                MessageBox.Show("Please enter a message.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_hubConnection.State == HubConnectionState.Connected)
            {
                try
                {
                    // Gửi tin nhắn qua SignalR
                    await _hubConnection.InvokeAsync("SendMessage", _currentUserId, _currentChatroomId, messageContent);
                    txtMessageInput.Clear(); // Xóa nội dung input sau khi gửi
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sending message via SignalR: {ex.Message}", "Send Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"SignalR send message error: {ex}");
                }
            }
            else
            {
                // Nếu SignalR chưa kết nối, có thể gửi qua API RESTful như fallback
                // Hoặc yêu cầu người dùng chờ kết nối.
                MessageBox.Show("Not connected to chat. Attempting to send via API (fallback)...", "Connection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await SendMessageViaApi(_currentUserId, _currentChatroomId, messageContent);
            }
        }

        // Phương thức gửi tin nhắn qua API RESTful (fallback hoặc cho các tác vụ ban đầu)
        private async Task SendMessageViaApi(int senderId, int chatroomId, string content)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var messageData = new
                    {
                        senderId = senderId,
                        chatroomId = chatroomId,
                        content = content
                    };
                    string jsonContent = JsonSerializer.Serialize(messageData);
                    StringContent httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{ApiBaseUrl}messages", httpContent); // Kiểm tra lại URL API của bạn

                    if (response.IsSuccessStatusCode)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            rtbMessage.AppendText("[System]: Message sent via API successfully.\n");
                        });
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        this.Invoke((MethodInvoker)delegate
                        {
                            rtbMessage.AppendText($"[System]: Failed to send message via API: {response.StatusCode} - {errorResponse}\n");
                        });
                        Console.WriteLine($"API send message error: {response.StatusCode} - {errorResponse}");
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtbMessage.AppendText($"[System]: Error sending message via API: {ex.Message}\n");
                    });
                    Console.WriteLine($"API send message exception: {ex}");
                }
            }
        }

        // Xử lý khi Form đóng để ngắt kết nối SignalR
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_hubConnection != null && _hubConnection.State != HubConnectionState.Disconnected)
            {
                await _hubConnection.StopAsync();
                _hubConnection.DisposeAsync();
                Console.WriteLine("SignalR connection stopped and disposed.");
            }
        }

        // Tùy chọn: Gọi API để lấy lịch sử tin nhắn khi Form load
        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadChatroomMessages(_currentChatroomId);
        }

        private async Task LoadChatroomMessages(int chatroomId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Giả sử bạn có API để lấy tin nhắn của một chatroom
                    // GET /api/chatrooms/{chatroomId}/messages
                    HttpResponseMessage response = await client.GetAsync($"{ApiBaseUrl}chatrooms/{chatroomId}/messages");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        // Deserialize danh sách tin nhắn từ JSON
                        var messages = JsonSerializer.Deserialize<List<ChatMessageDisplay>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        this.Invoke((MethodInvoker)delegate
                        {
                            rtbMessage.Clear(); // Xóa tin nhắn cũ nếu có
                            foreach (var msg in messages)
                            {
                                rtbMessage.AppendText($"[{msg.SenderUsername}]: {msg.Content}\n");
                            }
                            rtbMessage.ScrollToCaret(); // Scroll to bottom
                        });
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        this.Invoke((MethodInvoker)delegate
                        {
                            rtbMessage.AppendText($"[System]: Failed to load chatroom messages: {response.StatusCode} - {errorResponse}\n");
                        });
                        Console.WriteLine($"API load messages error: {response.StatusCode} - {errorResponse}");
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtbMessage.AppendText($"[System]: Error loading chatroom messages: {ex.Message}\n");
                    });
                    Console.WriteLine($"API load messages exception: {ex}");
                }
            }
        }        // DTO để ánh xạ dữ liệu tin nhắn từ API
        // Bạn cần đảm bảo các thuộc tính khớp với JSON trả về từ API của bạn
        public class ChatMessageDisplay
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public string SenderUsername { get; set; } // Phải có trường này từ API
            public int ChatroomId { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        // Phương thức trợ giúp để thêm tin nhắn vào RichTextBox với định dạng đẹp
        private void AddMessageToRichTextBox(string message, Color color = default)
        {
            if (color == default)
                color = Color.Black;

            rtbMessage.SelectionStart = rtbMessage.TextLength;
            rtbMessage.SelectionLength = 0;
            rtbMessage.SelectionColor = color;
            rtbMessage.AppendText(message + "\n");
            rtbMessage.SelectionColor = rtbMessage.ForeColor; // Reset color
            rtbMessage.ScrollToCaret();
        }

        // Phương thức để thêm tin nhắn hệ thống với màu khác
        private void AddSystemMessage(string message)
        {
            AddMessageToRichTextBox($"[System]: {message}", Color.Gray);
        }

        // Phương thức để thêm tin nhắn người dùng với timestamp
        private void AddUserMessage(string username, string content, DateTime? timestamp = null)
        {
            string timeStr = timestamp?.ToString("HH:mm") ?? DateTime.Now.ToString("HH:mm");
            string formattedMessage = $"[{timeStr}] {username}: {content}";
            AddMessageToRichTextBox(formattedMessage, username == "System" ? Color.Gray : Color.Black);
        }
    }
}
