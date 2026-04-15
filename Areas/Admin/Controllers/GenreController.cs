using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Admin;

namespace Web_Novel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly WebnoveldbContext _db;

        public GenreController(WebnoveldbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var vm = new GenrePageViewModel
            {
                Genres = _db.Genres
                    .Select(g => new GenreViewModel
                    {
                        GenreId = g.GenreId,
                        Name = g.Name,
                        Slug = g.Slug
                    }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(GenrePageViewModel vm)
        {
            var genre = new Genre
            {
                Name = vm.Form.Name,
                Slug = vm.Form.Slug
            };

            _db.Genres.Add(genre);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(GenrePageViewModel vm)
        {
            var genre = _db.Genres.Find(vm.Form.GenreId);
            if (genre == null) return NotFound();

            genre.Name = vm.Form.Name;
            genre.Slug = vm.Form.Slug;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var genre = _db.Genres.Find(id);
            if (genre == null) return NotFound();

            _db.Genres.Remove(genre);
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
