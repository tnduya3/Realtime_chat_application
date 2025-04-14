namespace project_cuoi_ky
{
    partial class login
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
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userTB = new System.Windows.Forms.TextBox();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.forgotLabel = new System.Windows.Forms.LinkLabel();
            this.loginBtn = new System.Windows.Forms.Button();
            this.registerLabel = new System.Windows.Forms.Label();
            this.registerLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoBox
            // 
            this.logoBox.Location = new System.Drawing.Point(251, 95);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(158, 145);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logoBox.TabIndex = 0;
            this.logoBox.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.titleLabel.Location = new System.Drawing.Point(253, 243);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(156, 65);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Chat4";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.Location = new System.Drawing.Point(151, 311);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(30, 13);
            this.userLabel.TabIndex = 2;
            this.userLabel.Text = "User";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(151, 340);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(55, 13);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "Password";
            // 
            // userTB
            // 
            this.userTB.Location = new System.Drawing.Point(210, 311);
            this.userTB.Name = "userTB";
            this.userTB.Size = new System.Drawing.Size(261, 20);
            this.userTB.TabIndex = 4;
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(210, 337);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(261, 20);
            this.passwordTB.TabIndex = 5;
            // 
            // forgotLabel
            // 
            this.forgotLabel.ActiveLinkColor = System.Drawing.Color.Purple;
            this.forgotLabel.AutoSize = true;
            this.forgotLabel.LinkColor = System.Drawing.Color.Black;
            this.forgotLabel.Location = new System.Drawing.Point(210, 364);
            this.forgotLabel.Name = "forgotLabel";
            this.forgotLabel.Size = new System.Drawing.Size(91, 13);
            this.forgotLabel.TabIndex = 6;
            this.forgotLabel.TabStop = true;
            this.forgotLabel.Text = "Forgot password?";
            this.forgotLabel.VisitedLinkColor = System.Drawing.Color.Black;
            this.forgotLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotLabel_LinkClicked);
            // 
            // loginBtn
            // 
            this.loginBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.loginBtn.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.loginBtn.Location = new System.Drawing.Point(251, 380);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(158, 36);
            this.loginBtn.TabIndex = 7;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = false;
            // 
            // registerLabel
            // 
            this.registerLabel.AutoSize = true;
            this.registerLabel.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerLabel.Location = new System.Drawing.Point(193, 419);
            this.registerLabel.Name = "registerLabel";
            this.registerLabel.Size = new System.Drawing.Size(180, 13);
            this.registerLabel.TabIndex = 8;
            this.registerLabel.Text = "If this is the first time you visit, let\'s";
            // 
            // registerLinkLabel
            // 
            this.registerLinkLabel.ActiveLinkColor = System.Drawing.Color.Firebrick;
            this.registerLinkLabel.AutoSize = true;
            this.registerLinkLabel.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerLinkLabel.LinkColor = System.Drawing.Color.Black;
            this.registerLinkLabel.Location = new System.Drawing.Point(379, 419);
            this.registerLinkLabel.Name = "registerLinkLabel";
            this.registerLinkLabel.Size = new System.Drawing.Size(91, 13);
            this.registerLinkLabel.TabIndex = 9;
            this.registerLinkLabel.TabStop = true;
            this.registerLinkLabel.Text = "Create an accout";
            this.registerLinkLabel.VisitedLinkColor = System.Drawing.Color.Black;
            this.registerLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerLinkLabel_LinkClicked);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(683, 524);
            this.Controls.Add(this.registerLinkLabel);
            this.Controls.Add(this.registerLabel);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.forgotLabel);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.userTB);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.logoBox);
            this.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.Name = "login";
            this.Opacity = 0.9D;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat4_Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox userTB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.LinkLabel forgotLabel;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Label registerLabel;
        private System.Windows.Forms.LinkLabel registerLinkLabel;
    }
}

