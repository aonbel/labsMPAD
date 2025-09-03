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
            ImagePath = "../images/FPSGAME.jpg"
        },

        new Game
        {
            Id = 2,
            GenreId = 2,
            Price = 12.0m,
            Description = "HORROR GAME",
            Name = "HORROR GAME",
            ImagePath = "../images/HORRORGAME.jpg"
        },

        new Game
        {
            Id = 3,
            GenreId = 3,
            Price = 13.0m,
            Description = "ADVENTURE GAME",
            Name = "ADVENTURE GAME",
            ImagePath = "../images/ADVENTUREGAME.jpg"
        },

        new Game
        {
            Id = 4,
            GenreId = 4,
            Price = 14.0m,
            Description = "SURVIVAL GAME",
            Name = "SURVIVAL GAME",
            ImagePath = "../images/SURVIVALGAME.jpg"
        },

        new Game
        {
            Id = 5,
            GenreId = 5,
            Price = 15.0m,
            Description = "SANDBOX GAME 1",
            Name = "SANDBOX GAME 1",
            ImagePath = "../images/SANDBOXGAME.jpg"
        },

        new Game
        {
            Id = 6,
            GenreId = 5,
            Price = 16.0m,
            Description = "SANDBOX GAME 2",
            Name = "SANDBOX GAME 2",
            ImagePath = "../images/SANDBOXGAME.jpg"
        },

        new Game
        {
            Id = 7,
            GenreId = 5,
            Price = 17.0m,
            Description = "SANDBOX GAME 3",
            Name = "SANDBOX GAME 3",
            ImagePath = "../images/SANDBOXGAME.jpg"
        },

        new Game
        {
            Id = 8,
            GenreId = 5,
            Price = 18.0m,
            Description = "SANDBOX GAME 4",
            Name = "SANDBOX GAME 4",
            ImagePath = "../images/SANDBOXGAME.jpg"
        }
    ];

    public Task<ResponseData<List<Game>>> GetAllAsync()
    {
        return Task.FromResult(ResponseData<List<Game>>.Success(_games));
    }

    public Task<ResponseData<ListModel<Game>>> GetByPageAsync(int pageNumber = 1, int pageSize = 10)
    {
        var result = new ListModel<Game>
        {
            Items = _games.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
            CurrentPage = pageNumber,
            TotalPages = (_games.Count + pageSize - 1) / pageSize
        };

        return Task.FromResult(ResponseData<ListModel<Game>>.Success(result));
    }

    public Task<ResponseData<ListModel<Game>>> GetByFilterAndPageAsync(Func<Game, bool> predicate, int pageNumber = 1,
        int pageSize = 10)
    {
        var result = new ListModel<Game>
        {
            Items = _games.Where(predicate).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList(),
            CurrentPage = pageNumber,
            TotalPages = (_games.Where(predicate).Count() + pageSize - 1) / pageSize
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