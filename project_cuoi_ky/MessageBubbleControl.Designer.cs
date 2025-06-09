namespace project_cuoi_ky
{
    partial class MessageBubbleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBubble = new System.Windows.Forms.Panel();
            this.lblTimestamp = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.lblSender = new System.Windows.Forms.Label();
            this.pnlBubble.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBubble
            // 
            this.pnlBubble.BackColor = System.Drawing.Color.Snow;
            this.pnlBubble.Controls.Add(this.lblTimestamp);
            this.pnlBubble.Controls.Add(this.lblContent);
            this.pnlBubble.Controls.Add(this.lblSender);
            this.pnlBubble.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBubble.Location = new System.Drawing.Point(0, 0);
            this.pnlBubble.Name = "pnlBubble";
            this.pnlBubble.Size = new System.Drawing.Size(220, 120);
            this.pnlBubble.TabIndex = 0;
            // 
            // lblTimestamp
            // 
            this.lblTimestamp.AutoSize = true;
            this.lblTimestamp.Location = new System.Drawing.Point(159, 40);
            this.lblTimestamp.Name = "lblTimestamp";
            this.lblTimestamp.Size = new System.Drawing.Size(35, 13);
            this.lblTimestamp.TabIndex = 2;
            this.lblTimestamp.Text = "label1";
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(4, 40);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(44, 13);
            this.lblContent.TabIndex = 1;
            this.lblContent.Text = "Content";
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(4, 4);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(41, 13);
            this.lblSender.TabIndex = 0;
            this.lblSender.Text = "Sender";
            // 
            // MessageBubbleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBubble);
            this.Name = "MessageBubbleControl";
            this.Size = new System.Drawing.Size(220, 120);
            this.pnlBubble.ResumeLayout(false);
            this.pnlBubble.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBubble;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.Label lblTimestamp;
    }
}
