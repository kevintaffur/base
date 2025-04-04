using api.Models;

namespace api.Repositories
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerTodos();
        Task<Producto> ObtenerPorId(int id);
        Task<Producto> Crear(Producto entidad);
        Task<Producto> Modificar(Producto entidad);
    }
}
