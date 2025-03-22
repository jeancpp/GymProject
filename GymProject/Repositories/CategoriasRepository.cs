using GymProject.Data;
using GymProject.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace GymProject.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly GymDbContext gymDbContext;

        public CategoriasRepository(GymDbContext gymDbContext)
        {
            this.gymDbContext = gymDbContext;
        }
        public async Task<Categorias> AddAsync(Categorias categoria)
        {
            await gymDbContext.Categorias.AddAsync(categoria);
            await gymDbContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<IEnumerable<Categorias>> GetAllAsync()
        {
            return await gymDbContext.Categorias.ToListAsync();
        }

        public async Task<Categorias?> GetAsync(int id)
        {
            return await gymDbContext.Categorias.FirstOrDefaultAsync(x => x.IdCategoria == id);
        }

        public async Task<Categorias?> GetByName(string Nombre)
        {
            return await gymDbContext.Categorias.FirstOrDefaultAsync(x => x.Nombre == Nombre);
        }
    }
}
