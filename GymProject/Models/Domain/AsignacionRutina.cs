using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GymProject.Models.Domain
{
    public class AsignacionRutina
    {
        [Key]
        public int IdAsignacion { get; set; }

        [Required]
        [ForeignKey("Rutina")]
        public int IdRutina { get; set; }

        [Required]
        public string IdUsuario { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha { get; set; }

        // Navegación
        public virtual Rutinas Rutina { get; set; } = null!;
    }

}
