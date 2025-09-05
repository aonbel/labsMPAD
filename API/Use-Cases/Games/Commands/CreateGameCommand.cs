using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Commands;

public sealed record CreateGameCommand(Game Game) : IRequest<ResponseData<Game>>;