using GymProject.Data;
using GymProject.Models.Domain;
using GymProject.Models.DTO;
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
    }
}
