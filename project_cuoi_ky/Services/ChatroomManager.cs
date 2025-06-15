using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace project_cuoi_ky.Services
{
    public class ChatroomManager
    {
        private List<ChatroomInfo> _allChatrooms;
        private List<ChatroomInfo> _filteredChatrooms;
        private ChatroomCard _selectedChatroomCard;
        private FlowLayoutPanel _chatroomListPanel;
        
        public event Action<int> ChatroomSelected;
        public event Action<string> StatusChanged;
        
        public int SelectedChatroomId { get; private set; }
        public ChatroomInfo SelectedChatroom => _allChatrooms?.FirstOrDefault(c => c.chatRoomId == SelectedChatroomId);
        
        public ChatroomManager(FlowLayoutPanel chatroomListPanel)
        {
            _chatroomListPanel = chatroomListPanel;
            _allChatrooms = new List<ChatroomInfo>();
            _filteredChatrooms = new List<ChatroomInfo>();
        }        
        
        public void LoadChatrooms(List<ChatroomInfo> chatrooms)
        {
            _allChatrooms = chatrooms ?? new List<ChatroomInfo>();
            _filteredChatrooms = new List<ChatroomInfo>(_allChatrooms);
            
            if (_allChatrooms.Count > 0)
            {
                DisplayChatrooms();
                
                // Auto-select first chatroom if none selected
                if (SelectedChatroomId <= 0)
                {
                    SelectChatroom(_allChatrooms[0].chatRoomId);
                }
                
                StatusChanged?.Invoke($"Loaded {_allChatrooms.Count} chatrooms");
            }
            else
            {
                ShowNoChatroomsMessage();
            }
        }        public void ShowNoChatroomsMessage()
        {
            try
            {
                _chatroomListPanel.Controls.Clear();
                
                var noDataLabel = new Label
                {
                    Text = "No chatrooms found.\nPlease check your connection or contact support.\n\nDouble-click status bar to retry.",
                    Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular),
                    ForeColor = System.Drawing.Color.Gray,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Width = _chatroomListPanel.Width - 20,
                    Height = 120,
                    Margin = new Padding(10)
                };
                
                _chatroomListPanel.Controls.Add(noDataLabel);
                StatusChanged?.Invoke("No chatrooms available");
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Error displaying no chatrooms message: {ex.Message}");
            }
        }

        private void DisplayChatrooms()
        {
            try
            {
                _chatroomListPanel.Controls.Clear();
                
                foreach (var chatroom in _filteredChatrooms)
                {
                    var chatroomCard = new ChatroomCard();
                    chatroomCard.SetChatroomInfo(chatroom);
                    chatroomCard.ChatroomSelected += OnChatroomCardSelected;
                    chatroomCard.Width = _chatroomListPanel.Width - 20; // Leave some margin
                    
                    _chatroomListPanel.Controls.Add(chatroomCard);
                }
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Error displaying chatrooms: {ex.Message}");
            }
        }

        private void OnChatroomCardSelected(object sender, int chatroomId)
        {
            SelectChatroom(chatroomId);
        }

        public void SelectChatroom(int chatroomId)
        {
            try
            {
                var selectedChatroom = _allChatrooms.FirstOrDefault(c => c.chatRoomId == chatroomId);
                if (selectedChatroom == null) return;
                
                // Deselect previous chatroom card
                if (_selectedChatroomCard != null)
                {
                    _selectedChatroomCard.SetSelected(false);
                }
                
                // Find and select new chatroom card by chatroom ID
                foreach (Control control in _chatroomListPanel.Controls)
                {
                    if (control is ChatroomCard card && card.ChatroomId == chatroomId)
                    {
                        card.SetSelected(true);
                        _selectedChatroomCard = card;
                        break;
                    }
                }
                
                SelectedChatroomId = chatroomId;
                ChatroomSelected?.Invoke(chatroomId);
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Error selecting chatroom: {ex.Message}");
            }
        }

        public void FilterChatrooms(string searchText)
        {
            try
            {
                searchText = searchText?.Trim().ToLower() ?? "";
                
                if (string.IsNullOrEmpty(searchText) || searchText == "search chatrooms...")
                {
                    _filteredChatrooms = new List<ChatroomInfo>(_allChatrooms);
                }
                else
                {
                    _filteredChatrooms = _allChatrooms.Where(c => 
                        c.name.ToLower().Contains(searchText)).ToList();
                }
                
                DisplayChatrooms();
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Error filtering chatrooms: {ex.Message}");
            }
        }

        public ChatroomInfo GetChatroomById(int chatroomId)
        {
            return _allChatrooms?.FirstOrDefault(c => c.chatRoomId == chatroomId);
        }
    }
}
