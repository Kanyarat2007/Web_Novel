using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Writer
{
    public class MyNovelItemViewModel
    {
        public int UserId { get; set; }
        public int NovelId { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public string PenName { get; set; }

        public string Name { get; set; }

        public int TotalChapters { get; set; }
        public int TotalViews { get; set; }

        public int TotalLikes { get; set; }
        public int TotalBookshelf { get; set; }

        public bool IsCoinRequired { get; set; }
        public int CoinPrice { get; set; }

        public string PublishStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public List<MyNovelItemViewModel> Novels { get; set; } = new();
    }
}