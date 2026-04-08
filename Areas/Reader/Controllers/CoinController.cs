using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;
using Web_Novel.Helpers;

namespace Web_Novel.Controllers;

[Area("Reader")]
[SessionAuthorize]
public class CoinController : Controller
{
    private readonly WebnoveldbContext _db;
    public CoinController(WebnoveldbContext db)
    {
        _db = db;
    }

    // ── หน้าเติมเหรียญ ──
    public IActionResult TopUp()
    {
        var username = HttpContext.Session.GetString("Username");

        var user = _db.Users.FirstOrDefault(u => u.Username == username);

        var UserCoins = _db.UserCoins.FirstOrDefault(x => x.UserId == user.UserId);

        var vm = new TopUpViewModel
        {
            Username = username,
            CurrentBalance = UserCoins?.Balance ?? 0,
            Packages = _db.Coinpackages
                            .Where(p => p.IsActive)
                            .ToList()
        };

        return View(vm);
    }

    // ── ยืนยันซื้อแพ็ค (Mock) ──
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Purchase(int packageId)
    {
        var username = HttpContext.Session.GetString("Username");

        if (string.IsNullOrEmpty(username))
            return RedirectToAction("Login", "Account");

        var user = _db.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) return NotFound();

        var package = _db.Coinpackages
            .FirstOrDefault(p => p.PackageId == packageId && p.IsActive);

        if (package == null)
        {
            TempData["Error"] = "ไม่พบแพ็คเกจ";
            return RedirectToAction("TopUp");
        }

        // เริ่ม transaction กันข้อมูลพัง
        using var dbTransaction = _db.Database.BeginTransaction();

        try
        {
            // หา wallet
            var UserCoins = _db.UserCoins
                .FirstOrDefault(x => x.UserId == user.UserId);

            if (UserCoins == null)
            {
                UserCoins = new UserCoin
                {
                    UserId = user.UserId,
                    Balance = 0
                };
                _db.UserCoins.Add(UserCoins);
                _db.SaveChanges(); // ต้อง save ก่อนเพื่อให้มี row
            }

            int totalCoin = package.CoinAmount + package.BonusCoin;

            int before = UserCoins.Balance;

            // เพิ่มเหรียญ
            UserCoins.Balance += totalCoin;

            // บันทึก transaction
            _db.Cointransactions.Add(new Cointransaction
            {
                UserId = user.UserId,
                PackageId = package.PackageId,
                Type = "topup",
                Amount = totalCoin,
                BalanceBefore = before,
                BalanceAfter = UserCoins.Balance,
                Note = $"ซื้อแพ็ค {package.Name}"
            });

            _db.SaveChanges();
            dbTransaction.Commit();

            TempData["Success"] = $"เติมเหรียญสำเร็จ +{totalCoin} เหรียญ 🎉";

            return RedirectToAction("TopUp");
        }
        catch (Exception)
        {
            dbTransaction.Rollback();

            TempData["Error"] = "เกิดข้อผิดพลาด กรุณาลองใหม่";
            return RedirectToAction("TopUp");
        }
    }

    // ── ซื้อตอนนิยาย ──
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BuyChapter(int chapterId, int novelId)
    {
        // ดึง user
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);
        if (userId == 0)
        {
            TempData["Error"] = "กรุณาเข้าสู่ระบบก่อนซื้อ";
            return RedirectToAction("NovelChapter", new { novelId, chapterNumber = 1 });
        }

        var user = _db.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null) return NotFound();

        // ดึง chapter
        var chapter = _db.Chapters.FirstOrDefault(c => c.ChapterId == chapterId && c.PublishStatus == "published");
        if (chapter == null) return NotFound();

        // เช็คว่าต้องใช้เหรียญหรือไม่
        if (!chapter.IsCoinRequired || chapter.CoinPrice <= 0)
        {
            TempData["Error"] = "ตอนนี้ไม่ติดเหรียญ";
            return RedirectToAction(actionName: "NovelChapter",
    controllerName: "Novel",
    routeValues: new { area = "Reader", novelId = novelId, chapterNumber = chapter.ChapterNumber }
);
        }

        // เช็คว่าซื้อแล้วหรือยัง
        bool isPurchased = _db.Novelpurchases.Any(p => p.UserId == userId && p.ChapterId == chapterId);
        if (isPurchased)
        {
            TempData["Info"] = "คุณซื้อบทนี้แล้ว";
            return RedirectToAction(actionName: "NovelChapter",
    controllerName: "Novel",
    routeValues: new { area = "Reader", novelId = novelId, chapterNumber = chapter.ChapterNumber }
);
        }

        // ดึง UserCoins
        var UserCoins = _db.UserCoins.FirstOrDefault(w => w.UserId == userId);

        if (UserCoins == null)
        {
            TempData["Error"] = "ไม่พบเหรียญของคุณ";
            return RedirectToAction(actionName: "TopUp",
    controllerName: "Coin",
    routeValues: new { area = "Reader", novelId = novelId, chapterNumber = chapter.ChapterNumber }
    );
        }

        if (UserCoins.Balance < chapter.CoinPrice)
        {
            TempData["Error"] = $"เหรียญไม่พอ (มี {UserCoins.Balance} ต้องใช้ {chapter.CoinPrice})";
            return RedirectToAction(actionName: "TopUp",
    controllerName: "Coin",
    routeValues: new { area = "Reader", novelId = novelId, chapterNumber = chapter.ChapterNumber }
    );
        }

        // ── ใช้ transaction กัน race condition ──
        using var dbTransaction = _db.Database.BeginTransaction();
        try
        {
            int before = UserCoins.Balance;

            // หักเหรียญ
            UserCoins.Balance -= chapter.CoinPrice;

            // บันทึก CoinTransaction
            _db.Cointransactions.Add(new Cointransaction
            {
                UserId = userId,
                PackageId = null,
                Type = "spend",
                Amount = chapter.CoinPrice,
                BalanceBefore = before,
                BalanceAfter = UserCoins.Balance,
                Note = $"ซื้อบท {chapter.Title}"
            });

            // บันทึก NovelPurchase
            _db.Novelpurchases.Add(new Novelpurchase
            {
                UserId = userId,
                NovelId = novelId,
                ChapterId = chapterId,
                ContentType = "novel",
                CoinSpent = chapter.CoinPrice,
                PurchasedAt = DateTime.UtcNow
            });

            _db.SaveChanges();
            dbTransaction.Commit();

            TempData["Success"] = $"ซื้อบท {chapter.Title} สำเร็จ 🎉";

            return RedirectToAction(
    actionName: "NovelChapter",
    controllerName: "Novel",
    routeValues: new { area = "Reader", novelId = novelId, chapterNumber = chapter.ChapterNumber }
);
        }
        catch (Exception)
        {
            dbTransaction.Rollback();
            TempData["Error"] = "เกิดข้อผิดพลาด กรุณาลองใหม่";
            return RedirectToAction(
    actionName: "NovelChapter",
    controllerName: "Novel",
    routeValues: new { area = "Reader", novelId = novelId, chapterNumber = chapter.ChapterNumber }
);
        }
    }

    // ── ประวัติการซื้อ ──
    public IActionResult History()
    {
        int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);
        if (userId == 0)
        {
            TempData["Error"] = "กรุณาเข้าสู่ระบบก่อนดูประวัติการซื้อ";
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        var purchases = (from p in _db.Novelpurchases
                         join n in _db.Novels on p.NovelId equals n.NovelId
                         join c in _db.Chapters on p.ChapterId equals c.ChapterId
                         where p.UserId == userId
                         orderby n.Title, p.PurchasedAt descending
                         select new { Novel = n, Chapter = c, Purchase = p })
                        .ToList();

        var grouped = purchases
            .GroupBy(x => x.Novel.NovelId)
            .Select(g => new PurchasedNovelGroupViewModel
            {
                NovelId = g.Key,
                NovelTitle = g.First().Novel.Title,
                CoverImageUrl = g.First().Novel.CoverImageUrl,
                Chapters = g.Select(x => new PurchasedChapterViewModel
                {
                    ChapterId = x.Chapter.ChapterId,
                    ChapterNumber = x.Chapter.ChapterNumber,
                    ChapterTitle = x.Chapter.Title,
                    CoinSpent = x.Purchase.CoinSpent,
                    PurchasedAt = x.Purchase.PurchasedAt
                }).OrderBy(c => c.ChapterNumber).ToList()
            })
            .ToList();

        var vm = new PurchaseHistoryViewModel
        {
            NovelGroups = grouped
        };

        return View(vm);
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
