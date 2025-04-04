using api.Dtos.Productos;

namespace api.Services
{
    public interface IProductoService
    {
        Task<List<ProductoShowDto>> ObtenerTodos();
        Task<ProductoShowDto> ObtenerPorId(int id);
        Task<ProductoShowDto> Crear(ProductoCreacionDto entidadCreacionDto);
        Task<ProductoShowDto> Modificar(int id, ProductoModificacionDto entidadModificacionDto);
    }
}
