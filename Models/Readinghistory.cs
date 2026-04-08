using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Readinghistory
{
    public int HistoryId { get; set; }

    public int UserId { get; set; }

    public string ContentType { get; set; } = null!;

    public int? NovelId { get; set; }

    public int? LastChapterId { get; set; }

    public int? MangaId { get; set; }

    public int? LastMangaChapterId { get; set; }

    public DateTime LastReadAt { get; set; }

    public virtual Chapter? LastChapter { get; set; }

    public virtual Mangachapter? LastMangaChapter { get; set; }

    public virtual Manga? Manga { get; set; }

    public virtual Novel? Novel { get; set; }

    public virtual User User { get; set; } = null!;
}
