using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public class NovelTriggerWarning
{
    public int NovelId { get; set; }
    public int WarningId { get; set; }

    // Navigation
    public virtual Novel Novel { get; set; }
    public virtual TriggerWarning TriggerWarning { get; set; }
    // public int? MangaId { get; set; }
    // public virtual Manga Manga { get; set; }
}
