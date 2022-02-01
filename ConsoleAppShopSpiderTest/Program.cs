using System;
using System.IO;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;

namespace ConsoleAppShopSpiderTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {


          //  await ShopSpider.ShopSpider.run();
          //  Console.WriteLine("保存商品完成!");



            GlobalConfiguration.Configuration
                .UseColouredConsoleLogProvider()
                .UseMemoryStorage();

            var configure = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            using (var server = new BackgroundJobServer())
            {
                // BackgroundJob.Schedule(() => Console.WriteLine(DateTime.Now + " 延迟执行Hello, world"), TimeSpan.FromMinutes(1));
                string cron = configure["cron"];
                RecurringJob.AddOrUpdate("MyJobId1", () => ShopSpider.ShopSpider.run(), cron, TimeZoneInfo.Local);

                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadLine();
            }



        }
    }
}
