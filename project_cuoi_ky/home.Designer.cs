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
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(502, 415);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // tbMsg
            // 
            this.tbMsg.Location = new System.Drawing.Point(226, 415);
            this.tbMsg.Multiline = true;
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.Size = new System.Drawing.Size(270, 20);
            this.tbMsg.TabIndex = 1;
            // 
            // lvChat
            // 
            this.lvChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvChat.HideSelection = false;
            this.lvChat.Location = new System.Drawing.Point(226, 13);
            this.lvChat.Name = "lvChat";
            this.lvChat.Size = new System.Drawing.Size(351, 396);
            this.lvChat.TabIndex = 2;
            this.lvChat.UseCompatibleStateImageBehavior = false;
            // 
            // lvInfo
            // 
            this.lvInfo.HideSelection = false;
            this.lvInfo.Location = new System.Drawing.Point(13, 12);
            this.lvInfo.Name = "lvInfo";
            this.lvInfo.Size = new System.Drawing.Size(207, 423);
            this.lvInfo.TabIndex = 3;
            this.lvInfo.UseCompatibleStateImageBehavior = false;
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 450);
            this.Controls.Add(this.lvInfo);
            this.Controls.Add(this.lvChat);
            this.Controls.Add(this.tbMsg);
            this.Controls.Add(this.btnSend);
            this.Name = "home";
            this.Text = "Home";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.ListView lvChat;
        private System.Windows.Forms.ListView lvInfo;
    }
}