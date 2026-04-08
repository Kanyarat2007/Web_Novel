using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Mangapage
{
    public int PageId { get; set; }

    public int MangaChapterId { get; set; }

    public int PageNumber { get; set; }

    public string ImageUrl { get; set; } = null!;

    public int? Width { get; set; }

    public int? Height { get; set; }

    public virtual Mangachapter MangaChapter { get; set; } = null!;
}
