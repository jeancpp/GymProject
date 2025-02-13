using GymProject.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GymProject.Models.ViewModels
{
    public class CategoriasVM
    {
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public List<Categorias> Categorias { get; set; }

    }
}
