using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.GameGenres.Queries;

public sealed record GetAllGameGenresQuery : IRequest<ResponseData<List<GameGenre>>>;