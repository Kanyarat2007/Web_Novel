using Web_Novel.Models;
using System.Collections.Generic;

// ใช้แสดง “รายการคนที่เราบล็อค”
// เอาไว้แสดงในหน้า “Blocked Users List”

namespace Web_Novel.ViewModels.Reader

{
    public class TopUpViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public int CurrentBalance { get; set; }

        public List<Coinpackage> CoinPackages { get; set; }
        public List<PackageViewModel> Packages { get; set; } = new();
    }

    public class PackageViewModel
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int CoinAmount { get; set; }
        public int BonusCoin { get; set; }

        public int FinalPrice { get; set; }
        public int FinalCoin { get; set; }

        public float DiscountApplied { get; set; }
        public int BonusApplied { get; set; }
    }
}