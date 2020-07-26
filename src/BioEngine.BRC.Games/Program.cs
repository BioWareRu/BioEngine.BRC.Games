using System.Threading.Tasks;
using BioEngine.BRC.Site;
using Microsoft.Extensions.Hosting;

namespace BioEngine.BRC.Games
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var application = CreateApplication(args);
            await application.RunAsync<Startup>();
        }

        // need for migrations
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            CreateApplication(args).CreateBasicHostBuilder<Startup>();

        public static BRCSiteApplication CreateApplication(string[] args) => new BRCSiteApplication(args);
    }
}
