using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Admin;

namespace Web_Novel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly WebnoveldbContext _db;

        public BannerController(WebnoveldbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var banners = _db.Banners
                .Where(b => !b.IsDeleted)
                .OrderBy(b => b.DisplayOrder)
                .Select(b => new BannerPageViewModel
                {
                    BannerId = b.BannerId,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    RedirectUrl = b.RedirectUrl,
                    Position = b.Position,
                    DisplayOrder = b.DisplayOrder,
                    IsActive = b.IsActive
                })
                .ToList();

            var vm = new BannerPageViewModel
            {
                Banners = banners
            };

            return View(vm);
        }

        // Edit (load data เข้า modal)
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var banner = _db.Banners
                .Where(b => b.BannerId == id)
                .Select(b => new BannerPageViewModel
                {
                    BannerId = b.BannerId,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    RedirectUrl = b.RedirectUrl,
                    Position = b.Position,
                    DisplayOrder = b.DisplayOrder,
                    IsActive = b.IsActive,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate
                })
                .FirstOrDefault();

            return Json(banner);
        }

        [HttpPost]
        public IActionResult Save(BannerPageViewModel model)
        {
            string imagePath = null;

            // 🔥 upload image (ถ้ามี)
            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/banners");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }

                imagePath = "/images/banners/" + fileName;
            }

            if (model.BannerId == 0)
            {
                // CREATE
                var banner = new Banner
                {
                    Title = model.Title,
                    ImageUrl = imagePath,
                    RedirectUrl = model.RedirectUrl,
                    DisplayOrder = model.DisplayOrder,
                    IsActive = model.IsActive,
                    CreatedAt = DateTime.Now
                };

                _db.Banners.Add(banner);
            }
            else
            {
                // UPDATE
                var banner = _db.Banners.Find(model.BannerId);
                if (banner == null) return NotFound();

                banner.Title = model.Title;
                banner.RedirectUrl = model.RedirectUrl;
                banner.DisplayOrder = model.DisplayOrder;
                banner.IsActive = model.IsActive;
                banner.UpdatedAt = DateTime.Now;

                if (imagePath != null)
                    banner.ImageUrl = imagePath;
            }

            _db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var banner = _db.Banners.Find(id);
            if (banner == null)
                return Json(new { success = false });

            banner.IsDeleted = true;

            _db.SaveChanges();

            return Json(new { success = true });
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
