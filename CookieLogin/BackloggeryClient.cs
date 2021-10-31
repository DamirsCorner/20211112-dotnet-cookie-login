using System;
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

        public async Task<string> GetHtmlAsync(string username)
        {
            var uri = new Uri($"https://backloggery.com/ajax_moregames.php?alpha=1&user={username}");
            return await httpClient.GetStringAsync(uri);
        }
    }
}
