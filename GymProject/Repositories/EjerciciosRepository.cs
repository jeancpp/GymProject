using GymProject.Data;
using GymProject.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<Ejercicios> GetAsync(int id)
        {
            return await gymDbContext.Ejercicios.FirstOrDefaultAsync(x => x.IdEjercicio == id);
        }

        public async Task<Ejercicios?> UpdateAsync(Ejercicios ejercicio)
        {
           var ejercicioExistente = await gymDbContext.Ejercicios.Include(x => x.Categorias).
                FirstOrDefaultAsync(x => x.IdEjercicio == ejercicio.IdEjercicio);

            if (ejercicioExistente != null) 
            {
                ejercicioExistente.Nombre = ejercicio.Nombre;
                ejercicioExistente.Categorias = ejercicio.Categorias;
            
            await gymDbContext.SaveChangesAsync();
            return ejercicioExistente;
            }
            return null;
        }
        

    }
}
