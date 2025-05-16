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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            FirebaseAuthService.InitializeFirebaseApp();
        }

        private void registerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            register_form registerForm = new register_form();
            this.Hide(); // Ẩn form đăng nhập hiện tại

            if (registerForm.ShowDialog() == DialogResult.OK || registerForm.DialogResult == DialogResult.Cancel)
            {
                this.Show();
            }

        }

        private void forgotLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forgot forgotForm = new forgot();
            this.Hide(); // Ẩn form đăng nhập hiện tại

            if (forgotForm.ShowDialog() == DialogResult.OK || forgotForm.DialogResult == DialogResult.Cancel)
            {
                this.Show();
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string email = tbUser.Text;
            string password = tbPassword.Text;
            btnLogin.Enabled = false; // Vô hiệu hóa nút đăng nhập trong khi đang xử lý
            btnLogin.BackColor = Color.Gray; // Thay đổi màu nút để hiển thị trạng thái đang xử lý
            btnLogin.Text = "Processing..."; // Thay đổi văn bản nút để hiển thị trạng thái đang xử lý

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập email và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string result = await FirebaseAuthService.SignInWithEmailAndPasswordAsync(email, password);

            if (result.StartsWith("EMAIL_NOT_FOUND") || result.StartsWith("INVALID_PASSWORD"))
            {
                MessageBox.Show("Email hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result.StartsWith("Lỗi kết nối"))
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Chuyển đến form chính của ứng dụng hoặc thực hiện các hành động khác sau khi đăng nhập thành công
                this.Hide(); // Ẩn form đăng nhập hiện tại
                home homeForm = new home();
                homeForm.Show(); // Hiển thị form chính
            }
        }

        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím Enter được nhấn
            if (e.KeyCode == Keys.Enter)
            {
                // Gọi sự kiện nhấn nút đăng nhập
                btnLogin_Click(sender, e);
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím Enter được nhấn
            if (e.KeyCode == Keys.Enter)
            {
                // Gọi sự kiện nhấn nút đăng nhập
                btnLogin_Click(sender, e);
            }
        }
    }
}
