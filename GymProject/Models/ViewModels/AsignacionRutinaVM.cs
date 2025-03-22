using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Models.ViewModels
{
    public class AsignacionRutinaVM
    {
        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public string[] UsuariosSeleccionados { get; set; } = Array.Empty<string>();

        public IEnumerable<SelectListItem> Rutinas { get; set; }
        public string RutinaSeleccionada { get; set; }

    }
}
