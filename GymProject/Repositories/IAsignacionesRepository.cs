using GymProject.Models.Domain;

namespace GymProject.Repositories
{
    public interface IAsignacionesRepository
    {
        Task<AsignacionRutina> AddAsync(AsignacionRutina Asignacion);
    }
}
