using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace project_cuoi_ky.Services
{
    public class ApiService
    {
        private readonly string _apiBaseUrl;
        private readonly HttpClient _httpClient;

        public ApiService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
            _httpClient = new HttpClient();
        }

        // User API Methods
        public async Task<UserInfo> GetUserInfo(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}users/{userId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserInfo>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log error if needed
            }
            
            return null;
        }

        // Chatroom API Methods
        public async Task<GetChatroomsResponse> GetUserChatrooms(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/Users/{userId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<GetChatroomsResponse>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                
                return new GetChatroomsResponse(); // Return empty response
            }
            catch (Exception)
            {
                return new GetChatroomsResponse(); // Return empty response on error
            }
        }

        public async Task<ChatroomInfo> GetChatroomInfo(int chatroomId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}chatrooms/{chatroomId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ChatroomInfo>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Log error if needed
            }
            
            return null;
        }

        public async Task<List<MessageDisplayData>> GetChatroomMessages(int chatroomId, int page = 1, int pageSize = 20)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}messages/chatrooms/{chatroomId}?page={page}&pageSize={pageSize}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    // Try enhanced format first
                    try
                    {
                        var enhancedMessages = JsonSerializer.Deserialize<List<EnhancedMessageData>>(jsonString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        // Convert to MessageDisplayData
                        var messages = new List<MessageDisplayData>();
                        foreach (var msg in enhancedMessages)
                        {
                            messages.Add(new MessageDisplayData
                            {
                                messageId = msg.messageId,
                                senderId = msg.senderId,
                                SenderName = msg.SenderName,
                                chatRoomId = msg.chatRoomId,
                                content = msg.content,
                                createdAt = msg.createdAt
                            });
                        }
                        return messages;
                    }
                    catch (JsonException)
                    {
                        // Fallback to regular format
                        return JsonSerializer.Deserialize<List<MessageDisplayData>>(jsonString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }) ?? new List<MessageDisplayData>();
                    }
                }
            }
            catch (Exception)
            {
                // Log error if needed
            }
            
            return new List<MessageDisplayData>();
        }

        public async Task<bool> SendMessage(int senderId, int chatroomId, string content)
        {
            try
            {
                var messageData = new
                {
                    SenderId = senderId,
                    ChatRoomId = chatroomId,
                    Content = content,
                    MessageType = "text"
                };
                
                var jsonContent = JsonSerializer.Serialize(messageData);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}messages", httpContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> JoinChatroom(int chatroomId, int userId, string role = "member")
        {
            try
            {
                var joinData = new
                {
                    role = role,
                    addedBy = userId
                };
                
                var jsonContent = JsonSerializer.Serialize(joinData);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}chatrooms/{chatroomId}/users/{userId}", httpContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LoadChatroomParticipants(int chatroomId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}chatrooms/{chatroomId}/participants");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LoadChatroomStats(int chatroomId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}chatrooms/{chatroomId}/stats");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
