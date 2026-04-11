// ViewModels/NovelDetailViewModel.cs
using Web_Novel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Novel.Models.Enums;

namespace Web_Novel.ViewModels.Writer
{
    public class WriterEarningItemViewModel
    {
        public int TransactionId { get; set; }
        public int? NovelId { get; set; }
        public string NovelTitle { get; set; }
        public int Amount { get; set; }

        public string Type { get; set; } // purchase / read / bonus

        public DateTime CreatedAt { get; set; }

        public int BalanceAfter { get; set; }
    }
}