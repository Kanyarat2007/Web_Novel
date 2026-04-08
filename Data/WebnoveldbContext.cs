using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web_Novel.Data;

public partial class WebnoveldbContext : DbContext
{
    public WebnoveldbContext(DbContextOptions<WebnoveldbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
