namespace project_cuoi_ky
{
    partial class home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(home));
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlChatroomList = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearchChatrooms = new System.Windows.Forms.TextBox();
            this.contactsLabel = new System.Windows.Forms.Label();
            this.userStatusPanel = new System.Windows.Forms.Panel();
            this.btnFriend = new System.Windows.Forms.Button();
            this.userStatusLabel = new System.Windows.Forms.Label();
            this.txtCurrentUserId = new System.Windows.Forms.Label();
            this.userProfilePicture = new System.Windows.Forms.PictureBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtMessageInput = new System.Windows.Forms.TextBox();
            this.pnlChatMessages = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEditChatroom = new System.Windows.Forms.Button();
            this.chatTitleLabel = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.userStatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userProfilePicture)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.SystemColors.Control;
            this.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeft.Controls.Add(this.pnlChatroomList);
            this.pnlLeft.Controls.Add(this.txtSearchChatrooms);
            this.pnlLeft.Controls.Add(this.contactsLabel);
            this.pnlLeft.Controls.Add(this.userStatusPanel);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(300, 450);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlChatroomList
            // 
            this.pnlChatroomList.AutoScroll = true;
            this.pnlChatroomList.BackColor = System.Drawing.SystemColors.Control;
            this.pnlChatroomList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlChatroomList.Location = new System.Drawing.Point(0, 80);
            this.pnlChatroomList.Name = "pnlChatroomList";
            this.pnlChatroomList.Size = new System.Drawing.Size(298, 335);
            this.pnlChatroomList.TabIndex = 7;
            this.pnlChatroomList.WrapContents = false;
            // 
            // txtSearchChatrooms
            // 
            this.txtSearchChatrooms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchChatrooms.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchChatrooms.ForeColor = System.Drawing.Color.Gray;
            this.txtSearchChatrooms.Location = new System.Drawing.Point(10, 45);
            this.txtSearchChatrooms.Name = "txtSearchChatrooms";
            this.txtSearchChatrooms.Size = new System.Drawing.Size(278, 27);
            this.txtSearchChatrooms.TabIndex = 6;
            this.txtSearchChatrooms.Text = "Search chatrooms...";
            this.txtSearchChatrooms.TextChanged += new System.EventHandler(this.txtSearchChatrooms_TextChanged);
            this.txtSearchChatrooms.Enter += new System.EventHandler(this.txtSearchChatrooms_Enter);
            this.txtSearchChatrooms.Leave += new System.EventHandler(this.txtSearchChatrooms_Leave);
            // 
            // contactsLabel
            // 
            this.contactsLabel.AutoSize = true;
            this.contactsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.contactsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.contactsLabel.Location = new System.Drawing.Point(10, 10);
            this.contactsLabel.Name = "contactsLabel";
            this.contactsLabel.Size = new System.Drawing.Size(119, 30);
            this.contactsLabel.TabIndex = 5;
            this.contactsLabel.Text = "Chatrooms";
            // 
            // userStatusPanel
            // 
            this.userStatusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.userStatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userStatusPanel.Controls.Add(this.btnFriend);
            this.userStatusPanel.Controls.Add(this.userStatusLabel);
            this.userStatusPanel.Controls.Add(this.txtCurrentUserId);
            this.userStatusPanel.Controls.Add(this.userProfilePicture);
            this.userStatusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userStatusPanel.Location = new System.Drawing.Point(0, 416);
            this.userStatusPanel.Name = "userStatusPanel";
            this.userStatusPanel.Size = new System.Drawing.Size(298, 32);
            this.userStatusPanel.TabIndex = 6;
            // 
            // btnFriend
            // 
            this.btnFriend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnFriend.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFriend.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFriend.Location = new System.Drawing.Point(163, -3);
            this.btnFriend.Name = "btnFriend";
            this.btnFriend.Size = new System.Drawing.Size(139, 40);
            this.btnFriend.TabIndex = 8;
            this.btnFriend.Text = "Friend";
            this.btnFriend.UseVisualStyleBackColor = false;
            this.btnFriend.Click += new System.EventHandler(this.btnFriend_Click);
            // 
            // userStatusLabel
            // 
            this.userStatusLabel.AutoSize = true;
            this.userStatusLabel.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userStatusLabel.ForeColor = System.Drawing.Color.Green;
            this.userStatusLabel.Location = new System.Drawing.Point(34, 15);
            this.userStatusLabel.Name = "userStatusLabel";
            this.userStatusLabel.Size = new System.Drawing.Size(45, 17);
            this.userStatusLabel.TabIndex = 2;
            this.userStatusLabel.Text = "Online";
            this.userStatusLabel.Click += new System.EventHandler(this.userStatusLabel_Click);
            this.userStatusLabel.DoubleClick += new System.EventHandler(this.userStatusLabel_DoubleClick);
            // 
            // txtCurrentUserId
            // 
            this.txtCurrentUserId.AutoSize = true;
            this.txtCurrentUserId.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentUserId.Location = new System.Drawing.Point(34, 3);
            this.txtCurrentUserId.Name = "txtCurrentUserId";
            this.txtCurrentUserId.Size = new System.Drawing.Size(78, 19);
            this.txtCurrentUserId.TabIndex = 1;
            this.txtCurrentUserId.Text = "User Name";
            this.txtCurrentUserId.Click += new System.EventHandler(this.userStatusLabel_Click);
            this.txtCurrentUserId.DoubleClick += new System.EventHandler(this.userStatusLabel_DoubleClick);
            // 
            // userProfilePicture
            // 
            this.userProfilePicture.BackColor = System.Drawing.Color.White;
            this.userProfilePicture.Location = new System.Drawing.Point(3, 3);
            this.userProfilePicture.Name = "userProfilePicture";
            this.userProfilePicture.Size = new System.Drawing.Size(25, 25);
            this.userProfilePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.userProfilePicture.TabIndex = 0;
            this.userProfilePicture.TabStop = false;
            this.userProfilePicture.DoubleClick += new System.EventHandler(this.userProfilePicture_DoubleClick);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.SystemColors.Control;
            this.pnlRight.Controls.Add(this.btnSendMessage);
            this.pnlRight.Controls.Add(this.txtMessageInput);
            this.pnlRight.Controls.Add(this.pnlChatMessages);
            this.pnlRight.Controls.Add(this.btnEditChatroom);
            this.pnlRight.Controls.Add(this.chatTitleLabel);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(300, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(289, 450);
            this.pnlRight.TabIndex = 1;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMessage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSendMessage.Location = new System.Drawing.Point(204, 415);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(75, 26);
            this.btnSendMessage.TabIndex = 0;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = false;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // txtMessageInput
            // 
            this.txtMessageInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessageInput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageInput.Location = new System.Drawing.Point(10, 415);
            this.txtMessageInput.Multiline = true;
            this.txtMessageInput.Name = "txtMessageInput";
            this.txtMessageInput.Size = new System.Drawing.Size(188, 26);
            this.txtMessageInput.TabIndex = 1;
            this.txtMessageInput.Click += new System.EventHandler(this.txtMessageInput_Click);
            this.txtMessageInput.Enter += new System.EventHandler(this.txtMessageInput_Enter);
            this.txtMessageInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessageInput_KeyDown);
            this.txtMessageInput.Leave += new System.EventHandler(this.txtMessageInput_Leave);
            this.txtMessageInput.MouseLeave += new System.EventHandler(this.txtMessageInput_Leave);
            // 
            // pnlChatMessages
            // 
            this.pnlChatMessages.AutoScroll = true;
            this.pnlChatMessages.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlChatMessages.Location = new System.Drawing.Point(10, 40);
            this.pnlChatMessages.Name = "pnlChatMessages";
            this.pnlChatMessages.Size = new System.Drawing.Size(269, 369);
            this.pnlChatMessages.TabIndex = 9;
            // 
            // btnEditChatroom
            // 
            this.btnEditChatroom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnEditChatroom.FlatAppearance.BorderSize = 0;
            this.btnEditChatroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditChatroom.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnEditChatroom.ForeColor = System.Drawing.Color.White;
            this.btnEditChatroom.Location = new System.Drawing.Point(217, 9);
            this.btnEditChatroom.Name = "btnEditChatroom";
            this.btnEditChatroom.Size = new System.Drawing.Size(60, 25);
            this.btnEditChatroom.TabIndex = 5;
            this.btnEditChatroom.Text = "Edit";
            this.btnEditChatroom.UseVisualStyleBackColor = false;
            this.btnEditChatroom.Visible = false;
            this.btnEditChatroom.Click += new System.EventHandler(this.btnEditChatroom_Click);
            // 
            // chatTitleLabel
            // 
            this.chatTitleLabel.AutoSize = true;
            this.chatTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.chatTitleLabel.Location = new System.Drawing.Point(10, 13);
            this.chatTitleLabel.Name = "chatTitleLabel";
            this.chatTitleLabel.Size = new System.Drawing.Size(160, 25);
            this.chatTitleLabel.TabIndex = 4;
            this.chatTitleLabel.Text = "Select a chatroom";
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(589, 450);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "home";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat4 - Home";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.userStatusPanel.ResumeLayout(false);
            this.userStatusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userProfilePicture)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.FlowLayoutPanel pnlChatroomList;
        private System.Windows.Forms.TextBox txtSearchChatrooms;
        private System.Windows.Forms.Label contactsLabel;
        private System.Windows.Forms.Panel userStatusPanel;
        private System.Windows.Forms.Label userStatusLabel;
        private System.Windows.Forms.Label txtCurrentUserId;
        private System.Windows.Forms.PictureBox userProfilePicture;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox txtMessageInput;        private System.Windows.Forms.FlowLayoutPanel pnlChatMessages;
        private System.Windows.Forms.Label chatTitleLabel;
        private System.Windows.Forms.Button btnEditChatroom;
        private System.Windows.Forms.Button btnFriend;
    }
}
