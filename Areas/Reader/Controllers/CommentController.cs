using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;
using Web_Novel.Helpers;

namespace Web_Novel.Controllers;

[Area("Reader")]
public class CommentController : Controller
{
    private readonly WebnoveldbContext _db;
    public CommentController(WebnoveldbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult AddComment(int novelId, int? chapterId, string content)
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        if (userId <= 0)
            return Unauthorized();

        var comment = new Comment
        {
            UserId = userId,
            NovelId = novelId,
            ChapterId = chapterId, // null ได้
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        _db.Comments.Add(comment);
        _db.SaveChanges();

        // ถ้าเป็นคอมเมนต์ทั้งเรื่อง → กลับ NovelDetail
        if (chapterId == null)
        {
            return RedirectToAction("NovelDetail", "Novel", new
            {
                area = "Reader",
                id = novelId
            });
        }

        // ถ้าเป็นคอมเมนต์ตอน → กลับ Chapter
        var chapterNumber = _db.Chapters
            .Where(c => c.ChapterId == chapterId)
            .Select(c => c.ChapterNumber)
            .FirstOrDefault();

        return RedirectToAction("NovelChapter", "Novel", new
        {
            area = "Reader",
            novelId = novelId,
            chapterNumber = chapterNumber
        });
    }

    [HttpPost]
    public IActionResult DeleteComment(int commentId)
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        var comment = _db.Comments.FirstOrDefault(c => c.CommentId == commentId);

        if (comment == null)
            return NotFound();

        if (comment.UserId != userId)
            return Unauthorized();

        _db.Comments.Remove(comment);
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
