using System;
using System.Collections.Generic;


namespace Web_Novel.Models
{
    public class NovelGenre
{
    public int NovelId { get; set; }
    public int GenreId { get; set; }

    public Novel Novel { get; set; }
    public Genre Genre { get; set; }
}
}
