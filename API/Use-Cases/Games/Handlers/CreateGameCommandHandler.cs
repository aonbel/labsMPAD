using API.Data;
using API.Use_Cases.Games.Commands;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Handlers;

public sealed class CreateGameCommandHandler(AppDbContext context)
    : IRequestHandler<CreateGameCommand, ResponseData<Game>>
{
    public async Task<ResponseData<Game>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        context.Games.Add(request.Game);

        await context.SaveChangesAsync(cancellationToken);

        return ResponseData<Game>.Success(request.Game);
    }
}