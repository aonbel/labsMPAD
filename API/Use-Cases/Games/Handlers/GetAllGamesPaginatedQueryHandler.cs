using API.Data;
using API.Use_Cases.Games.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Use_Cases.Games.Handlers;

public sealed class GetAllGamesPaginatedQueryHandler(AppDbContext appDbContext, IConfiguration configuration)
    : IRequestHandler<GetAllGamesPaginatedQuery, ResponseData<ListModel<Game>>>
{
    private readonly int _defaultItemsPerPage = int.Parse(configuration["Application:ItemsPerPage"]!);

    public async Task<ResponseData<ListModel<Game>>> Handle(GetAllGamesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize ?? _defaultItemsPerPage;

        pageSize = Math.Clamp(pageSize, 1, _defaultItemsPerPage);

        var totalPages = (appDbContext.Games.Count() + pageSize - 1) / pageSize;

        if (request.PageNumber < 1 || request.PageNumber > totalPages)
            return ResponseData<ListModel<Game>>.Fail("Page number out of bounds");

        var pageItems = await appDbContext.Games
            .Skip((request.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return ResponseData<ListModel<Game>>.Success(new ListModel<Game>
        {
            Items = pageItems,
            CurrentPage = request.PageNumber,
            TotalPages = totalPages
        });
    }
}