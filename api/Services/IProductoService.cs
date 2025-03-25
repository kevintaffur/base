using api.Dtos.Productos;

namespace api.Services
{
    public interface IProductoService
    {
        Task<List<ProductoShowDto>> ObtenerProductos();
        Task<ProductoShowDto> ObtenerProductoPorId(int id);
        Task<ProductoShowDto> CrearProducto(ProductoCreacionDto productoCreacionDto);
        Task<ProductoShowDto> ModificarProducto(int id, ProductoModificacionDto productoModificacionDto);
    }
}
