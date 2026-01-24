using Microsoft.JSInterop;

namespace WoodenFurnitureRestoration.Blazor.Services
{
    public class LoginService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginService> _logger;
        private readonly AuthService _authService;

        public LoginService(
            IConfiguration configuration,
            ILogger<LoginService> logger,
            AuthService authService)
        {
            _configuration = configuration;
            _logger = logger;
            _authService = authService;
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            try
            {
                _logger.LogInformation($"🔑 Login attempt for user: {username}");

                var configUsername = _configuration["AdminCredentials:Username"];
                var configPassword = _configuration["AdminCredentials:Password"];

                _logger.LogInformation($"🔍 Config Username: '{configUsername}' vs Input: '{username}'");
                _logger.LogInformation($"🔍 Config Password: '{configPassword}' vs Input: '{password}'");

                if (username == configUsername && password == configPassword)
                {
                    _logger.LogInformation("✅ Credentials match - Login successful!");

                    var token = Guid.NewGuid().ToString();
                    await _authService.SetAdminTokenAsync(token, username);

                    _logger.LogInformation($"✅ Token saved: {token}");

                    return new LoginResult
                    {
                        IsSuccess = true,
                        Message = "Giriş başarılı!",
                        Token = token
                    };
                }
                else
                {
                    _logger.LogWarning("❌ Invalid credentials");
                    return new LoginResult
                    {
                        IsSuccess = false,
                        Message = "❌ Kullanıcı adı veya şifre hatalı",
                        Token = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error");
                return new LoginResult
                {
                    IsSuccess = false,
                    Message = $"❌ Hata: {ex.Message}",
                    Token = string.Empty
                };
            }
        }
    }

    // ❌ KALDIR! LoginResult sınıfını LoginResult.cs'de tut
    // public class LoginResult
    // {
    //     public bool IsSuccess { get; set; }
    //     public string Message { get; set; } = string.Empty;
    //     public string Token { get; set; } = string.Empty;
    // }
}