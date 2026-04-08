using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class Novelnote
{
    public int NoteId { get; set; }

    public int UserId { get; set; }

    public int ChapterId { get; set; }

    public string NoteContent { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Chapter Chapter { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
