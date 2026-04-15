using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;

public class NotificationController : Controller
{
    private readonly WebnoveldbContext _db;

    public NotificationController(WebnoveldbContext db)
    {
        _db = db;
    }

    // 🔔 หน้าแสดงแจ้งเตือนทั้งหมด
    public IActionResult Index()
    {
        var userIdStr = HttpContext.Session.GetString("UserId");

        if (string.IsNullOrEmpty(userIdStr))
            return RedirectToAction("Login", "Account");

        int userId = int.Parse(userIdStr);

        var notifications = _db.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NotificationViewModel
            {
                NotificationId = n.NotificationId,
                NovelTitle = n.Novel.Title,
                ChapterId = n.ChapterId,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            })
            .ToList();

        return View(notifications);
    }

    // ✔ กดอ่าน (mark as read)
    public IActionResult MarkAsRead(int id)
    {
        var noti = _db.Notifications.FirstOrDefault(n => n.NotificationId == id);

        if (noti == null)
            return RedirectToAction("Index");

        noti.IsRead = true;
        _db.SaveChanges();

        if (noti.ChapterId != null)
        {
            var chapter = _db.Chapters
                .FirstOrDefault(c => c.ChapterId == noti.ChapterId);

            if (chapter != null)
            {
                return RedirectToAction("NovelChapter", "Novel", new
                {
                    area = "Reader",
                    novelId = chapter.NovelId,
                    chapterNumber = chapter.ChapterNumber
                });
            }
        }

        // fallback
        return RedirectToAction("NovelChapter", "Novel", new
        {
            novelId = noti.NovelId
        });
    }


    // ✔ อ่านทั้งหมด
    public IActionResult MarkAllAsRead()
    {
        var userIdStr = HttpContext.Session.GetString("UserId");

        if (string.IsNullOrEmpty(userIdStr))
            return RedirectToAction("Login", "Account");

        int userId = int.Parse(userIdStr);

        var notis = _db.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToList();

        foreach (var n in notis)
        {
            n.IsRead = true;
        }

        _db.SaveChanges();

        return RedirectToAction("Index");
    }


}