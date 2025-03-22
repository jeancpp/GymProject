using GymProject.Data;
using GymProject.Models.Domain;
using GymProject.Models.DTO;
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
        }

        public async Task<IEnumerable<Rutinas>> GetRutinas()
        {
            var query = gymDbContext.Rutinas;

            return await query.ToListAsync();
        }

        public async Task<RutinaDto> GetAsync(int id)
        {
            var rutina = await gymDbContext.Rutinas
                .Include(x => x.Sets)
                    .ThenInclude(s => s.SetEjercicios)
                        .ThenInclude(e => e.Ejercicio)
                .FirstOrDefaultAsync(x => x.IdRutina == id);

            if (rutina == null) return null;

            var rutinaDto = new RutinaDto
            {
                IdRutina = rutina.IdRutina,
                Nombre = rutina.Nombre,
                Descripcion = rutina.Descripcion,
                Sets = rutina.Sets.Select(s => new SetDto
                {
                    IdSet = s.IdSet,
                    Nombre = s.Nombre,
                    Series = s.Series,
                    SetEjercicios = s.SetEjercicios.Select(se => new EjercicioDto
                    {
                        IdEjercicio = se.Ejercicio.IdEjercicio,
                        Nombre = se.Ejercicio.Nombre,
                        Repeticiones = se.Repeticiones
                    }).ToList()
                }).ToList()
            };

            return rutinaDto;
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
