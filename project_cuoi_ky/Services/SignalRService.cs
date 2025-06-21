using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_cuoi_ky.Services
{
    public class SignalRService
    {
        private HubConnection _hubConnection;
        private readonly string _hubUrl;
        
        public event Action<MessageDisplayData> MessageReceived;
        public event Action<string, string, string> NotificationReceived;
        public event Action<string, string, string, string> UserJoinedChatroom;
        public event Action<string, string, string, string> UserLeftChatroom;
        public event Action<string> ConnectionStatusChanged;
        
        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public SignalRService(string hubUrl)
        {
            _hubUrl = hubUrl;
        }

        public async Task InitializeAsync()
        {
            try
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_hubUrl)
                    .Build();

                // Register event handlers
                _hubConnection.On<MessageDisplayData>("ReceiveMessage", (messageData) =>
                {
                    MessageReceived?.Invoke(messageData);
                });

                _hubConnection.On<string, string, string>("ReceiveNotification", (type, title, body) =>
                {
                    NotificationReceived?.Invoke(type, title, body);
                });

                _hubConnection.On<string, string, string, string>("UserJoinedChatroom", (userId, username, chatroomId, joinedAt) =>
                {
                    UserJoinedChatroom?.Invoke(userId, username, chatroomId, joinedAt);
                });

                _hubConnection.On<string, string, string, string>("UserLeftChatroom", (userId, username, chatroomId, leftAt) =>
                {
                    UserLeftChatroom?.Invoke(userId, username, chatroomId, leftAt);
                });

                _hubConnection.Closed += async (error) =>
                {
                    ConnectionStatusChanged?.Invoke("Connection closed. Reconnecting...");
                    await Task.Delay(5000);
                    await ConnectAsync();
                };

                await ConnectAsync();
            }
            catch (Exception ex)
            {
                ConnectionStatusChanged?.Invoke($"Error initializing SignalR: {ex.Message}");
            }
        }

        private async Task ConnectAsync()
        {
            try
            {
                await _hubConnection.StartAsync();
                ConnectionStatusChanged?.Invoke("Connected to SignalR Hub.");
            }
            catch (Exception ex)
            {
                ConnectionStatusChanged?.Invoke($"Could not connect to SignalR Hub: {ex.Message}");
            }
        }

        public async Task RegisterUserAsync(int userId)
        {
            try
            {
                if (IsConnected)
                {
                    await _hubConnection.InvokeAsync("RegisterUser", userId);
                }
            }
            catch (Exception ex)
            {
                ConnectionStatusChanged?.Invoke($"Error registering user: {ex.Message}");
            }
        }

        public async Task JoinChatroomAsync(int chatroomId, int userId)
        {
            try
            {
                if (IsConnected)
                {
                    await _hubConnection.InvokeAsync("JoinChatroom", chatroomId.ToString(), userId.ToString());
                    ConnectionStatusChanged?.Invoke($"Joined chatroom {chatroomId}.");
                }
            }
            catch (Exception ex)
            {
                ConnectionStatusChanged?.Invoke($"Failed to join chatroom {chatroomId}: {ex.Message}");
            }
        }

        public async Task SendMessageAsync(int userId, int chatroomId, string content)
        {
            try
            {
                if (IsConnected)
                {
                    await _hubConnection.InvokeAsync("SendMessage", userId, chatroomId, content);
                }
                else
                {
                    throw new InvalidOperationException("SignalR not connected");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending message via SignalR: {ex.Message}", ex);
            }
        }

        public async Task SendMessageNotificationAsync(int recipientUserId, int senderId, string senderName, int chatroomId, string messageContent, int messageId)
        {
            try
            {
                if (IsConnected)
                {
                    await _hubConnection.InvokeAsync("SendNotificationToUser", recipientUserId, senderId, senderName, chatroomId, messageContent, messageId);
                }
                else
                {
                    throw new InvalidOperationException("SignalR not connected");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending notification via SignalR: {ex.Message}", ex);
            }
        }

        public async Task SendChatroomNotificationAsync(int chatroomId, int senderId, string senderName, string messageContent, int messageId)
        {
            try
            {
                if (IsConnected)
                {
                    await _hubConnection.InvokeAsync("SendChatroomNotification", chatroomId, senderId, senderName, messageContent, messageId);
                }
                else
                {
                    throw new InvalidOperationException("SignalR not connected");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending chatroom notification via SignalR: {ex.Message}", ex);
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                if (_hubConnection != null && _hubConnection.State != HubConnectionState.Disconnected)
                {
                    await _hubConnection.StopAsync();
                    await _hubConnection.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                ConnectionStatusChanged?.Invoke($"Error disconnecting: {ex.Message}");
            }
        }
    }
}
