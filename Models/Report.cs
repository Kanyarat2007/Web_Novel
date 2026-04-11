using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int ReporterUserId { get; set; }

    public string TargetType { get; set; } = null!;

    public int TargetId { get; set; }

    public string Reason { get; set; } = null!;
    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User ReporterUser { get; set; } = null!;
}
