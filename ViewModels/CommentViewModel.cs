using Web_Novel.Models;

namespace Web_Novel.ViewModels
{

    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public int? ChapterId { get; set; }  // ✅ ต้องเป็น nullable
        public int? ChapterNumber { get; set; }    // ✅ ต้องเป็น nullable
    }
}