using Domain.Entities;

namespace API.Data;

public static class DbInitializer
{
    public static async Task SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.EnsureCreatedAsync();

        if (!dbContext.GameGenres.Any())
        {
            dbContext.GameGenres.AddRange(new GameGenre { Name = "FPS", NormalizedName = "fps" },
                new GameGenre { Name = "Horror", NormalizedName = "horror" },
                new GameGenre { Name = "Adventure", NormalizedName = "adventure" },
                new GameGenre { Name = "Survival", NormalizedName = "survival" },
                new GameGenre { Name = "Sandbox", NormalizedName = "sandbox" });

            await dbContext.SaveChangesAsync();
        }

        var applicationUrl = app.Configuration["ApplicationUrl"]!;

        if (!dbContext.Games.Any())
        {
            dbContext.Games.AddRange(
                new Game
                {
                    GenreId = dbContext.GameGenres.First(g => g.NormalizedName == "fps").Id,
                    Price = 11.0m,
                    Description = "FPS GAME",
                    Name = "FPS GAME",
                    ImagePath = $"{applicationUrl}/images/FPSGAME.jpg"
                },
                new Game
                {
                    GenreId = dbContext.GameGenres.First(g => g.NormalizedName == "horror").Id,
                    Price = 12.0m,
                    Description = "HORROR GAME",
                    Name = "HORROR GAME",
                    ImagePath = $"{applicationUrl}/images/HORRORGAME.jpg"
                },
                new Game
                {
                    GenreId = dbContext.GameGenres.First(g => g.NormalizedName == "adventure").Id,
                    Price = 13.0m,
                    Description = "ADVENTURE GAME",
                    Name = "ADVENTURE GAME",
                    ImagePath = $"{applicationUrl}/images/ADVENTUREGAME.jpg"
                },
                new Game
                {
                    GenreId = dbContext.GameGenres.First(g => g.NormalizedName == "survival").Id,
                    Price = 14.0m,
                    Description = "SURVIVAL GAME",
                    Name = "SURVIVAL GAME",
                    ImagePath = $"{applicationUrl}/images/SURVIVALGAME.jpg"
                },
                new Game
                {
                    GenreId = dbContext.GameGenres.First(g => g.NormalizedName == "sandbox").Id,
                    Price = 15.0m,
                    Description = "SANDBOX GAME 1",
                    Name = "SANDBOX GAME 1",
                    ImagePath = $"{applicationUrl}/images/SANDBOXGAME.jpg"
                },
                new Game
                {
                    GenreId = dbContext.GameGenres.First(g => g.NormalizedName == "sandbox").Id,
                    Price = 16.0m,
                    Description = "SANDBOX GAME 2",
                    Name = "SANDBOX GAME 2",
                    ImagePath = $"{applicationUrl}/images/SANDBOXGAME.jpg"
                },
                new Game
                {
                    GenreId = dbContext.GameGenres.First(g => g.NormalizedName == "sandbox").Id,
                    Price = 17.0m,
                    Description = "SANDBOX GAME 3",
                    Name = "SANDBOX GAME 3",
                    ImagePath = $"{applicationUrl}/images/SANDBOXGAME.jpg"
                },
                new Game
                {
                    GenreId = dbContext.GameGenres.First(g => g.NormalizedName == "sandbox").Id,
                    Price = 18.0m,
                    Description = "SANDBOX GAME 4",
                    Name = "SANDBOX GAME 4",
                    ImagePath = $"{applicationUrl}/images/SANDBOXGAME.jpg"
                }
            );

            await dbContext.SaveChangesAsync();
        }
    }
}