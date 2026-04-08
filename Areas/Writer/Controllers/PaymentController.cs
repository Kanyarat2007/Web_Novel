using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web_Novel.Models;
using Web_Novel.ViewModels;

namespace Web_Novel.Controllers;

[Area("Writer")]
public class PaymentController : Controller
{
    private readonly WebnoveldbContext _db;
    public PaymentController(WebnoveldbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Create(WriterPaymentViewModel model)
    {
        var userIdStr = HttpContext.Session.GetString("UserId");
        if (!int.TryParse(userIdStr, out int userId))
            return RedirectToAction("Login", "Account", new { area = "" });


        if (userId <= 0)
        {
            return Unauthorized();
        }

        // 🔥 mock payment check
        // if (!model.IsPaymentConfirmed)
        // {
        //     ModelState.AddModelError("", "กรุณายืนยันการชำระเงิน");
        //     return RedirectToAction("Dashboard", "Writer", new { area = "Writer", tab = "payments" });
        // }

        var existing = _db.Writerprofiles
        .FirstOrDefault(w => w.UserId == userId);

        if (existing != null)
        {
            return RedirectToAction("Dashboard", "Writer", new { area = "Writer", tab = "payments" });
        }

        // save writer profile
        var writer = new Writerprofile
        {
            UserId = userId,
            FirstName = "-",
            LastName = "-",
            NationalId = "-",
            Address = "-",
            BankName = model.BankName,
            BankAccountName = model.BankAccountName,
            BankAccountNo = model.BankAccountNumber,
            CreatedAt = DateTime.Now
        };

        // update user
        var user = _db.Users.Find(userId);
        user.IsWriter = true;

        _db.Writerprofiles.Add(writer);
        _db.SaveChanges();

        return RedirectToAction("Dashboard", "Writer", new { area = "Writer", tab = "payments" });
    }

    [HttpPost]
    public IActionResult Edit(WriterPaymentViewModel model)
    {
        if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int userId) || userId <= 0)
            return Unauthorized();

        var payment = _db.Writerprofiles
            .FirstOrDefault(w => w.UserId == userId);

        if (payment == null)
            return NotFound();

        payment.BankName = model.BankName;
        payment.BankAccountName = model.BankAccountName;
        payment.BankAccountNo = model.BankAccountNumber;

        _db.SaveChanges();

        return RedirectToAction("Dashboard", "Writer", new { area = "Writer", tab = "payments" });
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int paymentId)
    {
        if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int userId) || userId <= 0)
            return Unauthorized();

        var payment = _db.Writerprofiles
            .FirstOrDefault(w => w.UserId == userId);

        if (payment == null)
            return NotFound();

        // reset writer profile
        payment.BankName = "-";
        payment.BankAccountName = "-";
        payment.BankAccountNo = "-";

        // update user
        var user = _db.Users.Find(userId);
        user.IsWriter = false;

        _db.SaveChanges();

        return RedirectToAction("Dashboard", "Writer", new { area = "Writer", tab = "payments" });
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
