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
    public partial class register_form : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string API_BASE_URL = "https://localhost:7092"; // Cùng URL với login form

        // Model classes cho API request và response
        public class RegisterRequest
        {
            public string email { get; set; }
            public string password { get; set; }
            public string confirmPassword { get; set; }
            public string displayName { get; set; }
        }

        public class RegisterResponse
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
            public string email { get; set; }
            public string displayName { get; set; }
            public bool emailVerified { get; set; }
            public string createdAt { get; set; }
            public string lastSignInAt { get; set; }
        }
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
            string email = txtRegisterEmail.Text.Trim();
            string password = txtRegisterPassword.Text;
            string confirmPassword = retypeTB.Text;
            string displayName = userTB.Text.Trim();
            
            // Lưu trạng thái ban đầu của button
            Color originalColor = btnRegister.BackColor;
            string originalText = btnRegister.Text;
            
            btnRegister.Enabled = false;
            btnRegister.BackColor = Color.Gray;
            btnRegister.Text = "Đang đăng ký...";

            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập email và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(confirmPassword))
                {
                    MessageBox.Show("Vui lòng nhập lại mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra định dạng email cơ bản
                if (!email.Contains("@") || !email.Contains("."))
                {
                    MessageBox.Show("Định dạng email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra mật khẩu khớp nhau
                if (password != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu không khớp. Vui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Nếu displayName trống, sử dụng phần trước @ của email
                if (string.IsNullOrEmpty(displayName))
                {
                    displayName = email.Split('@')[0];
                }

                // Gọi API signup trực tiếp
                string requestUri = $"{API_BASE_URL}/api/Auth/signup";
                
                var registerRequest = new RegisterRequest
                {
                    email = email,
                    password = password,
                    confirmPassword = confirmPassword,
                    displayName = displayName
                };

                var jsonRequest = JsonConvert.SerializeObject(registerRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(requestUri, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var registerResponse = JsonConvert.DeserializeObject<RegisterResponse>(responseContent);
                    
                    if (registerResponse != null && registerResponse.success)
                    {                        
                        // Đóng form register và quay về login
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(registerResponse?.message ?? "Đăng ký thất bại", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Xử lý lỗi từ API
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        try
                        {
                            dynamic errorResponse = JsonConvert.DeserializeObject(responseContent);
                            string errorMessage = errorResponse?.message ?? "Yêu cầu không hợp lệ";
                            
                            if (errorMessage.Contains("EMAIL_EXISTS") || errorMessage.Contains("email"))
                            {
                                MessageBox.Show("Email này đã được sử dụng.", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (errorMessage.Contains("WEAK_PASSWORD") || errorMessage.Contains("password"))
                            {
                                MessageBox.Show("Mật khẩu quá yếu. Vui lòng chọn mật khẩu mạnh hơn.", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show(errorMessage, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Yêu cầu không hợp lệ.", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        MessageBox.Show("Email này đã được sử dụng.", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi server: {response.StatusCode}", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                btnRegister.Enabled = true;
                btnRegister.BackColor = originalColor;
                btnRegister.Text = originalText;
            }
        }
    }
}
