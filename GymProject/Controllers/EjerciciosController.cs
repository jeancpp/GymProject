using GymProject.Data;
using GymProject.Models.Domain;
using GymProject.Models.ViewModels;
using GymProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Controllers
{
    public class EjerciciosController : Controller
    {
        private readonly IEjerciciosRepository ejerciciosRepository;
        private readonly ICategoriasRepository categoriasRepository;

        public EjerciciosController(IEjerciciosRepository ejerciciosRepository, ICategoriasRepository categoriasRepository)
        {
            this.ejerciciosRepository = ejerciciosRepository;
            this.categoriasRepository = categoriasRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await categoriasRepository.GetAllAsync();
            var ejercicios = await ejerciciosRepository.GetAllAsync();

            var model = new EjerciciosVM
            {
                Categorias = categorias.Select(x => new SelectListItem { Text = x.Nombre, Value = x.IdCategoria.ToString() }),
                Ejercicios = (List<Ejercicios>)ejercicios
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> Index(EjerciciosVM ejerciciosVM)
        {
            var ejercicio = new Ejercicios
            {
                Nombre = ejerciciosVM.Nombre,
            };

            var categoriasSeleccionadas = new List<Categorias>();

            foreach (var id in ejerciciosVM.CategoriaSeleccionada)
            {
                var idCategoria = int.Parse(id);
                var categoriaExistente = await categoriasRepository.GetAsync(idCategoria);

                if (categoriaExistente != null)
                {
                    categoriasSeleccionadas.Add(categoriaExistente);
                }

            }
            ejercicio.Categorias = categoriasSeleccionadas;

            await ejerciciosRepository.AddAsync(ejercicio);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditarEjercicio(EditarEjercicioVM editarEjercicio)
        {
            var ejercicio = new Ejercicios
            {
                IdEjercicio = editarEjercicio.IdEjercicio,
                Nombre = editarEjercicio.Nombre
            };

            var categoriasSeleccionadas = new List<Categorias>();

            foreach (var categoriaSeleccionada in editarEjercicio.CategoriaSeleccionada)
            {
                var categoria = await categoriasRepository.GetAsync(categoriaSeleccionada);
                if (categoria != null)
                {
                    categoriasSeleccionadas.Add(categoria);
                }
            }

            ejercicio.Categorias = categoriasSeleccionadas;

            var ejercicioActualizado = await ejerciciosRepository.UpdateAsync(ejercicio);

            if (ejercicioActualizado != null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
