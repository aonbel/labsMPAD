using API.Data;
using API.Use_Cases.Games.Commands;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Handlers;

public sealed class DeleteGameByIdCommandHandler(AppDbContext context)
    : IRequestHandler<DeleteGameByIdCommand, ResponseData<Game>>
{
    public async Task<ResponseData<Game>> Handle(DeleteGameByIdCommand request, CancellationToken cancellationToken)
    {
        var game = await context.Games.FindAsync([request.Id], cancellationToken);

        if (game is null) return ResponseData<Game>.Fail($"Game with id {request.Id} does not exist");

        context.Games.Remove(game);

        await context.SaveChangesAsync(cancellationToken);

        return ResponseData<Game>.Success(game);
    }
}