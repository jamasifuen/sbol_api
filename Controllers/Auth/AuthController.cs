using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAPI.Models.Auth;
using PersonalAPI.Services.Auth;

namespace PersonalAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Autenticar usuario y obtener token JWT
        /// </summary>
        /// <param name="loginModel">Credenciales de usuario</param>
        /// <returns>Token JWT si las credenciales son válidas</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
                {
                    return BadRequest(new AuthResponse 
                    { 
                        Success = false, 
                        Message = "Usuario y contraseña son requeridos" 
                    });
                }

                var result = await _authService.LoginAsync(loginModel);
                
                if (!result.Success)
                {
                    return Unauthorized(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de login");
                return StatusCode(500, new AuthResponse 
                { 
                    Success = false, 
                    Message = "Error interno del servidor" 
                });
            }
        }

        /// <summary>
        /// Verificar si el token actual es válido
        /// </summary>
        /// <returns>Información del usuario autenticado</returns>
        [HttpGet("verify")]
        [Authorize]
        public ActionResult<object> VerifyToken()
        {
            try
            {
                var username = User.Identity?.Name;
                
                return Ok(new 
                { 
                    Success = true,
                    Username = username,
                    Message = "Token válido",
                    Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar token");
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor" });
            }
        }
    }
}
