using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieLogin
{
    public class BackloggeryClient
    {
        private readonly HttpClient httpClient;

        public BackloggeryClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetHtmlAsync(string username, string? password = null)
        {
            IEnumerable<KeyValuePair<string, string>>? loginCookies = null;

            if (!string.IsNullOrEmpty(password))
            {
                loginCookies = await LoginAsync(username, password).ConfigureAwait(false);
            }

            var uri = new Uri($"https://backloggery.com/ajax_moregames.php?alpha=1&user={username}");
            return await httpClient.GetStringAsync(uri, loginCookies).ConfigureAwait(false);
        }

        private async Task<IEnumerable<KeyValuePair<string, string>>> LoginAsync(string username, string password)
        {
            var loginUri = new Uri("https://backloggery.com/login.php");

            var loginParams = new KeyValuePair<string?, string?>[]
            {
                new ("username", username),
                new ("password", password),
            };

            using var content = new FormUrlEncodedContent(loginParams);

            var response = await httpClient.PostAsync(loginUri, content).ConfigureAwait(false);
            var loginCookies = response.GetCookies();

            if (!loginCookies.Any())
            {
                throw new ArgumentException("Login failed. Invalid username or password.");
            }

            return loginCookies;
        }
    }
}
