using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Blocklist
{
    public int BlockId { get; set; }

    public int BlockerUserId { get; set; }

    public int BlockedUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User BlockedUser { get; set; } = null!;

    public virtual User BlockerUser { get; set; } = null!;
}
