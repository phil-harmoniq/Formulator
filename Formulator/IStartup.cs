using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Formulator
{
    public interface IStartup
    {
        /// <summary>
        /// Add <see cref="IConfiguration"/> sources using AddResourceJson()
        /// or create your own custom configuration provider.<para/>
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration
        /// </summary>
        void Configure(IConfigurationBuilder configBuilder);

        /// <summary>
        /// Register dependencies with the inversion-of-control container.<para/>
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
        /// </summary>
        void RegisterDependencies(IServiceCollection services, IConfiguration config);
    }
}
