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
        public int TotalEarning { get; set; }

        // รายได้เดือนนี้
        public int MonthlyEarning { get; set; }

        // รายได้วันนี้
        public int TodayEarning { get; set; }

        // จำนวน transaction
        public int TotalTransactions { get; set; }
    }
}