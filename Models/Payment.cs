using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int UserId { get; set; }

    public int PackageId { get; set; }

    public int Amount { get; set; }

    public string Status { get; set; } = null!; // Pending, Success, Failed

    public DateTime CreatedAt { get; set; }

    // 🔗 Navigation
    public virtual User User { get; set; } = null!;
    public virtual Coinpackage Package { get; set; } = null!;
}
