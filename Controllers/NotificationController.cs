using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;

namespace Web_Novel.Controllers;

public class NotificationController : Controller
{
    private readonly WebnoveldbContext _db;
    public NotificationController(WebnoveldbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var username = HttpContext.Session.GetString("Username");

        ViewBag.Username = username;

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
