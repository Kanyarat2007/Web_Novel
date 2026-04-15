
using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin
{
    public class EditUserRoleViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public List<int> SelectedRoleIds { get; set; } = new();
        public List<Role> Roles { get; set; } = new();
    }
}