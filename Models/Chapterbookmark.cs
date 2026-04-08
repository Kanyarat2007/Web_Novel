using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Chapterbookmark
{
    public int BookmarkId { get; set; }

    public int UserId { get; set; }

    public int? NovelId { get; set; }

    public int? ChapterId { get; set; }

    public int? MangaId { get; set; }

    public int? MangaChapterId { get; set; }

    public DateTime BookmarkedAt { get; set; }

    public virtual Chapter? Chapter { get; set; }

    public virtual Manga? Manga { get; set; }

    public virtual Mangachapter? MangaChapter { get; set; }

    public virtual Novel? Novel { get; set; }

    public virtual User User { get; set; } = null!;
}
