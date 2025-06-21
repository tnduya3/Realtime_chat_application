namespace project_cuoi_ky
{
    partial class friend_list
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAllUsers = new System.Windows.Forms.TabPage();
            this.pnlAllUsers = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAllUsersStatus = new System.Windows.Forms.Label();
            this.tabFriends = new System.Windows.Forms.TabPage();
            this.pnlFriends = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFriendsStatus = new System.Windows.Forms.Label();
            this.tabRequests = new System.Windows.Forms.TabPage();
            this.pnlRequests = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRequestsStatus = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabAllUsers.SuspendLayout();
            this.tabFriends.SuspendLayout();
            this.tabRequests.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabAllUsers);
            this.tabControl.Controls.Add(this.tabFriends);
            this.tabControl.Controls.Add(this.tabRequests);
            this.tabControl.Location = new System.Drawing.Point(12, 80);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(776, 358);
            this.tabControl.TabIndex = 0;
            // 
            // tabAllUsers
            // 
            this.tabAllUsers.Controls.Add(this.pnlAllUsers);
            this.tabAllUsers.Controls.Add(this.lblAllUsersStatus);
            this.tabAllUsers.Location = new System.Drawing.Point(4, 22);
            this.tabAllUsers.Name = "tabAllUsers";
            this.tabAllUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllUsers.Size = new System.Drawing.Size(768, 332);
            this.tabAllUsers.TabIndex = 0;
            this.tabAllUsers.Text = "All Users";
            this.tabAllUsers.UseVisualStyleBackColor = true;
            // 
            // pnlAllUsers
            // 
            this.pnlAllUsers.AutoScroll = true;
            this.pnlAllUsers.BackColor = System.Drawing.Color.White;
            this.pnlAllUsers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlAllUsers.Location = new System.Drawing.Point(6, 35);
            this.pnlAllUsers.Name = "pnlAllUsers";
            this.pnlAllUsers.Size = new System.Drawing.Size(756, 291);
            this.pnlAllUsers.TabIndex = 1;
            this.pnlAllUsers.WrapContents = false;
            // 
            // lblAllUsersStatus
            // 
            this.lblAllUsersStatus.AutoSize = true;
            this.lblAllUsersStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAllUsersStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblAllUsersStatus.Location = new System.Drawing.Point(6, 10);
            this.lblAllUsersStatus.Name = "lblAllUsersStatus";
            this.lblAllUsersStatus.Size = new System.Drawing.Size(103, 19);
            this.lblAllUsersStatus.TabIndex = 0;
            this.lblAllUsersStatus.Text = "Loading users...";
            // 
            // tabFriends
            // 
            this.tabFriends.Controls.Add(this.pnlFriends);
            this.tabFriends.Controls.Add(this.lblFriendsStatus);
            this.tabFriends.Location = new System.Drawing.Point(4, 22);
            this.tabFriends.Name = "tabFriends";
            this.tabFriends.Padding = new System.Windows.Forms.Padding(3);
            this.tabFriends.Size = new System.Drawing.Size(768, 332);
            this.tabFriends.TabIndex = 1;
            this.tabFriends.Text = "My Friends";
            this.tabFriends.UseVisualStyleBackColor = true;
            // 
            // pnlFriends
            // 
            this.pnlFriends.AutoScroll = true;
            this.pnlFriends.BackColor = System.Drawing.Color.White;
            this.pnlFriends.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlFriends.Location = new System.Drawing.Point(6, 35);
            this.pnlFriends.Name = "pnlFriends";
            this.pnlFriends.Size = new System.Drawing.Size(756, 291);
            this.pnlFriends.TabIndex = 1;
            this.pnlFriends.WrapContents = false;
            // 
            // lblFriendsStatus
            // 
            this.lblFriendsStatus.AutoSize = true;
            this.lblFriendsStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFriendsStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblFriendsStatus.Location = new System.Drawing.Point(6, 10);
            this.lblFriendsStatus.Name = "lblFriendsStatus";
            this.lblFriendsStatus.Size = new System.Drawing.Size(112, 19);
            this.lblFriendsStatus.TabIndex = 0;
            this.lblFriendsStatus.Text = "Loading friends...";
            // 
            // tabRequests
            // 
            this.tabRequests.Controls.Add(this.pnlRequests);
            this.tabRequests.Controls.Add(this.lblRequestsStatus);
            this.tabRequests.Location = new System.Drawing.Point(4, 22);
            this.tabRequests.Name = "tabRequests";
            this.tabRequests.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequests.Size = new System.Drawing.Size(768, 332);
            this.tabRequests.TabIndex = 2;
            this.tabRequests.Text = "Friend Requests";
            this.tabRequests.UseVisualStyleBackColor = true;
            // 
            // pnlRequests
            // 
            this.pnlRequests.AutoScroll = true;
            this.pnlRequests.BackColor = System.Drawing.Color.White;
            this.pnlRequests.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlRequests.Location = new System.Drawing.Point(6, 35);
            this.pnlRequests.Name = "pnlRequests";
            this.pnlRequests.Size = new System.Drawing.Size(756, 291);
            this.pnlRequests.TabIndex = 1;
            this.pnlRequests.WrapContents = false;
            // 
            // lblRequestsStatus
            // 
            this.lblRequestsStatus.AutoSize = true;
            this.lblRequestsStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRequestsStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblRequestsStatus.Location = new System.Drawing.Point(6, 10);
            this.lblRequestsStatus.Name = "lblRequestsStatus";
            this.lblRequestsStatus.Size = new System.Drawing.Size(123, 19);
            this.lblRequestsStatus.TabIndex = 0;
            this.lblRequestsStatus.Text = "Loading requests...";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(12, 50);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 25);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search users...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(170, 30);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Friend Manager";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnRefresh.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefresh.Location = new System.Drawing.Point(623, 50);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(158, 36);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnQuit.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnQuit.Location = new System.Drawing.Point(715, 8);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(73, 36);
            this.btnQuit.TabIndex = 9;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // friend_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "friend_list";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Friend Manager";
            this.Load += new System.EventHandler(this.friend_list_Load);
            this.tabControl.ResumeLayout(false);
            this.tabAllUsers.ResumeLayout(false);
            this.tabAllUsers.PerformLayout();
            this.tabFriends.ResumeLayout(false);
            this.tabFriends.PerformLayout();
            this.tabRequests.ResumeLayout(false);
            this.tabRequests.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAllUsers;
        private System.Windows.Forms.TabPage tabFriends;
        private System.Windows.Forms.TabPage tabRequests;
        private System.Windows.Forms.FlowLayoutPanel pnlAllUsers;
        private System.Windows.Forms.FlowLayoutPanel pnlFriends;
        private System.Windows.Forms.FlowLayoutPanel pnlRequests;
        private System.Windows.Forms.Label lblAllUsersStatus;
        private System.Windows.Forms.Label lblFriendsStatus;
        private System.Windows.Forms.Label lblRequestsStatus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnQuit;
    }
}