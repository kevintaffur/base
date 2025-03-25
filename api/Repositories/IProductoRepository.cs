using api.Models;

namespace api.Repositories
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProductoPorId(int id);
        Task<Producto> CrearProducto(Producto producto);
        Task<Producto> ModificarProducto(Producto producto);
    }
}
