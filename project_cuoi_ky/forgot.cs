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
    public partial class forgot : Form
    {
        public forgot()
        {
            InitializeComponent();
        }

        private async void btnForgotPassword_Click(object sender, EventArgs e)
        {
            string email = txtForgotPasswordEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email của bạn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string result = await FirebaseAuthService.SendPasswordResetEmailAsync(email);

            if (result.StartsWith("EMAIL_NOT_FOUND"))
            {
                MessageBox.Show("Không tìm thấy người dùng với email này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result.StartsWith("Lỗi kết nối"))
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(result, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Có thể ẩn form quên mật khẩu hoặc hiển thị thông báo khác
                this.DialogResult = DialogResult.OK; // Đặt DialogResult thành OK
                this.Close(); // Đóng form
            }
        }

        private void btnBackClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Đặt DialogResult thành Cancel
            this.Close(); // Đóng form
        }
    }
}
