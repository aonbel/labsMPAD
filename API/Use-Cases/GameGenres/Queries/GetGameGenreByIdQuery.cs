using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.GameGenres.Queries;

public sealed record GetGameGenreByIdQuery(int Id) : IRequest<ResponseData<GameGenre>>;