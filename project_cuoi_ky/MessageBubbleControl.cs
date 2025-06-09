using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_cuoi_ky
{
    public partial class MessageBubbleControl : UserControl
    {
        public bool IsMyMessage { get; set; }

        public MessageBubbleControl()
        {
            InitializeComponent();
            // Thiết lập mặc định cho các label
            lblSender.Text = "";
            lblContent.Text = "";
            lblTimestamp.Text = "";

            // Cấu hình các thuộc tính của pnlBubble
            pnlBubble.Padding = new Padding(10); // Đệm bên trong bong bóng
            pnlBubble.Margin = new Padding(5, 5, 5, 5); // Khoảng cách giữa các bong bóng
            pnlBubble.AutoSize = true; // Panel tự động điều chỉnh kích thước theo nội dung
            pnlBubble.AutoSizeMode = AutoSizeMode.GrowAndShrink; // Quan trọng để bong bóng co giãn
            lblContent.MaximumSize = new Size(this.Width - pnlBubble.Padding.Horizontal - 20, 0); // Chiều rộng tối đa của nội dung tin nhắn

            this.AutoSize = true; // UserControl tự động điều chỉnh kích thước theo pnlBubble
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        // Phương thức để thiết lập dữ liệu cho bong bóng tin nhắn
        public void SetMessage(string senderUsername, string content, DateTime timestamp, bool isMyMessage)
        {
            lblSender.Text = senderUsername;
            lblContent.Text = content;
            lblTimestamp.Text = timestamp.ToString("HH:mm"); // Định dạng thời gian
            IsMyMessage = isMyMessage;

            // Tùy chỉnh giao diện dựa trên IsMyMessage
            if (IsMyMessage)
            {
                pnlBubble.BackColor = Color.FromArgb(226, 111, 111); // Màu xanh lá nhạt cho tin nhắn của tôi
                pnlBubble.Anchor = AnchorStyles.Right; // Đặt bong bóng sang phải
                lblSender.TextAlign = ContentAlignment.MiddleRight; // Căn phải tên người gửi
                lblTimestamp.TextAlign = ContentAlignment.MiddleRight; // Căn phải timestamp
            }
            else
            {
                pnlBubble.BackColor = Color.FromArgb(230, 230, 230); // Màu xám nhạt cho tin nhắn của người khác
                pnlBubble.Anchor = AnchorStyles.Left; // Đặt bong bóng sang trái
                lblSender.TextAlign = ContentAlignment.MiddleLeft; // Căn trái tên người gửi
                lblTimestamp.TextAlign = ContentAlignment.MiddleLeft; // Căn trái timestamp
            }
            // Điều chỉnh lại chiều rộng tối đa của nội dung dựa trên chiều rộng của UserControl
            // Điều này quan trọng khi form/panel thay đổi kích thước
            lblContent.MaximumSize = new Size(this.Width - pnlBubble.Padding.Horizontal - 20, 0);

            // Cần cập nhật lại layout sau khi thay đổi thuộc tính
            this.PerformLayout();
            pnlBubble.PerformLayout();
        }

        // Xử lý sự kiện Resize của MessageBubbleControl để cập nhật MaximumSize của lblContent
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (pnlBubble != null && lblContent != null)
            {
                // Đảm bảo lblContent không vượt quá chiều rộng của panel chứa nó
                lblContent.MaximumSize = new Size(this.Width - pnlBubble.Padding.Horizontal - 20, 0);
            }
        }
    }
}
