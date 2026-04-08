using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Mangachapter
{
    public int MangaChapterId { get; set; }

    public int MangaId { get; set; }

    public int ChapterNumber { get; set; }

    public string Title { get; set; } = null!;

    public bool IsCoinRequired { get; set; }

    public int CoinPrice { get; set; }

    public string PublishStatus { get; set; } = null!;

    public int ViewCount { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Chapterbookmark> Chapterbookmarks { get; set; } = new List<Chapterbookmark>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Manga Manga { get; set; } = null!;

    public virtual ICollection<Mangapage> Mangapages { get; set; } = new List<Mangapage>();

    public virtual ICollection<Novelpurchase> Novelpurchases { get; set; } = new List<Novelpurchase>();

    public virtual ICollection<Readinghistory> Readinghistories { get; set; } = new List<Readinghistory>();
}
