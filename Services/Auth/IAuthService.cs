using PersonalAPI.Models.Auth;

namespace PersonalAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginModel loginModel);
        string GenerateJwtToken(string username);
        bool ValidateCredentials(string username, string password);
    }
}
