using GymProject.Models.Domain;
using GymProject.Models.ViewModels;
using GymProject.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Controllers
{
    public class AsignacionesController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IRutinasRepository rutinasRepository;
        private readonly IAsignacionesRepository asignacionesRepository;

        public AsignacionesController(IUserRepository userRepository, IRutinasRepository rutinasRepository, IAsignacionesRepository asignacionesRepository)
        {
            this.userRepository = userRepository;
            this.rutinasRepository = rutinasRepository;
            this.asignacionesRepository = asignacionesRepository;
        }
        public async Task <IActionResult> Index()
        {
            var users = await userRepository.GetAll();
            var rutinas = await rutinasRepository.GetRutinas();
            var model = new AsignacionRutinaVM
            {
                Usuarios = users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id.ToString() }),
                Rutinas = rutinas.Select(x => new SelectListItem { Text = x.Nombre, Value = x.IdRutina.ToString() }),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ObtenerRutina([FromBody] int id)
        {
            var rutina = await rutinasRepository.GetAsync(id);
            if(rutina == null)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true, rutina});
        }

        [HttpPost]
        public async Task<ActionResult> GuardarAsignacion([FromBody] AsignacionVM asignacionVM)
        {
            var asignacion = new AsignacionRutina
            {
                IdRutina = asignacionVM.IdRutina,
                Fecha = asignacionVM.Fecha
            };

            var usuariosSeleccionados = new List<IdentityUser>();

            foreach (var id in asignacionVM.IdUsuarios)
            {
                var IdUsuario = Guid.Parse(id);
                var usuarioexistente = await userRepository.GetAsyncUser(IdUsuario);

                if (usuarioexistente != null)
                {
                    usuariosSeleccionados.Add(usuarioexistente);
                }

            }
            asignacion.Usuarios = usuariosSeleccionados;
            var resultado = await asignacionesRepository.AddAsync(asignacion);

            return Json(new { success = true });
        }
    }
}
