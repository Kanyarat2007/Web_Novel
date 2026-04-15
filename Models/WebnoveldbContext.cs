using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Web_Novel.Models;

public partial class WebnoveldbContext : DbContext
{
    public WebnoveldbContext()
    {
    }

    public WebnoveldbContext(DbContextOptions<WebnoveldbContext> options)
        : base(options)
    {
    }


    public DbSet<Banner> Banners { get; set; }
    public virtual DbSet<Blocklist> Blocklists { get; set; }

    public virtual DbSet<Bookshelf> Bookshelves { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Chapterbookmark> Chapterbookmarks { get; set; }

    public virtual DbSet<Coinpackage> Coinpackages { get; set; }

    public virtual DbSet<Cointransaction> Cointransactions { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentReport> CommentReports { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Loginstreak> Loginstreaks { get; set; }

    public virtual DbSet<Manga> Mangas { get; set; }

    public virtual DbSet<Mangachapter> Mangachapters { get; set; }

    public virtual DbSet<Mangalike> Mangalikes { get; set; }

    public virtual DbSet<Mangapage> Mangapages { get; set; }

    public virtual DbSet<Moderationlog> Moderationlogs { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Novel> Novels { get; set; }

    public virtual DbSet<NovelGenre> NovelGenres { get; set; }

    public virtual DbSet<Novellike> Novellikes { get; set; }

    public virtual DbSet<Novelnote> Novelnotes { get; set; }

    public virtual DbSet<Novelpurchase> Novelpurchases { get; set; }

    public virtual DbSet<NovelTriggerWarning> NovelTriggerWarnings { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Penname> Pennames { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Readinghistory> Readinghistories { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Spinlog> Spinlogs { get; set; }
    public virtual DbSet<TriggerWarning> TriggerWarnings { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserCoin> UserCoins { get; set; }
    public virtual DbSet<UserGenre> UserGenres { get; set; }

    public virtual DbSet<Userpromotion> Userpromotions { get; set; }
    public virtual DbSet<UserReward> UserRewards { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Writerprofile> Writerprofiles { get; set; }

    public virtual DbSet<Writerstat> Writerstats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=webnoveldb;user=root;password=kanyarat272004", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.6.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.BannerId).HasName("PRIMARY");

            entity
                .ToTable("banners")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Title)
                .HasMaxLength(200);

            entity.Property(e => e.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.RedirectUrl)
                .HasMaxLength(500);

            entity.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("HomepageTop");

            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(0);

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.StartDate)
                .HasColumnType("datetime");

            entity.Property(e => e.EndDate)
                .HasColumnType("datetime");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<Blocklist>(entity =>
        {
            entity.HasKey(e => e.BlockId).HasName("PRIMARY");

            entity
                .ToTable("blocklist")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.BlockedUserId, "BlockedUserId");

            entity.HasIndex(e => new { e.BlockerUserId, e.BlockedUserId }, "UQ_Block").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.BlockedUser).WithMany(p => p.BlocklistBlockedUsers)
                .HasForeignKey(d => d.BlockedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blocklist_ibfk_2");

            entity.HasOne(d => d.BlockerUser).WithMany(p => p.BlocklistBlockerUsers)
                .HasForeignKey(d => d.BlockerUserId)
                .HasConstraintName("blocklist_ibfk_1");
        });

        modelBuilder.Entity<Bookshelf>(entity =>
        {
            entity.HasKey(e => e.BookshelfId).HasName("PRIMARY");

            entity
                .ToTable("bookshelves")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.MangaId, "MangaId");

            entity.HasIndex(e => e.NovelId, "NovelId");

            entity.HasIndex(e => new { e.UserId, e.MangaId }, "UQ_Bookshelf_Manga").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.NovelId }, "UQ_Bookshelf_Novel").IsUnique();

            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ContentType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'novel'");

            entity.HasOne(d => d.Manga).WithMany(p => p.Bookshelves)
                .HasForeignKey(d => d.MangaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("bookshelves_ibfk_3");

            entity.HasOne(d => d.Novel).WithMany(p => p.Bookshelves)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("bookshelves_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Bookshelves)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("bookshelves_ibfk_1");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PRIMARY");

            entity
                .ToTable("chapters")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.NovelId, e.ChapterNumber }, "UQ_Chapter").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PublishStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'draft'");
            entity.Property(e => e.PublishedAt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.Novel).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.NovelId)
                .HasConstraintName("chapters_ibfk_1");
        });

        modelBuilder.Entity<Chapterbookmark>(entity =>
        {
            entity.HasKey(e => e.BookmarkId).HasName("PRIMARY");

            entity
                .ToTable("chapterbookmarks")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ChapterId, "ChapterId");

            entity.HasIndex(e => e.MangaChapterId, "MangaChapterId");

            entity.HasIndex(e => e.MangaId, "MangaId");

            entity.HasIndex(e => e.NovelId, "NovelId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.BookmarkedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Chapterbookmarks)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("chapterbookmarks_ibfk_3");

            entity.HasOne(d => d.MangaChapter).WithMany(p => p.Chapterbookmarks)
                .HasForeignKey(d => d.MangaChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("chapterbookmarks_ibfk_5");

            entity.HasOne(d => d.Manga).WithMany(p => p.Chapterbookmarks)
                .HasForeignKey(d => d.MangaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("chapterbookmarks_ibfk_4");

            entity.HasOne(d => d.Novel).WithMany(p => p.Chapterbookmarks)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("chapterbookmarks_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Chapterbookmarks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("chapterbookmarks_ibfk_1");
        });

        modelBuilder.Entity<Coinpackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PRIMARY");

            entity
                .ToTable("coinpackages")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Cointransaction>(entity =>
        {
            entity.HasIndex(e => e.NovelId, "NovelId");

            entity.HasOne(d => d.Novel)
                .WithMany()
                .HasForeignKey(d => d.NovelId)
                // .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("cointransactions_ibfk_novel");

            entity.HasKey(e => e.TransactionId).HasName("PRIMARY");

            entity
                .ToTable("cointransactions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.PackageId, "PackageId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(300);
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.Package).WithMany(p => p.Cointransactions)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("cointransactions_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Cointransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cointransactions_ibfk_1");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PRIMARY");

            entity
                .ToTable("comments")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ChapterId, "ChapterId");

            entity.HasIndex(e => e.MangaChapterId, "MangaChapterId");

            entity.HasIndex(e => e.MangaId, "MangaId");

            entity.HasIndex(e => e.NovelId, "NovelId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.Content).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("comments_ibfk_3");

            entity.HasOne(d => d.MangaChapter).WithMany(p => p.Comments)
                .HasForeignKey(d => d.MangaChapterId)
                .HasConstraintName("comments_ibfk_5");

            entity.HasOne(d => d.Manga).WithMany(p => p.Comments)
                .HasForeignKey(d => d.MangaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_ibfk_4");

            entity.HasOne(d => d.Novel).WithMany(p => p.Comments)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comments_ibfk_1");
        });

        modelBuilder.Entity<CommentReport>()
            .HasKey(cr => cr.ReportId);

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PRIMARY");

            entity
                .ToTable("genres")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.HasIndex(e => e.Slug, "Slug").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(100);
        });

        modelBuilder.Entity<Loginstreak>(entity =>
        {
            entity.HasKey(e => e.StreakId).HasName("PRIMARY");

            entity
                .ToTable("loginstreaks")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "UserId").IsUnique();

            entity.Property(e => e.CurrentStreak).HasDefaultValueSql("'1'");
            entity.Property(e => e.LongestStreak).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.LastRewardDate)
    .HasColumnType("date");

            entity.HasOne(d => d.User).WithOne(p => p.Loginstreak)
                .HasForeignKey<Loginstreak>(d => d.UserId)
                .HasConstraintName("loginstreaks_ibfk_1");
        });

        modelBuilder.Entity<Manga>(entity =>
        {
            entity.HasKey(e => e.MangaId).HasName("PRIMARY");

            entity
                .ToTable("mangas")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.AuthorUserId, "AuthorUserId");

            entity.HasIndex(e => e.PenNameId, "PenNameId");

            entity.HasIndex(e => e.Slug, "Slug").IsUnique();

            entity.Property(e => e.CoverImageUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PublishStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'draft'");
            entity.Property(e => e.ReadDirection)
                .HasMaxLength(10)
                .HasDefaultValueSql("'ltr'");
            entity.Property(e => e.SeriesType)
                .HasMaxLength(20)
                .HasDefaultValueSql("'series'");
            entity.Property(e => e.Slug).HasMaxLength(300);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ongoing'");
            entity.Property(e => e.Title).HasMaxLength(300);
            // entity.Property(e => e.TriggerWarnings).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AuthorUser).WithMany(p => p.Mangas)
                .HasForeignKey(d => d.AuthorUserId)
                .HasConstraintName("mangas_ibfk_1");

            entity.HasOne(d => d.PenName).WithMany(p => p.Mangas)
                .HasForeignKey(d => d.PenNameId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("mangas_ibfk_2");

            entity.HasMany(d => d.Genres).WithMany(p => p.Mangas)
                .UsingEntity<Dictionary<string, object>>(
                    "Mangagenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("mangagenres_ibfk_2"),
                    l => l.HasOne<Manga>().WithMany()
                        .HasForeignKey("MangaId")
                        .HasConstraintName("mangagenres_ibfk_1"),
                    j =>
                    {
                        j.HasKey("MangaId", "GenreId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("mangagenres")
                            .UseCollation("utf8mb4_unicode_ci");
                        j.HasIndex(new[] { "GenreId" }, "GenreId");
                    });
        });

        modelBuilder.Entity<Mangachapter>(entity =>
        {
            entity.HasKey(e => e.MangaChapterId).HasName("PRIMARY");

            entity
                .ToTable("mangachapters")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.MangaId, e.ChapterNumber }, "UQ_MangaChapter").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PublishStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'draft'");
            entity.Property(e => e.PublishedAt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.Manga).WithMany(p => p.Mangachapters)
                .HasForeignKey(d => d.MangaId)
                .HasConstraintName("mangachapters_ibfk_1");
        });

        modelBuilder.Entity<Mangalike>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MangaId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("mangalikes")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.MangaId, "MangaId");

            entity.Property(e => e.LikedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Manga).WithMany(p => p.Mangalikes)
                .HasForeignKey(d => d.MangaId)
                .HasConstraintName("mangalikes_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Mangalikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mangalikes_ibfk_1");
        });

        modelBuilder.Entity<Mangapage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PRIMARY");

            entity
                .ToTable("mangapages")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.MangaChapterId, e.PageNumber }, "UQ_MangaPage").IsUnique();

            entity.Property(e => e.ImageUrl).HasMaxLength(500);

            entity.HasOne(d => d.MangaChapter).WithMany(p => p.Mangapages)
                .HasForeignKey(d => d.MangaChapterId)
                .HasConstraintName("mangapages_ibfk_1");
        });

        modelBuilder.Entity<Moderationlog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity
                .ToTable("moderationlogs")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ModeratorUserId, "ModeratorUserId");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.TargetType).HasMaxLength(20);

            entity.HasOne(d => d.ModeratorUser).WithMany(p => p.Moderationlogs)
                .HasForeignKey(d => d.ModeratorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("moderationlogs_ibfk_1");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PRIMARY");

            entity
                .ToTable("notifications")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "UserId");
            entity.HasIndex(e => e.NovelId, "NovelId");
            entity.HasIndex(e => e.ChapterId, "ChapterId");

            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.IsRead)
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // FK → User
            entity.HasOne(d => d.User)
                .WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notifications_ibfk_1");

            // FK → Novel
            entity.HasOne(d => d.Novel)
                .WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notifications_ibfk_2");

            // FK → Chapter
            entity.HasOne(d => d.Chapter)
                .WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("notifications_ibfk_3");
        });

        modelBuilder.Entity<Novel>(entity =>
        {
            entity.HasKey(e => e.NovelId).HasName("PRIMARY");

            entity
                .ToTable("novels")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.AuthorUserId, "AuthorUserId");

            entity.HasIndex(e => e.PenNameId, "PenNameId");

            entity.HasIndex(e => e.Slug, "Slug").IsUnique();

            entity.Property(e => e.CoverImageUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PublishStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'draft'");
            entity.Property(e => e.SeriesType)
                .HasMaxLength(20)
                .HasDefaultValueSql("'series'");
            entity.Property(e => e.Slug).HasMaxLength(300);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ongoing'");
            entity.Property(e => e.Title).HasMaxLength(300);
            // entity.Property(e => e.TriggerWarnings).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AuthorUser).WithMany(p => p.Novels)
                .HasForeignKey(d => d.AuthorUserId)
                .HasConstraintName("novels_ibfk_1");

            entity.HasOne(d => d.PenName).WithMany(p => p.Novels)
                .HasForeignKey(d => d.PenNameId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("novels_ibfk_2");

            // entity.HasOne(d => d.Genre)
            //     .WithMany(p => p.Novels)
            //     .HasForeignKey(d => d.GenreId)
            //     .OnDelete(DeleteBehavior.SetNull)
            //     .HasConstraintName("novels_ibfk_genre");
        });

        modelBuilder.Entity<NovelGenre>(entity =>
{
    entity.ToTable("novelgenres");

    entity.HasKey(x => new { x.NovelId, x.GenreId });

    entity.HasOne(x => x.Novel)
        .WithMany(n => n.NovelGenres)
        .HasForeignKey(x => x.NovelId);

    entity.HasOne(x => x.Genre)
        .WithMany()
        .HasForeignKey(x => x.GenreId);
});

        modelBuilder.Entity<Novellike>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.NovelId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("novellikes")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.NovelId, "NovelId");

            entity.Property(e => e.LikedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Novel).WithMany(p => p.Novellikes)
                .HasForeignKey(d => d.NovelId)
                .HasConstraintName("novellikes_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Novellikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("novellikes_ibfk_1");
        });

        modelBuilder.Entity<Novelnote>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PRIMARY");

            entity
                .ToTable("novelnotes")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ChapterId, "ChapterId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.NoteContent).HasMaxLength(2000);

            entity.HasOne(d => d.Chapter).WithMany(p => p.Novelnotes)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("novelnotes_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Novelnotes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("novelnotes_ibfk_1");
        });

        modelBuilder.Entity<Novelpurchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PRIMARY");

            entity
                .ToTable("novelpurchases")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ChapterId, "ChapterId");

            entity.HasIndex(e => e.MangaChapterId, "MangaChapterId");

            entity.HasIndex(e => e.MangaId, "MangaId");

            entity.HasIndex(e => e.NovelId, "NovelId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.ContentType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'novel'");
            entity.Property(e => e.PurchasedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Novelpurchases)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("novelpurchases_ibfk_3");

            entity.HasOne(d => d.MangaChapter).WithMany(p => p.Novelpurchases)
                .HasForeignKey(d => d.MangaChapterId)
                .HasConstraintName("novelpurchases_ibfk_5");

            entity.HasOne(d => d.Manga).WithMany(p => p.Novelpurchases)
                .HasForeignKey(d => d.MangaId)
                .HasConstraintName("novelpurchases_ibfk_4");

            entity.HasOne(d => d.Novel).WithMany(p => p.Novelpurchases)
                .HasForeignKey(d => d.NovelId)
                .HasConstraintName("novelpurchases_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Novelpurchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("novelpurchases_ibfk_1");
        });

        modelBuilder.Entity<NovelTriggerWarning>(entity =>
        {
            entity.ToTable("NovelTriggerWarnings");

            entity.HasKey(e => new { e.NovelId, e.WarningId });

            entity.HasOne(e => e.Novel)
                .WithMany(n => n.NovelTriggerWarnings)
                .HasForeignKey(e => e.NovelId);

            entity.HasOne(e => e.TriggerWarning)
                .WithMany(t => t.NovelTriggerWarnings)
                .HasForeignKey(e => e.WarningId);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payments");

            entity.HasKey(e => e.PaymentId);

            entity.Property(e => e.Amount)
                .IsRequired();

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Package)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Penname>(entity =>
        {
            entity.HasKey(e => e.PenNameId).HasName("PRIMARY");

            entity
                .ToTable("pennames")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.AvatarUrl).HasMaxLength(500);
            entity.Property(e => e.Bio).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PenName).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Pennames)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("pennames_ibfk_1");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PRIMARY");

            entity
                .ToTable("promotions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.MaxUsagePerUser).HasDefaultValueSql("'1'");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Readinghistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PRIMARY");

            entity
                .ToTable("readinghistory")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.LastChapterId, "LastChapterId");

            entity.HasIndex(e => e.LastMangaChapterId, "LastMangaChapterId");

            entity.HasIndex(e => e.MangaId, "MangaId");

            entity.HasIndex(e => e.NovelId, "NovelId");

            entity.HasIndex(e => new { e.UserId, e.MangaId }, "UQ_History_Manga").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.NovelId }, "UQ_History_Novel").IsUnique();

            entity.Property(e => e.ContentType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'novel'");
            entity.Property(e => e.LastReadAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.LastChapter).WithMany(p => p.Readinghistories)
                .HasForeignKey(d => d.LastChapterId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("readinghistory_ibfk_3");

            entity.HasOne(d => d.LastMangaChapter).WithMany(p => p.Readinghistories)
                .HasForeignKey(d => d.LastMangaChapterId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("readinghistory_ibfk_5");

            entity.HasOne(d => d.Manga).WithMany(p => p.Readinghistories)
                .HasForeignKey(d => d.MangaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("readinghistory_ibfk_4");

            entity.HasOne(d => d.Novel).WithMany(p => p.Readinghistories)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("readinghistory_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Readinghistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("readinghistory_ibfk_1");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PRIMARY");

            entity
                .ToTable("reports")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ReporterUserId, "ReporterUserId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'");
            entity.Property(e => e.TargetType).HasMaxLength(20);

            entity.Property(e => e.Description)
    .HasColumnType("text")
    .IsRequired(false);

            entity.HasOne(d => d.ReporterUser).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReporterUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reports_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity
                .ToTable("roles")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.RoleName, "RoleName").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Spinlog>(entity =>
        {
            entity.HasKey(e => e.SpinLogId).HasName("PRIMARY");

            entity
                .ToTable("spinlogs")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.PromotionId, "PromotionId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.RewardType).HasMaxLength(30);
            entity.Property(e => e.SpinAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Spinlogs)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("spinlogs_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Spinlogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("spinlogs_ibfk_1");
        });

        modelBuilder.Entity<TriggerWarning>(entity =>
        {
            entity.ToTable("TriggerWarnings");

            entity.HasKey(e => e.WarningId); // 🔥 บอก EF ว่านี่คือ PK

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnType("text");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity
                .ToTable("users")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.Username, "Username").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.ProfileImageUrl).HasMaxLength(500);
            entity.Property(e => e.Username).HasMaxLength(100);

            // entity.HasMany(d => d.Genres).WithMany(p => p.Users)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "Usergenrepreference",
            //         r => r.HasOne<Genre>().WithMany()
            //             .HasForeignKey("GenreId")
            //             .HasConstraintName("usergenrepreferences_ibfk_2"),
            //         l => l.HasOne<User>().WithMany()
            //             .HasForeignKey("UserId")
            //             .HasConstraintName("usergenrepreferences_ibfk_1"),
            //         j =>
            //         {
            //             j.HasKey("UserId", "GenreId")
            //                 .HasName("PRIMARY")
            //                 .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
            //             j
            //                 .ToTable("usergenrepreferences")
            //                 .UseCollation("utf8mb4_unicode_ci");
            //             j.HasIndex(new[] { "GenreId" }, "GenreId");
            //         });

            // entity.HasMany(d => d.Roles).WithMany(p => p.Users)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "Userrole",
            //         r => r.HasOne<Role>().WithMany()
            //             .HasForeignKey("RoleId")
            //             .HasConstraintName("userroles_ibfk_2"),
            //         l => l.HasOne<User>().WithMany()
            //             .HasForeignKey("UserId")
            //             .HasConstraintName("userroles_ibfk_1"),
            //         j =>
            //         {
            //             j.HasKey("UserId", "RoleId")
            //                 .HasName("PRIMARY")
            //                 .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
            //             j
            //                 .ToTable("userroles")
            //                 .UseCollation("utf8mb4_unicode_ci");
            //             j.HasIndex(new[] { "RoleId" }, "RoleId");
            //         });
        });

        modelBuilder.Entity<UserCoin>(entity =>
        {
            entity.ToTable("UserCoins");

            entity.HasKey(e => e.UserId);

            entity.Property(e => e.Balance)
                .HasDefaultValue(0);

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.User)
                .WithOne(p => p.UserCoin)
                .HasForeignKey<UserCoin>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserGenre>(entity =>
{
    entity.ToTable("usergenrepreferences");

    entity.HasKey(e => new { e.UserId, e.GenreId });

    entity.HasOne(e => e.User)
        .WithMany(u => u.UserGenres)
        .HasForeignKey(e => e.UserId);

    entity.HasOne(e => e.Genre)
        .WithMany(g => g.UserGenres)
        .HasForeignKey(e => e.GenreId);
});

        modelBuilder.Entity<Userpromotion>(entity =>
        {
            entity.HasKey(e => e.UserPromotionId).HasName("PRIMARY");

            entity
                .ToTable("userpromotions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.PromotionId, "PromotionId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.UsedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Userpromotions)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("userpromotions_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Userpromotions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userpromotions_ibfk_1");
        });

        modelBuilder.Entity<UserReward>(entity =>
{
    entity.HasKey(e => e.UserRewardId);

    entity.Property(e => e.RewardType)
        .HasMaxLength(50)
        .IsRequired();

    entity.Property(e => e.CoinAmount)
        .HasDefaultValue(0);

    entity.Property(e => e.CreatedAt)
        .HasDefaultValueSql("CURRENT_TIMESTAMP");

    entity.HasOne(d => d.User)
        .WithMany(p => p.UserRewards)
        .HasForeignKey(d => d.UserId);
});

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("userroles");

            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasOne(e => e.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(e => e.RoleId);
        });

        modelBuilder.Entity<Writerprofile>(entity =>
        {
            entity.HasKey(e => e.WriterProfileId).HasName("PRIMARY");

            entity
                .ToTable("writerprofiles")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "UserId").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.BankAccountName).HasMaxLength(200);
            entity.Property(e => e.BankAccountNo).HasMaxLength(20);
            entity.Property(e => e.BankName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.NationalId).HasMaxLength(20);
            entity.Property(e => e.VerifiedAt).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithOne(p => p.Writerprofile)
                .HasForeignKey<Writerprofile>(d => d.UserId)
                .HasConstraintName("writerprofiles_ibfk_1");
        });

        modelBuilder.Entity<Writerstat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PRIMARY");

            entity
                .ToTable("writerstats")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.MangaId, "MangaId");

            entity.HasIndex(e => e.NovelId, "NovelId");

            entity.HasIndex(e => new { e.UserId, e.MangaId }, "UQ_WriterStats_Manga").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.NovelId }, "UQ_WriterStats_Novel").IsUnique();

            entity.Property(e => e.ContentType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'novel'");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Manga).WithMany(p => p.Writerstats)
                .HasForeignKey(d => d.MangaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("writerstats_ibfk_3");

            entity.HasOne(d => d.Novel).WithMany(p => p.Writerstats)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("writerstats_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Writerstats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("writerstats_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
