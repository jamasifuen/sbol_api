using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonalAPI.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IOptions<JwtSettings> jwtSettings, ILogger<AuthService> logger)
        {
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public async Task<AuthResponse> LoginAsync(LoginModel loginModel)
        {
            try
            {
                _logger.LogInformation($"Intento de login para usuario: {loginModel.Username}");

                // Validar credenciales
                if (!ValidateCredentials(loginModel.Username, loginModel.Password))
                {
                    _logger.LogWarning($"Credenciales inválidas para usuario: {loginModel.Username}");
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Credenciales inválidas"
                    };
                }

                // Generar token JWT
                var token = GenerateJwtToken(loginModel.Username);
                var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);

                _logger.LogInformation($"Login exitoso para usuario: {loginModel.Username}");

                return new AuthResponse
                {
                    Success = true,
                    Token = token,
                    Username = loginModel.Username,
                    Expires = expires,
                    Message = "Login exitoso"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error durante el login para usuario: {loginModel.Username}");
                return new AuthResponse
                {
                    Success = false,
                    Message = "Error interno del servidor"
                };
            }
        }

        public string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, 
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), 
                    ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateCredentials(string username, string password)
        {
            // IMPORTANTE: Esta es una implementación básica para demostración
            // En producción, deberías validar contra una base de datos con passwords hasheados
            
            // Usuarios de ejemplo (en producción usar base de datos)
            var validUsers = new Dictionary<string, string>
            {
                { "sbol_token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFkbWluIiwic3ViIjoiYWRtaW4iLCJqdGkiOiJjZmMyOGM5YS02MTM3LTQ1NjUtOTk1Mi1hYThmOGU1MTQ3YjMiLCJpYXQiOjE3NTU3MTAzNDIsImV4cCI6MTc1NTcxMzk0MiwiaXNzIjoiUGVyc29uYWxBUEkiLCJhdWQiOiJQZXJzb25hbEFQSV9Vc2VycyJ9.VPISH1ZERAeu9hlstgVmYy_lQFrcOORKYEz0TpaNvuM" },
            };

            return validUsers.ContainsKey(username.ToLower()) && 
                   validUsers[username.ToLower()] == password;
        }
    }
}
