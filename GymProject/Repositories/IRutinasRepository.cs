using GymProject.Models.Domain;

namespace GymProject.Repositories
{
    public interface IRutinasRepository
    {
        Task<IEnumerable<Rutinas>>GetAllAsync(string? searchQuery, DateTime? searchDate, int pageNumber = 1, int pageSize = 9);
        Task<Rutinas> GetAsync(int id);
        Task<Rutinas> AddAsync(Rutinas rutina);
        Task<Rutinas> UpdateAsync(Rutinas rutina);
        Task<Rutinas> DeleteAsync(int id);
        Task<int>CountAsync();

    }
}
