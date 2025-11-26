using API.Data;
using API.Use_Cases.Games.Commands;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Handlers;

public sealed class UpdateGameByIdCommandHandler(AppDbContext context)
    : IRequestHandler<UpdateGameByIdCommand, ResponseData<Game>>
{
    public async Task<ResponseData<Game>> Handle(UpdateGameByIdCommand request, CancellationToken cancellationToken)
    {
        var game = await context.Games.FindAsync([request.Game.Id], cancellationToken);

        if (game is null) return ResponseData<Game>.Fail($"Game with id {request.Game.Id} does not exist");

        game.Name = request.Game.Name;
        game.Description = request.Game.Description;
        game.ImagePath = request.Game.ImagePath;
        game.Price = request.Game.Price;
        game.GenreId = request.Game.GenreId;

        await context.SaveChangesAsync(cancellationToken);

        return ResponseData<Game>.Success(game);
    }
}