using System;
using System.Collections.Generic;

namespace project_cuoi_ky
{
    public class ChatroomInfo
    {
        public int ChatRoomId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsGroup { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsArchived { get; set; }
        public int CreatedBy { get; set; }
        public string CreatorName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime LastActivity { get; set; }
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
