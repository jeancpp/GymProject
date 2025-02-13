using GymProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var result = await signInManager.PasswordSignInAsync(loginVM.Username, loginVM.Password, false, false);

            if (result.Succeeded && result!= null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(loginVM);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "Login");
        }


    }
}
