using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Queries;

public sealed record GetAllGamesPaginatedQuery(int PageNumber, int PageSize) : IRequest<ResponseData<ListModel<Game>>>;