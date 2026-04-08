using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Penname
{
    public int PenNameId { get; set; }

    public int UserId { get; set; }

    public string PenName { get; set; } = null!;

    public string? Bio { get; set; }

    public string? AvatarUrl { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Manga> Mangas { get; set; } = new List<Manga>();

    public virtual ICollection<Novel> Novels { get; set; } = new List<Novel>();

    public virtual User User { get; set; } = null!;
}
