using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Novel.Models;
using Web_Novel.ViewModels;
using Web_Novel.Helpers;

namespace Web_Novel.Controllers;

public class AccountController : Controller
{
    private readonly WebnoveldbContext _db;

    public AccountController(WebnoveldbContext db)
    {
        _db = db;
    }

    // =========================
    // LOGIN
    // =========================
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel data)
    {
        if (!ModelState.IsValid)
            return View(data);

        var user = _db.Users
            .Include(u => u.Roles)
            .FirstOrDefault(u => u.Username == data.Username);

        if (user == null)
        {
            ViewBag.Error = "Username หรือ Password ไม่ถูกต้อง";
            return View();
        }

        // ✅ ใช้ Verify แทน hash เทียบตรง
        var isPasswordValid = PasswordHelper.VerifyPassword(
            data.Password,
            user.PasswordHash
        );

        if (!isPasswordValid)
        {
            ViewBag.Error = "Username หรือ Password ไม่ถูกต้อง";
            return View();
        }

        // =========================
        // update login time
        // =========================
        user.LastLoginAt = DateTime.UtcNow;
        _db.SaveChanges();

        // =========================
        // ROLE PRIORITY
        // =========================
        var roleName =
            user.Roles.Any(r => r.RoleName == "Admin") ? "Admin" :
            user.Roles.Any(r => r.RoleName == "Moderator") ? "Moderator" :
            user.Roles.Any(r => r.RoleName == "Writer") ? "Writer" :
            "Reader";

        var role = HttpContext.Session.GetString("Role");

        // =========================
        // SESSION
        // =========================
        HttpContext.Session.SetString("UserId", user.UserId.ToString());
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("DisplayName", user.DisplayName ?? user.Username);
        HttpContext.Session.SetString("CoinBalance", user.CoinBalance.ToString());
        HttpContext.Session.SetString("AvatarUrl", user.ProfileImageUrl ?? "");
        HttpContext.Session.SetString("Role", roleName);

        // =========================
        // REDIRECT BY ROLE
        // =========================
        return roleName switch
        {
            "Admin" => RedirectToAction("Index", "Report", new { area = "Admin" }),

            "Moderator" => RedirectToAction("Index", "Report", new { area = "Admin" }),

            "Writer" => RedirectToAction("Dashboard", "Writer", new { area = "Writer" }),

            _ => RedirectToAction("Index", "Home", new { area = "Reader" })
        };
    }

    // =========================
    // REGISTER
    // =========================
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel data)
    {
        if (!ModelState.IsValid)
            return View(data);

        if (string.IsNullOrWhiteSpace(data.Password))
        {
            ViewBag.Error = "กรุณากรอกรหัสผ่าน";
            return View(data);
        }

        // ตรวจ username ซ้ำ
        if (_db.Users.Any(u => u.Username == data.Username))
        {
            ViewBag.Error = "Username นี้มีคนใช้แล้ว";
            return View(data);
        }

        // ตรวจ email ซ้ำ
        if (_db.Users.Any(u => u.Email == data.Email))
        {
            ViewBag.Error = "Email นี้มีคนใช้แล้ว";
            return View(data);
        }

        // parse birthdate
        if (!DateOnly.TryParseExact(data.BirthDate, "yyyy-MM-dd",
    System.Globalization.CultureInfo.InvariantCulture,
    System.Globalization.DateTimeStyles.None,
    out DateOnly birth))
        {
            ViewBag.Error = "รูปแบบวันเกิดไม่ถูกต้อง";
            return View(data);
        }

        var readerRole = _db.Roles.FirstOrDefault(r => r.RoleName == "Reader");

        if (readerRole == null)
        {
            ViewBag.Error = "ไม่พบ Role Reader";
            return View(data);
        }

        var newUser = new User
        {
            Username = data.Username,
            DisplayName = data.Username,
            Email = data.Email,
            BirthDate = birth,
            PasswordHash = PasswordHelper.HashPassword(data.Password),

            CoinBalance = 0,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        newUser.Roles.Add(readerRole);

        _db.Users.Add(newUser);
        _db.SaveChanges();

        // login ทันที
        HttpContext.Session.SetString("UserId", newUser.UserId.ToString());
        HttpContext.Session.SetString("Username", newUser.Username);
        HttpContext.Session.SetString("DisplayName", newUser.DisplayName);
        HttpContext.Session.SetString("CoinBalance", "0");
        HttpContext.Session.SetString("AvatarUrl", "");
        HttpContext.Session.SetString("Role", "Reader");

        return RedirectToAction("Index", "Home", new { area = "Reader" });
    }

    // =========================
    // LOGOUT
    // =========================
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    // =========================
    // ERROR
    // =========================
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}