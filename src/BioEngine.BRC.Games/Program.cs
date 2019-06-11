using System.Threading.Tasks;
using BioEngine.BRC.Site;

namespace BioEngine.BRC.Games
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var bioEngine = new Core.BioEngine(args).AddBrcSite();

            await bioEngine.RunAsync<Startup>();
        }
    }
}
