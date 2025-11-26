using System.Text.Json;
using Domain.Models;

namespace UI.Services.GameGenreService;

public class ApiGameGenreService(
    HttpClient httpClient,
    IConfiguration _configuration,
    ILogger<ApiGameGenreService> logger) : IGameGenreService
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<ResponseData<List<GameGenre>>> GetAllAsync()
    {
        var urlString = $"{httpClient.BaseAddress?.AbsoluteUri}/";

        var response = await httpClient.GetAsync(urlString);

        return (await response.Content.ReadFromJsonAsync<ResponseData<List<GameGenre>>>(_serializerOptions))!;
    }

    public async Task<ResponseData<GameGenre>> GetByIdAsync(int id)
    {
        var urlString = $"{httpClient.BaseAddress?.AbsoluteUri}/{id}";

        var response = await httpClient.GetAsync(urlString);

        return (await response.Content.ReadFromJsonAsync<ResponseData<GameGenre>>(_serializerOptions))!;
    }
}