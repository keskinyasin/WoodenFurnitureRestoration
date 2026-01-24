namespace WoodenFurnitureRestoration.Blazor.Services
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;  // ← EKLE
    }
}