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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlChatroomList = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearchChatrooms = new System.Windows.Forms.TextBox();
            this.contactsLabel = new System.Windows.Forms.Label();
            this.userStatusPanel = new System.Windows.Forms.Panel();
            this.userStatusLabel = new System.Windows.Forms.Label();
            this.txtCurrentUserId = new System.Windows.Forms.Label();
            this.userProfilePicture = new System.Windows.Forms.PictureBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtMessageInput = new System.Windows.Forms.TextBox();
            this.pnlChatMessages = new System.Windows.Forms.FlowLayoutPanel();
            this.chatTitleLabel = new System.Windows.Forms.Label();
            this.txtCurrentChatroomId = new System.Windows.Forms.TextBox();
            this.pnlLeft.SuspendLayout();
            this.userStatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userProfilePicture)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeft.Controls.Add(this.pnlChatroomList);
            this.pnlLeft.Controls.Add(this.txtSearchChatrooms);
            this.pnlLeft.Controls.Add(this.contactsLabel);
            this.pnlLeft.Controls.Add(this.userStatusPanel);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(300, 450);
            this.pnlLeft.TabIndex = 0;            // 
            // pnlChatroomList
            // 
            this.pnlChatroomList.AutoScroll = true;
            this.pnlChatroomList.BackColor = System.Drawing.Color.White;
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
            this.txtSearchChatrooms.Location = new System.Drawing.Point(10, 45);
            this.txtSearchChatrooms.Name = "txtSearchChatrooms";
            this.txtSearchChatrooms.Size = new System.Drawing.Size(278, 25);
            this.txtSearchChatrooms.TabIndex = 6;
            this.txtSearchChatrooms.Text = "Search chatrooms...";
            this.txtSearchChatrooms.ForeColor = System.Drawing.Color.Gray;
            this.txtSearchChatrooms.Enter += new System.EventHandler(this.txtSearchChatrooms_Enter);
            this.txtSearchChatrooms.Leave += new System.EventHandler(this.txtSearchChatrooms_Leave);
            this.txtSearchChatrooms.TextChanged += new System.EventHandler(this.txtSearchChatrooms_TextChanged);
            // 
            // contactsLabel
            // 
            this.contactsLabel.AutoSize = true;
            this.contactsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.contactsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.contactsLabel.Location = new System.Drawing.Point(10, 10);
            this.contactsLabel.Name = "contactsLabel";
            this.contactsLabel.Size = new System.Drawing.Size(95, 25);
            this.contactsLabel.TabIndex = 5;
            this.contactsLabel.Text = "Chatrooms";
            // 
            // userStatusPanel
            // 
            this.userStatusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.userStatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userStatusPanel.Controls.Add(this.userStatusLabel);
            this.userStatusPanel.Controls.Add(this.txtCurrentUserId);
            this.userStatusPanel.Controls.Add(this.userProfilePicture);
            this.userStatusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userStatusPanel.Location = new System.Drawing.Point(0, 416);
            this.userStatusPanel.Name = "userStatusPanel";
            this.userStatusPanel.Size = new System.Drawing.Size(298, 32);
            this.userStatusPanel.TabIndex = 6;
            // 
            // userStatusLabel
            // 
            this.userStatusLabel.AutoSize = true;
            this.userStatusLabel.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userStatusLabel.ForeColor = System.Drawing.Color.Green;
            this.userStatusLabel.Location = new System.Drawing.Point(34, 15);
            this.userStatusLabel.Name = "userStatusLabel";
            this.userStatusLabel.Size = new System.Drawing.Size(39, 13);
            this.userStatusLabel.TabIndex = 2;
            this.userStatusLabel.Text = "Online";
            // 
            // txtCurrentUserId
            // 
            this.txtCurrentUserId.AutoSize = true;
            this.txtCurrentUserId.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentUserId.Location = new System.Drawing.Point(34, 3);
            this.txtCurrentUserId.Name = "txtCurrentUserId";
            this.txtCurrentUserId.Size = new System.Drawing.Size(65, 15);
            this.txtCurrentUserId.TabIndex = 1;
            this.txtCurrentUserId.Text = "User Name";
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
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.btnSendMessage);
            this.pnlRight.Controls.Add(this.txtMessageInput);
            this.pnlRight.Controls.Add(this.pnlChatMessages);
            this.pnlRight.Controls.Add(this.chatTitleLabel);
            this.pnlRight.Controls.Add(this.txtCurrentChatroomId);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(300, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(289, 450);
            this.pnlRight.TabIndex = 1;
            this.contactsLabel.Name = "contactsLabel";            // 
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
            // chatTitleLabel
            // 
            this.chatTitleLabel.AutoSize = true;
            this.chatTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.chatTitleLabel.Location = new System.Drawing.Point(10, 13);
            this.chatTitleLabel.Name = "chatTitleLabel";
            this.chatTitleLabel.Size = new System.Drawing.Size(153, 21);
            this.chatTitleLabel.TabIndex = 4;
            this.chatTitleLabel.Text = "Select a chatroom";
            // 
            // txtCurrentChatroomId
            // 
            this.txtCurrentChatroomId.Location = new System.Drawing.Point(169, 13);
            this.txtCurrentChatroomId.Name = "txtCurrentChatroomId";
            this.txtCurrentChatroomId.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentChatroomId.TabIndex = 8;
            this.txtCurrentChatroomId.Visible = false;            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(589, 450);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
        private System.Windows.Forms.TextBox txtMessageInput;
        private System.Windows.Forms.FlowLayoutPanel pnlChatMessages;
        private System.Windows.Forms.Label chatTitleLabel;
        private System.Windows.Forms.TextBox txtCurrentChatroomId;
    }
}
