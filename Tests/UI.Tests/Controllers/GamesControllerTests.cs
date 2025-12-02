using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using UI.Controllers;
using UI.Services.GameGenreService;
using UI.Services.GameService;

namespace Tests.UI.Tests.Controllers;

public class GamesControllerTests
{
    private const int GameGenreIdToTest = 1;
    private const int ExpectedPageNumber = 1;
    private const string ErrorString = "error string we are expecting to see";
    private const int ItemsPerPage = 3;

    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();

    private readonly GameGenre _expectedGameGenre;

    private readonly ListModel<Game> _expectedGamesListModel;

    private readonly IGameGenreService _gameGenreService = Substitute.For<IGameGenreService>();

    private readonly List<GameGenre> _gameGenresList =
    [
        new()
        {
            Id = 1,
            Name = "Genre 1",
            NormalizedName = "Genre 1"
        },
        new()
        {
            Id = 2,
            Name = "Genre 2",
            NormalizedName = "Genre 2"
        },
        new()
        {
            Id = 3,
            Name = "Genre 3",
            NormalizedName = "Genre 3"
        }
    ];

    private readonly GamesController _gamesController;

    private readonly IGameService _gameService = Substitute.For<IGameService>();

    private readonly List<Game> _gamesList =
    [
        new()
        {
            Id = 1,
            Name = "Game 1",
            Description = "Game 1 Description",
            GenreId = 1,
            ImagePath = "Game 1 Image path",
            Price = 1
        },
        new()
        {
            Id = 2,
            Name = "Game 2",
            Description = "Game 2 Description",
            GenreId = 2,
            ImagePath = "Game 2 Image path",
            Price = 2
        },
        new()
        {
            Id = 3,
            Name = "Game 3",
            Description = "Game 3 Description",
            GenreId = 3,
            ImagePath = "Game 3 Image path",
            Price = 3
        },
        new()
        {
            Id = 4,
            Name = "Game of tested game genre",
            Description = "Game of tested game genre Description",
            GenreId = GameGenreIdToTest,
            ImagePath = "Game of tested game genre Image path",
            Price = 4
        },
        new()
        {
            Id = 5,
            Name = "Game of tested game genre",
            Description = "Game of tested game genre Description",
            GenreId = GameGenreIdToTest,
            ImagePath = "Game of tested game genre Image path",
            Price = 5
        },
        new()
        {
            Id = 6,
            Name = "Game of tested game genre",
            Description = "Game of tested game genre Description",
            GenreId = GameGenreIdToTest,
            ImagePath = "Game of tested game genre Image path",
            Price = 6
        },
        new()
        {
            Id = 7,
            Name = "Game of tested game genre",
            Description = "Game of tested game genre Description",
            GenreId = GameGenreIdToTest,
            ImagePath = "Game of tested game genre Image path",
            Price = 7
        }
    ];

    private readonly ControllerContext _mockedControllerContext = new();

    private readonly HttpContext _mockedHttpContext = Substitute.For<HttpContext>();

    private readonly HttpRequest _mockedHttpRequest = Substitute.For<HttpRequest>();

    private readonly ITempDataProvider _mockedTempDataProvider =
        Substitute.For<ITempDataProvider>();

    public GamesControllerTests()
    {
        _gamesController = new GamesController(_gameService, _gameGenreService, _configuration);

        _expectedGamesListModel = new ListModel<Game>
        {
            CurrentPage = ExpectedPageNumber,
            Items = _gamesList.Where(game => game.GenreId == GameGenreIdToTest).Skip(ExpectedPageNumber * ItemsPerPage)
                .Take(ItemsPerPage).ToList(),
            TotalPages =
                (_gamesList.Count(game => game.GenreId == GameGenreIdToTest) + ItemsPerPage - 1) /
                ItemsPerPage
        };

        _configuration["Application:RequestedItemsPerPage"].Returns(ItemsPerPage.ToString());

        _expectedGameGenre = _gameGenresList.First(gameGenre => gameGenre.Id == GameGenreIdToTest);

        _mockedHttpRequest.Headers.Returns(new HeaderDictionary());
        _mockedHttpContext.Request.Returns(_mockedHttpRequest);

        _mockedControllerContext.HttpContext = _mockedHttpContext;
        _gamesController.ControllerContext = _mockedControllerContext;
        _gamesController.TempData = new TempDataDictionary(_mockedHttpContext, _mockedTempDataProvider);
    }

    [Fact]
    public async Task Returns404WhenCantGetGameGenres()
    {
        // arrange

        _gameGenreService.GetAllAsync().Returns(ResponseData<List<GameGenre>>.Fail(ErrorString));

        // act

        var result = await _gamesController.Index(GameGenreIdToTest);

        // assert

        Assert.IsType<NotFoundObjectResult>(result);

        var resultAsNotFound = (result as NotFoundObjectResult)!;

        Assert.Same(ErrorString, resultAsNotFound.Value?.ToString());
    }

    [Fact]
    public async Task Returns404WhenCantGetGames()
    {
        // arrange

        _gameGenreService.GetAllAsync().Returns(ResponseData<List<GameGenre>>.Success(_gameGenresList));
        _gameGenreService.GetByIdAsync(GameGenreIdToTest)
            .Returns(ResponseData<GameGenre>.Success(_expectedGameGenre));
        _gameService.GetByGenreIdAndPageAsync(GameGenreIdToTest, ItemsPerPage)
            .Returns(ResponseData<ListModel<Game>>.Fail(ErrorString));

        // act

        var result = await _gamesController.Index(GameGenreIdToTest);

        // assert

        Assert.IsType<NotFoundObjectResult>(result);

        var resultAsNotFound = (result as NotFoundObjectResult)!;

        Assert.Same(ErrorString, resultAsNotFound.Value?.ToString());
    }

    [Fact]
    public async Task ReturnsGameGenresAndCurrentGameGenreAndPageListWhenCanGetGamesAndGameGenres()
    {
        // arrange

        _gameGenreService.GetAllAsync().Returns(ResponseData<List<GameGenre>>.Success(_gameGenresList));
        _gameGenreService.GetByIdAsync(GameGenreIdToTest)
            .Returns(ResponseData<GameGenre>.Success(_expectedGameGenre));
        _gameService.GetByGenreIdAndPageAsync(GameGenreIdToTest, ItemsPerPage)
            .Returns(ResponseData<ListModel<Game>>.Success(_expectedGamesListModel));

        // act

        var result = await _gamesController.Index(GameGenreIdToTest);

        // assert

        Assert.IsType<ViewResult>(result);

        var resultAsView = (result as ViewResult)!;

        Assert.Same(_gameGenresList, resultAsView.ViewData["GameGenres"]);
        Assert.Same(_expectedGameGenre, resultAsView.ViewData["CurrentGameGenre"]);
        Assert.Same(_expectedGamesListModel, resultAsView.Model);
    }
}