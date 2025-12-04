namespace UI.Middleware;

public static class NonSuccessResponseLogMiddlewareExtensions
{
    public static void UseNonSuccessResponseLogMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<NonSuccessResponseLogMiddleware>();
    }
}