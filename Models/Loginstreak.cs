using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Loginstreak
{
    public int StreakId { get; set; }

    public int UserId { get; set; }

    public DateOnly LastLoginDate { get; set; }

    public int CurrentStreak { get; set; }

    public int LongestStreak { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
