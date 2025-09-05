using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<GameGenre> GameGenres { get; set; }
}