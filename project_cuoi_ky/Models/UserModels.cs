using System;
using System.Collections.Generic;

namespace project_cuoi_ky
{
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
        public DateTime LastLogin { get; set; }
        public List<int> ChatRoomIds { get; set; } = new List<int>();
    }
}