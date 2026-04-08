using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Writerstat
{
    public int StatId { get; set; }

    public int UserId { get; set; }

    public string ContentType { get; set; } = null!;

    public int? NovelId { get; set; }

    public int? MangaId { get; set; }

    public int TotalViews { get; set; }

    public int TotalLikes { get; set; }

    public int TotalBookshelf { get; set; }

    public int TotalCoinEarned { get; set; }

    public int TotalChaptersSold { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Manga? Manga { get; set; }

    public virtual Novel? Novel { get; set; }

    public virtual User User { get; set; } = null!;
}
