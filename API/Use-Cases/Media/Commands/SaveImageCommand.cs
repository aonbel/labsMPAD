using Domain.Models;
using MediatR;

namespace API.Use_Cases.Media.Commands;

public record SaveImageCommand(IFormFile Image) : IRequest<ResponseData<string>>;