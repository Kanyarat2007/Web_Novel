using System;
using System.Collections.Generic;
using Web_Novel.Models.Enums;

namespace Web_Novel.Models;

public partial class Novel
{
    public int NovelId { get; set; }

    public int AuthorUserId { get; set; }

    public int PenNameId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Synopsis { get; set; }
    public string? AuthorNote { get; set; }

    public string? CoverImageUrl { get; set; }

    public string SeriesType { get; set; } = null!;
    public NovelStatus Status { get; set; }

    public string PublishStatus { get; set; } = null!;

    public virtual ICollection<NovelTriggerWarning> NovelTriggerWarnings { get; set; }

    public bool IsCoinRequired { get; set; }

    public int CoinPrice { get; set; }

    public int TotalViews { get; set; }

    public int TotalLikes { get; set; }

    public int TotalBookshelf { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int GenreId { get; set; }

    public virtual User AuthorUser { get; set; } = null!;

    public virtual ICollection<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();

    public virtual ICollection<Chapterbookmark> Chapterbookmarks { get; set; } = new List<Chapterbookmark>();

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Novellike> Novellikes { get; set; } = new List<Novellike>();

    public virtual ICollection<Novelpurchase> Novelpurchases { get; set; } = new List<Novelpurchase>();

    public virtual Penname? PenName { get; set; }

    public virtual ICollection<Readinghistory> Readinghistories { get; set; } = new List<Readinghistory>();

    public virtual ICollection<Writerstat> Writerstats { get; set; } = new List<Writerstat>();

    public ICollection<NovelGenre> NovelGenres { get; set; } = new List<NovelGenre>();

    public bool IsDeleted { get; set; } = false;
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();




}
