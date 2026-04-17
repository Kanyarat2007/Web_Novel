// ViewModels/NovelDetailViewModel.cs
using Web_Novel.Models;
using Web_Novel.Models.Enums;

namespace Web_Novel.ViewModels.Reader
{
    public class NovelDetailViewModel
    {
        // ข้อมูลนิยาย
        public int NovelId { get; set; }
        public int UserId { get; set; }
        public int PenNameId { get; set; }
        public string PenName { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public string? AuthorNote { get; set; }
        public string? CoverImageUrl { get; set; }
        public NovelStatus Status { get; set; }
        public string SeriesType { get; set; } = string.Empty;
        public int TotalViews { get; set; }
        public int TotalLikes { get; set; }
        public int TotalBookshelf { get; set; }
        public virtual ICollection<NovelTriggerWarning> NovelTriggerWarnings { get; set; }
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
        public int? ChapterId { get; set; }
        public List<TriggerWarningDisplay> NovelWarnings { get; set; } = new();
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