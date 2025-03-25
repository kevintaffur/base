using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Dtos.Productos
{
    public class ProductoCreacionDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 20 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "La descripción debe tener entre 3 y 100 caracteres.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "El estado es requerido.")]
        [RegularExpression("^[NAI]$", ErrorMessage = "El estado solo puede ser N, A o I.")]
        public string Estado { get; set; } = null!;

        [Required(ErrorMessage = "El id del usuario es requerido.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El id de usuario debe ser un número entero positivo.")]
        [JsonPropertyName("usuario_id")]
        public int UsuarioId { get; set; }
    }
}
