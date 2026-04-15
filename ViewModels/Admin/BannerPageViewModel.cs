using Web_Novel.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Novel.ViewModels.Admin
{
    public class BannerPageViewModel
    {
        public int BannerId { get; set; }

        public string? Title { get; set; }

        public string? ImageUrl { get; set; }

        public string? RedirectUrl { get; set; }

        public string Position { get; set; } = "HomepageTop";

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IFormFile? ImageFile { get; set; }

        public bool IsDeleted { get; set; } = false;

        // ใช้แค่โชว์ list
        public List<BannerPageViewModel> Banners { get; set; } = new();
    }
}