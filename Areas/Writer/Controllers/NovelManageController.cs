using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;

namespace Web_Novel.Controllers;

[Area("Writer")]
public class NovelManageController : Controller
{
    private readonly WebnoveldbContext _db;
    public NovelManageController(WebnoveldbContext db)
    {
        _db = db;
    }

    public IActionResult Create()
    {
        var username = HttpContext.Session.GetString("Username");

        return RedirectToAction("Writer", "Dashboard");
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
