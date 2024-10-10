using System.ComponentModel.DataAnnotations;

namespace GymProject.Models.Domain
{
    public class Rutinas
    {
        [Key]
        public int IdRutina { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Categorias> Categorias { get; set; }
    }
}
