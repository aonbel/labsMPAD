using Domain.Models;

namespace UI.Services.GameService;

public class MemoryGameService : IGameService
{
    private readonly List<Game> _games =
    [
        new Game
        {
            Id = 1,
            GenreId = 1,
            Price = 11.0m,
            Description = "FPS GAME",
            Name = "FPS GAME",
            ImagePath = "../Images/FPSGAME.png"
        },

        new Game
        {
            Id = 2,
            GenreId = 2,
            Price = 12.0m,
            Description = "HORROR GAME",
            Name = "HORROR GAME",
            ImagePath = "../Images/HORRORGAME.png"
        },

        new Game
        {
            Id = 3,
            GenreId = 3,
            Price = 13.0m,
            Description = "ADVENTURE GAME",
            Name = "ADVENTURE GAME",
            ImagePath = "../Images/ADVENTUREGAME.png"
        },

        new Game
        {
            Id = 4,
            GenreId = 4,
            Price = 14.0m,
            Description = "SURVIVAL GAME",
            Name = "SURVIVAL GAME",
            ImagePath = "../Images/SURVIVALGAME.png"
        },

        new Game
        {
            Id = 4,
            GenreId = 4,
            Price = 15.0m,
            Description = "SANDBOX GAME",
            Name = "SANDBOX GAME",
            ImagePath = "../Images/SANDBOXGAME.png"
        }

    ];
    
    public Task<ResponseData<ListModel<Game>>> GetAllAsync()
    {
        return Task.FromResult(ResponseData<ListModel<Game>>.Success(new ListModel<Game>()
        {
            Items = _games
        }));
    }

    public Task<ResponseData<ListModel<Game>>> GetByPageAsync(int pageNumber = 1, int pageSize = 10)
    {
        var result = new ListModel<Game>()
        {
            Items = _games.GetRange(pageSize * (pageNumber - 1), pageSize),
            CurrentPage = pageNumber,
            TotalPages = (_games.Count + pageSize - 1) / pageSize
        };
        
        return Task.FromResult(ResponseData<ListModel<Game>>.Success(result));
    }

    public Task<ResponseData<Game>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Game game)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Game game)
    {
        throw new NotImplementedException();
    }
}