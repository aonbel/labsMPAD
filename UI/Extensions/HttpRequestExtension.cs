namespace UI.Extensions;

public static class HttpRequestExtension
{
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        return request.Headers.XRequestedWith.ToString().Equals("XMLHttpRequest");
    }
}