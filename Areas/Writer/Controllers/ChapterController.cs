using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Writer;

namespace Web_Novel.Controllers;

[Area("Writer")]
public class ChapterController : Controller
{
    private readonly WebnoveldbContext _db;
    public ChapterController(WebnoveldbContext db)
    {
        _db = db;
    }

    public IActionResult Index(int novelId)
    {
        var chapters = _db.Chapters
            .Where(c => c.NovelId == novelId && !c.IsDeleted)
            .OrderBy(c => c.ChapterNumber)
            .Select(c => new ChapterViewModel
            {
                ChapterId = c.ChapterId,
                NovelId = c.NovelId,
                Title = c.Title,
                Content = c.Content,
                ChapterNumber = c.ChapterNumber,
                IsCoinRequired = c.IsCoinRequired,
                CoinPrice = c.CoinPrice,
                PublishStatus = c.PublishStatus
            })
            .ToList();

        ViewBag.NovelId = novelId;

        return View(chapters); // ✅ ตอนนี้ type ตรงแล้ว
    }

    [HttpGet]
    public IActionResult Create(int novelId)
    {
        var model = new ChapterViewModel
        {
            NovelId = novelId,
            ChapterNumber = _db.Chapters
                .Where(c => c.NovelId == novelId)
                .Count() + 1
        };

        return View("CreateEdit", model); // ✅ ถูกแล้ว
    }

    [HttpPost]
    public IActionResult Save(ChapterViewModel model)
    {
        Chapter chapter;

        if (model.ChapterId == 0)
        {
            var maxChapter = _db.Chapters
                .Where(c => c.NovelId == model.NovelId)
                .Select(c => (int?)c.ChapterNumber)
                .Max() ?? 0;

            chapter = new Chapter
            {
                NovelId = model.NovelId,
                ChapterNumber = maxChapter + 1,
                Title = model.Title,
                Content = model.Content,
                IsCoinRequired = model.IsCoinRequired,
                CoinPrice = model.IsCoinRequired ? model.CoinPrice : 0,
                PublishStatus = model.PublishStatus,
                PublishedAt = model.PublishStatus == "published"
                    ? DateTime.Now
                    : null
            };

            var exists = _db.Chapters.Any(c =>
                c.NovelId == chapter.NovelId &&
                c.ChapterNumber == chapter.ChapterNumber);

            if (exists)
            {
                ModelState.AddModelError("", "เลขตอนซ้ำ");
                return View("CreateEdit", model);
            }

            _db.Chapters.Add(chapter); // ✅ ใช้ตัวนี้
        }
        else
        {
            chapter = _db.Chapters.FirstOrDefault(c => c.ChapterId == model.ChapterId);

            if (chapter == null) return NotFound();

            chapter.Title = model.Title;
            chapter.Content = model.Content;
            chapter.IsCoinRequired = model.IsCoinRequired;
            chapter.CoinPrice = model.IsCoinRequired ? model.CoinPrice : 0;
            chapter.PublishStatus = model.PublishStatus;
            chapter.PublishedAt = model.PublishStatus == "published"
                ? DateTime.Now
                : null;
        }

        _db.SaveChanges();

        // ✅ ยิง Notification เฉพาะตอนที่ publish
        if (model.ChapterId == 0 && model.PublishStatus == "published")
        {
            // 1. หา user ที่ติดตามเรื่องนี้
            var followers = _db.Bookshelves
                .Where(b => b.NovelId == model.NovelId)
                .Select(b => b.UserId)
                .ToList();

            // 2. ดึงชื่อเรื่อง (ไว้ใส่ message)
            var novelTitle = _db.Novels
                .Where(n => n.NovelId == model.NovelId)
                .Select(n => n.Title)
                .FirstOrDefault();

            // 3. สร้าง notification
            var notifications = new List<Notification>();

            foreach (var userId in followers)
            {
                notifications.Add(new Notification
                {
                    UserId = userId,
                    NovelId = model.NovelId,
                    ChapterId = chapter.ChapterId, // ⭐ สำคัญ
                    Message = $"นิยาย {novelTitle} มีตอนใหม่แล้ว",
                    IsRead = false,
                    CreatedAt = DateTime.Now
                });
            }

            // 4. save
            _db.Notifications.AddRange(notifications);
            _db.SaveChanges();
        }

        return RedirectToAction("Index", new { novelId = model.NovelId });
    }

    public IActionResult Edit(int id)
    {
        var chapter = _db.Chapters.FirstOrDefault(c => c.ChapterId == id);

        if (chapter == null) return NotFound();

        var model = new ChapterViewModel
        {
            ChapterId = chapter.ChapterId,
            NovelId = chapter.NovelId,
            Title = chapter.Title,
            Content = chapter.Content,
            ChapterNumber = chapter.ChapterNumber,
            IsCoinRequired = chapter.IsCoinRequired,
            CoinPrice = chapter.CoinPrice,
            PublishStatus = chapter.PublishStatus
        };

        return View("CreateEdit", model); // ✅ ต้องเป็น ViewModel
    }

    [HttpPost]
    public IActionResult Reorder(int novelId, List<int> orderedIds)
    {
        var chapters = _db.Chapters
     .Where(c => c.NovelId == novelId && !c.IsDeleted)
     .OrderBy(c => c.ChapterNumber)
     .ToList();

        for (int i = 0; i < chapters.Count; i++)
        {
            chapters[i].ChapterNumber = i + 1;
        }

        _db.SaveChanges();

        return Ok();
    }

    public IActionResult Delete(int id)
    {
        var chapter = _db.Chapters.FirstOrDefault(c => c.ChapterId == id);

        if (chapter == null)
            return NotFound();

        chapter.IsDeleted = true;

        _db.SaveChanges();

        return RedirectToAction("Index", new { novelId = chapter.NovelId });
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
