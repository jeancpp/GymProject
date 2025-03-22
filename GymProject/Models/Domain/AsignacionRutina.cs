using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GymProject.Models.Domain
{
    public class AsignacionRutina
    {
        [Key]
        public int IdAsignacion { get; set; } // ID único de la asignación

        [Required]
        [ForeignKey("Rutina")]
        public int IdRutina { get; set; } // Rutina asignada

        [Required]
        public ICollection<IdentityUser> Usuarios { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public virtual Rutinas Rutina { get; set; }
    }
}
