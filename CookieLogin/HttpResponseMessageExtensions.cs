using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace CookieLogin
{
    public static class HttpResponseMessageExtensions
    {
        public static IDictionary<string, string> GetCookies(this HttpResponseMessage response)
        {
            if (response == null)
            {
                return new Dictionary<string, string>();
            }

            var cookies = response.Headers.GetValues("Set-Cookie").Select(setCookieString =>
            {
                var match = Regex.Match(setCookieString, @"(?<key>\w+)=(?<value>\w+);");
                return new KeyValuePair<string, string>(match.Groups["key"].Value, match.Groups["value"].Value);
            });

            return new Dictionary<string, string>(cookies);
        }
    }
}
