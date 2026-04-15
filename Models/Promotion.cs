using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string Name { get; set; } = null!;

    // public string Type { get; set; } = null!;

    public float DiscountValue { get; set; }

    public int BonusCoin { get; set; }

    public int? MaxUsageTotal { get; set; }

    public int MaxUsagePerUser { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Spinlog> Spinlogs { get; set; } = new List<Spinlog>();

    public virtual ICollection<Userpromotion> Userpromotions { get; set; } = new List<Userpromotion>();
}
