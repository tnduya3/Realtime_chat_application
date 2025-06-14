using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace project_cuoi_ky
{
    public class ApiAuthService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string apiBaseUrl = "https://localhost:7092"; // Thay đổi URL API của bạn ở đây

        // Model classes cho request và response
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

        public class ApiErrorResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
            public string error { get; set; }
        }

        /// <summary>
        /// Đăng nhập người dùng bằng email và password
        /// </summary>
        /// <param name="email">Email của người dùng</param>
        /// <param name="password">Mật khẩu của người dùng</param>
        /// <returns>Trả về kết quả đăng nhập dưới dạng string</returns>
        public static async Task<string> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                string requestUri = $"{apiBaseUrl}/api/Auth/login";
                
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
                        // Lưu token và thông tin user vào session hoặc local storage
                        SaveUserSession(loginResponse);
                        return "SUCCESS";
                    }
                    else
                    {
                        return loginResponse?.message ?? "Đăng nhập thất bại";
                    }
                }
                else
                {
                    // Xử lý lỗi từ API
                    try
                    {
                        var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(responseContent);
                        return errorResponse?.message ?? $"Lỗi đăng nhập: {response.StatusCode}";
                    }
                    catch
                    {
                        return $"Lỗi đăng nhập: {response.StatusCode}";
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Lỗi kết nối: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Lỗi không xác định: {ex.Message}";
            }
        }

        /// <summary>
        /// Lưu thông tin session của user sau khi đăng nhập thành công
        /// </summary>
        /// <param name="loginResponse">Response từ API chứa thông tin user và token</param>
        private static void SaveUserSession(LoginResponse loginResponse)
        {
            try
            {
                // Lưu vào Properties.Settings hoặc file config
                Properties.Settings.Default.AccessToken = loginResponse.accessToken;
                Properties.Settings.Default.RefreshToken = loginResponse.refreshToken;
                Properties.Settings.Default.UserId = loginResponse.userId;
                Properties.Settings.Default.UserEmail = loginResponse.email;
                Properties.Settings.Default.IsLoggedIn = true;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu session: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Lấy thông tin user hiện tại từ session
        /// </summary>
        /// <returns>Thông tin user nếu đã đăng nhập</returns>
        public static UserInfo GetCurrentUser()
        {
            try
            {
                if (Properties.Settings.Default.IsLoggedIn && 
                    !string.IsNullOrEmpty(Properties.Settings.Default.AccessToken))
                {
                    return new UserInfo
                    {
                        userId = Properties.Settings.Default.UserId,
                        email = Properties.Settings.Default.UserEmail
                    };
                }
            }
            catch { }
            
            return null;
        }

        /// <summary>
        /// Đăng xuất user và xóa session
        /// </summary>
        public static void SignOut()
        {
            try
            {
                Properties.Settings.Default.AccessToken = "";
                Properties.Settings.Default.RefreshToken = "";
                Properties.Settings.Default.UserId = "";
                Properties.Settings.Default.UserEmail = "";
                Properties.Settings.Default.IsLoggedIn = false;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng xuất: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Kiểm tra xem user có đang đăng nhập không
        /// </summary>
        /// <returns>True nếu user đã đăng nhập</returns>
        public static bool IsUserLoggedIn()
        {
            return Properties.Settings.Default.IsLoggedIn && 
                   !string.IsNullOrEmpty(Properties.Settings.Default.AccessToken);
        }
    }
}
