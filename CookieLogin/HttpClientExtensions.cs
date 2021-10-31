using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieLogin
{
    public static class HttpClientExtensions
    {
        public static async Task<string> GetStringAsync(this HttpClient httpClient, Uri uri, IEnumerable<KeyValuePair<string, string>>? cookies)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

            if (cookies != null)
            {
                var cookieValue = string.Join("; ", cookies.Select(cookie => $"{cookie.Key}={cookie.Value}"));
                request.Headers.Add("cookie", cookieValue);
            }

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
