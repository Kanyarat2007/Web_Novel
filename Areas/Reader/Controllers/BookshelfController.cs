using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Novel.Models;
using Web_Novel.ViewModels.Reader;
using Web_Novel.Helpers;

namespace Web_Novel.Controllers;

[Area("Reader")]
[SessionAuthorize]
public class BookshelfController : Controller
{
    private readonly WebnoveldbContext _db;

    public BookshelfController(WebnoveldbContext db)
    {
        _db = db;
    }

    public IActionResult Index(string tab = "shelf")
    {
        // UserId เก็บเป็น string ต้อง parse เอง
        var userIdStr = HttpContext.Session.GetString("UserId");
        if (!int.TryParse(userIdStr, out int userId))
            return RedirectToAction("Login", "Account", new { area = "" });

        var vm = new BookshelfViewModel
        {
            UserId = userId
        };

        switch (tab)
        {
            // ── ชั้นหนังสือ (นิยาย + มังงะที่กด Add) ──
            case "shelf":
            default:
                vm.Novels = (from b in _db.Bookshelves
                                 .Include(b => b.Novel)
                             where b.UserId == userId
                               && b.ContentType == "novel"
                               && b.Novel != null
                             orderby b.AddedAt descending
                             select b.Novel!).ToList();

                vm.Mangas = (from b in _db.Bookshelves
                                 .Include(b => b.Manga)
                             where b.UserId == userId
                               && b.ContentType == "manga"
                               && b.Manga != null
                             orderby b.AddedAt descending
                             select b.Manga!).ToList();
                break;

            // ── บุ๊คมาร์ค (ตอนที่บุ๊คมาร์คไว้) ──
            case "bookmark":
                vm.Bookmarks = _db.Chapterbookmarks
                    .Where(bm => bm.UserId == userId)
                    .Include(bm => bm.Chapter)
                        .ThenInclude(c => c.Novel)   // 🔥 ตัวสำคัญ
                    .OrderByDescending(bm => bm.BookmarkedAt)
                    .ToList();
                break;

            // ── อ่านล่าสุด ──
            case "recent":
                vm.RecentNovels = (from rh in _db.Readinghistories
                                       .Include(rh => rh.Novel)
                                       .Include(rh => rh.LastChapter)
                                   where rh.UserId == userId
                                     && rh.ContentType == "novel"
                                     && rh.Novel != null
                                   orderby rh.LastReadAt descending
                                   select rh).ToList();
                break;
        }

        ViewBag.Tab = tab;
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(int novelId)
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        if (userId <= 0)
            return Unauthorized();

        var exists = _db.Bookshelves
            .Any(b => b.UserId == userId && b.NovelId == novelId);

        if (!exists)
        {
            _db.Bookshelves.Add(new Bookshelf
            {
                UserId = userId,
                NovelId = novelId,
                AddedAt = DateTime.Now
            });

            // อัปเดต count (ถ้ามี)
            var novel = _db.Novels.Find(novelId);
            if (novel != null)
                novel.TotalBookshelf++;
        }

        _db.SaveChanges();

        return RedirectToAction("NovelDetail", "Novel", new { area = "Reader", id = novelId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int novelId)
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        if (userId <= 0)
            return Unauthorized();

        var item = _db.Bookshelves
            .FirstOrDefault(b => b.UserId == userId && b.NovelId == novelId);

        if (item != null)
        {
            _db.Bookshelves.Remove(item);

            var novel = _db.Novels.Find(novelId);
            if (novel != null && novel.TotalBookshelf > 0)
                novel.TotalBookshelf--;
        }

        _db.SaveChanges();

        return RedirectToAction("NovelDetail", "Novel", new { area = "Reader", id = novelId });
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