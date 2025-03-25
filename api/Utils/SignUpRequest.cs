using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Utils
{
    public class SignUpRequest
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El id del rol es requerido.")]
        [JsonPropertyName("rol_id")]
        public int RolId { get; set; }
    }
}
