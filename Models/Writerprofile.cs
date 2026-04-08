using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Writerprofile
{
    public int WriterProfileId { get; set; }

    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string NationalId { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public string BankAccountNo { get; set; } = null!;

    public string BankAccountName { get; set; } = null!;

    public bool IsVerified { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
