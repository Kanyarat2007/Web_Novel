// ViewModels/NovelDetailViewModel.cs
using Web_Novel.Models;

namespace Web_Novel.ViewModels
{
    public class NovelDetailViewModel
    {
        // ข้อมูลนิยาย
        public int NovelId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public string? AuthorNote { get; set; }
        public string? CoverImageUrl { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SeriesType { get; set; } = string.Empty;
        public int TotalViews { get; set; }
        public int TotalLikes { get; set; }
        public int TotalBookshelf { get; set; }
        public string? TriggerWarnings { get; set; }

        // Genre
        public List<string> GenreNames { get; set; } = new();

        // สารบัญ
        public List<ChapterItemViewModel> Chapters { get; set; } = new();

        // สถานะของ User คนนี้กับนิยายเรื่องนี้
        public bool IsInBookshelf { get; set; }
        public bool IsLiked { get; set; }
        public int? LastReadChapterNumber { get; set; }
        public bool IsLoggedIn { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new();

        public string Sort { get; set; } = "new";
        public int CurrentUserId { get; set; }
        public int? ChapterId { get; set; }  // ✅ ต้องเป็น nullable
    }

    public class ChapterItemViewModel
    {
        public int ChapterId { get; set; }
        public int ChapterNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCoinRequired { get; set; }
        public int CoinPrice { get; set; }
        public int ViewCount { get; set; }
        public bool IsPurchased { get; set; }
        // public List<CommentViewModel> Comments { get; set; } = new();
        public string Sort { get; set; } = "new";
        public int CurrentUserId { get; set; }
    }
}