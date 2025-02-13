using GymProject.Data;
using GymProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Repositories
{
    public class RutinasRepository : IRutinasRepository
    {
        private readonly GymDbContext gymDbContext;

        public RutinasRepository(GymDbContext gymDbContext)
        {
            this.gymDbContext = gymDbContext;
        }
        public async Task<Rutinas> AddAsync(Rutinas rutina)
        {
            await gymDbContext.Rutinas.AddAsync(rutina);
            await gymDbContext.SaveChangesAsync();

            return rutina;
        }

        public async Task<IEnumerable<Rutinas>> GetAllAsync(string? searchQuery, DateTime? searchDate, int pageNumber = 1, int pageSize = 9)
        {
            var query = gymDbContext.Rutinas.Include(x => x.Sets).ThenInclude(s => s.SetEjercicios).ThenInclude(e => e.Ejercicio).AsQueryable();

            //Filtrar
            if (string.IsNullOrEmpty(searchQuery) == false)
            {
                query = query.Where(x => x.Nombre.Contains(searchQuery)
                || x.Descripcion.Contains(searchQuery)
                || x.Sets.Any(set =>
                set.SetEjercicios.Any(ejercicio => ejercicio.Ejercicio.Nombre.Contains(searchQuery))));
            }

            if (searchDate.HasValue)
            {
                query = query.Where(x => x.FechaCreacion.Year == searchDate.Value.Year &&
                            x.FechaCreacion.Month == searchDate.Value.Month &&
                            x.FechaCreacion.Day == searchDate.Value.Day);
            }


            //Paginación
            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            return await query.ToListAsync();
            //return await gymDbContext.Rutinas.Include(x => x.Sets).ThenInclude(s => s.SetEjercicios).ThenInclude(e => e.Ejercicio).ToListAsync();
        }

        public Task<Rutinas> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Rutinas> UpdateAsync(Rutinas rutina)
        {
            throw new NotImplementedException();
        }


        public async Task<int> CountAsync()
        {
            return await gymDbContext.Rutinas.CountAsync();
        }

        public async Task<Rutinas> DeleteAsync(int id)
        {
            var rutina = await gymDbContext.Rutinas.FindAsync(id);

            if (rutina != null)
            {
                gymDbContext.Rutinas.Remove(rutina);
                await gymDbContext.SaveChangesAsync();
                return rutina;
            }

            return null;
        }
    }
}
