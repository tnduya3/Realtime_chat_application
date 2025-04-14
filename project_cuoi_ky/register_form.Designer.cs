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
            this.registerBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.emailTB = new System.Windows.Forms.TextBox();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.retypeLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.retypeTB = new System.Windows.Forms.TextBox();
            this.userTB = new System.Windows.Forms.TextBox();
            this.genderLabel = new System.Windows.Forms.Label();
            this.maleBtn = new System.Windows.Forms.RadioButton();
            this.femaleBtn = new System.Windows.Forms.RadioButton();
            this.otherBtn = new System.Windows.Forms.RadioButton();
            this.birthdayLabel = new System.Windows.Forms.Label();
            this.birthdayPicker = new System.Windows.Forms.DateTimePicker();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.phoneTB = new System.Windows.Forms.TextBox();
            this.cityLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.cityTB = new System.Windows.Forms.ComboBox();
            this.addressTB = new System.Windows.Forms.TextBox();
            this.avatarBox = new System.Windows.Forms.PictureBox();
            this.fileBtn = new System.Windows.Forms.Button();
            this.avatarLabel = new System.Windows.Forms.Label();
            this.registerGB = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).BeginInit();
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
            // registerBtn
            // 
            this.registerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.registerBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.registerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registerBtn.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.registerBtn.Location = new System.Drawing.Point(659, 405);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(129, 33);
            this.registerBtn.TabIndex = 10;
            this.registerBtn.Text = "Register and Login";
            this.registerBtn.UseVisualStyleBackColor = false;
            // 
            // backBtn
            // 
            this.backBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.backBtn.Location = new System.Drawing.Point(12, 8);
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
            this.passwordLabel.Location = new System.Drawing.Point(6, 209);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password";
            // 
            // emailTB
            // 
            this.emailTB.Location = new System.Drawing.Point(6, 182);
            this.emailTB.Name = "emailTB";
            this.emailTB.Size = new System.Drawing.Size(244, 20);
            this.emailTB.TabIndex = 4;
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(6, 226);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(244, 20);
            this.passwordTB.TabIndex = 6;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(6, 165);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(32, 13);
            this.emailLabel.TabIndex = 3;
            this.emailLabel.Text = "Email";
            // 
            // retypeLabel
            // 
            this.retypeLabel.AutoSize = true;
            this.retypeLabel.Location = new System.Drawing.Point(6, 253);
            this.retypeLabel.Name = "retypeLabel";
            this.retypeLabel.Size = new System.Drawing.Size(112, 13);
            this.retypeLabel.TabIndex = 7;
            this.retypeLabel.Text = "Retype your password";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(6, 122);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(60, 13);
            this.userLabel.TabIndex = 1;
            this.userLabel.Text = "User Name";
            // 
            // retypeTB
            // 
            this.retypeTB.Location = new System.Drawing.Point(6, 270);
            this.retypeTB.Name = "retypeTB";
            this.retypeTB.Size = new System.Drawing.Size(244, 20);
            this.retypeTB.TabIndex = 8;
            // 
            // userTB
            // 
            this.userTB.Location = new System.Drawing.Point(6, 138);
            this.userTB.Name = "userTB";
            this.userTB.Size = new System.Drawing.Size(244, 20);
            this.userTB.TabIndex = 2;
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Location = new System.Drawing.Point(421, 122);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(42, 13);
            this.genderLabel.TabIndex = 9;
            this.genderLabel.Text = "Gender";
            // 
            // maleBtn
            // 
            this.maleBtn.AutoSize = true;
            this.maleBtn.Location = new System.Drawing.Point(465, 141);
            this.maleBtn.Name = "maleBtn";
            this.maleBtn.Size = new System.Drawing.Size(48, 17);
            this.maleBtn.TabIndex = 10;
            this.maleBtn.TabStop = true;
            this.maleBtn.Text = "Male";
            this.maleBtn.UseVisualStyleBackColor = true;
            // 
            // femaleBtn
            // 
            this.femaleBtn.AutoSize = true;
            this.femaleBtn.Location = new System.Drawing.Point(556, 141);
            this.femaleBtn.Name = "femaleBtn";
            this.femaleBtn.Size = new System.Drawing.Size(59, 17);
            this.femaleBtn.TabIndex = 11;
            this.femaleBtn.TabStop = true;
            this.femaleBtn.Text = "Female";
            this.femaleBtn.UseVisualStyleBackColor = true;
            // 
            // otherBtn
            // 
            this.otherBtn.AutoSize = true;
            this.otherBtn.Location = new System.Drawing.Point(647, 139);
            this.otherBtn.Name = "otherBtn";
            this.otherBtn.Size = new System.Drawing.Size(60, 17);
            this.otherBtn.TabIndex = 12;
            this.otherBtn.TabStop = true;
            this.otherBtn.Text = "Other...";
            this.otherBtn.UseVisualStyleBackColor = true;
            // 
            // birthdayLabel
            // 
            this.birthdayLabel.AutoSize = true;
            this.birthdayLabel.Location = new System.Drawing.Point(424, 165);
            this.birthdayLabel.Name = "birthdayLabel";
            this.birthdayLabel.Size = new System.Drawing.Size(45, 13);
            this.birthdayLabel.TabIndex = 13;
            this.birthdayLabel.Text = "Birthday";
            // 
            // birthdayPicker
            // 
            this.birthdayPicker.Location = new System.Drawing.Point(427, 181);
            this.birthdayPicker.Name = "birthdayPicker";
            this.birthdayPicker.Size = new System.Drawing.Size(238, 20);
            this.birthdayPicker.TabIndex = 14;
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Location = new System.Drawing.Point(424, 209);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(38, 13);
            this.phoneLabel.TabIndex = 15;
            this.phoneLabel.Text = "Phone";
            // 
            // phoneTB
            // 
            this.phoneTB.Location = new System.Drawing.Point(424, 226);
            this.phoneTB.Name = "phoneTB";
            this.phoneTB.Size = new System.Drawing.Size(241, 20);
            this.phoneTB.TabIndex = 16;
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Location = new System.Drawing.Point(424, 253);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(24, 13);
            this.cityLabel.TabIndex = 17;
            this.cityLabel.Text = "City";
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(557, 253);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(45, 13);
            this.addressLabel.TabIndex = 19;
            this.addressLabel.Text = "Address";
            // 
            // cityTB
            // 
            this.cityTB.FormattingEnabled = true;
            this.cityTB.Location = new System.Drawing.Point(424, 268);
            this.cityTB.Name = "cityTB";
            this.cityTB.Size = new System.Drawing.Size(121, 21);
            this.cityTB.TabIndex = 20;
            // 
            // addressTB
            // 
            this.addressTB.Location = new System.Drawing.Point(552, 269);
            this.addressTB.Name = "addressTB";
            this.addressTB.Size = new System.Drawing.Size(180, 20);
            this.addressTB.TabIndex = 21;
            // 
            // avatarBox
            // 
            this.avatarBox.Location = new System.Drawing.Point(9, 19);
            this.avatarBox.Name = "avatarBox";
            this.avatarBox.Size = new System.Drawing.Size(105, 84);
            this.avatarBox.TabIndex = 22;
            this.avatarBox.TabStop = false;
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(120, 80);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(75, 23);
            this.fileBtn.TabIndex = 23;
            this.fileBtn.Text = "Upload File";
            this.fileBtn.UseVisualStyleBackColor = true;
            // 
            // avatarLabel
            // 
            this.avatarLabel.AutoSize = true;
            this.avatarLabel.Location = new System.Drawing.Point(121, 61);
            this.avatarLabel.Name = "avatarLabel";
            this.avatarLabel.Size = new System.Drawing.Size(97, 13);
            this.avatarLabel.TabIndex = 25;
            this.avatarLabel.Text = "Choose your image";
            // 
            // registerGB
            // 
            this.registerGB.Controls.Add(this.avatarLabel);
            this.registerGB.Controls.Add(this.fileBtn);
            this.registerGB.Controls.Add(this.avatarBox);
            this.registerGB.Controls.Add(this.addressTB);
            this.registerGB.Controls.Add(this.cityTB);
            this.registerGB.Controls.Add(this.addressLabel);
            this.registerGB.Controls.Add(this.cityLabel);
            this.registerGB.Controls.Add(this.phoneTB);
            this.registerGB.Controls.Add(this.phoneLabel);
            this.registerGB.Controls.Add(this.birthdayPicker);
            this.registerGB.Controls.Add(this.birthdayLabel);
            this.registerGB.Controls.Add(this.otherBtn);
            this.registerGB.Controls.Add(this.femaleBtn);
            this.registerGB.Controls.Add(this.maleBtn);
            this.registerGB.Controls.Add(this.genderLabel);
            this.registerGB.Controls.Add(this.userTB);
            this.registerGB.Controls.Add(this.retypeTB);
            this.registerGB.Controls.Add(this.userLabel);
            this.registerGB.Controls.Add(this.retypeLabel);
            this.registerGB.Controls.Add(this.emailLabel);
            this.registerGB.Controls.Add(this.passwordTB);
            this.registerGB.Controls.Add(this.emailTB);
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
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.registerGB);
            this.Controls.Add(this.titleLabel);
            this.Name = "register_form";
            this.Opacity = 0.9D;
            this.Text = "register_form";
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).EndInit();
            this.registerGB.ResumeLayout(false);
            this.registerGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox emailTB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label retypeLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.TextBox retypeTB;
        private System.Windows.Forms.TextBox userTB;
        private System.Windows.Forms.Label genderLabel;
        private System.Windows.Forms.RadioButton maleBtn;
        private System.Windows.Forms.RadioButton femaleBtn;
        private System.Windows.Forms.RadioButton otherBtn;
        private System.Windows.Forms.Label birthdayLabel;
        private System.Windows.Forms.DateTimePicker birthdayPicker;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox phoneTB;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.ComboBox cityTB;
        private System.Windows.Forms.TextBox addressTB;
        private System.Windows.Forms.PictureBox avatarBox;
        private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.Label avatarLabel;
        private System.Windows.Forms.GroupBox registerGB;
    }
}