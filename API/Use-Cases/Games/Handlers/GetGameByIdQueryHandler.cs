using API.Data;
using API.Use_Cases.Games.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Handlers;

public sealed class GetGameByIdQueryHandler(AppDbContext context) : IRequestHandler<GetGameByIdQuery, ResponseData<Game>>
{
    public async Task<ResponseData<Game>> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        var game = await context.Games.FindAsync([request.Id], cancellationToken);

        if (game is null)
        {
            return ResponseData<Game>.Fail($"Game with id {request.Id} does not exist");
        }

        return ResponseData<Game>.Success(game);
    }
}