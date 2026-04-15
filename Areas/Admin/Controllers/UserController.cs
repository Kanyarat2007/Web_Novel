using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Microsoft.EntityFrameworkCore;
using Web_Novel.Helpers;
using Web_Novel.ViewModels.Admin;

namespace Web_Novel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly WebnoveldbContext _db;

        public UserController(WebnoveldbContext db)
        {
            _db = db;
        }

        // =========================
        // LIST USERS
        // =========================
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
                return Unauthorized();

            var vm = new ManageUserPageViewModel
            {
                Users = _db.Users
                .Where(u => u.IsActive == true)
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .Select(u => new ManageUserViewModel
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        DisplayName = u.DisplayName,
                        Roles = u.UserRoles.Select(r => r.Role.RoleName).ToList(),
                        RoleIds = u.UserRoles.Select(r => r.RoleId).ToList()
                    })
                    .ToList(),

                Roles = _db.Roles.ToList()
            };

            return View(vm);
        }

        // =========================
        // CREATE USER
        // =========================
        public IActionResult Create()
        {
            var vm = new CreateUserViewModel
            {
                Roles = _db.Roles.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CreateUserViewModel vm)
        {
            if (_db.Users.Any(u => u.Username == vm.Username))
            {
                ModelState.AddModelError("Username", "Username นี้มีอยู่แล้ว");

                vm.Roles = _db.Roles.ToList();
                return View(vm);
            }

            var user = new User
            {
                Username = vm.Username,
                Email = vm.Email,
                PasswordHash = PasswordHelper.HashPassword(vm.Password),
                DisplayName = vm.Username,
                IsActive = true
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            if (vm.SelectedRoleIds != null)
            {
                foreach (var roleId in vm.SelectedRoleIds)
                {
                    _db.UserRoles.Add(new UserRole
                    {
                        UserId = user.UserId,
                        RoleId = roleId
                    });
                }
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // EDIT ROLE
        // =========================
        public IActionResult EditRole(int id)
        {
            var user = _db.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.UserId == id);

            var vm = new EditUserRoleViewModel
            {
                UserId = user.UserId,
                Username = user.Username,
                SelectedRoleIds = user.UserRoles.Select(r => r.RoleId).ToList(),
                Roles = _db.Roles.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditRole(EditUserRoleViewModel vm)
        {
            var existing = _db.UserRoles.Where(x => x.UserId == vm.UserId);
            _db.UserRoles.RemoveRange(existing);

            foreach (var roleId in vm.SelectedRoleIds)
            {
                _db.UserRoles.Add(new UserRole
                {
                    UserId = vm.UserId,
                    RoleId = roleId
                });
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // DELETE USER
        // =========================
        public IActionResult Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == id);

            if (user == null) return NotFound();

            user.IsActive = false;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}