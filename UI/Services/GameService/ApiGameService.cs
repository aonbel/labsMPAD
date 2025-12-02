using System.Text.Json;
using Domain.Models;
using UI.Authorization;

namespace UI.Services.GameService;

public class ApiGameService(HttpClient httpClient, ITokenAccessor tokenAccessor) : IGameService
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<ResponseData<List<Game>?>> GetAllAsync()
    {
        await tokenAccessor.SetAuthorizationHeaderAsync(httpClient, false);

        var uriBuilder = new UriBuilder($"{httpClient.BaseAddress?.AbsoluteUri!}");

        var uri = uriBuilder.Uri;

        var response = await httpClient.GetAsync(uri);

        return (await response.Content.ReadFromJsonAsync<ResponseData<List<Game>>>(_serializerOptions))!;
    }

    public async Task<ResponseData<Game>> GetByIdAsync(int id)
    {
        await tokenAccessor.SetAuthorizationHeaderAsync(httpClient, false);

        var uriBuilder = new UriBuilder($"{httpClient.BaseAddress?.AbsoluteUri!}/{id}");

        var uri = uriBuilder.Uri;

        var response = await httpClient.GetAsync(uri);

        return (await response.Content.ReadFromJsonAsync<ResponseData<Game>>(_serializerOptions))!;
    }

    public async Task UpdateAsync(Game game, IFormFile? file)
    {
        await tokenAccessor.SetAuthorizationHeaderAsync(httpClient, false);

        game.ImagePath = "images/noimage.png";

        var uriBuilder = new UriBuilder($"{httpClient.BaseAddress?.AbsoluteUri!}/");

        var uri = uriBuilder.Uri;
        var request = new HttpRequestMessage(HttpMethod.Put, uri);

        var multipartFormDataContent = new MultipartFormDataContent();

        if (file is not null)
        {
            var streamContent = new StreamContent(file.OpenReadStream());

            multipartFormDataContent.Add(streamContent, "file", file.FileName);
        }

        var gameData = new StringContent(JsonSerializer.Serialize(game));

        multipartFormDataContent.Add(gameData, "gameJson");

        request.Content = multipartFormDataContent;
        await httpClient.SendAsync(request);
    }

    public async Task DeleteAsync(int id)
    {
        await tokenAccessor.SetAuthorizationHeaderAsync(httpClient, false);

        var uriBuilder = new UriBuilder($"{httpClient.BaseAddress?.AbsoluteUri!}/{id}");

        var uri = uriBuilder.Uri;

        await httpClient.DeleteAsync(uri);
    }

    public async Task CreateAsync(Game game, IFormFile? file)
    {
        await tokenAccessor.SetAuthorizationHeaderAsync(httpClient, false);

        game.ImagePath = "images/noimage.png";

        var uriBuilder = new UriBuilder($"{httpClient.BaseAddress?.AbsoluteUri!}/");

        var uri = uriBuilder.Uri;

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var multipartFormDataContent = new MultipartFormDataContent();

        if (file is not null)
        {
            var streamContent = new StreamContent(file.OpenReadStream());

            multipartFormDataContent.Add(streamContent, "file", file.FileName);
        }

        var gameData = new StringContent(JsonSerializer.Serialize(game));

        multipartFormDataContent.Add(gameData, "gameJson");

        request.Content = multipartFormDataContent;
        await httpClient.SendAsync(request);
    }

    public async Task<ResponseData<ListModel<Game>>> GetByPageAsync(int pageSize, int page = 1)
    {
        await tokenAccessor.SetAuthorizationHeaderAsync(httpClient, false);

        var uriBuilder = new UriBuilder($"{httpClient.BaseAddress?.AbsoluteUri!}/Paginated")
        {
            Query = $"page={page}&pageSize={pageSize}"
        };

        var uri = uriBuilder.Uri;

        var response = await httpClient.GetAsync(uri);

        return (await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Game>>>(_serializerOptions))!;
    }

    public async Task<ResponseData<ListModel<Game>>> GetByGenreIdAndPageAsync(int genreId, int pageSize, int page = 1)
    {
        await tokenAccessor.SetAuthorizationHeaderAsync(httpClient, false);

        var uriBuilder = new UriBuilder($"{httpClient.BaseAddress?.AbsoluteUri!}/Paginated")
        {
            Query = $"genreId={genreId}&page={page}&pageSize={pageSize}"
        };

        var uri = uriBuilder.Uri;

        var response = await httpClient.GetAsync(uri);

        return (await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Game>>>(_serializerOptions))!;
    }
}