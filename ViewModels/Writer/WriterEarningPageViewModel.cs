// ViewModels/NovelDetailViewModel.cs
using Web_Novel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Novel.Models.Enums;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Writer
{
    public class WriterEarningPageViewModel
    {
        public int UserId { get; set; }

        public WriterEarningSummaryViewModel Summary { get; set; }
        public List<WriterEarningItemViewModel> Earnings { get; set; } = new();

        public List<WriterEarningItemViewModel> Transactions { get; set; }

        public List<WriterPaymentViewModel> Payments { get; set; } = new();

        // optional: filter
        public string? FilterType { get; set; }
    }
}