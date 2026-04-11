using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Writer
{
    public class WriterStatsViewModel
    {
        public int UserId { get; set; }

        public string ContentType { get; set; } = string.Empty;

        public int TotalViews { get; set; }

        public int TotalLikes { get; set; }

        public int TotalBookshelf { get; set; }

        public int TotalCoinEarned { get; set; }

        public int TotalChaptersSold { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // เอาไว้แสดงชื่อเรื่องด้วย (สำคัญมากใน dashboard)
        public int? NovelId { get; set; }
        public string? NovelTitle { get; set; }

        public int? MangaId { get; set; }
        public string? MangaTitle { get; set; }
    }
}