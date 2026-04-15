using Web_Novel.Models;

namespace Web_Novel.ViewModels.Reader
{
    public class BannerViewModel
    {
        public int BannerId { get; set; }
        public string? Title { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? RedirectUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}