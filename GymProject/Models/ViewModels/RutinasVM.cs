using GymProject.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace GymProject.Models.ViewModels
{
    public class RutinasVM
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public List<SetVM> Sets { get; set; }
        public List<Ejercicio> ListaEjercicios { get; set; } = new List<Ejercicio>();
        public List<Rutinas> Rutinas { get; set; } = new List<Rutinas>();

    }

    public class SetVM
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Series { get; set; }
        [Required]
        public List<EjercicioVM> Ejercicios { get; set; }
    }

    public class EjercicioVM
    {
        [Required]
        public int IdEjercicio { get; set; }
        [Required]
        public string Repeticiones { get; set; }
    }

}
