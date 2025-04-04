using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ProductosContext Context;

        public ProductoRepository(ProductosContext context)
        {
            Context = context;
        }

        public async Task<Producto> Crear(Producto entidad)
        {
            await Context.Productos.AddAsync(entidad);
            await Context.SaveChangesAsync();

            Producto e = await ObtenerPorId(entidad.Id);
            return e;
        }

        public async Task<Producto> Modificar(Producto entidad)
        {
            Context.Entry(entidad).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            Producto e = await ObtenerPorId(entidad.Id);
            return e;
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            return await Context.Productos.Include(p => p.Usuario).ThenInclude(u => u.Rol).Where(p => !p.Estado.Equals("N")).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            return await Context.Productos.Include(p => p.Usuario).ThenInclude(u => u.Rol).Where(p => !p.Estado.Equals("N")).ToListAsync();
        }
    }
}
