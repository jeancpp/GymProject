using System.ComponentModel.DataAnnotations;

namespace GymProject.Models.Domain
{
    public class Ejercicios
    {
        [Key]
        public int IdEjercicio { get; set; }
        public string Nombre { get; set; }
        public ICollection<Categorias> Categorias { get; set; }
    }
}
