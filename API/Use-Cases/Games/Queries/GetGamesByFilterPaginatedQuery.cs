using System.Linq.Expressions;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Queries;

public sealed record GetGamesByFilterPaginatedQuery(Expression<Func<Game, bool>> Filter, int PageNumber, int PageSize)
    : IRequest<ResponseData<ListModel<Game>>>;