using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CookieLogin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<BackloggeryClient>();

            serviceCollection.AddHttpClient<BackloggeryClient>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var backloggeryClient = serviceProvider.GetRequiredService<BackloggeryClient>();

            Console.Write("Enter username: ");
            var username = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("No username entered.");
                return;
            }

            var html = await backloggeryClient.GetHtmlAsync(username);

            Console.WriteLine(html);
        }
    }
}
