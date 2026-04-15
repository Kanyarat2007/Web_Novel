using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin
{
    public class PromotionViewModel
    {
        public int PromotionId { get; set; }

        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!; // Discount / Bonus / Event

        public float DiscountValue { get; set; }

        public int BonusCoin { get; set; }

        public int? MaxUsageTotal { get; set; }

        public int MaxUsagePerUser { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}