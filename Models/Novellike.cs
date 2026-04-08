using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Novellike
{
    public int UserId { get; set; }

    public int NovelId { get; set; }

    public DateTime LikedAt { get; set; }

    public virtual Novel Novel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
