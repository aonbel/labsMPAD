using Domain.Models;

namespace UI.Services.GameGenreService;

public class MemoryGameGenreService : IGameGenreService
{
    private readonly List<GameGenre> _gameGenres =
    [
        new GameGenre { Id = 1, Name = "FPS", NormalizedName = "fps" },
        new GameGenre { Id = 2, Name = "Horror", NormalizedName = "horror" },
        new GameGenre { Id = 3, Name = "Adventure", NormalizedName = "adventure" },
        new GameGenre { Id = 4, Name = "Survival", NormalizedName = "survival" },
        new GameGenre { Id = 5, Name = "Sandbox", NormalizedName = "sandbox" }
    ];
    
    public Task<ResponseData<List<GameGenre>>> GetAllAsync()
    {
        return Task.FromResult(ResponseData<List<GameGenre>>.Success(_gameGenres));
    }

    public Task<ResponseData<GameGenre>> GetByIdAsync(int id)
    {
        var gameGenre = _gameGenres.FirstOrDefault(g => g.Id == id);

        if (gameGenre is null)
        {
            return Task.FromResult(ResponseData<GameGenre>.Fail($"Game genre with id {id} does not exist"));
        }
        
        return Task.FromResult(ResponseData<GameGenre>.Success(gameGenre));
    }
}