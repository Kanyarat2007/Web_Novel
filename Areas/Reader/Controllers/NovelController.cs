using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Novel.Models;
using Web_Novel.ViewModels.Reader;
using Web_Novel.Models.Enums;

namespace Web_Novel.Controllers;

[Area("Reader")]
public class NovelController : Controller
{
    private readonly WebnoveldbContext _db;
    public NovelController(WebnoveldbContext db)
    {
        _db = db;
    }

    public IActionResult NovelDetail(int id)
    {

        var novel = _db.Novels
            .Include(n => n.Chapters)
            .Include(n => n.PenName)
            .Include(n => n.NovelTriggerWarnings)
        .ThenInclude(ntw => ntw.TriggerWarning)
            .FirstOrDefault(n => n.NovelId == id);

        if (novel == null)
            return NotFound();

        // ดึง UserId จาก Session
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        // เช็คว่า User เพิ่มเข้าชั้นแล้วหรือยัง
        var isInBookshelf = userId > 0 && (from b in _db.Bookshelves
                                           where b.UserId == userId
                                             && b.NovelId == id
                                           select b).Any();

        // เช็คว่า User กดไลค์แล้วหรือยัง
        var isLiked = userId > 0 && (from l in _db.Novellikes
                                     where l.UserId == userId
                                       && l.NovelId == id
                                     select l).Any();

        // ดึงว่าอ่านถึงตอนไหนแล้ว
        var lastRead = userId > 0
            ? (from rh in _db.Readinghistories
                    .Include(rh => rh.LastChapter)
               where rh.UserId == userId
                 && rh.NovelId == id
               select rh.LastChapter).FirstOrDefault()
            : null;

        // ดึงตอนที่ User ซื้อแล้ว
        var purchasedChapterIds = userId > 0
            ? (from p in _db.Novelpurchases
               where p.UserId == userId
                 && p.NovelId == id
               select p.ChapterId).ToHashSet()
            : new HashSet<int?>();

        var blockedUserIds = _db.Blocklists
            .Where(x => x.BlockerUserId == userId)
            .Select(x => x.BlockedUserId);

        var comments = _db.Comments
            .Include(c => c.User)
            .Include(c => c.Chapter)
            .Where(c => c.NovelId == id)
            .Where(c => !blockedUserIds.Contains(c.UserId))
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new CommentViewModel
            {
                CommentId = c.CommentId,
                UserId = c.UserId,
                UserName = c.User != null ? c.User.DisplayName : "Unknown",
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                ChapterId = c.ChapterId,
                ChapterNumber = c.Chapter != null ? c.Chapter.ChapterNumber : null,
                IsDeleted = c.IsDeleted,
                IsSpoiler = c.IsSpoiler,
                Status = c.Status,
                DeleteReason = c.DeleteReason
            })
            .ToList();

        // Map ไปยัง ViewModel
        var vm = new NovelDetailViewModel
        {
            UserId = novel.PenName != null ? novel.PenName.UserId : 0,
            NovelId = novel.NovelId,
            PenNameId = novel.PenNameId,
            PenName = novel.PenName != null ? novel.PenName.PenName : "Unknown",
            Title = novel.Title,
            Synopsis = novel.Synopsis,
            CoverImageUrl = novel.CoverImageUrl,
            Status = novel.Status,
            SeriesType = novel.SeriesType,
            TotalViews = novel.TotalViews,
            TotalLikes = novel.TotalLikes,
            TotalBookshelf = novel.TotalBookshelf,
            NovelTriggerWarnings = novel.NovelTriggerWarnings,
            Comments = comments,
            CurrentUserId = userId,
            GenreNames = novel.GenreId > 0
        ? (from g in _db.Genres
           where g.GenreId == novel.GenreId
           select g.Name).ToList()
        : new List<string>(),

            AuthorNote = novel.AuthorNote,

            IsLoggedIn = userId > 0,
            IsInBookshelf = isInBookshelf,
            IsLiked = isLiked,
            LastReadChapterNumber = lastRead?.ChapterNumber,
            Chapters = (from c in novel.Chapters
                        orderby c.ChapterNumber
                        select new ChapterItemViewModel
                        {
                            ChapterId = c.ChapterId,
                            ChapterNumber = c.ChapterNumber,
                            Title = c.Title,
                            IsCoinRequired = c.IsCoinRequired,
                            CoinPrice = c.CoinPrice,
                            ViewCount = c.ViewCount,
                            IsPurchased = purchasedChapterIds.Contains(c.ChapterId)
                        }).ToList()
        };

        // เพิ่มยอดวิว
        string viewKey = $"viewed_novel_{id}";

        if (string.IsNullOrEmpty(HttpContext.Session.GetString(viewKey)))
        {
            novel.TotalViews += 1;
            _db.SaveChanges();

            HttpContext.Session.SetString(viewKey, "1");
        }

        return View(vm);
    }

    public IActionResult NovelChapter(int novelId, int chapterNumber, string sort = "new")
    {
        var chapter = _db.Chapters
    .FirstOrDefault(c =>
        c.NovelId == novelId &&
        c.ChapterNumber == chapterNumber &&
        c.PublishStatus.ToLower() == "published");

        if (novelId == null || chapterNumber == null)
            return BadRequest();

        var chapters = _db.Chapters
            .Where(c => c.NovelId == novelId &&
            c.PublishStatus.ToLower() == "published")
            .OrderBy(c => c.ChapterNumber)
            .Select(c => new ChapterItemViewModel
            {
                ChapterNumber = c.ChapterNumber,
                Title = c.Title
            })
            .ToList();

        var novel = (from n in _db.Novels
                     where n.NovelId == novelId
                     select n).FirstOrDefault();


        // var novel = _db.Novels
        //     .Include(n => n.Chapters)
        //     .Include(n => n.PenName)
        //     .Include(n => n.NovelTriggerWarnings)
        //     .FirstOrDefault(n => n.NovelId == id);

        if (novel == null)
            return NotFound();

        if (chapter == null)
            return NotFound();

        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);
        var isLoggedIn = userId > 0;

        // เช็คซื้อแล้วหรือยัง
        var isPurchased = isLoggedIn && (from p in _db.Novelpurchases
                                         where p.UserId == userId
                                           && p.ChapterId == chapter.ChapterId
                                         select p).Any();

        // ── กำหนดสิทธิ์ ──
        // Guest      → อ่านได้แค่ตอนแรก
        // Reader     → อ่านได้ทุกตอนที่ไม่ติดเหรียญ
        // ติดเหรียญ  → ต้องซื้อก่อน
        bool canRead = false;

        if (!isLoggedIn)
        {
            canRead = chapterNumber == 1; // Guest อ่านแค่ตอนแรก
        }
        else
        {
            canRead = !chapter.IsCoinRequired || isPurchased;
        }

        // ดึง prev/next
        var prevNumber = _db.Chapters
    .Where(c => c.NovelId == novelId &&
                c.PublishStatus == "published" &&
                c.ChapterNumber < chapterNumber)
    .OrderByDescending(c => c.ChapterNumber)
    .Select(c => c.ChapterNumber)
    .FirstOrDefault();

        var nextNumber = _db.Chapters
            .Where(c => c.NovelId == novelId &&
                        c.PublishStatus == "published" &&
                        c.ChapterNumber > chapterNumber)
            .OrderBy(c => c.ChapterNumber)
            .Select(c => c.ChapterNumber)
            .FirstOrDefault();

        // โน้ตส่วนตัว (เฉพาะ login)
        var note = isLoggedIn
            ? (from n in _db.Novelnotes
               where n.UserId == userId
                 && n.ChapterId == chapter.ChapterId
               select n.NoteContent).FirstOrDefault()
            : null;

        // อัปเดต ReadingHistory เฉพาะตอนอ่านได้และ login อยู่
        if (canRead && isLoggedIn)
        {
            var history = (from rh in _db.Readinghistories
                           where rh.UserId == userId
                             && rh.NovelId == novelId
                           select rh).FirstOrDefault();

            if (history == null)
            {
                _db.Readinghistories.Add(new Readinghistory
                {
                    UserId = userId,
                    ContentType = "novel",
                    NovelId = novelId,
                    LastChapterId = chapter.ChapterId,
                    LastReadAt = DateTime.UtcNow
                });
            }
            else
            {
                history.LastChapterId = chapter.ChapterId;
                history.LastReadAt = DateTime.UtcNow;
            }

            _db.SaveChanges();
        }

        var blockedUserIds = _db.Blocklists
        .Where(x => x.BlockerUserId == userId)
        .Select(x => x.BlockedUserId);

        var query = _db.Comments
        .Include(c => c.User)
        .Where(c => c.ChapterId == chapter.ChapterId)
        .Where(c => !blockedUserIds.Contains(c.UserId));

        Console.WriteLine("Comments found: " + query.Count());

        query = sort == "old"
            ? query.OrderBy(c => c.CreatedAt)
            : query.OrderByDescending(c => c.CreatedAt);

        var comments = query.Select(c => new CommentViewModel
        {
            CommentId = c.CommentId,
            UserId = c.UserId,
            UserName = c.User != null ? c.User.DisplayName : "Unknown",
            Content = c.Content,
            CreatedAt = c.CreatedAt,

            IsDeleted = c.IsDeleted,
            IsSpoiler = c.IsSpoiler,
            Status = c.Status,
            DeleteReason = c.DeleteReason
        }).ToList();

        var isBookmarked = false;

        if (isLoggedIn)
        {
            isBookmarked = _db.Chapterbookmarks.Any(b =>
                b.UserId == userId &&
                b.ChapterId == chapter.ChapterId);
        }

        var vm = new NovelChapterViewModel
        {
            NovelId = novelId,
            NovelTitle = novel.Title,
            ChapterId = chapter.ChapterId,
            ChapterNumber = chapter.ChapterNumber,
            ChapterTitle = chapter.Title,
            Content = canRead ? chapter.Content : "",
            IsCoinRequired = chapter.IsCoinRequired,
            CoinPrice = chapter.CoinPrice,
            CanRead = canRead,
            IsPurchased = isPurchased,
            IsLoggedIn = isLoggedIn,       // ← เพิ่มใหม่
            PrevChapterNumber = prevNumber == 0 ? null : prevNumber,
            NextChapterNumber = nextNumber == 0 ? null : nextNumber,
            UserNote = note,
            Chapters = chapters,
            Comments = comments,
            Sort = sort,
            CurrentUserId = userId,
            IsBookmarked = isBookmarked
        };

        ViewBag.Debug = $"CoinRequired: {chapter.IsCoinRequired}, Purchased: {isPurchased}, CanRead: {canRead}";

        return View(vm);
    }

    [HttpGet]
    public IActionResult NovelSearch(string q)
    {
        // ถ้าไม่มีคำค้นหา ส่งกลับ list ว่าง
        if (string.IsNullOrWhiteSpace(q))
            return View(new SearchViewModel { Query = q, Novels = new List<Novel>() });

        var novels = _db.Novels
    .Where(n => n.PublishStatus == "published"
        && (
            n.Title.Contains(q) ||
            (n.Synopsis != null && n.Synopsis.Contains(q)) ||
            n.AuthorUser.DisplayName.Contains(q)
        ))
    .OrderByDescending(n => n.TotalViews)
    .ToList();

        var vm = new SearchViewModel
        {
            Query = q,
            Novels = novels
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult ChapterBookmark(int chapterId)
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        if (userId <= 0)
            return Unauthorized();

        var bookmark = _db.Chapterbookmarks.FirstOrDefault(b =>
            b.UserId == userId &&
            b.ChapterId == chapterId);

        var bookmarks = _db.Chapterbookmarks
            .Include(b => b.Chapter)
                .ThenInclude(c => c.Novel)
            .Where(b => b.UserId == userId)
            .ToList();

        if (bookmark == null)
        {
            _db.Chapterbookmarks.Add(new Chapterbookmark
            {
                UserId = userId,
                ChapterId = chapterId
            });

            _db.SaveChanges();
            return Json(new { success = true, bookmarked = true });
        }
        else
        {
            _db.Chapterbookmarks.Remove(bookmark);
            _db.SaveChanges();
            return Json(new { success = true, bookmarked = false });
        }
    }

    [HttpPost]
    public IActionResult SaveNote(int chapterId, string content)
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        if (userId <= 0)
            return Unauthorized();

        var note = _db.Novelnotes
            .FirstOrDefault(n => n.UserId == userId
                              && n.ChapterId == chapterId);

        if (note == null)
        {
            note = new Novelnote
            {
                UserId = userId,
                ChapterId = chapterId,
                NoteContent = content
            };
            _db.Novelnotes.Add(note);
        }
        else
        {
            note.NoteContent = content;
            _db.Novelnotes.Update(note);
        }

        _db.SaveChanges();

        return Json(new { success = true });
    }

    [HttpGet]
    public IActionResult ByGenre(int genreId, int page = 1)
    {
        int pageSize = 12;

        var genre = _db.Genres
            .FirstOrDefault(g => g.GenreId == genreId);

        if (genre == null)
            return NotFound();

        var query = _db.Novels
            .Where(n => n.GenreId == genreId
                     && n.PublishStatus == "published");

        var totalCount = query.Count();

        var novels = query
            .OrderByDescending(n => n.TotalViews)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var vm = new GenrePageViewModel
        {
            GenreId = genreId,
            GenreName = genre.Name,
            Novels = novels,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleLike(int novelId)
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

        if (userId <= 0)
            return Unauthorized();

        var novel = _db.Novels.Find(novelId);
        if (novel == null)
            return NotFound();

        var like = _db.Novellikes
            .FirstOrDefault(l => l.UserId == userId && l.NovelId == novelId);

        if (like == null)
        {
            _db.Novellikes.Add(new Novellike
            {
                UserId = userId,
                NovelId = novelId
            });
        }
        else
        {
            _db.Novellikes.Remove(like);
        }

        _db.SaveChanges();

        // ✅ กันค่าผิด
        novel.TotalLikes = _db.Novellikes.Count(l => l.NovelId == novelId);
        _db.SaveChanges();

        return RedirectToAction("NovelDetail", new { id = novelId });
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
