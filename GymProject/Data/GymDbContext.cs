using GymProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Categorias>Categorias { get; set; }
        public DbSet<Ejercicios> Ejercicios { get; set; }
        public DbSet<Rutinas> Rutinas { get; set; }


    }
}
