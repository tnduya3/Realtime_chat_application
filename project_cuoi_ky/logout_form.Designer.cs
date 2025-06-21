namespace project_cuoi_ky
{
    partial class logout_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(logout_form));
            this.btnTrue = new System.Windows.Forms.Button();
            this.btnFalse = new System.Windows.Forms.Button();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTrue
            // 
            this.btnTrue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.btnTrue.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrue.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnTrue.Location = new System.Drawing.Point(12, 85);
            this.btnTrue.Name = "btnTrue";
            this.btnTrue.Size = new System.Drawing.Size(158, 36);
            this.btnTrue.TabIndex = 8;
            this.btnTrue.Text = "Confirm";
            this.btnTrue.UseVisualStyleBackColor = false;
            this.btnTrue.Click += new System.EventHandler(this.btnTrue_Click);
            // 
            // btnFalse
            // 
            this.btnFalse.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnFalse.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFalse.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFalse.Location = new System.Drawing.Point(176, 85);
            this.btnFalse.Name = "btnFalse";
            this.btnFalse.Size = new System.Drawing.Size(158, 36);
            this.btnFalse.TabIndex = 8;
            this.btnFalse.Text = "No";
            this.btnFalse.UseVisualStyleBackColor = false;
            this.btnFalse.Click += new System.EventHandler(this.btnFalse_Click);
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirm.Location = new System.Drawing.Point(34, 27);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(273, 28);
            this.lblConfirm.TabIndex = 9;
            this.lblConfirm.Text = "Do you want to logout?";
            // 
            // logout_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 133);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.btnFalse);
            this.Controls.Add(this.btnTrue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "logout_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "logout_form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTrue;
        private System.Windows.Forms.Button btnFalse;
        private System.Windows.Forms.Label lblConfirm;
    }
}