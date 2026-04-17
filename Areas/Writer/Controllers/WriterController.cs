using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Writer;
using Microsoft.EntityFrameworkCore;

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

                    // 💳 ช่องทางการชำระเงิน
                    vm.Payments = _db.Writerprofiles
                        .Where(w => w.UserId == userId)
                        .Select(w => new WriterPaymentViewModel
                        {
                            UserId = w.UserId,
                            PaymentId = w.WriterProfileId,
                            BankName = w.BankName,
                            BankAccountName = w.BankAccountName,
                            BankAccountNumber = w.BankAccountNo,
                            IsPaymentConfirmed = w.IsVerified,
                        })
                        .ToList();

                    // 💰 รายได้ของนักเขียน (จากนิยายของตัวเอง)
                    vm.Earnings = _db.Novelpurchases
                        .Where(x => x.Novel.AuthorUserId == userId)
                        .OrderByDescending(x => x.PurchasedAt)
                        .Select(x => new WriterEarningItemViewModel
                        {
                            TransactionId = x.PurchaseId,
                            Amount = x.CoinSpent,
                            Type = "spend",
                            CreatedAt = x.PurchasedAt,

                            NovelId = x.NovelId,
                            NovelTitle = x.Novel != null ? x.Novel.Title : "-",

                            BalanceAfter = 0 // ยังไม่ทำก็ได้
                        })
                        .ToList();

                    // 📤 ประวัติการถอนเงิน
                    vm.Payouts = _db.WriterPayouts
                        .Where(x => x.UserId == userId)
                        .OrderByDescending(x => x.RequestedAt)
                        .Select(x => new WriterPayoutViewModel
                        {
                            PayoutId = x.PayoutId,
                            Amount = x.Amount,
                            Status = x.Status,
                            RequestedAt = x.RequestedAt,
                            PaidAt = x.PaidAt
                        })
                        .ToList();

                    // 💵 คำนวณรายได้รวม
                    var totalEarn = _db.Novelpurchases
                        .Where(x => x.Novel.AuthorUserId == userId)
                        .Sum(x => (decimal?)x.CoinSpent) ?? 0;

                    // 💸 เงินที่จ่ายไปแล้ว
                    var totalPaid = _db.WriterPayouts
                        .Where(x => x.UserId == userId && x.Status == "Paid")
                        .Sum(x => (decimal?)x.Amount) ?? 0;

                    // 💰 ยอดคงเหลือ
                    vm.Balance = totalEarn - totalPaid;

                    // 📊 summary รายได้
                    vm.EarningSummary = new WriterEarningSummaryViewModel
                    {
                        UserId = userId,

                        TotalEarning = totalEarn,

                        MonthlyEarning = _db.Novelpurchases
                            .Where(x => x.Novel.AuthorUserId == userId &&
                                        x.PurchasedAt.Month == DateTime.Now.Month &&
                                        x.PurchasedAt.Year == DateTime.Now.Year)
                            .Sum(x => (decimal?)x.CoinSpent) ?? 0,

                        TodayEarning = _db.Novelpurchases
                            .Where(x => x.Novel.AuthorUserId == userId &&
                                        x.PurchasedAt.Date == DateTime.Now.Date)
                            .Sum(x => (decimal?)x.CoinSpent) ?? 0,

                        TotalTransactions = _db.Novelpurchases
                            .Count(x => x.Novel.AuthorUserId == userId)
                    };

                    break;

                case "stats":

                    vm.Stats = _db.Novels
                        .Where(x => x.AuthorUserId == userId)
                        .Select(x => new WriterStatsViewModel
                        {
                            NovelId = x.NovelId,
                            NovelTitle = x.Title,

                            ContentType = "Novel",

                            TotalViews = x.TotalViews,
                            TotalLikes = x.TotalLikes,
                            TotalBookshelf = x.TotalBookshelf,

                            TotalCoinEarned = 0, // ยังไม่มีระบบนี้
                            TotalChaptersSold = 0,

                            UpdatedAt = x.UpdatedAt
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