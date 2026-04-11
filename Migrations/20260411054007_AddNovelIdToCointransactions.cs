using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Novel.Migrations
{
    /// <inheritdoc />
    public partial class AddNovelIdToCointransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "coinpackages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CoinAmount = table.Column<int>(type: "int", nullable: false),
                    BonusCoin = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PackageId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GenreId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountValue = table.Column<float>(type: "float", nullable: false),
                    BonusCoin = table.Column<int>(type: "int", nullable: false),
                    MaxUsageTotal = table.Column<int>(type: "int", nullable: true),
                    MaxUsagePerUser = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'1'"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PromotionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "TriggerWarnings",
                columns: table => new
                {
                    WarningId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerWarnings", x => x.WarningId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bio = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoinBalance = table.Column<int>(type: "int", nullable: false),
                    IsWriter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    LastLoginAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "blocklist",
                columns: table => new
                {
                    BlockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlockerUserId = table.Column<int>(type: "int", nullable: false),
                    BlockedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.BlockId);
                    table.ForeignKey(
                        name: "blocklist_ibfk_1",
                        column: x => x.BlockerUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "blocklist_ibfk_2",
                        column: x => x.BlockedUserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "loginstreaks",
                columns: table => new
                {
                    StreakId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastLoginDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CurrentStreak = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'1'"),
                    LongestStreak = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'1'"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.StreakId);
                    table.ForeignKey(
                        name: "loginstreaks_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "moderationlogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModeratorUserId = table.Column<int>(type: "int", nullable: false),
                    TargetType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.LogId);
                    table.ForeignKey(
                        name: "moderationlogs_ibfk_1",
                        column: x => x.ModeratorUserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_coinpackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "coinpackages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pennames",
                columns: table => new
                {
                    PenNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PenName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bio = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AvatarUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PenNameId);
                    table.ForeignKey(
                        name: "pennames_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReporterUserId = table.Column<int>(type: "int", nullable: false),
                    TargetType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'pending'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ReportId);
                    table.ForeignKey(
                        name: "reports_ibfk_1",
                        column: x => x.ReporterUserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "spinlogs",
                columns: table => new
                {
                    SpinLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: true),
                    RewardType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RewardValue = table.Column<int>(type: "int", nullable: false),
                    SpinAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.SpinLogId);
                    table.ForeignKey(
                        name: "spinlogs_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "spinlogs_ibfk_2",
                        column: x => x.PromotionId,
                        principalTable: "promotions",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "UserCoins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCoins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCoins_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "usergenrepreferences",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.UserId, x.GenreId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "usergenrepreferences_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "usergenrepreferences_ibfk_2",
                        column: x => x.GenreId,
                        principalTable: "genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "userpromotions",
                columns: table => new
                {
                    UserPromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UserPromotionId);
                    table.ForeignKey(
                        name: "userpromotions_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "userpromotions_ibfk_2",
                        column: x => x.PromotionId,
                        principalTable: "promotions",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "userroles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.UserId, x.RoleId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "userroles_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userroles_ibfk_2",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "writerprofiles",
                columns: table => new
                {
                    WriterProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NationalId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankAccountNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankAccountName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.WriterProfileId);
                    table.ForeignKey(
                        name: "writerprofiles_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "mangas",
                columns: table => new
                {
                    MangaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AuthorUserId = table.Column<int>(type: "int", nullable: false),
                    PenNameId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Synopsis = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoverImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeriesType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'series'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'ongoing'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublishStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'draft'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCoinRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CoinPrice = table.Column<int>(type: "int", nullable: false),
                    ReadDirection = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValueSql: "'ltr'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalViews = table.Column<int>(type: "int", nullable: false),
                    TotalLikes = table.Column<int>(type: "int", nullable: false),
                    TotalBookshelf = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MangaId);
                    table.ForeignKey(
                        name: "mangas_ibfk_1",
                        column: x => x.AuthorUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "mangas_ibfk_2",
                        column: x => x.PenNameId,
                        principalTable: "pennames",
                        principalColumn: "PenNameId",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "novels",
                columns: table => new
                {
                    NovelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AuthorUserId = table.Column<int>(type: "int", nullable: false),
                    PenNameId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Synopsis = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorNote = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoverImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeriesType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'series'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false, defaultValueSql: "'ongoing'"),
                    PublishStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'draft'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCoinRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CoinPrice = table.Column<int>(type: "int", nullable: false),
                    TotalViews = table.Column<int>(type: "int", nullable: false),
                    TotalLikes = table.Column<int>(type: "int", nullable: false),
                    TotalBookshelf = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NovelId);
                    table.ForeignKey(
                        name: "novels_ibfk_1",
                        column: x => x.AuthorUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "novels_ibfk_2",
                        column: x => x.PenNameId,
                        principalTable: "pennames",
                        principalColumn: "PenNameId",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "mangachapters",
                columns: table => new
                {
                    MangaChapterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    ChapterNumber = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCoinRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CoinPrice = table.Column<int>(type: "int", nullable: false),
                    PublishStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'draft'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MangaChapterId);
                    table.ForeignKey(
                        name: "mangachapters_ibfk_1",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "mangagenres",
                columns: table => new
                {
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.MangaId, x.GenreId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "mangagenres_ibfk_1",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "mangagenres_ibfk_2",
                        column: x => x.GenreId,
                        principalTable: "genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "mangalikes",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.UserId, x.MangaId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "mangalikes_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "mangalikes_ibfk_2",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "bookshelves",
                columns: table => new
                {
                    BookshelfId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValueSql: "'novel'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NovelId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.BookshelfId);
                    table.ForeignKey(
                        name: "bookshelves_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "bookshelves_ibfk_2",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "bookshelves_ibfk_3",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "chapters",
                columns: table => new
                {
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NovelId = table.Column<int>(type: "int", nullable: false),
                    ChapterNumber = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCoinRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CoinPrice = table.Column<int>(type: "int", nullable: false),
                    PublishStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'draft'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ChapterId);
                    table.ForeignKey(
                        name: "chapters_ibfk_1",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "cointransactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NovelId = table.Column<int>(type: "int", nullable: true),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BalanceBefore = table.Column<int>(type: "int", nullable: false),
                    BalanceAfter = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TransactionId);
                    table.ForeignKey(
                        name: "cointransactions_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "cointransactions_ibfk_2",
                        column: x => x.PackageId,
                        principalTable: "coinpackages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "cointransactions_ibfk_novel",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "novellikes",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NovelId = table.Column<int>(type: "int", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.UserId, x.NovelId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "novellikes_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "novellikes_ibfk_2",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "NovelTriggerWarnings",
                columns: table => new
                {
                    NovelId = table.Column<int>(type: "int", nullable: false),
                    WarningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovelTriggerWarnings", x => new { x.NovelId, x.WarningId });
                    table.ForeignKey(
                        name: "FK_NovelTriggerWarnings_TriggerWarnings_WarningId",
                        column: x => x.WarningId,
                        principalTable: "TriggerWarnings",
                        principalColumn: "WarningId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovelTriggerWarnings_novels_NovelId",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "writerstats",
                columns: table => new
                {
                    StatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValueSql: "'novel'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NovelId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    TotalViews = table.Column<int>(type: "int", nullable: false),
                    TotalLikes = table.Column<int>(type: "int", nullable: false),
                    TotalBookshelf = table.Column<int>(type: "int", nullable: false),
                    TotalCoinEarned = table.Column<int>(type: "int", nullable: false),
                    TotalChaptersSold = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.StatId);
                    table.ForeignKey(
                        name: "writerstats_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "writerstats_ibfk_2",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "writerstats_ibfk_3",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "mangapages",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MangaChapterId = table.Column<int>(type: "int", nullable: false),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PageId);
                    table.ForeignKey(
                        name: "mangapages_ibfk_1",
                        column: x => x.MangaChapterId,
                        principalTable: "mangachapters",
                        principalColumn: "MangaChapterId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "chapterbookmarks",
                columns: table => new
                {
                    BookmarkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NovelId = table.Column<int>(type: "int", nullable: true),
                    ChapterId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MangaChapterId = table.Column<int>(type: "int", nullable: true),
                    BookmarkedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.BookmarkId);
                    table.ForeignKey(
                        name: "chapterbookmarks_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chapterbookmarks_ibfk_2",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chapterbookmarks_ibfk_3",
                        column: x => x.ChapterId,
                        principalTable: "chapters",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chapterbookmarks_ibfk_4",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chapterbookmarks_ibfk_5",
                        column: x => x.MangaChapterId,
                        principalTable: "mangachapters",
                        principalColumn: "MangaChapterId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NovelId = table.Column<int>(type: "int", nullable: true),
                    ChapterId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MangaChapterId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsHidden = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSpoiler = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.CommentId);
                    table.ForeignKey(
                        name: "comments_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "comments_ibfk_2",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "comments_ibfk_3",
                        column: x => x.ChapterId,
                        principalTable: "chapters",
                        principalColumn: "ChapterId");
                    table.ForeignKey(
                        name: "comments_ibfk_4",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "comments_ibfk_5",
                        column: x => x.MangaChapterId,
                        principalTable: "mangachapters",
                        principalColumn: "MangaChapterId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "novelnotes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: false),
                    NoteContent = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NoteId);
                    table.ForeignKey(
                        name: "novelnotes_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "novelnotes_ibfk_2",
                        column: x => x.ChapterId,
                        principalTable: "chapters",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "novelpurchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValueSql: "'novel'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NovelId = table.Column<int>(type: "int", nullable: true),
                    ChapterId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MangaChapterId = table.Column<int>(type: "int", nullable: true),
                    CoinSpent = table.Column<int>(type: "int", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "novelpurchases_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "novelpurchases_ibfk_2",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId");
                    table.ForeignKey(
                        name: "novelpurchases_ibfk_3",
                        column: x => x.ChapterId,
                        principalTable: "chapters",
                        principalColumn: "ChapterId");
                    table.ForeignKey(
                        name: "novelpurchases_ibfk_4",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId");
                    table.ForeignKey(
                        name: "novelpurchases_ibfk_5",
                        column: x => x.MangaChapterId,
                        principalTable: "mangachapters",
                        principalColumn: "MangaChapterId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "readinghistory",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValueSql: "'novel'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NovelId = table.Column<int>(type: "int", nullable: true),
                    LastChapterId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    LastMangaChapterId = table.Column<int>(type: "int", nullable: true),
                    LastReadAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.HistoryId);
                    table.ForeignKey(
                        name: "readinghistory_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "readinghistory_ibfk_2",
                        column: x => x.NovelId,
                        principalTable: "novels",
                        principalColumn: "NovelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "readinghistory_ibfk_3",
                        column: x => x.LastChapterId,
                        principalTable: "chapters",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "readinghistory_ibfk_4",
                        column: x => x.MangaId,
                        principalTable: "mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "readinghistory_ibfk_5",
                        column: x => x.LastMangaChapterId,
                        principalTable: "mangachapters",
                        principalColumn: "MangaChapterId",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateIndex(
                name: "BlockedUserId",
                table: "blocklist",
                column: "BlockedUserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Block",
                table: "blocklist",
                columns: new[] { "BlockerUserId", "BlockedUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "MangaId",
                table: "bookshelves",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "NovelId",
                table: "bookshelves",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "UQ_Bookshelf_Manga",
                table: "bookshelves",
                columns: new[] { "UserId", "MangaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Bookshelf_Novel",
                table: "bookshelves",
                columns: new[] { "UserId", "NovelId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ChapterId",
                table: "chapterbookmarks",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "MangaChapterId",
                table: "chapterbookmarks",
                column: "MangaChapterId");

            migrationBuilder.CreateIndex(
                name: "MangaId1",
                table: "chapterbookmarks",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "NovelId1",
                table: "chapterbookmarks",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "UserId",
                table: "chapterbookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Chapter",
                table: "chapters",
                columns: new[] { "NovelId", "ChapterNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "NovelId2",
                table: "cointransactions",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "PackageId",
                table: "cointransactions",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "UserId1",
                table: "cointransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ChapterId1",
                table: "comments",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "MangaChapterId1",
                table: "comments",
                column: "MangaChapterId");

            migrationBuilder.CreateIndex(
                name: "MangaId2",
                table: "comments",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "NovelId3",
                table: "comments",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "UserId2",
                table: "comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "Name",
                table: "genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Slug",
                table: "genres",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserId3",
                table: "loginstreaks",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_MangaChapter",
                table: "mangachapters",
                columns: new[] { "MangaId", "ChapterNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "GenreId",
                table: "mangagenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "MangaId3",
                table: "mangalikes",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "UQ_MangaPage",
                table: "mangapages",
                columns: new[] { "MangaChapterId", "PageNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AuthorUserId",
                table: "mangas",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "PenNameId",
                table: "mangas",
                column: "PenNameId");

            migrationBuilder.CreateIndex(
                name: "Slug1",
                table: "mangas",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ModeratorUserId",
                table: "moderationlogs",
                column: "ModeratorUserId");

            migrationBuilder.CreateIndex(
                name: "NovelId4",
                table: "novellikes",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "ChapterId2",
                table: "novelnotes",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "UserId4",
                table: "novelnotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ChapterId3",
                table: "novelpurchases",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "MangaChapterId2",
                table: "novelpurchases",
                column: "MangaChapterId");

            migrationBuilder.CreateIndex(
                name: "MangaId4",
                table: "novelpurchases",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "NovelId5",
                table: "novelpurchases",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "UserId5",
                table: "novelpurchases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "AuthorUserId1",
                table: "novels",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "PenNameId1",
                table: "novels",
                column: "PenNameId");

            migrationBuilder.CreateIndex(
                name: "Slug2",
                table: "novels",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NovelTriggerWarnings_WarningId",
                table: "NovelTriggerWarnings",
                column: "WarningId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PackageId",
                table: "Payments",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UserId6",
                table: "pennames",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "LastChapterId",
                table: "readinghistory",
                column: "LastChapterId");

            migrationBuilder.CreateIndex(
                name: "LastMangaChapterId",
                table: "readinghistory",
                column: "LastMangaChapterId");

            migrationBuilder.CreateIndex(
                name: "MangaId5",
                table: "readinghistory",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "NovelId6",
                table: "readinghistory",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "UQ_History_Manga",
                table: "readinghistory",
                columns: new[] { "UserId", "MangaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_History_Novel",
                table: "readinghistory",
                columns: new[] { "UserId", "NovelId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ReporterUserId",
                table: "reports",
                column: "ReporterUserId");

            migrationBuilder.CreateIndex(
                name: "RoleName",
                table: "roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PromotionId",
                table: "spinlogs",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "UserId7",
                table: "spinlogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "GenreId1",
                table: "usergenrepreferences",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "PromotionId1",
                table: "userpromotions",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "UserId8",
                table: "userpromotions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleId",
                table: "userroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Username",
                table: "users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserId9",
                table: "writerprofiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "MangaId6",
                table: "writerstats",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "NovelId7",
                table: "writerstats",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "UQ_WriterStats_Manga",
                table: "writerstats",
                columns: new[] { "UserId", "MangaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_WriterStats_Novel",
                table: "writerstats",
                columns: new[] { "UserId", "NovelId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blocklist");

            migrationBuilder.DropTable(
                name: "bookshelves");

            migrationBuilder.DropTable(
                name: "chapterbookmarks");

            migrationBuilder.DropTable(
                name: "cointransactions");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "loginstreaks");

            migrationBuilder.DropTable(
                name: "mangagenres");

            migrationBuilder.DropTable(
                name: "mangalikes");

            migrationBuilder.DropTable(
                name: "mangapages");

            migrationBuilder.DropTable(
                name: "moderationlogs");

            migrationBuilder.DropTable(
                name: "novellikes");

            migrationBuilder.DropTable(
                name: "novelnotes");

            migrationBuilder.DropTable(
                name: "novelpurchases");

            migrationBuilder.DropTable(
                name: "NovelTriggerWarnings");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "readinghistory");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "spinlogs");

            migrationBuilder.DropTable(
                name: "UserCoins");

            migrationBuilder.DropTable(
                name: "usergenrepreferences");

            migrationBuilder.DropTable(
                name: "userpromotions");

            migrationBuilder.DropTable(
                name: "userroles");

            migrationBuilder.DropTable(
                name: "writerprofiles");

            migrationBuilder.DropTable(
                name: "writerstats");

            migrationBuilder.DropTable(
                name: "TriggerWarnings");

            migrationBuilder.DropTable(
                name: "coinpackages");

            migrationBuilder.DropTable(
                name: "chapters");

            migrationBuilder.DropTable(
                name: "mangachapters");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "novels");

            migrationBuilder.DropTable(
                name: "mangas");

            migrationBuilder.DropTable(
                name: "pennames");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
