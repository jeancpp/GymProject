using GymProject.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Models.ViewModels
{
    public class EjerciciosVM
    {
        public int? IdEjercicio { get; set; }
        public string? Nombre { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }
        public string[] CategoriaSeleccionada { get; set; } = Array.Empty<string>();
        public List<Ejercicios> Ejercicios { get; set; }

    }
}
