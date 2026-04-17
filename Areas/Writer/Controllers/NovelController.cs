using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Writer;
using Web_Novel.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Web_Novel.Areas.Writer.Controllers;

[Area("Writer")]
public class NovelController : Controller
{
    private readonly WebnoveldbContext _db;
    public NovelController(WebnoveldbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Create()
    {
        var userIdStr = HttpContext.Session.GetString("UserId");
        if (!int.TryParse(userIdStr, out int userId) || userId <= 0)
            return RedirectToAction("Login", "Account");

        var model = new CreateNovelViewModel
        {
            PenNames = _db.Pennames.Where(p => p.UserId == userId).ToList(),
            Genres = _db.Genres.ToList(),
            TriggerWarnings = _db.TriggerWarnings
                .OrderBy(t => t.Name)
                .ToList(),

            SelectedWarningIds = new List<int>() // ✅ ว่าง
        };

        return View("Create", model);
    }

    // [HttpGet]
    // public IActionResult Create(int[]? selectedIds = null)
    // {
    //     var userIdStr = HttpContext.Session.GetString("UserId");
    //     if (!int.TryParse(userIdStr, out int userId) || userId <= 0)
    //         return RedirectToAction("Login", "Account");

    //     // สร้าง ViewModel
    //     var model = new CreateNovelViewModel
    //     {
    //         PenNames = _db.Pennames
    //                      .Where(p => p.UserId == userId)
    //                      .ToList(),

    //         Genres = _db.Genres.ToList(),

    //         TriggerWarnings = _db.TriggerWarnings
    //                              .OrderBy(tw => tw.Name)
    //                              .ToList(),

    //         SelectedTriggerWarnings = selectedIds != null && selectedIds.Length > 0
    //             ? _db.TriggerWarnings
    //                  .Where(tw => selectedIds.Contains(tw.WarningId))
    //                  .OrderBy(tw => tw.Name)
    //                  .ToList()
    //             : new List<TriggerWarning>()
    //     };

    //     return View(model);
    // }

    // [HttpPost]
    // public IActionResult Create(CreateNovelViewModel model, string triggerWarningIds)
    // {
    //     // ✅ 1. เช็ค login
    //     var userIdStr = HttpContext.Session.GetString("UserId");
    //     if (!int.TryParse(userIdStr, out int userId) || userId <= 0)
    //         return RedirectToAction("Login", "Account");

    //     // ✅ 2. เช็คมีนามปากกา
    //     var penNames = _db.Pennames.Where(p => p.UserId == userId).ToList();
    //     if (!penNames.Any())
    //     {
    //         TempData["ErrorMessage"] = "กรุณาสร้างนามปากกาก่อน";
    //         return RedirectToAction("ManagePenNames", "Writer");
    //     }

    //     // ✅ 3. Upload รูป
    //     string? coverPath = null;

    //     if (Request.Form["removeCoverImage"] == "true")
    //     {
    //         coverPath = null;
    //     }
    //     else if (model.CoverImage != null && model.CoverImage.Length > 0)
    //     {
    //         var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.CoverImage.FileName);

    //         var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

    //         using (var stream = new FileStream(filePath, FileMode.Create))
    //         {
    //             model.CoverImage.CopyTo(stream);
    //         }

    //         coverPath = "/uploads/" + fileName;
    //     }

    //     // ✅ 4. สร้าง Slug
    //     string GenerateSlug(string title)
    //     {
    //         var slug = title.ToLowerInvariant()
    //                         .Replace(" ", "-")
    //                         .Replace(",", "")
    //                         .Replace(".", "");
    //         return slug;
    //     }

    //     var baseSlug = GenerateSlug(model.Title);
    //     var slug = baseSlug;
    //     int count = 1;

    //     while (_db.Novels.Any(n => n.Slug == slug))
    //     {
    //         slug = $"{baseSlug}-{count}";
    //         count++;
    //     }


    //     // ✅ 5. สร้าง Novel
    //     var novel = new Novel
    //     {
    //         PenNameId = model.PenNameId,
    //         AuthorUserId = userId,
    //         Title = model.Title,
    //         Slug = slug,
    //         Synopsis = model.Synopsis,
    //         AuthorNote = model.AuthorNote, // ⭐ สำคัญ
    //         GenreId = model.GenreId,
    //         SeriesType = model.SeriesType, // ⭐ เพิ่มถ้ามี
    //         CoverImageUrl = coverPath,
    //         Status = NovelStatus.Ongoing, // หรือมาจาก model.Status ถ้ามีใน form
    //         CreatedAt = DateTime.UtcNow,
    //         PublishStatus = model.PublishStatus,
    //     };

    //     _db.Novels.Add(novel);
    //     _db.SaveChanges();

    //     // ✅ 6. เพิ่ม TriggerWarnings
    //     // แปลงค่า triggerWarningIds จาก hidden input เป็น int[]
    //     if (!string.IsNullOrEmpty(triggerWarningIds))
    //     {
    //         var ids = triggerWarningIds
    //             .Split(',', StringSplitOptions.RemoveEmptyEntries)
    //             .Select(int.Parse)
    //             .ToList();

    //         foreach (var id in ids)
    //         {
    //             _db.NovelTriggerWarnings.Add(new NovelTriggerWarning
    //             {
    //                 NovelId = novel.NovelId,
    //                 WarningId = id
    //             });
    //         }
    //         _db.SaveChanges();
    //     }

    //     return RedirectToAction("Dashboard", "Writer", new { area = "Writer" });
    // }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var userIdStr = HttpContext.Session.GetString("UserId");
        if (!int.TryParse(userIdStr, out int userId) || userId <= 0)
            return RedirectToAction("Login", "Account");

        var novel = _db.Novels
            .Include(n => n.NovelTriggerWarnings)
            .FirstOrDefault(n => n.NovelId == id && n.AuthorUserId == userId);

        if (novel == null) return NotFound();

        var model = new CreateNovelViewModel
        {
            NovelId = novel.NovelId,
            PenNameId = novel.PenNameId,
            GenreId = novel.GenreId,
            Title = novel.Title,
            Synopsis = novel.Synopsis,
            AuthorNote = novel.AuthorNote,
            SeriesType = novel.SeriesType,
            Slug = novel.Slug,
            PublishStatus = novel.PublishStatus,
            ExistingCoverImageUrl = novel.CoverImageUrl,
            Status = novel.Status,

            SelectedWarningIds = novel.NovelTriggerWarnings
                .Select(x => x.WarningId)
                .ToList(),

            PenNames = _db.Pennames.Where(p => p.UserId == userId).ToList(),
            SelectedGenreIds = novel.NovelGenres.Select(x => x.GenreId).ToList(),
            TriggerWarnings = _db.TriggerWarnings.OrderBy(t => t.Name).ToList(),

            Genres = _db.Genres.ToList(),

        };

        return View("Create", model);
    }

    // [HttpGet]
    // public IActionResult Edit(int id)
    // {
    //     var userIdStr = HttpContext.Session.GetString("UserId");
    //     if (!int.TryParse(userIdStr, out int userId) || userId <= 0)
    //         return RedirectToAction("Login", "Account");

    //     var model = _db.Novels
    //         .Where(n => n.NovelId == id && n.AuthorUserId == userId)
    //         .Select(n => new CreateNovelViewModel
    //         {
    //             NovelId = n.NovelId,
    //             Title = n.Title,
    //             Synopsis = n.Synopsis,
    //             AuthorNote = n.AuthorNote,
    //             PenNameId = n.PenNameId,
    //             GenreId = n.GenreId,
    //             SeriesType = n.SeriesType,
    //             ExistingCoverImageUrl = n.CoverImageUrl,
    //             TriggerWarningIds = n.NovelTriggerWarnings
    //                          .Select(ntw => ntw.WarningId)
    //                          .ToList(),
    //             SelectedTriggerWarnings = n.NovelTriggerWarnings
    //                               .Select(ntw => ntw.TriggerWarning)
    //                               .ToList(),

    //             PenNames = _db.Pennames.Where(p => p.UserId == userId).ToList(),
    //             Genres = _db.Genres.ToList(),
    //             TriggerWarnings = _db.TriggerWarnings.OrderBy(t => t.Name).ToList(),
    //         })
    //         .FirstOrDefault();

    //     if (model == null)
    //         return NotFound();

    //     return View("Create", model);
    // }

    // [HttpPost]
    // public IActionResult Edit(int id, CreateNovelViewModel model)
    // {
    //     var userIdStr = HttpContext.Session.GetString("UserId");
    //     if (!int.TryParse(userIdStr, out int userId) || userId <= 0)
    //         return RedirectToAction("Login", "Account");

    //     var novel = _db.Novels.FirstOrDefault(n => n.NovelId == id && n.AuthorUserId == userId);
    //     if (novel == null) return NotFound();

    //     // อัปเดตข้อมูล
    //     novel.PenNameId = model.PenNameId;
    //     novel.Title = model.Title;
    //     novel.Synopsis = model.Synopsis;
    //     novel.AuthorNote = model.AuthorNote;
    //     novel.GenreId = model.GenreId;
    //     novel.SeriesType = model.SeriesType;

    //     // อัปเดตรูป
    //     if (Request.Form["removeCoverImage"] == "true")
    //         novel.CoverImageUrl = null;
    //     else if (model.CoverImage != null && model.CoverImage.Length > 0)
    //     {
    //         var fileName = Guid.NewGuid() + Path.GetExtension(model.CoverImage.FileName);
    //         var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
    //         using var stream = new FileStream(filePath, FileMode.Create);
    //         model.CoverImage.CopyTo(stream);
    //         novel.CoverImageUrl = "/uploads/" + fileName;
    //     }

    //     // 🔹 อัปเดต TriggerWarnings
    //     var oldTW = _db.NovelTriggerWarnings.Where(ntw => ntw.NovelId == id);
    //     _db.NovelTriggerWarnings.RemoveRange(oldTW);

    //     var ids = (Request.Form["triggerWarningIds"].ToString() ?? "")
    //               .Split(',', StringSplitOptions.RemoveEmptyEntries)
    //               .Select(int.Parse)
    //               .ToList();

    //     foreach (var twId in ids)
    //     {
    //         _db.NovelTriggerWarnings.Add(new NovelTriggerWarning
    //         {
    //             NovelId = id,
    //             WarningId = twId
    //         });
    //     }

    //     _db.SaveChanges();

    //     return RedirectToAction("Dashboard", "Writer", new { area = "Writer" });
    // }

    [HttpPost]
    public IActionResult Save(CreateNovelViewModel model)
    {
        var userIdStr = HttpContext.Session.GetString("UserId");
        if (!int.TryParse(userIdStr, out int userId) || userId <= 0)
            return RedirectToAction("Login", "Account");

        Novel novel;

        if (model.IsEdit)
        {
            novel = _db.Novels
                .Include(n => n.NovelTriggerWarnings)
                .FirstOrDefault(n => n.NovelId == model.NovelId && n.AuthorUserId == userId);

            if (novel == null) return NotFound();
        }
        else
        {
            novel = new Novel
            {
                AuthorUserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _db.Novels.Add(novel);
        }

        // ✅ map fields
        novel.PenNameId = model.PenNameId;

        // novel.NovelGenres ??= new List<NovelGenre>();

        // novel.NovelGenres.Clear();

        if (model.GenreId == 0)
        {
            ModelState.AddModelError("GenreId", "กรุณาเลือกหมวดหมู่");
        }

        // if (model.SelectedGenreIds != null)
        // {
        //     foreach (var id in model.SelectedGenreIds)
        //     {
        //         novel.NovelGenres.Add(new NovelGenre
        //         {
        //             GenreId = id,
        //             Novel = novel
        //         });
        //     }
        // }

        novel.Title = model.Title;
        novel.Synopsis = model.Synopsis;
        novel.AuthorNote = model.AuthorNote;
        novel.SeriesType = model.SeriesType;
        novel.PublishStatus = model.PublishStatus;
        novel.Status = model.Status;
        novel.PenNameId = model.PenNameId;
        novel.GenreId = model.GenreId; // ✅ สำคัญมาก

        if (!model.IsEdit)
        {
            novel.Status = NovelStatus.Ongoing;
        }

        // ✅ cover image
        if (model.CoverImage != null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(model.CoverImage.FileName);
            var path = Path.Combine("wwwroot/uploads", fileName);

            using var stream = new FileStream(path, FileMode.Create);
            model.CoverImage.CopyTo(stream);

            novel.CoverImageUrl = "/uploads/" + fileName;
        }

        // สร้าง Slug
        string GenerateSlug(string title)
        {
            return title.ToLower()
                        .Replace(" ", "-")
                        .Replace(",", "")
                        .Replace(".", "");
        }

        var baseSlug = GenerateSlug(model.Title);
        var slug = baseSlug;
        int count = 1;

        while (_db.Novels.Any(n => n.Slug == slug))
        {
            slug = $"{baseSlug}-{count}";
            count++;
        }

        novel.Slug = slug;


        // 👉 ค่อย add + save
        if (!model.IsEdit)
        {
            _db.Novels.Add(novel);
        }

        if (model.GenreId == 0)
        {
            ModelState.AddModelError("GenreId", "กรุณาเลือกหมวดหมู่");
        }

        var genreExists = _db.Genres.Any(g => g.GenreId == model.GenreId);

        if (!genreExists)
        {
            ModelState.AddModelError("GenreId", "หมวดหมู่ไม่ถูกต้อง");
        }

        if (!ModelState.IsValid)
        {
            // 👇 สำคัญ: ต้อง reload dropdown
            model.Genres = _db.Genres.ToList();
            model.PenNames = _db.Pennames.Where(p => p.UserId == userId).ToList();
            model.TriggerWarnings = _db.TriggerWarnings.OrderBy(t => t.Name).ToList();

            return View("Create", model);
        }

        _db.SaveChanges();

        // ✅ trigger warnings (FIX 100%)
        var old = _db.NovelTriggerWarnings.Where(x => x.NovelId == novel.NovelId);
        _db.NovelTriggerWarnings.RemoveRange(old);


        if (model.SelectedWarningIds != null && model.SelectedWarningIds.Any())
        {
            foreach (var id in model.SelectedWarningIds)
            {
                _db.NovelTriggerWarnings.Add(new NovelTriggerWarning
                {
                    NovelId = novel.NovelId,
                    WarningId = id
                });
            }
        }
    
        _db.SaveChanges();

        return RedirectToAction("Dashboard", "Writer", new { area = "Writer" });
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var userIdStr = HttpContext.Session.GetString("UserId");
        if (!int.TryParse(userIdStr, out int userId))
            return RedirectToAction("Login", "Account");

        var novel = _db.Novels
            .Include(n => n.NovelTriggerWarnings)
            .FirstOrDefault(n => n.NovelId == id && n.AuthorUserId == userId);

        if (novel == null)
            return NotFound();

        // ลบ relation ก่อน
        _db.NovelTriggerWarnings.RemoveRange(novel.NovelTriggerWarnings);

        // ลบ novel
        _db.Novels.Remove(novel);

        _db.SaveChanges();

        return RedirectToAction("Dashboard", "Writer", new { area = "Writer" });
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
