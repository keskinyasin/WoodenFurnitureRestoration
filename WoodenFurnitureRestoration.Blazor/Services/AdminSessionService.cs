namespace WoodenFurnitureRestoration.Blazor.Services
{
    public class AdminSessionService
    {
        private static readonly Dictionary<string, AdminSession> _sessions = new();
        private readonly ILogger<AdminSessionService> _logger;

        public AdminSessionService(ILogger<AdminSessionService> logger)
        {
            _logger = logger;
        }

        public string CreateSession(string username)
        {
            string sessionId = Guid.NewGuid().ToString();
            var session = new AdminSession
            {
                SessionId = sessionId,
                Username = username,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            _sessions[sessionId] = session;
            _logger.LogInformation($"✅ Session oluşturuldu: {sessionId} - Kullanıcı: {username}");

            return sessionId;
        }

        public bool ValidateSession(string? sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                _logger.LogWarning("❌ Session ID boş");
                return false;
            }

            if (!_sessions.TryGetValue(sessionId, out var session))
            {
                _logger.LogWarning($"❌ Session bulunamadı: {sessionId}");
                return false;
            }

            // ✅ Süresi doldu mu kontrol et
            if (DateTime.UtcNow > session.ExpiresAt)
            {
                _sessions.Remove(sessionId);
                _logger.LogWarning($"❌ Session süresi doldu: {sessionId}");
                return false;
            }

            _logger.LogInformation($"✅ Session geçerli: {sessionId}");
            return true;
        }

        public void InvalidateSession(string? sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                _logger.LogWarning("⚠️ InvalidateSession: SessionId boş");
                return;
            }

            if (_sessions.Remove(sessionId))
            {
                _logger.LogInformation($"✅ Session silindi: {sessionId}");
            }
            else
            {
                _logger.LogWarning($"⚠️ Session bulunamadı, silinemedi: {sessionId}");
            }
        }

        public AdminSession? GetSession(string? sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return null;

            _sessions.TryGetValue(sessionId, out var session);
            return session;
        }
    }

    public class AdminSession
    {
        public string SessionId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}