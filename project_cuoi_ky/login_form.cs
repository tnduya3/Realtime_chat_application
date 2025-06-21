using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace project_cuoi_ky
{
    public partial class login : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string API_BASE_URL = "https://localhost:7092"; // Thay đổi URL API của bạn ở đây

        // Model classes cho API request và response
        public class LoginRequest
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        public class LoginResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
            public string accessToken { get; set; }
            public string refreshToken { get; set; }
            public string userId { get; set; }
            public string email { get; set; }
            public string expiresAt { get; set; }
            public UserInfo user { get; set; }
        }

        public class UserInfo
        {
            public string userId { get; set; }
            public string displayName { get; set; }
            public string email { get; set; }
            public bool emailVerified { get; set; }
            public string createdAt { get; set; }
            public string lastSignInAt { get; set; }
        }
        
        public login()
        {
            InitializeComponent();
            // Không cần khởi tạo Firebase nữa vì đã chuyển sang sử dụng API
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
        }        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string email = tbUser.Text.Trim();
            string password = tbPassword.Text;
            
            // Lưu trạng thái ban đầu của button
            Color originalColor = btnLogin.BackColor;
            string originalText = btnLogin.Text;
            
            btnLogin.Enabled = false; // Vô hiệu hóa nút đăng nhập trong khi đang xử lý
            btnLogin.BackColor = Color.Gray; // Thay đổi màu nút để hiển thị trạng thái đang xử lý
            btnLogin.Text = "Processing..."; // Thay đổi văn bản nút để hiển thị trạng thái đang xử lý

            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập email và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra định dạng email cơ bản
                if (!email.Contains("@") || !email.Contains("."))
                {
                    MessageBox.Show("Định dạng email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi API login trực tiếp
                string requestUri = $"{API_BASE_URL}/api/Auth/login";
                
                var loginRequest = new LoginRequest
                {
                    email = email,
                    password = password
                };

                var jsonRequest = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(requestUri, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                      if (loginResponse != null && loginResponse.success)
                    {                        
                        // Lưu thông tin user vào Properties.Settings
                        Properties.Settings.Default.AccessToken = loginResponse.accessToken ?? "";
                        Properties.Settings.Default.RefreshToken = loginResponse.refreshToken ?? "";
                        Properties.Settings.Default.UserId = loginResponse.userId ?? "";
                        Properties.Settings.Default.UserEmail = loginResponse.email ?? "";
                        Properties.Settings.Default.IsLoggedIn = true;
                        
                        // Lưu thông tin user chi tiết
                        if (loginResponse.user != null)
                        {
                            Properties.Settings.Default.DisplayName = loginResponse.user.displayName ?? "";
                            Properties.Settings.Default.EmailVerified = loginResponse.user.emailVerified;
                            Properties.Settings.Default.CreatedAt = loginResponse.user.createdAt ?? "";
                            Properties.Settings.Default.LastSignInAt = loginResponse.user.lastSignInAt ?? "";
                        }
                        
                        Properties.Settings.Default.Save();

                        // Chuyển đến form chính của ứng dụng
                        this.Hide(); // Ẩn form đăng nhập hiện tại
                        home homeForm = new home();
                        homeForm.Show(); // Hiển thị form chính
                    }
                    else
                    {
                        MessageBox.Show(loginResponse?.message ?? "Đăng nhập thất bại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Xử lý lỗi từ API
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        MessageBox.Show("Email hoặc mật khẩu không đúng.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        try
                        {
                            dynamic errorResponse = JsonConvert.DeserializeObject(responseContent);
                            string errorMessage = errorResponse?.message ?? "Yêu cầu không hợp lệ";
                            MessageBox.Show(errorMessage, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch
                        {
                            MessageBox.Show("Yêu cầu không hợp lệ.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi server: {response.StatusCode}", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng và thử lại.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Khôi phục trạng thái ban đầu của button
                btnLogin.Enabled = true;
                btnLogin.BackColor = originalColor;
                btnLogin.Text = originalText;
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
