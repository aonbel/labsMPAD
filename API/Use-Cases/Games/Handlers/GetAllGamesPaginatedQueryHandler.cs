using API.Data;
using API.Use_Cases.Games.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Use_Cases.Games.Handlers;

public sealed class GetAllGamesPaginatedQueryHandler(AppDbContext appDbContext)
    : IRequestHandler<GetAllGamesPaginatedQuery, ResponseData<ListModel<Game>>>
{
    public async Task<ResponseData<ListModel<Game>>> Handle(GetAllGamesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var pageItems = await appDbContext.Games
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return ResponseData<ListModel<Game>>.Success(new ListModel<Game>
        {
            Items = pageItems,
            CurrentPage = request.PageNumber,
            TotalPages = (appDbContext.Games.Count() + request.PageSize - 1) / request.PageSize
        });
    }
}