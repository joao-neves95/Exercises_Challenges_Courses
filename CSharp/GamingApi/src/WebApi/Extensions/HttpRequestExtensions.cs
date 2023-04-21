using Yld.GamingApi.WebApi.Constants;

namespace Yld.GamingApi.WebApi.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string? GetUserAgent(this HttpRequest @request)
        {
            return (@request?.Headers[Headers.UserAgent])?.ToString();
        }
    }
}
