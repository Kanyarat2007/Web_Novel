using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Cointransaction
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }
    public int? NovelId { get; set; }
    public virtual Novel? Novel { get; set; }

    public int? PackageId { get; set; }

    public string Type { get; set; } = null!;

    public int Amount { get; set; }

    public int BalanceBefore { get; set; }

    public int BalanceAfter { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Coinpackage? Package { get; set; }

    public virtual User User { get; set; } = null!;
}
