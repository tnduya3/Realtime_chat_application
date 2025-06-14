namespace project_cuoi_ky
{
    partial class register_form
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.txtRegisterEmail = new System.Windows.Forms.TextBox();
            this.txtRegisterPassword = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.retypeLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.retypeTB = new System.Windows.Forms.TextBox();
            this.userTB = new System.Windows.Forms.TextBox();
            this.registerGB = new System.Windows.Forms.GroupBox();
            this.registerGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.titleLabel.Location = new System.Drawing.Point(171, 8);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(482, 65);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Register Information";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnRegister.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegister.Location = new System.Drawing.Point(659, 405);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(129, 33);
            this.btnRegister.TabIndex = 10;
            this.btnRegister.Text = "Register and Login";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // backBtn
            // 
            this.backBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.backBtn.Location = new System.Drawing.Point(-7, 8);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(103, 23);
            this.backBtn.TabIndex = 11;
            this.backBtn.Text = "Back to Login";
            this.backBtn.UseVisualStyleBackColor = false;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(243, 155);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password";
            // 
            // txtRegisterEmail
            // 
            this.txtRegisterEmail.Location = new System.Drawing.Point(243, 128);
            this.txtRegisterEmail.Name = "txtRegisterEmail";
            this.txtRegisterEmail.Size = new System.Drawing.Size(244, 20);
            this.txtRegisterEmail.TabIndex = 4;
            // 
            // txtRegisterPassword
            // 
            this.txtRegisterPassword.Location = new System.Drawing.Point(243, 172);
            this.txtRegisterPassword.Name = "txtRegisterPassword";
            this.txtRegisterPassword.Size = new System.Drawing.Size(244, 20);
            this.txtRegisterPassword.TabIndex = 6;
            this.txtRegisterPassword.UseSystemPasswordChar = true;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(243, 111);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(32, 13);
            this.emailLabel.TabIndex = 3;
            this.emailLabel.Text = "Email";
            // 
            // retypeLabel
            // 
            this.retypeLabel.AutoSize = true;
            this.retypeLabel.Location = new System.Drawing.Point(243, 199);
            this.retypeLabel.Name = "retypeLabel";
            this.retypeLabel.Size = new System.Drawing.Size(112, 13);
            this.retypeLabel.TabIndex = 7;
            this.retypeLabel.Text = "Retype your password";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(243, 68);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(60, 13);
            this.userLabel.TabIndex = 1;
            this.userLabel.Text = "User Name";
            // 
            // retypeTB
            // 
            this.retypeTB.Location = new System.Drawing.Point(243, 216);
            this.retypeTB.Name = "retypeTB";
            this.retypeTB.Size = new System.Drawing.Size(244, 20);
            this.retypeTB.TabIndex = 8;
            this.retypeTB.UseSystemPasswordChar = true;
            // 
            // userTB
            // 
            this.userTB.Location = new System.Drawing.Point(243, 84);
            this.userTB.Name = "userTB";
            this.userTB.Size = new System.Drawing.Size(244, 20);
            this.userTB.TabIndex = 2;
            // 
            // registerGB
            // 
            this.registerGB.Controls.Add(this.userTB);
            this.registerGB.Controls.Add(this.retypeTB);
            this.registerGB.Controls.Add(this.userLabel);
            this.registerGB.Controls.Add(this.retypeLabel);
            this.registerGB.Controls.Add(this.emailLabel);
            this.registerGB.Controls.Add(this.txtRegisterPassword);
            this.registerGB.Controls.Add(this.txtRegisterEmail);
            this.registerGB.Controls.Add(this.passwordLabel);
            this.registerGB.Location = new System.Drawing.Point(12, 76);
            this.registerGB.Name = "registerGB";
            this.registerGB.Size = new System.Drawing.Size(776, 362);
            this.registerGB.TabIndex = 9;
            this.registerGB.TabStop = false;
            this.registerGB.Text = "Detail";
            // 
            // register_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.registerGB);
            this.Controls.Add(this.titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "register_form";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "register_form";
            this.registerGB.ResumeLayout(false);
            this.registerGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox txtRegisterEmail;
        private System.Windows.Forms.TextBox txtRegisterPassword;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label retypeLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.TextBox retypeTB;
        private System.Windows.Forms.TextBox userTB;
        private System.Windows.Forms.GroupBox registerGB;
    }
}