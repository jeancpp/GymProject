using GymProject.Models.Domain;
using GymProject.Models.ViewModels;

namespace GymProject.Repositories
{
    public interface IAsignacionesRepository
    {
        Task<AsignacionRutina> AddAsync(AsignacionRutina Asignacion);
        Task<List<AsignacionRutina>> GetAsignacionesEntreFechasAsync(DateTime start, DateTime end);
    }
}
