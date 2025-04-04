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

        public async Task<ProductoShowDto> Crear(ProductoCreacionDto entidadCreacionDto)
        {
            Producto entidad = new Producto
            {
                Nombre = entidadCreacionDto.Nombre,
                Descripcion = entidadCreacionDto.Descripcion,
                Estado = entidadCreacionDto.Estado,
                UsuarioId = entidadCreacionDto.UsuarioId
            };

            return MapToShow(await Repo.Crear(entidad));
        }

        public async Task<ProductoShowDto> Modificar(int id, ProductoModificacionDto entidadModificacionDto)
        {
            Producto entidadExistente = await Repo.ObtenerPorId(id);

            if (entidadExistente == null)
            {
                return null;
            }

            if (entidadModificacionDto.Nombre != null)
            {
                entidadExistente.Nombre = entidadModificacionDto.Nombre;
            }
            if (entidadModificacionDto.Descripcion != null)
            {
                entidadExistente.Descripcion = entidadModificacionDto.Descripcion;
            }
            if (entidadModificacionDto.Estado != null)
            {
                entidadExistente.Estado = entidadModificacionDto.Estado;
            }
            if (entidadModificacionDto.UsuarioId != null)
            {
                entidadExistente.UsuarioId = (int)entidadModificacionDto.UsuarioId;
            }

            return MapToShow(await Repo.Modificar(entidadExistente));
        }

        public async Task<ProductoShowDto> ObtenerPorId(int id)
        {
            Producto entidad = await Repo.ObtenerPorId(id);

            if (entidad == null)
            {
                return null;
            }

            return MapToShow(entidad);
        }

        public async Task<List<ProductoShowDto>> ObtenerTodos()
        {
            List<ProductoShowDto> entidades = new List<ProductoShowDto>();

            foreach (var p in await Repo.ObtenerTodos())
            {
                entidades.Add(MapToShow(p));
            }
            return entidades;
        }

        private ProductoShowDto MapToShow(Producto entidad)
        {
            if (entidad == null)
            {
                return new ProductoShowDto();
            }

            RolShowDto rol = new RolShowDto
            {
                Id = entidad.Usuario.Rol.Id,
                Nombre = entidad.Usuario.Rol.Nombre,
            };

            UsuarioShowDto usuario = new UsuarioShowDto
            {
                Id = entidad.Usuario.Id,
                Rol = rol,
                Username = entidad.Usuario.Username
            };

            return new ProductoShowDto
            {
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                Estado = entidad.Estado,
                Id = entidad.Id,
                Usuario = usuario
            };
        }
    }
}
