using api.Models;

namespace api.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Crear(Usuario usuario);
        Task<Usuario> ObtenerPorId(int id);
        Task<Usuario> ObtenerPorUsername(string username);
    }
}
