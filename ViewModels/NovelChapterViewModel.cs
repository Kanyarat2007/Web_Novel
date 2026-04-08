// ViewModels/NovelChapterViewModel.cs

using Web_Novel.Models;

namespace Web_Novel.ViewModels
{
    public class NovelChapterViewModel
    {
        public int NovelId { get; set; }
        public string NovelTitle { get; set; } = string.Empty;
        public int ChapterId { get; set; }
        public int ChapterNumber { get; set; }
        public string ChapterTitle { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsCoinRequired { get; set; }
        public int CoinPrice { get; set; }
        public bool CanRead { get; set; }
        public bool IsPurchased { get; set; }
        public bool IsLoggedIn { get; set; }   // ← เพิ่มใหม่
        public int? PrevChapterNumber { get; set; }
        public int? NextChapterNumber { get; set; }
        public string? UserNote { get; set; }
        public List<ChapterItemViewModel> Chapters { get; set; } = new();
        public bool IsBookmarked { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new();
        public string Sort { get; set; } = "new";
        public int CurrentUserId { get; set; }
    }
}