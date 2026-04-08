using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Novel.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    [NotMapped]
    public virtual ICollection<Novel> Novels { get; set; } = new List<Novel>();

    public virtual ICollection<Manga> Mangas { get; set; } = new List<Manga>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
