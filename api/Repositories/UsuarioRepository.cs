using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ProductosContext Context;

        public UsuarioRepository(ProductosContext context)
        {
            Context = context;
        }

        public async Task<Usuario> Crear(Usuario usuario)
        {
            await Context.Usuarios.AddAsync(usuario);
            await Context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> ObtenerPorId(int id)
        {
            return await Context.Usuarios.Include(u => u.Rol).FirstAsync(u => u.Id == id);
        }

        public async Task<Usuario> ObtenerPorUsername(string username)
        {
            return await Context.Usuarios.Include(u => u.Rol).FirstAsync(u => u.Username.ToLower().Equals(username.ToLower()));
        }
    }
}
