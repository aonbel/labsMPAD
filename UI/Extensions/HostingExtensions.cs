namespace UI.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder RegisterCustomServices(this WebApplicationBuilder builder)
    {
        var apiUri = builder.Configuration["UriData:ApiUri"];
        
        builder.Services.AddHttpClient<IGameGenreService, ApiGameGenreService>(opt =>
            opt.BaseAddress = new Uri($"{apiUri}/GameGenres"));
        builder.Services.AddHttpClient<IGameService, ApiGameService>(opt =>
            opt.BaseAddress = new Uri($"{apiUri}/Games"));
        
        return builder;
    }
}