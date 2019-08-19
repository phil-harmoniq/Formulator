using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Formulator
{
    public interface IStartup
    {
        void Configure(IConfigurationBuilder configBuilder);
        void RegisterDependencies(IServiceCollection services, IConfiguration config);
        void RegisterRoutes(RouteBuilder routing, IConfiguration config);
    }
}
