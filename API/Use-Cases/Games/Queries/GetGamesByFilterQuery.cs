using System.Linq.Expressions;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Queries;

public sealed record GetGamesByFilterQuery(Expression<Func<Game, bool>> Filter) : IRequest<ResponseData<List<Game>>>;