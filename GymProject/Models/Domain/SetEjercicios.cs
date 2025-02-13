using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymProject.Models.Domain
{
    public class SetEjercicios
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public int IdSet { get; set; } 

        [Required]
        public int IdEjercicio { get; set; } 

        [Required]
        public int Repeticiones { get; set; } 

        // Propiedades de navegación
        [ForeignKey("IdSet")]
        public Sets Set { get; set; } 

        [ForeignKey("IdEjercicio")]
        public Ejercicios Ejercicio { get; set; } 
    }
}
