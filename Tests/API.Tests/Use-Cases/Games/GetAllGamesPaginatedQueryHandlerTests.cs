using API.Data;
using API.Use_Cases.Games.Handlers;
using API.Use_Cases.Games.Queries;
using Domain.Entities;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace Tests.API.Tests.Use_Cases.Games;

public class GetAllGamesPaginatedQueryHandlerTests
{
    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();

    private readonly AppDbContext _dbContext;

    private readonly List<ListModel<Game>> _expectedGameListModels = [];

    private readonly GetAllGamesPaginatedQueryHandler _handler;
    private readonly int _itemsPerPage = 3;
    private readonly int _itemsPerPageThatExceedsBounds = 5;
    private readonly SqliteConnection _sqliteConnection;

    public GetAllGamesPaginatedQueryHandlerTests()
    {
        _sqliteConnection = new SqliteConnection("Filename=:memory:");
        _sqliteConnection.Open();

        var contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_sqliteConnection).Options;

        _configuration["Application:ItemsPerPage"].Returns(_itemsPerPage.ToString());

        _dbContext = new AppDbContext(contextOptions);

        _handler = new GetAllGamesPaginatedQueryHandler(_dbContext, _configuration);

        _dbContext.Database.EnsureCreated();

        _dbContext.AddRange(new Game
        {
            Id = 1,
            Name = "Game 1",
            Description = "Game 1 Description"
        }, new Game
        {
            Id = 2,
            Name = "Genre 2",
            Description = "Game 2 Description"
        }, new Game
        {
            Id = 3,
            Name = "Genre 3",
            Description = "Genre 3 Description"
        }, new Game
        {
            Id = 4,
            Name = "Genre 4",
            Description = "Genre 4 Description"
        }, new Game
        {
            Id = 5,
            Name = "Genre 5",
            Description = "Genre 5 Description"
        }, new Game
        {
            Id = 6,
            Name = "Genre 6",
            Description = "Genre 6 Description"
        }, new Game
        {
            Id = 7,
            Name = "Genre 7",
            Description = "Genre 7 Description"
        });

        _dbContext.SaveChanges();

        _expectedGameListModels.AddRange(
            new ListModel<Game>
            {
                CurrentPage = 1,
                TotalPages = 3,
                Items = _dbContext.Games.Take(_itemsPerPage).ToList()
            },
            new ListModel<Game>
            {
                CurrentPage = 2,
                TotalPages = 3,
                Items = _dbContext.Games.Skip(_itemsPerPage).Take(_itemsPerPage).ToList()
            },
            new ListModel<Game>
            {
                CurrentPage = 3,
                TotalPages = 3,
                Items = _dbContext.Games.Skip(2 * _itemsPerPage).Take(_itemsPerPage).ToList()
            });
    }

    [Fact]
    public async Task ReturnsFirstThreeGamesWhenDefaultParameters()
    {
        // arrange

        var request = new GetAllGamesPaginatedQuery(1, null);

        // act

        var response = await _handler.Handle(request, CancellationToken.None);

        // assert

        Assert.True(response.Successful);

        var data = response.Data;

        Assert.NotNull(data);

        Assert.Equal(_expectedGameListModels[0].CurrentPage, data.CurrentPage);
        Assert.Equal(_expectedGameListModels[0].TotalPages, data.TotalPages);
        Assert.Equal(_expectedGameListModels[0].Items, data.Items);
    }

    [Fact]
    public async Task ReturnsSecondThreeGamesWhenChosenSecondPage()
    {
        // arrange

        var request = new GetAllGamesPaginatedQuery(2, null);

        // act

        var response = await _handler.Handle(request, CancellationToken.None);

        // assert

        Assert.True(response.Successful);

        var data = response.Data;

        Assert.NotNull(data);

        Assert.Equal(_expectedGameListModels[1].CurrentPage, data.CurrentPage);
        Assert.Equal(_expectedGameListModels[1].TotalPages, data.TotalPages);
        Assert.Equal(_expectedGameListModels[1].Items, data.Items);
    }

    [Fact]
    public async Task ReturnsLastGameWhenChosenThirdPage()
    {
        // arrange

        var request = new GetAllGamesPaginatedQuery(3, null);

        // act

        var response = await _handler.Handle(request, CancellationToken.None);

        // assert

        Assert.True(response.Successful);

        var data = response.Data;

        Assert.NotNull(data);

        Assert.Equal(_expectedGameListModels[2].CurrentPage, data.CurrentPage);
        Assert.Equal(_expectedGameListModels[2].TotalPages, data.TotalPages);
        Assert.Equal(_expectedGameListModels[2].Items, data.Items);
    }

    [Fact]
    public async Task ReturnsThreeGamesWhenChosenMoreThanThreeGames()
    {
        // arrange

        var request = new GetAllGamesPaginatedQuery(2, _itemsPerPageThatExceedsBounds);

        // act

        var response = await _handler.Handle(request, CancellationToken.None);

        // assert

        Assert.True(response.Successful);

        var data = response.Data;

        Assert.NotNull(data);

        Assert.Equal(_expectedGameListModels[1].CurrentPage, data.CurrentPage);
        Assert.Equal(_expectedGameListModels[1].TotalPages, data.TotalPages);
        Assert.Equal(_expectedGameListModels[1].Items, data.Items);
    }

    [Fact]
    public async Task ReturnsFailWhenChosenNonExistentPage()
    {
        // arrange

        var request = new GetAllGamesPaginatedQuery(4, _itemsPerPageThatExceedsBounds);

        // act

        var response = await _handler.Handle(request, CancellationToken.None);

        // assert

        Assert.False(response.Successful);
    }
}