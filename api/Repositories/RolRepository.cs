using api.Models;

namespace api.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly ProductosContext Context;

        public RolRepository(ProductosContext context)
        {
            Context = context;
        }

        public async Task<Role> ObtenerPorId(int id)
        {
            return await Context.Roles.FindAsync(id);
        }
    }
}
