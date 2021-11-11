using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Project_PRN.Session
{
    //ref: https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
