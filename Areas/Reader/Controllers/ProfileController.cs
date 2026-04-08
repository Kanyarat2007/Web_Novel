using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;
using Web_Novel.Helpers;

namespace Web_Novel.Controllers;

[Area("Reader")]
[SessionAuthorize]
public class ProfileController : Controller
{
    private readonly WebnoveldbContext _db;
    public ProfileController(WebnoveldbContext db)
    {
        _db = db;
    }

    public IActionResult UserProfile()
    {
        var username = HttpContext.Session.GetString("Username");

        var user = (from u in _db.Users
                    where u.Username == username
                    select u).FirstOrDefault();

        var vm = new UserProfileViewModel
        {
            Username = user?.Username,
            DisplayName = user?.DisplayName,
            Email = user?.Email,
            BirthDate = user?.BirthDate,
            Bio = user?.Bio,
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UserProfileViewModel model, bool? removeImage)
    {
        var user = (from u in _db.Users
                    where u.Username == model.Username
                    select u).FirstOrDefault();

        if (user != null)
        {
            user.DisplayName = model.DisplayName;
            user.Email = model.Email;
            user.BirthDate = model.BirthDate;
            user.Bio = model.Bio;

            if (model.ProfileImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfileImageFile.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profiles", fileName);

                // จำกัดประเภทไฟล์
                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

                var ext = Path.GetExtension(model.ProfileImageFile.FileName).ToLower();

                if (!allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("", "Only JPG/PNG allowed");
                    return View(model);
                }

                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfileImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImageFile.CopyToAsync(stream);
                }

                user.ProfileImageUrl = "/uploads/profiles/" + fileName; // ✅ เก็บ path
            }

            if (removeImage == true)
            {
                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfileImageUrl.TrimStart('/'));

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                user.ProfileImageUrl = null; // 🔥 เคลียร์ค่า
            }

            // _db.Users.Update(user);
            _db.SaveChanges();
            Console.WriteLine(user.ProfileImageUrl);

            HttpContext.Session.SetString("DisplayName", user.DisplayName);
            HttpContext.Session.SetString(
                "AvatarUrl",
                user.ProfileImageUrl ?? "/images/default-avatar.png"
            );

        }

        return RedirectToAction("UserProfile");
    }

    public IActionResult ViewProfile(int id)
    {
        var user = _db.Users
            .Where(u => u.UserId == id)
            .Select(u => new UserProfileViewModel
            {
                UserId = u.UserId,
                DisplayName = u.DisplayName,
                Bio = u.Bio,
                JoinedAt = u.CreatedAt
            })
            .FirstOrDefault();

        if (user == null)
            return NotFound();

        int.TryParse(HttpContext.Session.GetString("UserId"), out int currentUserId);

        // เช็คว่า block อยู่มั้ย
        var isBlocked = _db.Blocklists.Any(b =>
            b.BlockerUserId == currentUserId &&
            b.BlockedUserId == id);

        user.IsBlocked = isBlocked;
        user.IsCurrentUser = currentUserId == id;

        return View(user);
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
