using System;
using System.Collections.Generic;

namespace project_cuoi_ky
{
    public class ChatroomInfo
    {
        public int chatRoomId { get; set; }
        public string name { get; set; } = "";
        public string description { get; set; } = "";
        public bool isGroup { get; set; }
        public bool isPrivate { get; set; }
        public bool isArchived { get; set; }
        public int createdBy { get; set; }
        public string creatorName { get; set; } = "";
        public DateTime createdAt { get; set; }
        public DateTime lastActivity { get; set; }
        public int ParticipantCount { get; set; }
        public List<int> Members { get; set; }
        
        // public ChatroomInfo()
        // {
        //     Members = new List<int>();
        // }
    }
    
    public class GetChatroomsResponse
    {
        public List<ChatroomInfo> Chatrooms { get; set; }
        
        public GetChatroomsResponse()
        {
            Chatrooms = new List<ChatroomInfo>();
        }
    }
}
