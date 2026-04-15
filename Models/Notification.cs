using System;

namespace Web_Novel.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }
        public int NovelId { get; set; }
        public int? ChapterId { get; set; }

        public string Message { get; set; } = null!;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Novel Novel { get; set; } = null!;
        public virtual Chapter? Chapter { get; set; }
    }
}