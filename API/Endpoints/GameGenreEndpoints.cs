using API.Use_Cases.GameGenres.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Endpoints;

public static class GameGenreEndpoints
{
    public static void MapGameGenreEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/GameGenres").WithTags(nameof(GameGenre));

        group.MapGet("/", async (ISender sender) =>
            {
                var query = new GetAllGameGenresQuery();

                var response = await sender.Send(query);

                return response;
            })
            .WithName("GetAllGameGenres")
            .Produces<ResponseData<List<GameGenre>>>()
            .AllowAnonymous();

        group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                var query = new GetGameGenreByIdQuery(id);

                var response = await sender.Send(query);

                return response;
            })
            .WithName("GetGameGenre")
            .Produces<ResponseData<GameGenre>>()
            .Produces<ResponseData<GameGenre>>(StatusCodes.Status404NotFound)
            .AllowAnonymous();
    }
}