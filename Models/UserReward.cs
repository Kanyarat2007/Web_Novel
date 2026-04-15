using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class UserReward
{
    public int UserRewardId { get; set; }

    public int UserId { get; set; }

    public string RewardType { get; set; } = null!;

    public int? ReferenceId { get; set; }

    public int CoinAmount { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    // 🔗 Navigation
    public virtual User User { get; set; } = null!;
}