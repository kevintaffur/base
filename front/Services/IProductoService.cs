using front.Dtos.Productos;
using front.Models;

namespace front.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerTodos(string token);
        Task<Producto> ObtenerPorId(int id, string token);
        Task<bool> Crear(ProductoCreacionDto entidadCreacionDto, string token);
        Task<bool> Modificar(int id, ProductoModificacionDto entidadModificacionDto, string token);
    }
}
