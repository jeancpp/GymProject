using GymProject.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>>GetAll();
        Task<IEnumerable<IdentityRole>> GetAllRoles ();
        Task<IdentityRole> GetRolById(Guid id);
        Task<IdentityUser?> GetAsyncUser(Guid id);
    }
}
