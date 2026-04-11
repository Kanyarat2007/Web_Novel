using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Writer
{
    public class WriterDashboardViewModel
    {
        public List<MyNovelItemViewModel> Novels { get; set; } = new();
        public List<PenNameViewModel> PenNames { get; set; } = new();
        public List<WriterMoneyViewModel> Money { get; set; } = new();

        public WriterEarningSummaryViewModel EarningSummary { get; set; }
        public List<WriterEarningItemViewModel> Earnings { get; set; } = new();
        public List<WriterPaymentViewModel> Payments { get; set; } = new();

        public List<WriterStatsViewModel> Stats { get; set; } = new();
        public int TotalViews { get; set; }
        public int TotalLikes { get; set; }
        public string ActiveTab { get; set; }
    }
}