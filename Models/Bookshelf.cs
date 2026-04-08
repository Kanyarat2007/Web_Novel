using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Bookshelf
{
    public int BookshelfId { get; set; }

    public int UserId { get; set; }

    public string ContentType { get; set; } = null!;

    public int? NovelId { get; set; }

    public int? MangaId { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual Manga? Manga { get; set; }

    public virtual Novel? Novel { get; set; }

    public virtual User User { get; set; } = null!;
}
