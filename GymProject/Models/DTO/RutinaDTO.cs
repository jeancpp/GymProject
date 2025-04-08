namespace GymProject.Models.DTO
{
    public class RutinaDto
    {
        public int IdRutina { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<SetDto> Sets { get; set; }
    }

    public class SetDto
    {
        public int IdSet { get; set; }
        public string Nombre { get; set; }
        public int Series { get; set; }
        public List<EjercicioDto> SetEjercicios { get; set; }
    }

    public class EjercicioDto
    {
        public int IdEjercicio { get; set; }
        public string Nombre { get; set; }
        public string Repeticiones { get; set; }
    }

}
