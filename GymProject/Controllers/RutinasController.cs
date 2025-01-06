using Microsoft.AspNetCore.Mvc;

namespace GymProject.Controllers
{
    public class RutinasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
