using System;

namespace Web_Novel.Models;

public partial class ChapterTriggerWarning
{
    public int ChapterId { get; set; }

    public int WarningId { get; set; }

    public virtual Chapter Chapter { get; set; } = null!;

    public virtual TriggerWarning Warning { get; set; } = null!;
}