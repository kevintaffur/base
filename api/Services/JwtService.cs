using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    public class JwtService
    {
        private readonly IConfiguration Config;

        public JwtService(IConfiguration configuration)
        {
            Config = configuration;
        }

        public string GenerarToken(Usuario usuario)
        {
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            SigningCredentials signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
            };

            var token = new JwtSecurityToken(
                issuer: Config["Jwt:Issuer"],
                audience: Config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
