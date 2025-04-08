using GymProject.Data;
using GymProject.Models.Domain;
using GymProject.Models.DTO;
using GymProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Repositories
{
    public class AsignacionesRepository : IAsignacionesRepository
    {
        private readonly GymDbContext gymDbContext;

        public AsignacionesRepository(GymDbContext gymDbContext)
        {
            this.gymDbContext = gymDbContext;
        }

        public async Task<AsignacionRutina> AddAsync(AsignacionRutina Asignacion)
        {
            await gymDbContext.AsignacionRutina.AddAsync(Asignacion);
            await gymDbContext.SaveChangesAsync();

            return Asignacion;
        }

        public async Task<List<AsignacionRutina>> GetAsignacionesEntreFechasAsync(DateTime start, DateTime end)
        {
            return await gymDbContext.AsignacionRutina.Where(x=> x.Fecha >= start && x.Fecha < end).ToListAsync();
        }
    }
}
