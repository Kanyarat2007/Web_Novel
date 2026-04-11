// ViewModels/NovelDetailViewModel.cs
using Web_Novel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Novel.Models.Enums;

namespace Web_Novel.ViewModels.Writer
{
    public class WriterMoneyViewModel
    {
        public WriterPaymentViewModel Payment { get; set; }

        // public WriterEarningsViewModel Earnings { get; set; }
    }
}