using API.Data;
using API.Use_Cases.GameGenres.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Use_Cases.GameGenres.Handlers;

public sealed class GetAllGameGenresQueryHandler(AppDbContext context)
    : IRequestHandler<GetAllGameGenresQuery, ResponseData<List<GameGenre>>>
{
    public async Task<ResponseData<List<GameGenre>>> Handle(GetAllGameGenresQuery request,
        CancellationToken cancellationToken)
    {
        var genres = await context.GameGenres.ToListAsync(cancellationToken);

        return ResponseData<List<GameGenre>>.Success(genres);
    }
}