using System;
using BioEngine.BRC.Domain;
using BioEngine.Core.Logging.Loki;
using BioEngine.Core.Seo;
using BioEngine.Core.Site;
using BioEngine.Extra.Ads;
using BioEngine.Extra.IPB;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BioEngine.BRC.Games
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        [PublicAPI]
        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            new Core.BioEngine(args)
                .AddPostgresDb()
                .AddBrcCommon()
                .AddModule<LokiLoggingModule, LokiLoggingConfig>((configuration, environment) =>
                    new LokiLoggingConfig(configuration["BRC_LOKI_URL"]))
                .AddElasticSearch()
                .AddS3Storage()
                .AddModule<SeoModule>()
                .AddModule<IPBSiteModule, IPBModuleConfig>((configuration, env) =>
                {
                    if (!Uri.TryCreate(configuration["BE_IPB_URL"], UriKind.Absolute, out var ipbUrl))
                    {
                        throw new ArgumentException($"Can't parse IPB url; {configuration["BE_IPB_URL"]}");
                    }

                    return new IPBModuleConfig(ipbUrl)
                    {
                        ApiClientId = configuration["BE_IPB_OAUTH_CLIENT_ID"],
                        ApiClientSecret = configuration["BE_IPB_OAUTH_CLIENT_SECRET"],
                        CallbackPath = "/login/ipb",
                        AuthorizationEndpoint = configuration["BE_IPB_AUTHORIZATION_ENDPOINT"],
                        TokenEndpoint = configuration["BE_IPB_TOKEN_ENDPOINT"],
                        ApiReadonlyKey = configuration["BE_IPB_API_READONLY_KEY"]
                    };
                })
                .AddModule<SiteModule, SiteModuleConfig>((configuration, env) =>
                    new SiteModuleConfig(Guid.Parse(configuration["BE_SITE_ID"])))
                .AddModule<AdsModule>()
                .GetHostBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
