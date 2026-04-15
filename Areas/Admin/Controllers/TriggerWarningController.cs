using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Admin;

namespace Web_Novel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TriggerWarningController : Controller
    {
        private readonly WebnoveldbContext _db;

        public TriggerWarningController(WebnoveldbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string sortBy)
        {
            var query = _db.TriggerWarnings.AsQueryable();

            // 🎯 เงื่อนไขการเรียง
            switch (sortBy)
            {
                case "name_asc":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "name_desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "newest":
                    query = query.OrderByDescending(x => x.CreatedAt);
                    break;

                case "oldest":
                    query = query.OrderBy(x => x.CreatedAt);
                    break;

                default:
                    query = query.OrderByDescending(x => x.WarningId);
                    break;
            }

            var vm = new TriggerWarningPageViewModel
            {
                SortBy = sortBy,
                Warnings = query.Select(w => new TriggerWarningViewModel
                {
                    WarningId = w.WarningId,
                    Name = w.Name,
                    Description = w.Description,
                    CreatedAt = w.CreatedAt
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(TriggerWarningPageViewModel vm)
        {
            var warning = new TriggerWarning
            {
                Name = vm.Form.Name,
                Description = vm.Form.Description
            };

            _db.TriggerWarnings.Add(warning);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(TriggerWarningPageViewModel vm)
        {
            var warning = _db.TriggerWarnings.Find(vm.Form.WarningId);
            if (warning == null) return NotFound();

            warning.Name = vm.Form.Name;
            warning.Description = vm.Form.Description;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var warning = _db.TriggerWarnings.Find(id);
            if (warning == null) return NotFound();

            _db.TriggerWarnings.Remove(warning);
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