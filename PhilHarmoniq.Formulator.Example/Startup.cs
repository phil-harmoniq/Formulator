using PhilHarmoniq.Formulator.Example.Services;
using PhilHarmoniq.Formulator.Example.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhilHarmoniq.Configuration.Extensions.EmbeddedResource;

namespace PhilHarmoniq.Formulator.Example
{
    public class Startup : IStartup
    {
        public void Configure(IConfigurationBuilder configBuilder)
        {
            configBuilder
                .AddEmbeddedJson("appsettings.json")
                .AddEmbeddedJson("appsettings.Development.json");
        }

        public void RegisterDependencies(IServiceCollection services, IConfiguration config)
        {

            // Register configuration with the IOptions provider.
            services
                .Configure<Configuration>(config.GetSection(nameof(Configuration)))
                .AddSingleton<SingletonViewModel>()
                .AddScoped<ScopedViewModel>()
                .AddTransient<TransientViewModel>()
                .AddScoped<IGuidService, GuidService>();
        }
    }
}
