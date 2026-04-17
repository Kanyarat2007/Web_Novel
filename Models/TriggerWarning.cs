using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class TriggerWarning
{
    public int WarningId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<NovelTriggerWarning> NovelTriggerWarnings { get; set; }
    public virtual ICollection<ChapterTriggerWarning> ChapterTriggerWarnings { get; set; } = new List<ChapterTriggerWarning>();
    
}
