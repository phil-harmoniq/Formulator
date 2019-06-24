using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Formulator.Setup
{
    public interface IAppConfig
    {
        void Configure(IConfigurationBuilder configBuilder);
        void RegisterDependencies(IServiceCollection services, IConfiguration config);
        void RegisterRoutes(IRoutingBuilder routing, IConfiguration config);
    }
}