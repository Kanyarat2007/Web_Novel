using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin
{
    public class ManageUserPageViewModel
    {
        public List<ManageUserViewModel> Users { get; set; } = new();
        public List<Role> Roles { get; set; } = new(); // ⭐ ใช้ใน modal
    }
}