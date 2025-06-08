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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtMessageInput = new System.Windows.Forms.TextBox();
            this.lvInfo = new System.Windows.Forms.ListView();
            this.chatTitleLabel = new System.Windows.Forms.Label();
            this.contactsLabel = new System.Windows.Forms.Label();
            this.userStatusPanel = new System.Windows.Forms.Panel();
            this.userStatusLabel = new System.Windows.Forms.Label();
            this.txtCurrentUserId = new System.Windows.Forms.Label();
            this.userProfilePicture = new System.Windows.Forms.PictureBox();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.txtCurrentChatroomId = new System.Windows.Forms.TextBox();
            this.userStatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userProfilePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMessage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSendMessage.Location = new System.Drawing.Point(502, 415);
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
            this.txtMessageInput.Location = new System.Drawing.Point(226, 415);
            this.txtMessageInput.Multiline = true;
            this.txtMessageInput.Name = "txtMessageInput";
            this.txtMessageInput.Size = new System.Drawing.Size(270, 26);
            this.txtMessageInput.TabIndex = 1;
            // 
            // lvInfo
            // 
            this.lvInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lvInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvInfo.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvInfo.HideSelection = false;
            this.lvInfo.Location = new System.Drawing.Point(13, 40);
            this.lvInfo.Name = "lvInfo";
            this.lvInfo.Size = new System.Drawing.Size(207, 369);
            this.lvInfo.TabIndex = 3;
            this.lvInfo.UseCompatibleStateImageBehavior = false;
            this.lvInfo.View = System.Windows.Forms.View.List;
            // 
            // chatTitleLabel
            // 
            this.chatTitleLabel.AutoSize = true;
            this.chatTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.chatTitleLabel.Location = new System.Drawing.Point(226, 13);
            this.chatTitleLabel.Name = "chatTitleLabel";
            this.chatTitleLabel.Size = new System.Drawing.Size(107, 21);
            this.chatTitleLabel.TabIndex = 4;
            this.chatTitleLabel.Text = "Chat Window";
            // 
            // contactsLabel
            // 
            this.contactsLabel.AutoSize = true;
            this.contactsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contactsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.contactsLabel.Location = new System.Drawing.Point(13, 13);
            this.contactsLabel.Name = "contactsLabel";
            this.contactsLabel.Size = new System.Drawing.Size(74, 21);
            this.contactsLabel.TabIndex = 5;
            this.contactsLabel.Text = "Contacts";
            // 
            // userStatusPanel
            // 
            this.userStatusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.userStatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userStatusPanel.Controls.Add(this.userStatusLabel);
            this.userStatusPanel.Controls.Add(this.txtCurrentUserId);
            this.userStatusPanel.Controls.Add(this.userProfilePicture);
            this.userStatusPanel.Location = new System.Drawing.Point(13, 409);
            this.userStatusPanel.Name = "userStatusPanel";
            this.userStatusPanel.Size = new System.Drawing.Size(207, 32);
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
            // rtbMessage
            // 
            this.rtbMessage.BackColor = System.Drawing.Color.White;
            this.rtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMessage.Location = new System.Drawing.Point(230, 40);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.ReadOnly = true;
            this.rtbMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbMessage.Size = new System.Drawing.Size(347, 368);
            this.rtbMessage.TabIndex = 7;
            this.rtbMessage.Text = "";
            // 
            // txtCurrentChatroomId
            // 
            this.txtCurrentChatroomId.Location = new System.Drawing.Point(340, 13);
            this.txtCurrentChatroomId.Name = "txtCurrentChatroomId";
            this.txtCurrentChatroomId.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentChatroomId.TabIndex = 8;
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(589, 450);
            this.Controls.Add(this.txtCurrentChatroomId);
            this.Controls.Add(this.rtbMessage);
            this.Controls.Add(this.userStatusPanel);
            this.Controls.Add(this.contactsLabel);
            this.Controls.Add(this.chatTitleLabel);
            this.Controls.Add(this.lvInfo);
            this.Controls.Add(this.txtMessageInput);
            this.Controls.Add(this.btnSendMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "home";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat4 - Home";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.userStatusPanel.ResumeLayout(false);
            this.userStatusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userProfilePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox txtMessageInput;
        private System.Windows.Forms.ListView lvInfo;
        private System.Windows.Forms.Label chatTitleLabel;
        private System.Windows.Forms.Label contactsLabel;
        private System.Windows.Forms.Panel userStatusPanel;
        private System.Windows.Forms.Label userStatusLabel;
        private System.Windows.Forms.Label txtCurrentUserId;
        private System.Windows.Forms.PictureBox userProfilePicture;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.TextBox txtCurrentChatroomId;
    }
}
