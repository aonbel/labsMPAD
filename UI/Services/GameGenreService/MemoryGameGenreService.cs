using Domain.Models;

namespace UI.Services.GameGenreService;

public class MemoryGameGenreService : IGameGenreService
{
    public Task<ResponseData<List<GameGenre>>> GetAllAsync()
    {
        var result = new List<GameGenre>
        {
            new GameGenre { Id = 1, Name = "FPS", NormalizedName = "fps"},
            new GameGenre { Id = 2, Name = "Horror", NormalizedName = "horror"},
            new GameGenre { Id = 3, Name = "Adventure", NormalizedName = "adventure"},
            new GameGenre { Id = 4, Name = "Survival", NormalizedName = "survival"},
            new GameGenre { Id = 5, Name = "Sandbox", NormalizedName = "sandbox"},
        };

        return Task.FromResult(ResponseData<List<GameGenre>>.Success(result));
    }
}