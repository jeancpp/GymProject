using GymProject.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Models.ViewModels
{
    public class CategoriasVM
    {
        public string? Nombre { get; set; }
        public List<Categorias> Categorias { get; set; }

    }
}
