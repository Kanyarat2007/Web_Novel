using System;

namespace Web_Novel.ViewModels
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }

        public string NovelTitle { get; set; } = null!;
        public int? ChapterId { get; set; }

        public string Message { get; set; } = null!;

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}