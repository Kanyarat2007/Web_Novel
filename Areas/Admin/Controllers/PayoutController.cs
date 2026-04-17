using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Novel.Models;
using Web_Novel.ViewModels.Admin;

namespace Web_Novel.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class PayoutController : Controller
    {
        private readonly WebnoveldbContext _db;
        public PayoutController(WebnoveldbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var vm = _db.WriterPayouts
                .OrderByDescending(x => x.RequestedAt)
                .Select(x => new PayoutViewModel
                {
                    PayoutId = x.PayoutId,
                    UserId = x.UserId,
                    Username = x.User.Username,

                    Amount = x.Amount,
                    Status = x.Status,

                    RequestedAt = x.RequestedAt,
                    ApprovedAt = x.ApprovedAt,
                    PaidAt = x.PaidAt
                })
                .ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var payout = _db.WriterPayouts.FirstOrDefault(x => x.PayoutId == id);

            if (payout == null) return NotFound();

            payout.Status = "Approved";
            payout.ApprovedAt = DateTime.Now;
            // payout.ApprovedBy = GetAdminId();

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            var payout = _db.WriterPayouts.FirstOrDefault(x => x.PayoutId == id);

            if (payout == null) return NotFound();

            payout.Status = "Rejected";

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkAsPaid(int id)
        {
            var payout = _db.WriterPayouts.FirstOrDefault(x => x.PayoutId == id);

            if (payout == null) return NotFound();

            payout.Status = "Paid";
            payout.PaidAt = DateTime.Now;

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
