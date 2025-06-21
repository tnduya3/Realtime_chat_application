using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace project_cuoi_ky.Services
{
    public class MessageManager
    {
        private FlowLayoutPanel _chatMessagesPanel;
        private int _currentUserId;
        
        public event Action<string> StatusChanged;        
        public MessageManager(FlowLayoutPanel chatMessagesPanel, int currentUserId)
        {
            _chatMessagesPanel = chatMessagesPanel;
            _currentUserId = currentUserId;
            
            // Configure panel for vertical message layout
            _chatMessagesPanel.FlowDirection = FlowDirection.TopDown;
            _chatMessagesPanel.WrapContents = false;
            _chatMessagesPanel.AutoScroll = true;
            _chatMessagesPanel.VerticalScroll.Visible = true;
            _chatMessagesPanel.Padding = new Padding(5);
        }

        public void UpdateCurrentUserId(int userId)
        {
            _currentUserId = userId;
        }

        public void ClearMessages()
        {
            _chatMessagesPanel.Controls.Clear();
        }

        public void LoadMessages(List<MessageDisplayData> messages)
        {
            ClearMessages();
            
            if (messages != null && messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    AddMessageToDisplay(message.SenderName, message.content, message.createdAt, message.senderId != _currentUserId);
                }
            }
            else
            {
                AddSystemMessage("No messages in this chatroom yet.");
            }
        }

        public void AddLoadMessage(int senderUsername, string content, DateTime timestamp, bool isOtherUser)
        {
            try
            {
                // Create message display similar to the old form
                bool isMyMessage = !isOtherUser;
                
                // Create container panel for proper alignment
                var containerPanel = new Panel
                {
                    Width = _chatMessagesPanel.ClientSize.Width - 20,
                    Margin = new Padding(5, 2, 5, 2),
                    BackColor = Color.Transparent
                };
                
                // Create message panel
                var messagePanel = new Panel
                {
                    Width = Math.Min(300, containerPanel.Width - 40), // Max width for bubbles
                    Height = 60, // Will be adjusted based on content
                    BackColor = isMyMessage ? Color.FromArgb(0, 0, 0) : Color.FromArgb(226, 111, 111)
                };
                
                // Create sender label
                var senderLabel = new Label
                {
                    Text = senderUsername.ToString(),
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                    ForeColor = isMyMessage ? Color.White : Color.Black,
                    Location = new Point(10, 5),
                    AutoSize = true
                };
                
                // Create content label
                var contentLabel = new Label
                {
                    Text = content,
                    Font = new Font("Segoe UI", 9f),
                    ForeColor = isMyMessage ? Color.White : Color.Black,
                    Location = new Point(10, 25),
                    MaximumSize = new Size(messagePanel.Width - 20, 0),
                    AutoSize = true
                };
                
                // Create time label
                var timeLabel = new Label
                {
                    Text = timestamp.ToString("HH:mm"),
                    Font = new Font("Segoe UI", 7.5f),
                    ForeColor = isMyMessage ? Color.LightGray : Color.Gray,
                    AutoSize = true
                };
                
                // Adjust panel height based on content
                int contentHeight = contentLabel.PreferredHeight;
                messagePanel.Height = Math.Max(50, contentHeight + 35);
                containerPanel.Height = messagePanel.Height + 10;
                
                // Position time label at bottom right of message panel
                timeLabel.Location = new Point(messagePanel.Width - timeLabel.PreferredWidth - 10, 
                                               messagePanel.Height - timeLabel.PreferredHeight - 5);
                
                // Align message panel within container
                if (isMyMessage)
                {
                    // My messages: align to the right
                    messagePanel.Location = new Point(containerPanel.Width - messagePanel.Width - 10, 5);
                }
                else
                {
                    // Other messages: align to the left
                    messagePanel.Location = new Point(10, 5);
                }
                
                // Add labels to message panel
                messagePanel.Controls.Add(senderLabel);
                messagePanel.Controls.Add(contentLabel);
                messagePanel.Controls.Add(timeLabel);
                
                // Add message panel to container
                containerPanel.Controls.Add(messagePanel);
                
                // Add container to main chat panel
                _chatMessagesPanel.Controls.Add(containerPanel);
                
                ScrollToBottom();
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Error adding message: {ex.Message}");
            }
        }        
        
        public void AddMessageToDisplay(string senderUsername, string content, DateTime timestamp, bool isOtherUser)
        {
            try
            {
                // Create message display similar to the old form
                bool isMyMessage = !isOtherUser;
                
                // Create container panel for proper alignment
                var containerPanel = new Panel
                {
                    Width = _chatMessagesPanel.ClientSize.Width - 20,
                    Margin = new Padding(5, 2, 5, 2),
                    BackColor = Color.Transparent
                };
                
                // Create message panel
                var messagePanel = new Panel
                {
                    Width = Math.Min(300, containerPanel.Width - 40), // Max width for bubbles
                    Height = 60,
                    BackColor = isMyMessage ? Color.FromArgb(226, 111, 111) : Color.FromArgb(250, 250, 250)
                };
                
                // Create sender label
                var senderLabel = new Label
                {
                    Text = senderUsername,
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                    ForeColor = isMyMessage ? Color.White : Color.Black,
                    Location = new Point(10, 5),
                    AutoSize = true
                };
                
                // Create content label
                var contentLabel = new Label
                {
                    Text = content,
                    Font = new Font("Segoe UI", 9f),
                    ForeColor = isMyMessage ? Color.White : Color.Black,
                    Location = new Point(10, 25),
                    MaximumSize = new Size(messagePanel.Width - 20, 0),
                    AutoSize = true
                };
                
                // Create time label
                var timeLabel = new Label
                {
                    Text = timestamp.ToString("HH:mm"),
                    Font = new Font("Segoe UI", 7.5f),
                    ForeColor = isMyMessage ? Color.LightGray : Color.Gray,
                    AutoSize = true
                };
                
                // Adjust panel height based on content
                int contentHeight = contentLabel.PreferredHeight;
                messagePanel.Height = Math.Max(50, contentHeight + 35);
                containerPanel.Height = messagePanel.Height + 10;
                
                // Position time label at bottom right of message panel
                timeLabel.Location = new Point(messagePanel.Width - timeLabel.PreferredWidth - 10, 
                                               messagePanel.Height - timeLabel.PreferredHeight - 5);
                
                // Align message panel within container
                if (isMyMessage)
                {
                    // My messages: align to the right
                    messagePanel.Location = new Point(containerPanel.Width - messagePanel.Width - 10, 5);
                }
                else
                {
                    // Other messages: align to the left
                    messagePanel.Location = new Point(10, 5);
                }
                
                // Add labels to message panel
                messagePanel.Controls.Add(senderLabel);
                messagePanel.Controls.Add(contentLabel);
                messagePanel.Controls.Add(timeLabel);
                
                // Add message panel to container
                containerPanel.Controls.Add(messagePanel);
                
                // Add container to main chat panel
                _chatMessagesPanel.Controls.Add(containerPanel);
                
                ScrollToBottom();
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Error adding message: {ex.Message}");
            }
        }

        public void AddSystemMessage(string message)
        {
            try
            {
                var systemMessageLabel = new Label
                {
                    Text = message,
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 8),
                    Padding = new Padding(5),
                    Margin = new Padding(5)
                };
                
                systemMessageLabel.MaximumSize = new Size(_chatMessagesPanel.ClientSize.Width - 10, 0);
                systemMessageLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                
                _chatMessagesPanel.Controls.Add(systemMessageLabel);
                ScrollToBottom();
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Error adding system message: {ex.Message}");
            }
        }

        private void ScrollToBottom()
        {
            try
            {
                if (_chatMessagesPanel.VerticalScroll.Visible)
                {
                    _chatMessagesPanel.VerticalScroll.Value = _chatMessagesPanel.VerticalScroll.Maximum;
                }
                
                if (_chatMessagesPanel.Controls.Count > 0)
                {
                    _chatMessagesPanel.ScrollControlIntoView(_chatMessagesPanel.Controls[_chatMessagesPanel.Controls.Count - 1]);
                }
            }
            catch (Exception)
            {
                // Ignore scroll errors
            }
        }
    }
}
