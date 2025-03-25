using api.Models;
using api.Repositories;
using api.Utils;

namespace api.Services
{
    public class UsuarioService : IUsuarioService
    {
        public IUsuarioRepository Repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            Repo = repo;
        }

        public async Task<Usuario> Crear(SignUpRequest request)
        {
            Usuario usuario = new Usuario
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RolId = request.RolId
            };

            return await Repo.Crear(usuario);
        }

        public async Task<Usuario> ObtenerPorId(int id)
        {
            Usuario usuario = await Repo.ObtenerPorId(id);

            if (usuario == null)
            {
                return null;
            }
            return usuario;
        }

        public async Task<Usuario> ObtenerPorUsername(string username)
        {
            Usuario usuario = await Repo.ObtenerPorUsername(username);

            if (usuario == null)
            {
                return null;
            }
            return usuario;
        }
    }
}
