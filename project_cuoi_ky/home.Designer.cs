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
            this.btnSend = new System.Windows.Forms.Button();
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.lvChat = new System.Windows.Forms.ListView();
            this.lvInfo = new System.Windows.Forms.ListView();
            this.chatTitleLabel = new System.Windows.Forms.Label();
            this.contactsLabel = new System.Windows.Forms.Label();
            this.userStatusPanel = new System.Windows.Forms.Panel();
            this.userProfilePicture = new System.Windows.Forms.PictureBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.userStatusLabel = new System.Windows.Forms.Label();
            this.userStatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userProfilePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSend.Location = new System.Drawing.Point(502, 415);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 26);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            // 
            // tbMsg
            // 
            this.tbMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMsg.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMsg.Location = new System.Drawing.Point(226, 415);
            this.tbMsg.Multiline = true;
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.Size = new System.Drawing.Size(270, 26);
            this.tbMsg.TabIndex = 1;
            // 
            // lvChat
            // 
            this.lvChat.BackColor = System.Drawing.Color.White;
            this.lvChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvChat.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvChat.HideSelection = false;
            this.lvChat.Location = new System.Drawing.Point(226, 40);
            this.lvChat.Name = "lvChat";
            this.lvChat.Size = new System.Drawing.Size(351, 369);
            this.lvChat.TabIndex = 2;
            this.lvChat.UseCompatibleStateImageBehavior = false;
            this.lvChat.View = System.Windows.Forms.View.List;
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
            this.chatTitleLabel.Size = new System.Drawing.Size(105, 21);
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
            this.contactsLabel.Size = new System.Drawing.Size(76, 21);
            this.contactsLabel.TabIndex = 5;
            this.contactsLabel.Text = "Contacts";
            // 
            // userStatusPanel
            // 
            this.userStatusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.userStatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userStatusPanel.Controls.Add(this.userStatusLabel);
            this.userStatusPanel.Controls.Add(this.userNameLabel);
            this.userStatusPanel.Controls.Add(this.userProfilePicture);
            this.userStatusPanel.Location = new System.Drawing.Point(13, 409);
            this.userStatusPanel.Name = "userStatusPanel";
            this.userStatusPanel.Size = new System.Drawing.Size(207, 32);
            this.userStatusPanel.TabIndex = 6;
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
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLabel.Location = new System.Drawing.Point(34, 3);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(68, 15);
            this.userNameLabel.TabIndex = 1;
            this.userNameLabel.Text = "User Name";
            // 
            // userStatusLabel
            // 
            this.userStatusLabel.AutoSize = true;
            this.userStatusLabel.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userStatusLabel.ForeColor = System.Drawing.Color.Green;
            this.userStatusLabel.Location = new System.Drawing.Point(34, 15);
            this.userStatusLabel.Name = "userStatusLabel";
            this.userStatusLabel.Size = new System.Drawing.Size(43, 13);
            this.userStatusLabel.TabIndex = 2;
            this.userStatusLabel.Text = "Online";
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(589, 450);
            this.Controls.Add(this.userStatusPanel);
            this.Controls.Add(this.contactsLabel);
            this.Controls.Add(this.chatTitleLabel);
            this.Controls.Add(this.lvInfo);
            this.Controls.Add(this.lvChat);
            this.Controls.Add(this.tbMsg);
            this.Controls.Add(this.btnSend);
            this.Name = "home";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat4 - Home";
            this.userStatusPanel.ResumeLayout(false);
            this.userStatusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userProfilePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.ListView lvChat;
        private System.Windows.Forms.ListView lvInfo;
        private System.Windows.Forms.Label chatTitleLabel;
        private System.Windows.Forms.Label contactsLabel;
        private System.Windows.Forms.Panel userStatusPanel;
        private System.Windows.Forms.Label userStatusLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.PictureBox userProfilePicture;
    }
}
