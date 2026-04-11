using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class TriggerWarning
{
    public int WarningId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<NovelTriggerWarning> NovelTriggerWarnings { get; set; }
}
