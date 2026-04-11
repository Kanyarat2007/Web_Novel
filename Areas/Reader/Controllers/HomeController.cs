using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Novel.Models;
using Web_Novel.ViewModels.Reader;
using Web_Novel.Helpers;

namespace Web_Novel.Areas.Reader.Controllers
{
    [Area("Reader")]
    public class HomeController : Controller
    {
        private readonly WebnoveldbContext _db;

        public HomeController(WebnoveldbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            int.TryParse(userIdStr, out int userId);

            var wallet = _db.UserCoins.FirstOrDefault(w => w.UserId == userId);

            var vm = new HomeViewModel
            {
                UserId = userId,
                // Username = HttpContext.Session.GetString("Username"),
                CurrentBalance = wallet?.Balance ?? 0
            };


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
                var interestedGenreIds = (from u in _db.Users
                                              .Include(u => u.Genres)
                                          where u.UserId == userId
                                          from g in u.Genres
                                          select g.GenreId).ToList();

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

        // ── บันทึก Genre ที่สนใจ ──
        [HttpPost]
        public IActionResult SaveGenrePreferences(List<int> genreIds)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var user = (from u in _db.Users
                            .Include(u => u.Genres)
                        where u.UserId == userId
                        select u).FirstOrDefault();

            if (user == null)
                return RedirectToAction("Index");

            user.Genres.Clear();

            var selected = (from g in _db.Genres
                            where genreIds.Contains(g.GenreId)
                            select g).ToList();

            foreach (var g in selected)
                user.Genres.Add(g);

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