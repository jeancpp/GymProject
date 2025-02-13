using GymProject.Data;
using GymProject.Models.Domain;
using GymProject.Models.ViewModels;
using GymProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace GymProject.Controllers
{
    [Authorize(Roles = "Admin, Entrenador")]
    public class CategoriasController : Controller
    {
        private readonly ICategoriasRepository categoriasRepository;

        public CategoriasController(ICategoriasRepository categoriasRepository)
        {
            this.categoriasRepository = categoriasRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categorias = await categoriasRepository.GetAllAsync();
            var model = new CategoriasVM
            {
                Categorias = (List<Categorias>)categorias
            };
            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> Index(CategoriasVM categoriasVM)
        {
            if (ModelState.IsValid) {

                var categoria = new Categorias
                {
                    Nombre = categoriasVM.Nombre,
                };

                await categoriasRepository.AddAsync(categoria);

            }
            return RedirectToAction("Index");
        }
    }
}
