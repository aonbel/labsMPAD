using System.Text.Json;
using Domain.Models;

namespace UI.Services.GameService;

public class ApiGameService(HttpClient httpClient, IConfiguration _configuration, ILogger<ApiGameService> logger) : IGameService
{
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    public async Task<ResponseData<ListModel<Game>>> GetByPageAsync(int pageNumber = 1, int pageSize = 10)
    {
        var urlString = $"{httpClient.BaseAddress?.AbsoluteUri}/Page/{pageNumber}/OfSize/{pageSize}";
        
        var response = await httpClient.GetAsync(urlString);
        
        return (await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Game>>>(_serializerOptions))!;
    }

    public async Task<ResponseData<ListModel<Game>>> GetByGenreIdAndPageAsync(int genreId, int pageNumber = 1, int pageSize = 10)
    {
        var urlString = $"{httpClient.BaseAddress?.AbsoluteUri}/Genre/{genreId}/Page/{pageNumber}/OfSize/{pageSize}";
        
        var response = await httpClient.GetAsync(urlString);
        
        return (await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Game>>>(_serializerOptions))!;
    }

    public Task<ResponseData<Game>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Game game)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Game game)
    {
        throw new NotImplementedException();
    }
}