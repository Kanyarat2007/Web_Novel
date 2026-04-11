using Web_Novel.Models;

namespace Web_Novel.ViewModels.Reader
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

        // 🔴 Soft Delete
        public bool IsDeleted { get; set; } = false;
        public string? DeleteReason { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }

        // 🟡 Spoiler
        public bool IsSpoiler { get; set; } = false;

        // 🟠 Status (active / hidden / deleted)
        public string Status { get; set; } = "active";

    }
}