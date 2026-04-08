using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;

namespace Web_Novel.Controllers;

public class ModeratorController : Controller
{
    private readonly WebnoveldbContext _db;
    public ModeratorController(WebnoveldbContext db)
    {
        _db = db;
    }
    public IActionResult Index(String Username)
    {
        var username = HttpContext.Session.GetString("Username");

        ViewBag.Username = Username;
        return View();
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
