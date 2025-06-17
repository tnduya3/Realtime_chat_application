using System;
using System.Collections.Generic;

namespace project_cuoi_ky.Models
{
    // Simplified model for API responses - only essential fields
    public class ApiUserInfo
    {
        public int userId { get; set; }
        public string userName { get; set; } = "";
        public string email { get; set; } = "";
        public bool isOnline { get; set; }
        public bool isActive { get; set; }
        // Removed DateTime fields to avoid JSON parsing issues
    }

    public class UserInfo
    {
        public int userId { get; set; }
        public string userName { get; set; } = "";
        public string email { get; set; } = "";
        public string token { get; set; } = "";
        public bool isOnline { get; set; }
        public bool isActive { get; set; }
        public string deviceToken { get; set; } = "";
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public List<FriendInfo> sentFriendRequests { get; set; }
        public List<FriendInfo> receivedFriendRequests { get; set; }
        
        public UserInfo()
        {
            sentFriendRequests = new List<FriendInfo>();
            receivedFriendRequests = new List<FriendInfo>();
        }
    }

    public class FriendInfo
    {
        public int userId { get; set; }
        public string userName { get; set; } = "";
        public string email { get; set; } = "";
        public bool isOnline { get; set; }
        public DateTime friendshipDate { get; set; }
    }

    public class FriendRequest
    {
        public int requestId { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string senderName { get; set; } = "";
        public string receiverName { get; set; } = "";
        public DateTime sentAt { get; set; }
        public string status { get; set; } = ""; // pending, accepted, rejected
    }

    public class FriendRequestDto
    {
        public int userId { get; set; }
        public int friendId { get; set; }
    }

    public class GetUsersResponse
    {
        public List<UserInfo> Users { get; set; }
        
        public GetUsersResponse()
        {
            Users = new List<UserInfo>();
        }
    }

    public class GetUserResponse
    {
        public List<ApiUserInfo> Users { get; set; }

        public GetUserResponse()
        {
            Users = new List<ApiUserInfo>();
        }
    }

    public class GetFriendsResponse
    {
        public List<FriendInfo> Friends { get; set; }
        
        public GetFriendsResponse()
        {
            Friends = new List<FriendInfo>();
        }
    }    public class GetFriendRequestsResponse
    {
        public List<ApiUserInfo> Requests { get; set; }
        
        public GetFriendRequestsResponse()
        {
            Requests = new List<ApiUserInfo>();
        }
    }

    public class FriendshipStatusResponse
    {
        public string Status { get; set; } = "";
    }

    public enum FriendshipStatus
    {
        None,           // Chưa có quan hệ gì
        Friend,         // Đã là bạn
        RequestSent,    // Đã gửi lời mời
        RequestReceived, // Nhận được lời mời
        Blocked         // Bị block
    }
}
