using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin
{
    public class CreateUserViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }

        public List<int> SelectedRoleIds { get; set; } = new();
        public List<Role> Roles { get; set; } = new();
    }
}