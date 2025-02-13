using GymProject.Models.ViewModels;
using GymProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Controllers
{
    [Authorize(Roles = "Admin, Entrenador")]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public UsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userRepository.GetAll();
            var roles = await userRepository.GetAllRoles();

            var userVM = new UsersVM();
            userVM.Users = new List<User>();
            foreach (var user in users)
            {
                var rolesUsuario = await userManager.GetRolesAsync(user);
                userVM.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email,
                    Rol = rolesUsuario.Contains("Admin") ? "Admin" : rolesUsuario.FirstOrDefault()
                });

            }

            userVM.Roles = roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UsersVM usersVM)
        {
            var identityUser = new IdentityUser
            {
                UserName = usersVM.UserName,
                Email = usersVM.Email
            };

            var result = await userManager.CreateAsync(identityUser, usersVM.Password);
            var rol = await userRepository.GetRolById(usersVM.rolSeleccionado);
            if (result is not null)
            {
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(identityUser, rol.Name);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
