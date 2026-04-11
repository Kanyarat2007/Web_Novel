// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Web_Novel.Models;
// using Web_Novel.ViewModels;

// namespace Web_Novel.Controllers;

// [Area("Writer")]
// public class StatsController : Controller
// {
//     private readonly WebnoveldbContext _db;
//     public StatsController(WebnoveldbContext db)
//     {
//         _db = db;
//     }

//     public IActionResult Stats()
//     {
//         var userIdStr = HttpContext.Session.GetString("UserId");
//         if (!int.TryParse(userIdStr, out int userId))
//             return RedirectToAction("Login", "Account", new { area = "" });

//         vm.Stats = _db.Writerstats
//     .Where(x => x.UserId == userId)
//     .Select(x => new WriterStatsViewModel
//     {
//         UserId = x.UserId,
//         ContentType = x.ContentType,
//         TotalViews = x.TotalViews,
//         TotalLikes = x.TotalLikes,
//         TotalBookshelf = x.TotalBookshelf,
//         TotalCoinEarned = x.TotalCoinEarned,
//         TotalChaptersSold = x.TotalChaptersSold,
//         UpdatedAt = x.UpdatedAt,
//         NovelId = x.NovelId,
//         NovelTitle = x.Novel.Title
//     })
//     .ToList();

//         return View("Dashboard", vm);
//     }

//     public IActionResult Privacy()
//     {
//         return View();
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }
