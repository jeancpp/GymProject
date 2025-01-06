namespace GymProject.Models.ViewModels
{
    public class EditarEjercicioVM
    {
        public int IdEjercicio { get; set; }
        public string? Nombre { get; set; }
        public int[] CategoriaSeleccionada { get; set; } = Array.Empty<int>();

    }
}
