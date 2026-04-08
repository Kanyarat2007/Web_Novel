using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Mangalike
{
    public int UserId { get; set; }

    public int MangaId { get; set; }

    public DateTime LikedAt { get; set; }

    public virtual Manga Manga { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
