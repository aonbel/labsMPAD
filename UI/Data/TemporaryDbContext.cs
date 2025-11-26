using Microsoft.EntityFrameworkCore;

namespace UI.Data;

public class TemporaryDbContext : DbContext
{
    private DbSet<Game> Games;

    public DbSet<Game> Game { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("");
    }
}