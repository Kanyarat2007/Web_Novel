// ViewModels/NovelDetailViewModel.cs
using Web_Novel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Novel.Models.Enums;

namespace Web_Novel.ViewModels.Writer
{
    public class WriterEarningSummaryViewModel
    {
        public int UserId { get; set; }

        // รายได้รวมทั้งหมด
        public decimal TotalEarning { get; set; }

        // รายได้เดือนนี้
        public decimal MonthlyEarning { get; set; }

        // รายได้วันนี้
        public decimal TodayEarning { get; set; }

        // จำนวน transaction
        public decimal TotalTransactions { get; set; }

        public List<WriterPayoutViewModel> Payouts { get; set; }
        public decimal Balance { get; set; }
    }
}