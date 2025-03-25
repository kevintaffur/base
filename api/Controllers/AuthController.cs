using api.Models;
using api.Services;
using api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private JwtService Service;
        private IUsuarioService UsuarioService;

        public AuthController(JwtService service, IUsuarioService usuarioService)
        {
            Service = service;
            UsuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                Usuario usuario = await UsuarioService.ObtenerPorUsername(request.Username);

                if (BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
                {
                    var token = Service.GenerarToken(usuario);
                    return Ok(new Response
                    {
                        Status = 200,
                        Message = "Sesión iniciada satisfactoriamente.",
                        Content = token
                    });
                }
                return Unauthorized(new Response
                {
                    Status = 401,
                    Message = "Datos incorrectos",
                    Content = null
                });
            }
            catch
            {
                return Unauthorized(new Response
                {
                    Status = 401,
                    Message = "Datos incorrectos",
                    Content = null
                });
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try
            {
                await UsuarioService.Crear(request);
                return Created("", new Response
                {
                    Status = 201,
                    Message = "Usuario creado satisfactoriamente.",
                    Content = null,
                });
            }
            catch
            {
                return BadRequest(new Response
                {
                    Status = 400,
                    Message = "No se pudo crear el usuario con los datos proporcionados.",
                    Content = null,
                });
            }
        }
    }
}
