using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Comment
{
    
    public int CommentId { get; set; }

    public int UserId { get; set; }

    public int? NovelId { get; set; }

    public int? ChapterId { get; set; }

    public int? MangaId { get; set; }

    public int? MangaChapterId { get; set; }

    public string Content { get; set; } = null!;

    public bool IsHidden { get; set; }

    public bool IsSpoiler { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Chapter? Chapter { get; set; }

    public virtual Manga? Manga { get; set; }

    public virtual Mangachapter? MangaChapter { get; set; }

    public virtual Novel? Novel { get; set; }

    public virtual User User { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;
    public string? DeleteReason { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }

    public string Status { get; set; } = "active";
}
