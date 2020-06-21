using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhilHarmoniq.Embedded.Json;
using PhilHarmoniq.Formulator.Example.Services;
using PhilHarmoniq.Formulator.Example.ViewModels;

namespace PhilHarmoniq.Formulator.Example
{
    public class Startup : IStartup
    {
        public void Configure(IConfigurationBuilder configBuilder)
        {
            configBuilder
                .AddEmbeddedJson<Startup>("appsettings.json")
                .AddEmbeddedJson<Startup>("appsettings.Development.json");
        }

        public void RegisterDependencies(IServiceCollection services, IConfiguration config)
        {

            // Register configuration with the IOptions provider.
            services
                .Configure<Configuration>(config.GetSection(nameof(Configuration)))
                .AddSingleton<SingletonViewModel>()
                .AddScoped<ScopedViewModel>()
                .AddTransient<TransientViewModel>()
                .AddScoped<MainViewModel>()
                .AddScoped<AboutViewModel>()
                .AddScoped<IGuidService, GuidService>();
        }
    }
}
