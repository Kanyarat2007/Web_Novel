using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Manga
{
    public int MangaId { get; set; }

    public int AuthorUserId { get; set; }

    public int? PenNameId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Synopsis { get; set; }

    public string? CoverImageUrl { get; set; }

    public string SeriesType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string PublishStatus { get; set; } = null!;

    public string? TriggerWarnings { get; set; }

    public bool IsCoinRequired { get; set; }

    public int CoinPrice { get; set; }

    public string ReadDirection { get; set; } = null!;

    public int TotalViews { get; set; }

    public int TotalLikes { get; set; }

    public int TotalBookshelf { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User AuthorUser { get; set; } = null!;

    public virtual ICollection<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();

    public virtual ICollection<Chapterbookmark> Chapterbookmarks { get; set; } = new List<Chapterbookmark>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Mangachapter> Mangachapters { get; set; } = new List<Mangachapter>();

    public virtual ICollection<Mangalike> Mangalikes { get; set; } = new List<Mangalike>();

    public virtual ICollection<Novelpurchase> Novelpurchases { get; set; } = new List<Novelpurchase>();

    public virtual Penname? PenName { get; set; }

    public virtual ICollection<Readinghistory> Readinghistories { get; set; } = new List<Readinghistory>();

    public virtual ICollection<Writerstat> Writerstats { get; set; } = new List<Writerstat>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
