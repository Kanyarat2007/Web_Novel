using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Writer;

namespace Web_Novel.Controllers;

[Area("Writer")]
public class PenNameController : Controller
{
    private readonly WebnoveldbContext _db;
    public PenNameController(WebnoveldbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Save(PenNameViewModel model)
    {
        // เพิ่มนามปากกา
        if (model.PenNameId == 0)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "Account", new { area = "" });


            if (!ModelState.IsValid)
                return View(model);

            // 🔥 ถ้าเลือก default → เคลียร์ของเก่า
            if (model.IsDefault)
            {
                var oldDefaults = _db.Pennames
                    .Where(p => p.UserId == userId && p.IsDefault);

                foreach (var p in oldDefaults)
                {
                    p.IsDefault = false;
                }
            }

            if (_db.Pennames.Any(p => p.UserId == userId && p.PenName == model.PenName))
            {
                ModelState.AddModelError("PenName", "ชื่อนี้ถูกใช้แล้ว");
                return View(model);
            }

            var pen = new Penname
            {
                UserId = userId,
                PenName = model.PenName,
                Bio = model.Bio,
                AvatarUrl = model.AvatarUrl,
                IsDefault = model.IsDefault,
                CreatedAt = DateTime.Now
            };

            _db.Pennames.Add(pen);
            _db.SaveChanges();
        }
        // แก้ไขนามปากกา
        else
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "Account", new { area = "" });

            if (!ModelState.IsValid)
                return View(model);

            var pen = _db.Pennames
                .FirstOrDefault(p => p.PenNameId == model.PenNameId && p.UserId == userId);

            if (pen == null)
                return NotFound();

            // reset default
            if (model.IsDefault)
            {
                var oldDefaults = _db.Pennames
                    .Where(p => p.UserId == userId);

                foreach (var p in oldDefaults)
                    p.IsDefault = false;
            }

            pen.PenName = model.PenName;
            pen.Bio = model.Bio;
            pen.AvatarUrl = model.AvatarUrl;
            pen.IsDefault = model.IsDefault;

            _db.SaveChanges();
        }

        return RedirectToAction("Dashboard", "Writer", new { area = "Writer", tab = "pennames" });
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int penNameId)
    {
        var userIdStr = HttpContext.Session.GetString("UserId");
        if (!int.TryParse(userIdStr, out int userId))
            return RedirectToAction("Login", "Account", new { area = "" });

        var pen = _db.Pennames
            .FirstOrDefault(p => p.PenNameId == penNameId && p.UserId == userId);

        if (pen != null)
        {
            _db.Pennames.Remove(pen);
            _db.SaveChanges();
        }

        return RedirectToAction("Dashboard", "Writer", new { area = "Writer", tab = "pennames" });
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
