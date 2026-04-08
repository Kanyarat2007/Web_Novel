using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;
using Web_Novel.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Web_Novel.Controllers;

public class AccountController : Controller
{
    private readonly WebnoveldbContext _db;

    public AccountController(WebnoveldbContext db)
    {
        _db = db;
    }

    public IActionResult Index(string username)
    {
        ViewBag.Username = username;
        return View();
    }

    // ── Login ──────────────────────────────────────────
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel data)
    {
        var user = (from u in _db.Users
                        .Include(u => u.Roles)
                    where u.Username == data.Username
                    select u).FirstOrDefault();

        var hash = PasswordHelper.HashPassword(data.Password);

        if (user == null || user.PasswordHash != hash)
        {
            ViewBag.Error = "Username หรือ Password ไม่ถูกต้อง";
            return View();
        }

        user.LastLoginAt = DateTime.UtcNow;
        _db.SaveChanges();

        var roleName = (from r in user.Roles
                        select r.RoleName).FirstOrDefault() ?? "Reader";

        HttpContext.Session.SetString("UserId", user.UserId.ToString());
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("DisplayName", user.DisplayName);
        HttpContext.Session.SetString("CoinBalance", user.CoinBalance.ToString());
        HttpContext.Session.SetString("AvatarUrl", user.ProfileImageUrl ?? "");
        HttpContext.Session.SetString("Role", roleName);

        if (roleName == "Admin" || roleName == "Moderator")
            return RedirectToAction("Dashboard", "Home", new { area = "Admin" });

        return RedirectToAction("Index", "Home", new { area = "Reader" });
    }

    // ── Register ───────────────────────────────────────
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel data)
    {
        if (string.IsNullOrEmpty(data.Password))
        {
            ViewBag.Error = "กรุณากรอกรหัสผ่าน";
            return View();
        }

        var dupUsername = (from u in _db.Users
                           where u.Username == data.Username
                           select u).FirstOrDefault();

        if (dupUsername != null)
        {
            ViewBag.Error = "Username นี้มีคนใช้แล้ว";
            return View();
        }

        var dupEmail = (from u in _db.Users
                        where u.Email == data.Email
                        select u).FirstOrDefault();

        if (dupEmail != null)
        {
            ViewBag.Error = "Email นี้มีคนใช้แล้ว";
            return View();
        }

        if (!DateOnly.TryParse(data.BirthDate, out DateOnly birth))
        {
            ViewBag.Error = "รูปแบบวันเกิดไม่ถูกต้อง";
            return View();
        }

        var readerRole = (from r in _db.Roles
                          where r.RoleName == "Reader"
                          select r).FirstOrDefault();

        if (readerRole == null)
        {
            ViewBag.Error = "ไม่พบข้อมูล Role กรุณาติดต่อผู้ดูแลระบบ";
            return View();
        }

        var newUser = new User
        {
            Username = data.Username,
            DisplayName = data.Username,
            PasswordHash = PasswordHelper.HashPassword(data.Password),
            Email = data.Email,
            BirthDate = birth,
            CoinBalance = 0,
            IsWriter = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        newUser.Roles.Add(readerRole);

        _db.Users.Add(newUser);
        _db.SaveChanges();

        var roleName = (from r in newUser.Roles
                        select r.RoleName).FirstOrDefault() ?? "Reader";

        HttpContext.Session.SetString("UserId", newUser.UserId.ToString());
        HttpContext.Session.SetString("Username", newUser.Username);
        HttpContext.Session.SetString("DisplayName", newUser.DisplayName);
        HttpContext.Session.SetString("CoinBalance", "0");
        HttpContext.Session.SetString("AvatarUrl", "");
        HttpContext.Session.SetString("Role", roleName);

        return RedirectToAction("Index", "Home", new { area = "Reader" });
    }

    // ── Logout ─────────────────────────────────────────
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}