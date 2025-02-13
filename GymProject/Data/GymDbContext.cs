using GymProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        public DbSet<Categorias>Categorias { get; set; }
        public DbSet<Ejercicios> Ejercicios { get; set; }
        public DbSet<Rutinas> Rutinas { get; set; }
        public DbSet<Sets> Sets { get; set; }
        public DbSet<SetEjercicios> SetEjercicios { get; set; }
    }
}
