using Web_Novel.Models;
using System.Collections.Generic;

// ใช้แสดง “รายการคนที่เราบล็อค”
// เอาไว้แสดงในหน้า “Blocked Users List”

namespace Web_Novel.ViewModels

{
    public class CoinHistoryViewModel
    {
        public int Amount { get; set; }
        public string Type { get; set; }

        public int BalanceAfter { get; set; }

        public string Note { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}