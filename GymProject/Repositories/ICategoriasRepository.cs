using GymProject.Models.Domain;

namespace GymProject.Repositories
{
    public interface ICategoriasRepository
    {
        Task<IEnumerable<Categorias>>GetAllAsync();
        Task<Categorias?> GetAsync(int id);
        Task<Categorias> AddAsync(Categorias categoria);
    }
}
