using BioEngine.BRC.Domain;
using BioEngine.Core.Logging.Controllers;
using BioEngine.Core.Site;
using BioEngine.Extra.Ads.Site;
using BioEngine.Extra.IPB.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BioEngine.BRC.Games
{
    public class Startup : BioEngineStartup
    {
        private readonly IHostEnvironment _environment;

        public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration)
        {
            _environment = environment;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services
                .AddControllersWithViews()
                .AddApplicationPart(typeof(LogsController).Assembly)
                .AddApplicationPart(typeof(UserController).Assembly)
                .AddApplicationPart(typeof(BrcDomainModule).Assembly)
                .AddApplicationPart(typeof(AdsSiteController).Assembly);
            if (_environment.IsDevelopment())
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation();
            }
        }

        protected override void ConfigureApp(IApplicationBuilder app, IHostEnvironment env)
        {
            base.ConfigureApp(app, env);

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
