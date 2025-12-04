using ILogger = Serilog.ILogger;

namespace UI.Middleware;

public class NonSuccessResponseLogMiddleware(RequestDelegate next, ILogger logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        await next(httpContext);

        if (httpContext.Response.StatusCode is < 200 or >= 300)
        {
            var request = httpContext.Request;
            var response = httpContext.Response;

            logger.Warning("request: {RequestMethod} {RequestPath} responded with {ResponseStatusCode}",
                request.Method, request.Path, response.StatusCode);
        }
    }
}