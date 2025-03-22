using GymProject.Data;
using GymProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();

            var adminUser = await authDbContext.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == "admin");

            if (adminUser != null)
            {
                users.Remove(adminUser);
            }

            return users;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRoles()
        {
            var roles = await authDbContext.Roles.ToListAsync();
            return roles;
        }

        public async Task <IdentityRole> GetRolById(Guid id)
        {
            var rol = await authDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id.ToString());
            return rol;
        }
    }
}
