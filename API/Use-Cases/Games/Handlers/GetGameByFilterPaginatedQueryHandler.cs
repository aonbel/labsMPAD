using API.Data;
using API.Use_Cases.Games.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Use_Cases.Games.Handlers;

public sealed class GetGameByFilterPaginatedQueryHandler(AppDbContext appDbContext)
    : IRequestHandler<GetGamesByFilterPaginatedQuery, ResponseData<ListModel<Game>>>
{
    public async Task<ResponseData<ListModel<Game>>> Handle(GetGamesByFilterPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var filteredGames = appDbContext.Games.Where(request.Filter);

        var pageItems = await filteredGames
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return ResponseData<ListModel<Game>>.Success(new ListModel<Game>
        {
            Items = pageItems,
            CurrentPage = request.PageNumber,
            TotalPages = (filteredGames.Count() + request.PageSize - 1) / request.PageSize
        });
    }
}