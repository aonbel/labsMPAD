using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace UI.Data;

public class TemporaryDbContext : DbContext
{
    DbSet<Game> Games;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("");
    }

public DbSet<Domain.Entities.Game> Game { get; set; } = default!;
}