namespace UI.Services;

public class LocalFileService(IWebHostEnvironment webHostEnvironment) : IFileService
{
    public async Task<string> SaveFileAsync(IFormFile file)
    {
        var fileName = Guid.NewGuid() + $"_{file.FileName}";
        
        var fileDirectory = $"{webHostEnvironment.WebRootPath}/images";

        if (!Directory.Exists(fileDirectory))
        {
            Directory.CreateDirectory(fileDirectory);
        }
        
        var filePath = $"{fileDirectory}/{fileName}";

        await using var fileStream = new FileStream(filePath, FileMode.CreateNew);
        
        await file.CopyToAsync(fileStream);

        return $"images/{fileName}";
    }
}