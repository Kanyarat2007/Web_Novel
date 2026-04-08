using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Moderationlog
{
    public int LogId { get; set; }

    public int ModeratorUserId { get; set; }

    public string TargetType { get; set; } = null!;

    public int TargetId { get; set; }

    public string Action { get; set; } = null!;

    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User ModeratorUser { get; set; } = null!;
}
