namespace GymProject.Models.ViewModels
{
    public class AsignacionVM
    {
        public int IdRutina { get; set; }
        public Guid[] IdUsuarios { get; set; } = Array.Empty<Guid>();
        public DateTime Fecha { get; set; }
    }
}
