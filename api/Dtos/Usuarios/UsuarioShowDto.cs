using api.Dtos.Roles;

namespace api.Dtos.Usuarios
{
    public class UsuarioShowDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public virtual RolShowDto Rol { get; set; }
    }
}
