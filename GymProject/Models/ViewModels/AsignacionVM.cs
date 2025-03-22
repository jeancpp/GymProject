namespace GymProject.Models.ViewModels
{
    public class AsignacionVM
    {
        public int IdRutina { get; set; }
        public string[] IdUsuarios { get; set; } = Array.Empty<string>();
        public DateTime Fecha { get; set; }
    }
}
