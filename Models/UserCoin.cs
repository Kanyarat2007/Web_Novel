using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public class UserCoin
{
    public int UserId { get; set; }

    public int Balance { get; set; }

    public DateTime UpdatedAt { get; set; }

    // 🔗 Navigation
    public virtual User User { get; set; } = null!;
}
