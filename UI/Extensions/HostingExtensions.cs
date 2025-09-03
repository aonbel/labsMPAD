namespace UI.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder RegisterCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IGameGenreService, MemoryGameGenreService>();
        builder.Services.AddScoped<IGameService, MemoryGameService>();
        
        return builder;
    }
}