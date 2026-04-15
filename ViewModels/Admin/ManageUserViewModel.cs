using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin
{
    public class ManageUserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public List<string> Roles { get; set; } = new();
         public List<int> RoleIds { get; set; } = new();
    }
}