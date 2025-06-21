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
        }        // Chatroom API Methods
        public async Task<GetChatroomsResponse> GetUserChatrooms(int userId)
        {
            try
            {
                // Use the correct endpoint from API documentation
                var url = $"{_apiBaseUrl}Chatrooms/user/{userId}";
                
                var response = await _httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    // Debug: log the response
                    System.Diagnostics.Debug.WriteLine($"API Response from {url}:");
                    System.Diagnostics.Debug.WriteLine(jsonString);
                    
                    // Debug: check if response is empty
                    if (string.IsNullOrWhiteSpace(jsonString))
                    {
                        return new GetChatroomsResponse(); // Return empty response
                    }
                    
                    // Based on the API response, it seems to return a single chatroom object
                    // Let's try to deserialize it as a single chatroom first, then as an array
                    try
                    {
                        // Try to deserialize as an array of chatrooms
                        var chatroomsList = JsonSerializer.Deserialize<List<ChatroomInfo>>(jsonString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        if (chatroomsList != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"Successfully parsed as array: {chatroomsList.Count} chatrooms");
                            return new GetChatroomsResponse { Chatrooms = chatroomsList };
                        }
                    }
                    catch (Exception arrayEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to parse as array: {arrayEx.Message}");
                        
                        // If that fails, try to deserialize as a single chatroom
                        try
                        {
                            var singleChatroom = JsonSerializer.Deserialize<ChatroomInfo>(jsonString, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                            
                            if (singleChatroom != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Successfully parsed as single chatroom: {singleChatroom.name}");
                                return new GetChatroomsResponse { Chatrooms = new List<ChatroomInfo> { singleChatroom } };
                            }
                        }
                        catch (Exception singleEx)
                        {
                            System.Diagnostics.Debug.WriteLine($"Failed to parse as single chatroom: {singleEx.Message}");
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"API call failed with status: {response.StatusCode}");
                }
                
                return new GetChatroomsResponse(); // Return empty response
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting user chatrooms: {ex.Message}");
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

        // Get chatroom participants with detailed information
        public async Task<List<ChatroomParticipant>> GetChatroomParticipants(int chatroomId)
        {
            try
            {
                var url = $"{_apiBaseUrl}Chatrooms/{chatroomId}/participants";
                var response = await _httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    System.Diagnostics.Debug.WriteLine($"Participants API Response from {url}:");
                    System.Diagnostics.Debug.WriteLine(jsonString);
                    
                    if (string.IsNullOrWhiteSpace(jsonString))
                    {
                        return new List<ChatroomParticipant>();
                    }
                    
                    var participants = JsonSerializer.Deserialize<List<ChatroomParticipant>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    
                    return participants ?? new List<ChatroomParticipant>();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Get participants failed with status: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Error response: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting chatroom participants: {ex.Message}");
            }
            
            return new List<ChatroomParticipant>();
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

        // Update chatroom
        public async Task<bool> UpdateChatroom(int chatroomId, string name, string description, int updatedBy)
        {
            try
            {
                var updateRequest = new
                {
                    id = chatroomId,
                    name = name,
                    description = description,
                    updatedBy = updatedBy
                };

                var jsonString = JsonSerializer.Serialize(updateRequest);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                System.Diagnostics.Debug.WriteLine($"Updating chatroom: id={chatroomId}, name={name}");
                System.Diagnostics.Debug.WriteLine($"API URL: {_apiBaseUrl}Chatrooms/{chatroomId}");
                System.Diagnostics.Debug.WriteLine($"Request payload: {jsonString}");

                var response = await _httpClient.PutAsync($"{_apiBaseUrl}Chatrooms/{chatroomId}", content);
                
                System.Diagnostics.Debug.WriteLine($"Update chatroom response: {response.StatusCode}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Update chatroom error response: {errorContent}");
                }
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating chatroom: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        // Add user to chatroom
        public async Task<bool> AddUserToChatroom(int chatroomId, int userIdToAdd, int addedByUserId, string role = "member")
        {
            try
            {
                var addUserData = new
                {
                    role = role,
                    addedBy = addedByUserId
                };
                
                var jsonContent = JsonSerializer.Serialize(addUserData);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var url = $"{_apiBaseUrl}Chatrooms/{chatroomId}/users/{userIdToAdd}";
                
                System.Diagnostics.Debug.WriteLine($"Adding user to chatroom:");
                System.Diagnostics.Debug.WriteLine($"URL: {url}");
                System.Diagnostics.Debug.WriteLine($"Payload: {jsonContent}");
                
                var response = await _httpClient.PostAsync(url, httpContent);
                
                System.Diagnostics.Debug.WriteLine($"Add user response: {response.StatusCode}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Add user error response: {errorContent}");
                }
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding user to chatroom: {ex.Message}");
                return false;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
