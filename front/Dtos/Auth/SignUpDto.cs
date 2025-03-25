using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace front.Dtos.Auth
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El id del rol es requerido.")]
        [JsonProperty("rol_id")]
        public int RolId { get; set; }
    }
}
