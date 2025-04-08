namespace GymProject.Models.ViewModels
{
    public class AsignacionesVM
    {
        public int IdAsignacion { get; set; }
        public int IdRutina { get; set; }
        public Guid IdUsuario { get; set; }
        public string Username { get; set; }
        public DateTime Fecha { get; set; }


    }
}
