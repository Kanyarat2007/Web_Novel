using Web_Novel.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Novel.ViewModels.Admin
{
    public class CoinPackageViewModel
    {
        public int PackageId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CoinAmount { get; set; }

        [Range(0, int.MaxValue)]
        public int BonusCoin { get; set; }

        public bool IsActive { get; set; }
    }
}