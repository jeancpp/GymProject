using System.ComponentModel.DataAnnotations;

namespace GymProject.Models.Domain
{
    public class Categorias
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public ICollection<Rutinas?> Rutinas { get; set; }
        public ICollection<Ejercicios?> Ejercicios { get; set; }

    }
}
