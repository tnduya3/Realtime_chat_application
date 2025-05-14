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
    public partial class register_form : Form
    {
        public register_form()
        {
            InitializeComponent();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Đặt DialogResult thành Cancel
            this.Close(); // Đóng form
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtRegisterEmail.Text;
            string password = txtRegisterPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập email và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string result = await FirebaseAuthService.SignUpWithEmailAndPasswordAsync(email, password);

            if (result.StartsWith("EMAIL_EXISTS"))
            {
                MessageBox.Show("Email này đã được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result.StartsWith("WEAK_PASSWORD"))
            {
                MessageBox.Show("Mật khẩu quá yếu. Vui lòng chọn mật khẩu mạnh hơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result.StartsWith("Lỗi kết nối"))
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"Đăng ký thành công! User ID Token: {result}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Có thể chuyển đến form đăng nhập hoặc thực hiện các hành động khác sau khi đăng ký thành công
                this.Close();
            }
        }
    }
}
