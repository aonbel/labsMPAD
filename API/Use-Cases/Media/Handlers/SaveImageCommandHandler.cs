using API.Use_Cases.Media.Commands;
using Domain.Models;
using MediatR;

namespace API.Use_Cases.Media.Handlers;

public class SaveImageCommandHandler(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    : IRequestHandler<SaveImageCommand, ResponseData<string>>
{
    public async Task<ResponseData<string>> Handle(SaveImageCommand request, CancellationToken cancellationToken)
    {
        var imagesPath = $"{webHostEnvironment.WebRootPath}/images";
        var imageName = Guid.NewGuid().ToString();
        var imageExtension = request.Image.FileName.Split('.').Last();

        while (true)
        {
            if (!File.Exists($"{imagesPath}/{imageName}.{imageExtension}"))
            {
                break;
            }
            imageName = Guid.NewGuid().ToString();
        }
        
        var imagePath = $"{imagesPath}/{imageName}.{imageExtension}";
        
        var urlOfApiApplication = configuration["ApplicationUrl"];

        await using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await request.Image.CopyToAsync(fileStream, cancellationToken);
        }

        return ResponseData<string>.Success($"{urlOfApiApplication}/images/{imageName}.{imageExtension}");
    }
}