using Newtonsoft.Json;

namespace UI.Extensions;

public static class SessionExtensions
{
    public static ISession Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));

        return session;
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value is null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}