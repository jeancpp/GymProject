using GymProject.Data;
using GymProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Repositories
{
    public class EjerciciosRepository : IEjerciciosRepository
    {
        private readonly GymDbContext gymDbContext;

        public EjerciciosRepository(GymDbContext gymDbContext)
        {
            this.gymDbContext = gymDbContext;
        }

        public async Task<Ejercicios> AddAsync(Ejercicios ejercicio)
        {
           await gymDbContext.Ejercicios.AddAsync(ejercicio);
           await gymDbContext.SaveChangesAsync();

            return ejercicio;   
        }

        public async Task<IEnumerable<Ejercicios>> GetAllAsync()
        {
            return await gymDbContext.Ejercicios.Include(x => x.Categorias).ToListAsync();
        }

        public Task<Ejercicios> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
