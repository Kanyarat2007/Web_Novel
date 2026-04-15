using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Admin;

namespace Web_Novel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly WebnoveldbContext _db;

        public ReportController(WebnoveldbContext db)
        {
            _db = db;
        }

        // 📋 รายการ report
        public IActionResult Index(string? type, string? status)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin" && role != "Moderator")
                return Unauthorized();
                
            var query = _db.Reports.AsQueryable();

            if (!string.IsNullOrEmpty(type))
                query = query.Where(r => r.TargetType == type);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status == status);

            var reports = query
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ReportAdminViewModel
                {
                    ReportId = r.ReportId,
                    TargetType = r.TargetType,
                    TargetId = r.TargetId,
                    Reason = r.Reason,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,
                    Status = r.Status,
                    ReporterName = r.ReporterUser.Username,

                    TargetContent =
                        r.TargetType == "comment"
                        ? _db.Comments
                            .Where(c => c.CommentId == r.TargetId)
                            .Select(c => c.Content)
                            .FirstOrDefault()
                        : _db.Novels
                            .Where(n => n.NovelId == r.TargetId)
                            .Select(n => n.Title)
                            .FirstOrDefault()
                })
                .ToList();

            return View(reports);
        }

        // ✅ อนุมัติ (mark handled)
        [HttpPost]
        public IActionResult Approve(int reportId)
        {
            var report = _db.Reports.Find(reportId);
            if (report == null) return NotFound();

            report.Status = "approved";
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // ❌ ปฏิเสธ
        [HttpPost]
        public IActionResult Reject(int reportId)
        {
            var report = _db.Reports.Find(reportId);
            if (report == null) return NotFound();

            report.Status = "rejected";
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // 🗑 ลบคอมเมนต์
        [HttpPost]
        public IActionResult DeleteComment(int reportId)
        {
            var report = _db.Reports.Find(reportId);
            if (report == null) return NotFound();

            if (report.TargetType == "comment")
            {
                var comment = _db.Comments.Find(report.TargetId);
                if (comment != null)
                {
                    comment.IsDeleted = true;
                    comment.Status = "deleted";
                }
            }

            report.Status = "approved";
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // ⚠️ mark spoiler
        [HttpPost]
        public IActionResult MarkSpoiler(int reportId)
        {
            var report = _db.Reports.Find(reportId);
            if (report == null) return NotFound();

            if (report.TargetType == "comment")
            {
                var comment = _db.Comments.Find(report.TargetId);
                if (comment != null)
                {
                    comment.IsSpoiler = true;
                }
            }

            report.Status = "approved";
            _db.SaveChanges();

            return RedirectToAction("Index");
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
}