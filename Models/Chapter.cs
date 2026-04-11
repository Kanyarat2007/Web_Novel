using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Chapter
{
    public bool IsDeleted { get; set; } = false;
    public int ChapterId { get; set; }

    public int NovelId { get; set; }

    public int ChapterNumber { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public bool IsCoinRequired { get; set; }

    public int CoinPrice { get; set; }

    public string PublishStatus { get; set; } = null!;

    public int ViewCount { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Chapterbookmark> Chapterbookmarks { get; set; } = new List<Chapterbookmark>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Novel Novel { get; set; } = null!;

    public virtual ICollection<Novelnote> Novelnotes { get; set; } = new List<Novelnote>();

    public virtual ICollection<Novelpurchase> Novelpurchases { get; set; } = new List<Novelpurchase>();

    public virtual ICollection<Readinghistory> Readinghistories { get; set; } = new List<Readinghistory>();
}
