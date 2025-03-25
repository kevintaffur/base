using front.Dtos.Productos;
using front.Models;

namespace front.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductos(string token);
        Task<Producto> ObtenerProductoPorId(int id, string token);
        Task<bool> CrearProducto(ProductoCreacionDto productoCreacionDto, string token);
        Task<bool> ModificarProducto(int id, ProductoModificacionDto productoModificacionDto, string token);
    }
}
