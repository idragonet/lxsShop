using System;
using System.Threading.Tasks;

namespace ConsoleAppShopSpiderTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            await ShopSpider.ShopSpider.run();

            Console.WriteLine("Hello World!");
        }
    }
}
