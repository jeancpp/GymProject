using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GymProject.Models.Domain
{
    public class Sets
    {
        [Key]
        public int IdSet { get; set; } // Clave primaria del set

        [Required]
        public string Nombre { get; set; } // Nombre del set

        [Required]
        public int Series { get; set; } // Número de series en el set

        // Clave foránea para la rutina
        [ForeignKey("Rutina")]
        public int IdRutina { get; set; }

        // Propiedad de navegación para la rutina
        public virtual Rutinas Rutina { get; set; }
        public virtual ICollection<SetEjercicios> SetEjercicios { get; set; }

    }
}
