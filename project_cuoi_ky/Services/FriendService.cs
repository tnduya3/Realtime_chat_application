using project_cuoi_ky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace project_cuoi_ky.Services
{
    public class FriendService
    {
        private readonly string _apiBaseUrl;
        private readonly HttpClient _httpClient;

        public FriendService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
            _httpClient = new HttpClient();
        }        
        
        // Get all users on server
        public async Task<GetUserResponse> GetAllUsers()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}Users");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    // API returns array of users directly
                    var apiUsersList = JsonSerializer.Deserialize<List<ApiUserInfo>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    
                    if (apiUsersList != null)
                    {
                        // Convert ApiUserInfo to UserInfo
                        var usersList = apiUsersList.Select(u => new ApiUserInfo
                        {
                            userId = u.userId,
                            userName = u.userName,
                            email = u.email,
                            isOnline = u.isOnline,
                            isActive = u.isActive
                            // Skip DateTime fields
                        }).ToList();
                        
                        return new GetUserResponse { Users = usersList };
                    }                
                }
                
                return new GetUserResponse();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all users: {ex.Message}");
                return new GetUserResponse();
            }
        }

        // Send friend request
        public async Task<bool> SendFriendRequest(int userId, int friendId)
        {
            try
            {
                var requestDto = new project_cuoi_ky.Models.FriendRequestDto
                {
                    userId = userId,
                    friendId = friendId
                };

                var jsonString = JsonSerializer.Serialize(requestDto);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}Friends/send-request", content);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error sending friend request: {ex.Message}");
                return false;
            }
        }        
        
        // Accept friend request
        public async Task<bool> AcceptFriendRequest(int userId, int friendId)
        {
            try
            {
                var requestDto = new project_cuoi_ky.Models.FriendRequestDto
                {
                    userId = userId,
                    friendId = friendId
                };

                var jsonString = JsonSerializer.Serialize(requestDto);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                System.Diagnostics.Debug.WriteLine($"Accepting friend request: userId={userId}, friendId={friendId}");
                System.Diagnostics.Debug.WriteLine($"API URL: {_apiBaseUrl}Friends/accept-request");
                System.Diagnostics.Debug.WriteLine($"Request payload: {jsonString}");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}Friends/accept-request", content);
                
                System.Diagnostics.Debug.WriteLine($"Accept friend request response: {response.StatusCode}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Accept friend request error response: {errorContent}");
                }
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error accepting friend request: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        
        // Reject friend request
        public async Task<bool> RejectFriendRequest(int userId, int friendId)
        {
            try
            {
                var requestDto = new project_cuoi_ky.Models.FriendRequestDto
                {
                    userId = userId,
                    friendId = friendId
                };

                var jsonString = JsonSerializer.Serialize(requestDto);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}Friends/reject-request", content);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error rejecting friend request: {ex.Message}");
                return false;
            }
        }

        // Get user's friends
        public async Task<project_cuoi_ky.Models.GetFriendsResponse> GetUserFriends(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}Friends/{userId}/friends");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    try
                    {
                        var friendsList = JsonSerializer.Deserialize<List<project_cuoi_ky.Models.FriendInfo>>(jsonString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        if (friendsList != null)
                        {
                            return new project_cuoi_ky.Models.GetFriendsResponse { Friends = friendsList };
                        }
                    }
                    catch
                    {
                        var friendsResponse = JsonSerializer.Deserialize<project_cuoi_ky.Models.GetFriendsResponse>(jsonString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        return friendsResponse ?? new project_cuoi_ky.Models.GetFriendsResponse();
                    }
                }
                
                return new project_cuoi_ky.Models.GetFriendsResponse();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting user friends: {ex.Message}");
                return new project_cuoi_ky.Models.GetFriendsResponse();
            }
        }

        // Get pending friend requests
        public async Task<GetFriendRequestsResponse> GetPendingRequests(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}Friends/{userId}/pending-requests");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    try
                    {
                        // API returns array of user objects directly
                        var usersList = JsonSerializer.Deserialize<List<project_cuoi_ky.Models.ApiUserInfo>>(jsonString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        if (usersList != null)
                        {
                            return new GetFriendRequestsResponse { Requests = usersList };
                        }
                    }
                    catch
                    {
                        var requestsResponse = JsonSerializer.Deserialize<GetFriendRequestsResponse>(jsonString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        return requestsResponse ?? new GetFriendRequestsResponse();
                    }
                }
                
                return new GetFriendRequestsResponse();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting pending requests: {ex.Message}");
                return new GetFriendRequestsResponse();
            }
        }

        // Block user
        public async Task<bool> BlockUser(int userId, int friendId)
        {
            try
            {
                var requestDto = new project_cuoi_ky.Models.FriendRequestDto
                {
                    userId = userId,
                    friendId = friendId
                };

                var jsonString = JsonSerializer.Serialize(requestDto);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}Friends/block-user", content);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error blocking user: {ex.Message}");
                return false;
            }
        }

        // Unfriend user
        public async Task<bool> UnfriendUser(int userId, int friendId)
        {
            try
            {
                var requestDto = new project_cuoi_ky.Models.FriendRequestDto
                {
                    userId = userId,
                    friendId = friendId
                };

                var jsonString = JsonSerializer.Serialize(requestDto);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}Friends/unfriend", content);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error unfriending user: {ex.Message}");
                return false;
            }
        }        
        
        // Get friendship status between two users
        public async Task<(FriendshipStatus status, string rawStatus)> GetFriendshipStatus(int userId, int friendId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}Friends/{userId}/status/{friendId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    var statusResponse = JsonSerializer.Deserialize<FriendshipStatusResponse>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    
                    if (statusResponse?.Status != null)
                    {
                        var parsedStatus = ParseFriendshipStatus(statusResponse.Status);
                        return (parsedStatus, statusResponse.Status);
                    }
                }
                
                return (FriendshipStatus.None, "none");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting friendship status: {ex.Message}");
                return (FriendshipStatus.None, "error");
            }
        }
          
        private FriendshipStatus ParseFriendshipStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                return FriendshipStatus.None;
                
            switch (status.ToLower())
            {
                case "accepted":
                    return FriendshipStatus.Friend;
                case "pending":
                    return FriendshipStatus.RequestSent;
                case "blocked":
                    return FriendshipStatus.Blocked;
                case "none":
                    return FriendshipStatus.None;
                default:
                    return FriendshipStatus.None;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
