using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace GymProject.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var rolAdmin = "5ec39c8c-ec88-4121-931e-3ebea6f8e572";
            var rolUser = "dccacb4f-7b51-43f6-9414-b890f4e77a16";
            var rolEnt = "403d7ca7-90e3-47f0-91e5-6aea5e790d9d";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName= "Admin",
                    Id = rolAdmin,
                    ConcurrencyStamp = rolAdmin
                },

                new IdentityRole
                {
                    Name="Entrenador",
                    NormalizedName= "Entrenador",
                    Id = rolEnt,
                    ConcurrencyStamp = rolEnt
                },

                new IdentityRole
                {
                    Name="Usuario",
                    NormalizedName= "Usuario",
                    Id = rolUser,
                    ConcurrencyStamp = rolUser
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var adminId = "0c5bd997-23b1-4a02-afa7-a9a6510cc3f6";
           
            var adminUser = new IdentityUser
            {
                UserName = "admin",
                Id = adminId,
                NormalizedUserName="admin"
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "prol27no323");

            builder.Entity<IdentityUser>().HasData(adminUser);

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                  RoleId= rolAdmin,
                  UserId= adminId,
                },

                 new IdentityUserRole<string>
                {
                  RoleId= rolEnt,
                  UserId= adminId,
                },

                  new IdentityUserRole<string>
                {
                  RoleId= rolUser,
                  UserId= adminId,
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
