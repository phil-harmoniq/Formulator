using Formulator.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Formulator.Setup
{
    public class AppStartup
    {
        /// <summary>
        /// Configures a new inversion-of-control container from the provided <see cref="IAppConfig"/>
        /// </summary>
        /// <typeparam name="TAppConfig"></typeparam>
        public static void Init<TAppConfig>() where TAppConfig : IAppConfig, new()
        {
            var appConfig = new TAppConfig();
            var configBuilder = new ConfigurationBuilder();
            appConfig.Configure(configBuilder);
            var config = configBuilder.Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IRouting, RoutingProxy>();
            serviceCollection.AddOptions();
            appConfig.RegisterDependencies(serviceCollection, config);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            IoC.LoadServiceProvider(serviceProvider);
            appConfig.RegisterRoutes(new RoutingBuilder(), config);
        }
    }
}
