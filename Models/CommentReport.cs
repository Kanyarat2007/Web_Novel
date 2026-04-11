using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Novel.Models;

public class CommentReport
{
    [Key]
    public int ReportId { get; set; }

    public int CommentId { get; set; }
    public int UserId { get; set; }

    public string? Reason { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = "pending";

    // Navigation
    public virtual Comment Comment { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
