using API.Use_Cases.Games.Commands;
using API.Use_Cases.Games.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Endpoints
{
    public static class GameEndpoints
    {
        public static void MapGameEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Games").WithTags(nameof(Game));

            group.MapGet("/", async (ISender sender) =>
                {
                    var query = new GetAllGamesQuery();

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("GetAllGames")
                .Produces<ResponseData<List<Game>>>();

            group.MapGet("/{id:int}", async (int id, ISender sender) =>
                {
                    var query = new GetGameByIdQuery(id);

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("GetGame")
                .Produces<ResponseData<Game>>()
                .Produces<ResponseData<Game>>(StatusCodes.Status404NotFound);
            
            group.MapGet("/Page/{page:int}/OfSize/{pageSize:int}",
                    async (int page, int pageSize, ISender sender) =>
                    {
                        var query = new GetAllGamesPaginatedQuery(page, pageSize);

                        var response = await sender.Send(query);

                        return response;
                    })
                .WithName("GetGamesPaginated")
                .Produces<ResponseData<ListModel<Game>>>();

            group.MapGet("Genre/{genreId:int}/Page/{page:int}/OfSize/{pageSize:int}",
                    async (int genreId, int page, int pageSize, ISender sender) =>
                    {
                        var query = new GetGamesByFilterPaginatedQuery(g => g.GenreId == genreId, page, pageSize);

                        var response = await sender.Send(query);

                        return response;
                    })
                .WithName("GetGamesByGenrePaginated")
                .Produces<ResponseData<ListModel<Game>>>();

            group.MapPost("/", async (Game game, ISender sender) =>
                {
                    var query = new CreateGameCommand(game);

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("CreateGame")
                .Produces<ResponseData<Game>>(StatusCodes.Status201Created);

            group.MapPut("/{id:int}", async (int id, Game game, ISender sender) =>
                {
                    game.Id = id;

                    var query = new UpdateGameByIdCommand(game);

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("UpdateGame")
                .Produces(StatusCodes.Status204NoContent)
                .Produces<ResponseData<Game>>(StatusCodes.Status404NotFound);

            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
                {
                    var query = new DeleteGameByIdCommand(id);

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("DeleteGame")
                .Produces<ResponseData<Game>>()
                .Produces<ResponseData<Game>>(StatusCodes.Status404NotFound);
        }
    }
}