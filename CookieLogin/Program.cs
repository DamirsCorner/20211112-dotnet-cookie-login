using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieLogin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<BackloggeryClient>();

            serviceCollection.AddHttpClient<BackloggeryClient>()
                .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
                {
                    AllowAutoRedirect = false,
                });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var backloggeryClient = serviceProvider.GetRequiredService<BackloggeryClient>();

            Console.Write("Enter username: ");
            var username = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("No username entered.");
                return;
            }

            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            var html = await backloggeryClient.GetHtmlAsync(username, password);

            Console.WriteLine(html);
        }
    }
}
