using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Reader;
using Web_Novel.Helpers;

namespace Web_Novel.Controllers;

[Area("Reader")]
[SessionAuthorize]
public class BlockController : Controller
{
    private readonly WebnoveldbContext _db;
    public BlockController(WebnoveldbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult BlockUser(int blockedUserId)
    {
        var userIdStr = HttpContext.Session.GetString("UserId");

        if (!int.TryParse(userIdStr, out int userId))
        {
            return RedirectToAction("Login", "Auth");
        }

        // ✅ เช็คว่ามีอยู่แล้วไหม
        var exists = _db.Blocklists
            .Any(x => x.BlockerUserId == userId
                   && x.BlockedUserId == blockedUserId);

        if (!exists)
        {
            // ✅ ใช้ Model (Blocklist) ไม่ใช่ ViewModel
            _db.Blocklists.Add(new Blocklist
            {
                BlockerUserId = userId,
                BlockedUserId = blockedUserId,
                CreatedAt = TimeHelper.ToThaiTime(DateTime.UtcNow)
            });

            _db.SaveChanges();
        }

        return RedirectToAction("ViewProfile", "Profile", new { id = blockedUserId });
    }

    [HttpPost]
    public IActionResult UnblockUser(int blockedUserId)
    {
        var userIdStr = HttpContext.Session.GetString("UserId");

        if (!int.TryParse(userIdStr, out int userId))
        {
            return RedirectToAction("Login", "Auth");
        }

        var block = _db.Blocklists
            .FirstOrDefault(x => x.BlockerUserId == userId
                              && x.BlockedUserId == blockedUserId);

        if (block != null)
        {
            _db.Blocklists.Remove(block);
            _db.SaveChanges();
        }

        return RedirectToAction("ViewProfile", "Profile", new { id = blockedUserId });
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
