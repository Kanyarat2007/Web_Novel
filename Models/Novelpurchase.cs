using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Novelpurchase
{
    public int PurchaseId { get; set; }

    public int UserId { get; set; }

    public string ContentType { get; set; } = null!;

    public int? NovelId { get; set; }

    public int? ChapterId { get; set; }

    public int? MangaId { get; set; }

    public int? MangaChapterId { get; set; }

    public int CoinSpent { get; set; }

    public DateTime PurchasedAt { get; set; }

    public virtual Chapter? Chapter { get; set; }

    public virtual Manga? Manga { get; set; }

    public virtual Mangachapter? MangaChapter { get; set; }

    public virtual Novel? Novel { get; set; }

    public virtual User User { get; set; } = null!;
}
