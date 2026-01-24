using Microsoft.JSInterop;

namespace WoodenFurnitureRestoration.Blazor.Services
{
    public class AuthService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<AuthService> _logger;
        private readonly AdminSessionService _sessionService;

        public AuthService(
            IJSRuntime jsRuntime,
            ILogger<AuthService> logger,
            AdminSessionService sessionService)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
            _sessionService = sessionService;
        }

        public async Task<bool> IsAdminLoggedInAsync()
        {
            try
            {
                _logger.LogInformation("🔐 Client-side auth check başladı...");

                var token = await _jsRuntime.InvokeAsync<string>("eval",
                    "sessionStorage.getItem('adminToken') || ''");

                var sessionId = await _jsRuntime.InvokeAsync<string>("eval",
                    "sessionStorage.getItem('sessionId') || ''");

                bool isLoggedIn = !string.IsNullOrEmpty(token) &&
                                  !string.IsNullOrEmpty(sessionId) &&
                                  _sessionService.ValidateSession(sessionId);

                _logger.LogInformation($"🔐 Auth result: {(isLoggedIn ? "✅ Valid" : "❌ Invalid")}");

                return isLoggedIn;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Auth check error");
                return false;
            }
        }

        public async Task SetAdminTokenAsync(string token, string username)
        {
            try
            {
                string sessionId = _sessionService.CreateSession(username);

                await _jsRuntime.InvokeVoidAsync("eval",
                    $"sessionStorage.setItem('adminToken', '{token}')");

                await _jsRuntime.InvokeVoidAsync("eval",
                    $"sessionStorage.setItem('sessionId', '{sessionId}')");

                _logger.LogInformation("✅ Session + Token kaydedildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Token/Session save error");
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                _logger.LogInformation("🚪 Logout başladı...");

                // ✅ Client-side'dan sessionId oku
                var sessionId = await _jsRuntime.InvokeAsync<string>("eval",
                    "sessionStorage.getItem('sessionId') || ''");

                _logger.LogInformation($"🚪 SessionId: {sessionId}");

                // ✅ Server-side session sil
                if (!string.IsNullOrEmpty(sessionId))
                {
                    _sessionService.InvalidateSession(sessionId);
                    _logger.LogInformation($"✅ Server-side session silindi: {sessionId}");
                }

                // ✅ Client-side storage temizle
                await _jsRuntime.InvokeVoidAsync("eval",
                    "sessionStorage.removeItem('adminToken')");

                await _jsRuntime.InvokeVoidAsync("eval",
                    "sessionStorage.removeItem('sessionId')");

                await _jsRuntime.InvokeVoidAsync("eval",
                    "localStorage.removeItem('adminToken')");

                await _jsRuntime.InvokeVoidAsync("eval",
                    "document.cookie = 'adminToken=; path=/; expires=Thu, 01 Jan 1970 00:00:00 UTC'");

                _logger.LogInformation("✅ Logout tamamlandı - Tüm cache temizlendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Logout error");
            }
        }

        public async Task<string?> GetAdminTokenAsync()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>("eval",
                    "sessionStorage.getItem('adminToken')");
            }
            catch
            {
                return null;
            }
        }
    }
}