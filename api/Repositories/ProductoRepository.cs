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

        public async Task<Producto> CrearProducto(Producto producto)
        {
            await Context.Productos.AddAsync(producto);
            await Context.SaveChangesAsync();

            Producto pr = await ObtenerProductoPorId(producto.Id);
            return pr;
        }

        public async Task<Producto> ModificarProducto(Producto producto)
        {
            Context.Entry(producto).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            Producto pr = await ObtenerProductoPorId(producto.Id);
            return pr;
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            return await Context.Productos.Include(p => p.Usuario).ThenInclude(u => u.Rol).Where(p => !p.Estado.Equals("N")).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await Context.Productos.Include(p => p.Usuario).ThenInclude(u => u.Rol).Where(p => !p.Estado.Equals("N")).ToListAsync();
        }
    }
}
