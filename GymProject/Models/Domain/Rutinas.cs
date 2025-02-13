using System.ComponentModel.DataAnnotations;

namespace GymProject.Models.Domain
{
    public class Rutinas
    {
        [Key]
        public int IdRutina { get; set; } // Clave primaria de la rutina

        [Required]
        public string Nombre { get; set; } // Nombre de la rutina

        public string Descripcion { get; set; } // Descripción de la rutina

        // Propiedad de navegación para los sets relacionados
        public virtual ICollection<Sets> Sets { get; set; }

        public DateTime FechaCreacion { get; set; }
    }

}
