using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Reader;
using Web_Novel.Helpers;

namespace Web_Novel.Controllers;

[Area("Reader")]
public class ReportController : Controller
{
    private readonly WebnoveldbContext _db;
    public ReportController(WebnoveldbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Create(string targetType, int targetId, string reason, string? description)
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        if (userId <= 0)
            return Unauthorized();

        // กัน report ซ้ำ
        bool alreadyReported = _db.Reports.Any(r =>
            r.TargetType == targetType &&
            r.TargetId == targetId &&
            r.ReporterUserId == userId);

        if (alreadyReported)
            return Redirect(Request.Headers["Referer"].ToString());

        var report = new Report
        {
            ReporterUserId = userId,
            TargetType = targetType, // "comment" / "novel"
            TargetId = targetId,
            Reason = reason,
            Description = description,
            Status = "pending",
            CreatedAt = DateTime.UtcNow
        };

        _db.Reports.Add(report);
        _db.SaveChanges();

        return Redirect(Request.Headers["Referer"].ToString());
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
