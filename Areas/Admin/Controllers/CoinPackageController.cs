using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Admin;

namespace Web_Novel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoinPackageController : Controller
    {
        private readonly WebnoveldbContext _db;

        public CoinPackageController(WebnoveldbContext db)
        {
            _db = db;
        }

        // GET: Index
        public IActionResult Index()
        {
            var data = _db.Coinpackages
                .Select(x => new CoinPackageViewModel
                {
                    PackageId = x.PackageId,
                    Name = x.Name,
                    Price = x.Price,
                    CoinAmount = x.CoinAmount,
                    BonusCoin = x.BonusCoin,
                    IsActive = x.IsActive
                }).ToList();

            return View(data);
        }

        // POST: Create
        [HttpPost]
        public IActionResult Create(CoinPackageViewModel vm)
        {
            if (!ModelState.IsValid) return View("Index");

            var model = new Coinpackage
            {
                Name = vm.Name,
                Price = vm.Price,
                CoinAmount = vm.CoinAmount,
                BonusCoin = vm.BonusCoin,
                IsActive = vm.IsActive,
                CreatedAt = DateTime.Now
            };

            _db.Coinpackages.Add(model);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(CoinPackageViewModel vm)
        {
            if (!ModelState.IsValid) return View("Index");

            var model = _db.Coinpackages.Find(vm.PackageId);
            if (model == null) return NotFound();

            model.Name = vm.Name;
            model.Price = vm.Price;
            model.CoinAmount = vm.CoinAmount;
            model.BonusCoin = vm.BonusCoin;
            model.IsActive = vm.IsActive;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _db.Coinpackages.Find(id);
            if (data == null) return NotFound();

            data.IsActive = false;
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
