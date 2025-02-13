using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Models.ViewModels
{
    public class UsersVM
    {
       public List<User> Users { get; set; }
       public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public Guid rolSeleccionado { get; set; }

    }
}
