using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Admin;

namespace Web_Novel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromotionController : Controller
    {
        private readonly WebnoveldbContext _db;

        public PromotionController(WebnoveldbContext db)
        {
            _db = db;
        }

        // 📌 LIST
        public IActionResult Index()
        {
            var data = _db.Promotions
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return View(data);
        }

        // 📌 CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Promotion model)
        {
            model.CreatedAt = DateTime.Now;
            _db.Promotions.Add(model);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            var data = _db.Promotions.Find(id);
            if (data == null) return NotFound();

            var vm = new PromotionViewModel
            {
                PromotionId = data.PromotionId,
                Name = data.Name,
                DiscountValue = data.DiscountValue,
                BonusCoin = data.BonusCoin,
                MaxUsageTotal = data.MaxUsageTotal,
                MaxUsagePerUser = data.MaxUsagePerUser,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                IsActive = data.IsActive ?? false
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(Promotion model)
        {
            var data = _db.Promotions.Find(model.PromotionId);
            if (data == null) return NotFound();

            data.Name = model.Name;
            data.DiscountValue = model.DiscountValue;
            data.BonusCoin = model.BonusCoin;
            data.StartDate = model.StartDate;
            data.EndDate = model.EndDate;
            data.IsActive = model.IsActive;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var data = _db.Promotions.Find(id);
            if (data == null) return NotFound();

            _db.Promotions.Remove(data);
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
