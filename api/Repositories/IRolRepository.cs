using api.Models;

namespace api.Repositories
{
    public interface IRolRepository
    {
        Task<Role> ObtenerPorId(int id);
    }
}
