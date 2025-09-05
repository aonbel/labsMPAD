using Domain.Entities;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Games.Commands;

public sealed record UpdateGameByIdCommand(Game Game) : IRequest<ResponseData<Game>>;