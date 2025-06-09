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
            this.FormClosing += MainForm_FormClosing;

            pnlChatMessages.FlowDirection = FlowDirection.TopDown;
            pnlChatMessages.WrapContents = false; // Quan trọng: Ngăn không cho các control xuống dòng nếu không đủ chỗ ngang
            pnlChatMessages.AutoScroll = true; // Cho phép cuộn khi nội dung vượt quá kích thước panel
            pnlChatMessages.VerticalScroll.Visible = true; // Đảm bảo thanh cuộn luôn hiển thị nếu muốn

        }

        private string GetSignalRHubUrlWithUserId()
        {
            int userId = 1;
            if (int.TryParse(txtCurrentUserId.Text, out int parsedId))
            {
                userId = parsedId;
            }
            return $"{SignalRHubUrl}?userId={userId}";
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
                    .Build();
                // Đăng ký phương thức nhận tin nhắn từ Hub
                _hubConnection.On<MessageDisplayData>("ReceiveMessage", (messageData) => // Nhận một đối tượng dữ liệu tin nhắn
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        // Gọi phương thức để thêm bong bóng tin nhắn vào display
                        AddMessageToDisplay(messageData.SenderUsername, messageData.Content, messageData.createdAt, messageData.senderId != _currentUserId);
                    });
                });
                // Trong MainForm.cs, trong InitializeSignalR()
                _hubConnection.On<string, string, string>("ReceiveNotification", (type, title, body) =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        // Hiển thị thông báo bằng MessageBox hoặc một UI thông báo tùy chỉnh
                        MessageBox.Show(body, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                });

                _hubConnection.Closed += async (error) =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        AddSystemMessage("Connection closed. Reconnecting...");
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
                    AddSystemMessage("Connected to SignalR Hub.");
                    _currentUserId = int.Parse(txtCurrentUserId.Text);
                    _hubConnection.InvokeAsync("RegisterUser", _currentUserId);
                    JoinChatroom(_currentChatroomId);
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    AddSystemMessage($"Could not connect to SignalR Hub: {ex.Message}");
                });
                Console.WriteLine($"SignalR connection error: {ex}");
            }
        }

        private async void JoinChatroom(int chatroomId)
        {
            try
            {
                await _hubConnection.InvokeAsync("JoinChatroom", _currentChatroomId.ToString(), _currentUserId.ToString());
                this.Invoke((MethodInvoker)delegate
                {
                    AddSystemMessage($"Joined chatroom {chatroomId}.");
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    AddSystemMessage($"Failed to join chatroom {chatroomId}: {ex.Message}");
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
                        message = content
                    };
                    string jsonContent = JsonSerializer.Serialize(messageData);
                    StringContent httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{ApiBaseUrl}Messages", httpContent); // Kiểm tra lại URL API của bạn

                    if (response.IsSuccessStatusCode)
                    {                        
                        this.Invoke((MethodInvoker)delegate
                        {
                            AddSystemMessage("Message sent via API successfully.");
                        });
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        this.Invoke((MethodInvoker)delegate
                        {
                            AddSystemMessage($"Failed to send message via API: {response.StatusCode} - {errorResponse}");
                        });
                        Console.WriteLine($"API send message error: {response.StatusCode} - {errorResponse}");
                    }
                }
                catch (Exception ex)
                {                    this.Invoke((MethodInvoker)delegate
                    {
                        AddSystemMessage($"Error sending message via API: {ex.Message}");
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
            // Lấy trang đầu tiên với 20 tin nhắn mỗi trang
            await LoadChatroomMessages(_currentChatroomId, 1, 20);
        }

        private async Task LoadChatroomMessages(int chatroomId, int page = 1, int pageSize = 20)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{ApiBaseUrl}Chatrooms/{chatroomId}/messages?page={page}&pageSize={pageSize}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var messages = JsonSerializer.Deserialize<List<ChatroomDisplayData>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        this.Invoke((MethodInvoker)delegate
                        {
                            pnlChatMessages.Controls.Clear(); // Xóa tin nhắn cũ
                            foreach (var msg in messages)
                            {
                                AddMessageToDisplay(msg.SenderUsername, msg.message, msg.createdAt, msg.senderId == _currentUserId);
                            }
                            ScrollToBottom(); // Cuộn xuống cuối
                        });
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        this.Invoke((MethodInvoker)delegate
                        {
                            AddSystemMessage($"Failed to load chatroom messages: {response.StatusCode} - {errorResponse}");
                        });
                        Console.WriteLine($"API load messages error: {response.StatusCode} - {errorResponse}");
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        AddSystemMessage($"Error loading chatroom messages: {ex.Message}");
                    });
                    Console.WriteLine($"API load messages exception: {ex}");
                }
            }
        }

        private void ScrollToBottom()
        {
            // Kiểm tra xem thanh cuộn dọc có hiển thị không
            if (pnlChatMessages.VerticalScroll.Visible)
            {
                // Đặt giá trị thanh cuộn đến mức tối đa
                pnlChatMessages.VerticalScroll.Value = pnlChatMessages.VerticalScroll.Maximum;
            }
            // Ngoài ra, bạn có thể sử dụng ScrollControlIntoView để đảm bảo control cuối cùng hiển thị
            if (pnlChatMessages.Controls.Count > 0)
            {
                pnlChatMessages.ScrollControlIntoView(pnlChatMessages.Controls[pnlChatMessages.Controls.Count - 1]);
            }
        }

        private void AddMessageToDisplay(string senderUsername, string content, DateTime timestamp, bool isMyMessage)
        {
            // 1. Tạo một instance mới của MessageBubbleControl
            var messageBubble = new MessageBubbleControl();

            // 2. Cấu hình MessageBubbleControl với dữ liệu tin nhắn
            messageBubble.SetMessage(senderUsername, content, timestamp, isMyMessage);

            // 3. Đặt kích thước của MessageBubbleControl
            // Chiều rộng của bong bóng sẽ bằng chiều rộng của panel chứa nó
            // Trừ đi padding/margin để nó không tràn ra ngoài
            // pnlChatMessages.ClientSize.Width: Chiều rộng khả dụng bên trong panel
            messageBubble.Width = pnlChatMessages.ClientSize.Width - (pnlChatMessages.Padding.Horizontal * 2) - 20; // 20 là để chừa chỗ cho thanh cuộn nếu có

            // 4. Đặt thuộc tính Anchor để nó co giãn theo chiều ngang của panel
            messageBubble.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            // 5. Thêm MessageBubbleControl vào Controls collection của pnlChatMessages
            pnlChatMessages.Controls.Add(messageBubble);

            // 6. Tính toán và đặt vị trí Y cho bong bóng tin nhắn mới
            // Các control được xếp chồng lên nhau theo chiều dọc
            int yPos = pnlChatMessages.Padding.Top; // Bắt đầu từ padding trên cùng
            foreach (Control ctrl in pnlChatMessages.Controls)
            {
                if (ctrl != messageBubble) // Tính vị trí của các control đã tồn tại
                {
                    yPos += ctrl.Height + ctrl.Margin.Vertical; // Cộng thêm chiều cao và margin của control đó
                }
            }
            messageBubble.Location = new Point(pnlChatMessages.Padding.Left, yPos);

            // 7. Đảm bảo tin nhắn mới nhất hiển thị ở dưới cùng
            // Vì chúng ta thêm vào cuối Controls collection, nó sẽ tự động hiển thị ở dưới cùng
            // Nhưng nếu bạn muốn đảm bảo thứ tự render, có thể dùng BringToFront hoặc SendToBack.
            // Trong trường hợp này, việc thêm vào cuối và tính yPos là đủ.

            ScrollToBottom(); // Cuộn xuống tin nhắn mới nhất
        }


        // DTO để ánh xạ dữ liệu tin nhắn từ API
        // Bạn cần đảm bảo các thuộc tính khớp với JSON trả về từ API của bạn
        public class MessageDisplayData
        {
            public int messageId { get; set; }
            public int senderId { get; set; }
            public string SenderUsername { get; set; }
            public int chatRoomId { get; set; }
            public string Content { get; set; }
            public DateTime createdAt { get; set; }
        }

        public class ChatroomDisplayData
        {
            public int messageId { get; set; }
            public int senderId { get; set; }
            public string SenderUsername { get; set; }
            public int chatRoomId { get; set; }
            public string message { get; set; }
            public DateTime createdAt { get; set; }
        }

        // Phương thức trợ giúp để thêm tin nhắn vào RichTextBox với định dạng chuyên nghiệp
        private void AddSystemMessage(string message)
        {
            var systemMessageLabel = new Label
            {
                Text = message,
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font(this.Font.FontFamily, 8),
                Padding = new Padding(5),
                Margin = new Padding(5)
            };
            // Giới hạn chiều rộng của label để nó không tràn ra ngoài panel
            systemMessageLabel.MaximumSize = new Size(pnlChatMessages.ClientSize.Width - 10, 0);
            systemMessageLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            pnlChatMessages.Controls.Add(systemMessageLabel);

            ScrollToBottom();
        }
    }
}
