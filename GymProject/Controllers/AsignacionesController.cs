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
        public async Task<IActionResult> Index()
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
            if (rutina == null)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true, rutina });
        }

        [HttpPost]
        public async Task<ActionResult> GuardarAsignacion([FromBody] AsignacionVM asignacionVM)
        {
            try
            {
                foreach (var id in asignacionVM.IdUsuarios)
                {
                    var asignacion = new AsignacionRutina
                    {
                        IdRutina = asignacionVM.IdRutina,
                        Fecha = asignacionVM.Fecha,
                        IdUsuario = id.ToString(),
                    };

                    await asignacionesRepository.AddAsync(asignacion);
                }

                return Json(new { success = true });
            }

            catch (Exception ex)
            {
                return Json(new { success = true });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAsignaciones(DateTime start, DateTime end)
        {
            var data = await asignacionesRepository.GetAsignacionesEntreFechasAsync(start, end);
            var asignaciones = new List<AsignacionesVM>();

            foreach (var id in data)
            {
                var user = await userRepository.GetAsyncUser(Guid.Parse(id.IdUsuario));

                asignaciones.Add(new AsignacionesVM
                {
                    IdAsignacion = id.IdAsignacion,
                    IdUsuario = Guid.Parse(id.IdUsuario),
                    Fecha = id.Fecha,
                    IdRutina = id.IdRutina,
                    Username = user.UserName,
                });

                
            }

            var eventos = asignaciones
            .GroupBy(a => new { a.Fecha, a.IdRutina })
            .Select(g => new {
                title = string.Join(", ", g.Select(x => x.Username)),
                start = g.Key.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"),
                allDay = false,
                nombreRutina = g.Key.IdRutina
            });



            return Json(eventos);
        }
    }
}
