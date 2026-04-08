using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Spinlog
{
    public int SpinLogId { get; set; }

    public int UserId { get; set; }

    public int? PromotionId { get; set; }

    public string RewardType { get; set; } = null!;

    public int RewardValue { get; set; }

    public DateTime SpinAt { get; set; }

    public virtual Promotion? Promotion { get; set; }

    public virtual User User { get; set; } = null!;
}
