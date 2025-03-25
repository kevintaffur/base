using api.Models;
using api.Utils;

namespace api.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> Crear(SignUpRequest request);
        Task<Usuario> ObtenerPorId(int id);
        Task<Usuario> ObtenerPorUsername(string username);
    }
}
