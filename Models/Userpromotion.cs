using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Userpromotion
{
    public int UserPromotionId { get; set; }

    public int UserId { get; set; }

    public int PromotionId { get; set; }

    public DateTime UsedAt { get; set; }

    public virtual Promotion Promotion { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
