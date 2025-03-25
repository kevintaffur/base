using api.Dtos.Productos;
using api.Dtos.Roles;
using api.Dtos.Usuarios;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository Repo;

        public ProductoService(IProductoRepository repo)
        {
            Repo = repo;
        }

        public async Task<ProductoShowDto> CrearProducto(ProductoCreacionDto productoCreacionDto)
        {
            Producto producto = new Producto
            {
                Nombre = productoCreacionDto.Nombre,
                Descripcion = productoCreacionDto.Descripcion,
                Estado = productoCreacionDto.Estado,
                UsuarioId = productoCreacionDto.UsuarioId
            };

            return MapToShow(await Repo.CrearProducto(producto));
        }

        public async Task<ProductoShowDto> ModificarProducto(int id, ProductoModificacionDto productoModificacionDto)
        {
            Producto productoExistente = await Repo.ObtenerProductoPorId(id);

            if (productoExistente == null)
            {
                return null;
            }

            if (productoModificacionDto.Nombre != null)
            {
                productoExistente.Nombre = productoModificacionDto.Nombre;
            }
            if (productoModificacionDto.Descripcion != null)
            {
                productoExistente.Descripcion = productoModificacionDto.Descripcion;
            }
            if (productoModificacionDto.Estado != null)
            {
                productoExistente.Estado = productoModificacionDto.Estado;
            }
            if (productoModificacionDto.UsuarioId != null)
            {
                productoExistente.UsuarioId = (int)productoModificacionDto.UsuarioId;
            }

            return MapToShow(await Repo.ModificarProducto(productoExistente));
        }

        public async Task<ProductoShowDto> ObtenerProductoPorId(int id)
        {
            Producto producto = await Repo.ObtenerProductoPorId(id);

            if (producto == null)
            {
                return null;
            }

            return MapToShow(producto);
        }

        public async Task<List<ProductoShowDto>> ObtenerProductos()
        {
            List<ProductoShowDto> productos = new List<ProductoShowDto>();

            foreach (var p in await Repo.ObtenerProductos())
            {
                productos.Add(MapToShow(p));
            }
            return productos;
        }

        private ProductoShowDto MapToShow(Producto producto)
        {
            if (producto == null)
            {
                return new ProductoShowDto();
            }

            RolShowDto rol = new RolShowDto
            {
                Id = producto.Usuario.Rol.Id,
                Nombre = producto.Usuario.Rol.Nombre,
            };

            UsuarioShowDto usuario = new UsuarioShowDto
            {
                Id = producto.Usuario.Id,
                Rol = rol,
                Username = producto.Usuario.Username
            };

            return new ProductoShowDto
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Estado = producto.Estado,
                Id = producto.Id,
                Usuario = usuario
            };
        }
    }
}
