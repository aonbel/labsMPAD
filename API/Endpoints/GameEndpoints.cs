using System.Text.Json;
using API.Use_Cases.Games.Commands;
using API.Use_Cases.Games.Queries;
using API.Use_Cases.Media.Commands;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class GameEndpoints
    {
        public static void MapGameEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Games").WithTags(nameof(Game)).DisableAntiforgery();

            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                var query = new GetGameByIdQuery(id);
                
                var response = await sender.Send(query);
                
                return response;
            })
                .WithName("GetGameById")
                .Produces<ResponseData<Game>>()
                .AllowAnonymous();

            group.MapGet("/", async (ISender sender) =>
                {
                    var query = new GetAllGamesQuery();

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("GetAllGames")
                .Produces<ResponseData<List<Game>>>()
                .AllowAnonymous();

            group.MapGet("/Paginated/",
                    async (ISender sender, [FromQuery] int? genreId = null, [FromQuery] int page = 1,
                        [FromQuery] int pageSize = 5) =>
                    {
                        ResponseData<ListModel<Game>> response;

                        if (genreId is null)
                        {
                            var query = new GetAllGamesPaginatedQuery(page, pageSize);

                            response = await sender.Send(query);
                        }
                        else
                        {
                            var query = new GetGamesByFilterPaginatedQuery(g => g.GenreId == genreId, page, pageSize);

                            response = await sender.Send(query);
                        }

                        return response;
                    })
                .WithName("GetGamesByGenrePaginated")
                .Produces<ResponseData<ListModel<Game>>>()
                .AllowAnonymous();

            group.MapPost("/", async ([FromForm] string gameJson, [FromForm] IFormFile? file, ISender sender) =>
                {
                    var game = JsonSerializer.Deserialize<Game>(gameJson)!;
                    
                    if (file is not null)
                    {
                        var saveImageCommand = new SaveImageCommand(file);
                        
                        var saveImageCommandResponse = await sender.Send(saveImageCommand);
                        
                        game.ImagePath = saveImageCommandResponse.Data!;
                    }
                    
                    var query = new CreateGameCommand(game);

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("CreateGame")
                .Produces<ResponseData<Game>>(StatusCodes.Status201Created)
                .RequireAuthorization("admin");

            group.MapPut("/", async ([FromForm] string gameJson, [FromForm] IFormFile? file, ISender sender) =>
                {
                    var game = JsonSerializer.Deserialize<Game>(gameJson)!;
                    
                    if (file is not null)
                    {
                        var saveImageCommand = new SaveImageCommand(file);
                        
                        var saveImageCommandResponse = await sender.Send(saveImageCommand);
                        
                        game.ImagePath = saveImageCommandResponse.Data!;
                    }
                    
                    var query = new UpdateGameByIdCommand(game);

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("UpdateGame")
                .Produces(StatusCodes.Status204NoContent)
                .Produces<ResponseData<Game>>(StatusCodes.Status404NotFound)
                .RequireAuthorization("admin");

            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
                {
                    var query = new DeleteGameByIdCommand(id);

                    var response = await sender.Send(query);

                    return response;
                })
                .WithName("DeleteGame")
                .Produces<ResponseData<Game>>()
                .Produces<ResponseData<Game>>(StatusCodes.Status404NotFound)
                .RequireAuthorization("admin");
        }
    }
}