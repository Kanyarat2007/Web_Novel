using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Coinpackage
{
    public int PackageId { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int CoinAmount { get; set; }

    public int BonusCoin { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Cointransaction> Cointransactions { get; set; } = new List<Cointransaction>();
}
