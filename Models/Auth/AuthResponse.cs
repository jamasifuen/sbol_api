namespace PersonalAPI.Models.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
