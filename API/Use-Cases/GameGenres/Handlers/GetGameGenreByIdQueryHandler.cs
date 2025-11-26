using API.Data;
using API.Use_Cases.GameGenres.Queries;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.GameGenres.Handlers;

public sealed class GetGameGenreByIdQueryHandler(AppDbContext context)
    : IRequestHandler<GetGameGenreByIdQuery, ResponseData<GameGenre>>
{
    public async Task<ResponseData<GameGenre>> Handle(GetGameGenreByIdQuery request,
        CancellationToken cancellationToken)
    {
        var genre = await context.GameGenres.FindAsync([request.Id], cancellationToken);

        if (genre is null) return ResponseData<GameGenre>.Fail($"Game genre with id {request.Id} does not exist");

        return ResponseData<GameGenre>.Success(genre);
    }
}