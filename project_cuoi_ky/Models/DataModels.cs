using System;

namespace project_cuoi_ky
{
    // DTO để ánh xạ dữ liệu tin nhắn từ Enhanced API
    // Updated to match server response format
    public class MessageDisplayData
    {
        public int messageId { get; set; }
        public int senderId { get; set; }
        public string SenderName { get; set; } = "";
        public int chatRoomId { get; set; }
        public string content { get; set; } = "";
        public DateTime createdAt { get; set; }
    }

    // Enhanced message data structure for new API
    public class EnhancedMessageData
    {
        public int messageId { get; set; }
        public int userId { get; set; }
        public int senderId { get; set; }
        public string SenderName { get; set; } = "";
        public int chatRoomId { get; set; }
        public string content { get; set; } = "";
        public string MessageType { get; set; } = "";
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool isDeleted { get; set; }
        public bool IsEdited { get; set; }
    }

    public class ChatroomDisplayData
    {
        public int messageId { get; set; }
        public int senderId { get; set; }
        public string SenderUsername { get; set; } = "";
        public int chatRoomId { get; set; }
        public string message { get; set; } = "";
        public DateTime createdAt { get; set; }
    }
}
