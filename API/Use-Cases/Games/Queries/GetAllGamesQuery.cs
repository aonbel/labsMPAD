using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Queries;

public sealed record GetAllGamesQuery : IRequest<ResponseData<List<Game>>>;