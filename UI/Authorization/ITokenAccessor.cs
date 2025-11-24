namespace UI.Authorization;

public interface ITokenAccessor
{
    Task SetAuthorizationHeaderAsync(HttpClient httpClient, bool isClient);
}