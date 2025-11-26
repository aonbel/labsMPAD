using API.Data;
using API.Use_Cases.Games.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Use_Cases.Games.Handlers;

public sealed class GetGameByFilterQueryHandler(AppDbContext context)
    : IRequestHandler<GetGamesByFilterQuery, ResponseData<List<Game>>>
{
    public async Task<ResponseData<List<Game>>> Handle(GetGamesByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var filteredGames = await context.Games.Where(request.Filter).ToListAsync(cancellationToken);

        return ResponseData<List<Game>>.Success(filteredGames);
    }
}