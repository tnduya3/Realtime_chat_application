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
    public partial class forgot : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string API_BASE_URL = "https://localhost:7092"; // Cùng URL với các form khác

        // Model classes cho API request và response
        public class ForgotPasswordRequest
        {
            public string email { get; set; }
        }

        public class ForgotPasswordResponse
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
        public forgot()
        {
            InitializeComponent();
        }        
        
        private async void btnForgotPassword_Click(object sender, EventArgs e)
        {
            string email = txtForgotPasswordEmail.Text.Trim();

            // Lưu trạng thái ban đầu của button
            Color originalColor = btnForgotPassword.BackColor;
            string originalText = btnForgotPassword.Text;
            
            btnForgotPassword.Enabled = false;
            btnForgotPassword.BackColor = Color.Gray;
            btnForgotPassword.Text = "Đang gửi...";

            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ email của bạn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra định dạng email cơ bản
                if (!email.Contains("@") || !email.Contains("."))
                {
                    MessageBox.Show("Định dạng email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi API forgot password trực tiếp
                string requestUri = $"{API_BASE_URL}/api/Auth/forgot-password";
                
                var forgotRequest = new ForgotPasswordRequest
                {
                    email = email
                };

                var jsonRequest = JsonConvert.SerializeObject(forgotRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(requestUri, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var forgotResponse = JsonConvert.DeserializeObject<ForgotPasswordResponse>(responseContent);
                    
                    if (forgotResponse != null && forgotResponse.success)
                    {
                        MessageBox.Show("Đã gửi email đặt lại mật khẩu. Vui lòng kiểm tra hộp thư đến của bạn.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Đóng form forgot password
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(forgotResponse?.message ?? "Gửi email thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Xử lý lỗi từ API
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Không tìm thấy người dùng với email này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        try
                        {
                            dynamic errorResponse = JsonConvert.DeserializeObject(responseContent);
                            string errorMessage = errorResponse?.message ?? "Yêu cầu không hợp lệ";
                            MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch
                        {
                            MessageBox.Show("Yêu cầu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi server: {response.StatusCode}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                btnForgotPassword.Enabled = true;
                btnForgotPassword.BackColor = originalColor;
                btnForgotPassword.Text = originalText;
            }
        }

        private void btnBackClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Đặt DialogResult thành Cancel
            this.Close(); // Đóng form
        }
    }
}
