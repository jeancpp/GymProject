using GymProject.Models.Domain;

namespace GymProject.Repositories
{
    public interface IEjerciciosRepository
    {
        Task<IEnumerable<Ejercicios>>GetAllAsync();
        Task<Ejercicios> GetAsync(int id);
        Task<Ejercicios> AddAsync(Ejercicios ejercicio);
        Task<Ejercicios> UpdateAsync(Ejercicios ejercicio);

    }
}
