using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels;

namespace Web_Novel.Areas.Writer.Controllers

{
    [Area("Writer")]
    public class WriterController : Controller
    {
        private readonly WebnoveldbContext _db;
        public WriterController(WebnoveldbContext db)
        {
            _db = db;
        }
        public IActionResult Dashboard(string tab = "novels")
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var vm = new WriterDashboardViewModel
            {
                ActiveTab = tab
            };

            switch (tab)
            {
                case "novels":
                default:
                    vm.Novels = _db.Novels
                        .Where(n => n.AuthorUserId == userId)
                        .Select(n => new MyNovelItemViewModel
                        {
                            NovelId = n.NovelId,
                            Title = n.Title,
                            CoverImageUrl = n.CoverImageUrl,
                            PenName = n.PenName != null ? n.PenName.PenName : "-",

                            TotalChapters = n.Chapters.Count(),
                            TotalViews = n.TotalViews,
                            TotalLikes = n.TotalLikes,
                            TotalBookshelf = n.TotalBookshelf,

                            IsCoinRequired = n.IsCoinRequired,
                            CoinPrice = n.CoinPrice,

                            PublishStatus = n.PublishStatus,

                            CreatedAt = n.CreatedAt,
                            UpdatedAt = n.UpdatedAt
                        })
                        .ToList();
                    break;

                case "pennames":
                    vm.PenNames = _db.Pennames
                        .Where(p => p.UserId == userId)
                        .Select(p => new PenNameViewModel
                        {
                            PenNameId = p.PenNameId,
                            PenName = p.PenName,
                            Bio = p.Bio,
                            AvatarUrl = p.AvatarUrl,
                            IsDefault = p.IsDefault
                        })
                        .ToList();
                    break;

                case "payments":
                    vm.Payments = _db.Writerprofiles
                        .Where(w => w.UserId == userId)
                        .Select(w => new WriterPaymentViewModel
                        {
                            UserId = w.UserId,
                            // PaymentId = w.PaymentId,
                            BankName = w.BankName,
                            BankAccountName = w.BankAccountName,
                            BankAccountNumber = w.BankAccountNo,
                            IsPaymentConfirmed = w.IsVerified
                        })
                        .ToList();
                    break;
            }

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
}