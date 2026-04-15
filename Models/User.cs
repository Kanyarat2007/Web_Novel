using System;
using System.Collections.Generic;

namespace Web_Novel.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string DisplayName { get; set; } = null!;
    public string? Bio { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? ProfileImageUrl { get; set; }

    public int CoinBalance { get; set; }

    public bool IsWriter { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public virtual ICollection<Blocklist> BlocklistBlockedUsers { get; set; } = new List<Blocklist>();

    public virtual ICollection<Blocklist> BlocklistBlockerUsers { get; set; } = new List<Blocklist>();

    public virtual ICollection<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();

    public virtual ICollection<Chapterbookmark> Chapterbookmarks { get; set; } = new List<Chapterbookmark>();

    public virtual ICollection<Cointransaction> Cointransactions { get; set; } = new List<Cointransaction>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Loginstreak? Loginstreak { get; set; }

    public virtual ICollection<Mangalike> Mangalikes { get; set; } = new List<Mangalike>();

    public virtual ICollection<Manga> Mangas { get; set; } = new List<Manga>();

    public virtual ICollection<Moderationlog> Moderationlogs { get; set; } = new List<Moderationlog>();

    public virtual ICollection<Novellike> Novellikes { get; set; } = new List<Novellike>();

    public virtual ICollection<Novelnote> Novelnotes { get; set; } = new List<Novelnote>();

    public virtual ICollection<Novelpurchase> Novelpurchases { get; set; } = new List<Novelpurchase>();

    public virtual ICollection<Novel> Novels { get; set; } = new List<Novel>();

    public virtual ICollection<Penname> Pennames { get; set; } = new List<Penname>();

    public virtual ICollection<Readinghistory> Readinghistories { get; set; } = new List<Readinghistory>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Spinlog> Spinlogs { get; set; } = new List<Spinlog>();

    public virtual ICollection<Userpromotion> Userpromotions { get; set; } = new List<Userpromotion>();

    public virtual Writerprofile? Writerprofile { get; set; }

    public virtual ICollection<Writerstat> Writerstats { get; set; } = new List<Writerstat>();

    // public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    // public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    public virtual UserCoin? UserCoin { get; set; }
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserGenre> UserGenres { get; set; } = new List<UserGenre>();
    public virtual ICollection<UserReward> UserRewards { get; set; } = new List<UserReward>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
