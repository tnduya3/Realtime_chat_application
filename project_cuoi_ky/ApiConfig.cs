using System.Configuration;

namespace project_cuoi_ky
{
    public static class ApiConfig
    {
        // URL base của API - bạn cần thay đổi giá trị này
        public static readonly string BaseUrl = "http://localhost:7092";
        
        // Endpoint cho login
        public static readonly string LoginEndpoint = "/api/Auth/login";
        
        // Endpoint cho register
        public static readonly string RegisterEndpoint = "/api/Auth/register";
        
        // Endpoint cho forgot password
        public static readonly string ForgotPasswordEndpoint = "/api/Auth/forgot-password";
        
        // Timeout cho HTTP requests (giây)
        public static readonly int TimeoutSeconds = 30;
        
        // Headers mặc định
        public static readonly string ContentType = "application/json";
    }
}
