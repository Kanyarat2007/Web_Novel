using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Novel.Models;
using Web_Novel.Services;
using Web_Novel.ViewModels.Reader;
using Web_Novel.Helpers;

namespace Web_Novel.Areas.Reader.Controllers
{
    [Area("Reader")]
    public class HomeController : Controller
    {
        private readonly WebnoveldbContext _db;
        private readonly RewardService _rewardService;

        public HomeController(WebnoveldbContext db, RewardService rewardService)
        {
            _db = db;
            _rewardService = rewardService;
        }
        public IActionResult Index()
        {
            _db.ChangeTracker.Clear();

            var userIdStr = HttpContext.Session.GetString("UserId");
            int.TryParse(userIdStr, out int userId);

            Console.WriteLine("📄 INDEX LOAD USER: " + userId);

            // if (!int.TryParse(userIdStr, out int userId))
            //     userId = 0;

            var wallet = _db.UserCoins
    .AsNoTracking()
    .FirstOrDefault(x => x.UserId == userId);

            var rewardCoins = _db.UserRewards
                .Where(x => x.UserId == userId)
                .Sum(x => (int?)x.CoinAmount) ?? 0;

            var vm = new HomeViewModel
            {
                UserId = userId,
                CurrentBalance = (wallet?.Balance ?? 0) + rewardCoins
            };

            var now = DateTime.Now;

            vm.Banners = _db.Banners
                .Where(b =>
                    b.IsActive &&
                    !b.IsDeleted &&
                    (b.StartDate == null || b.StartDate <= now) &&
                    (b.EndDate == null || b.EndDate >= now)
                )
                .OrderBy(b => b.DisplayOrder)
                .Select(b => new BannerViewModel
                {
                    BannerId = b.BannerId,
                    Title = b.Title,
                    ImageUrl = string.IsNullOrEmpty(b.ImageUrl)
                        ? "/images/default-banner.jpg"
                        : b.ImageUrl,
                    RedirectUrl = string.IsNullOrEmpty(b.RedirectUrl)
                        ? "#"
                        : b.RedirectUrl,
                    DisplayOrder = b.DisplayOrder
                })
                .ToList();

            var today = DateTime.Now.Date;

            var alreadyClaimed = _db.UserRewards.Any(x =>
                x.UserId == userId &&
                x.RewardType == "daily" &&
                x.CreatedAt.Date == today
            );

            vm.CanClaimDaily = !alreadyClaimed;

            var streak = _db.Loginstreaks
    .FirstOrDefault(x => x.UserId == userId);

            vm.CurrentStreak = streak?.CurrentStreak ?? 0;

            vm.TodayReward = _rewardService.CalculateStreakReward(vm.CurrentStreak);

            // var success = _rewardService.ClaimDailyReward(userId);

            // if (!success)
            // {
            //     TempData["Error"] = "วันนี้รับไปแล้ว";
            // }


            if (userId > 0)
            {
                // ── ประวัติการอ่านล่าสุด ──
                vm.RecentNovels = (from rh in _db.Readinghistories
                                       .Include(rh => rh.Novel)
                                       .Include(rh => rh.LastChapter)
                                   where rh.UserId == userId
                                     && rh.ContentType == "novel"
                                     && rh.Novel != null
                                   orderby rh.LastReadAt descending
                                   //    select rh).Take(5).ToList();
                                   select rh).ToList();

                // ── หมวดหมู่ที่ผู้ใช้สนใจ ──
                var interestedGenreIds = _db.UserGenres
                    .Where(x => x.UserId == userId)
                    .Select(x => x.GenreId)
                    .ToList();

                if (interestedGenreIds.Any())
                {
                    vm.InterestedGenres = (from g in _db.Genres
                                           where interestedGenreIds.Contains(g.GenreId)
                                           select new GenreWithNovels
                                           {
                                               GenreId = g.GenreId,
                                               GenreName = g.Name,
                                               // ── เปลี่ยนจาก n.Genres.Any() → n.GenreId == g.GenreId ──
                                               Novels = (from n in _db.Novels
                                                         where n.GenreId == g.GenreId
                                                           && n.PublishStatus == "published"
                                                         orderby n.TotalViews descending
                                                         select n).Take(6).ToList()
                                           }).ToList();
                }

                // ── หมวดหมู่ที่เหลือ ──
                vm.AllGenres = (from g in _db.Genres
                                where !interestedGenreIds.Contains(g.GenreId)
                                select new GenreWithNovels
                                {
                                    GenreId = g.GenreId,
                                    GenreName = g.Name,
                                    Novels = (from n in _db.Novels
                                              where n.GenreId == g.GenreId
                                                && n.PublishStatus == "published"
                                              orderby n.TotalViews descending
                                              select n).Take(6).ToList()
                                }).ToList();
            }
            else
            {
                // ── Guest เห็นทุกหมวด ──
                vm.AllGenres = (from g in _db.Genres
                                select new GenreWithNovels
                                {
                                    GenreId = g.GenreId,
                                    GenreName = g.Name,
                                    Novels = (from n in _db.Novels
                                              where n.GenreId == g.GenreId
                                                && n.PublishStatus == "published"
                                              orderby n.TotalViews descending
                                              select n).Take(6).ToList()
                                }).ToList();
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClaimDaily()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (!int.TryParse(userIdStr, out int userId) || userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var success = _rewardService.ClaimDailyReward(userId);

            if (success)
            {
                TempData["Success"] = "ได้รับฟรี 2 เหรียญ";
            }
            else
            {
                TempData["Error"] = "วันนี้คุณรับเหรียญไปแล้ว";
            }

            // _rewardService.ClaimDailyReward(userId);

            // 🔥 บังคับ reload context
            _db.ChangeTracker.Clear();

            return RedirectToAction("Index");
        }

        // ── บันทึก Genre ที่สนใจ ──
        [HttpPost]
        public IActionResult SaveGenrePreferences(List<int> genreIds)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var user = _db.Users
                .Include(u => u.UserGenres)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                return RedirectToAction("Index");

            // ลบของเก่า
            user.UserGenres.Clear();

            // เพิ่มของใหม่ (ต้องเป็น UserGenre ไม่ใช่ Genre)
            foreach (var genreId in genreIds)
            {
                user.UserGenres.Add(new UserGenre
                {
                    UserId = userId,
                    GenreId = genreId
                });
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}