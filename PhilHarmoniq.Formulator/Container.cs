using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace PhilHarmoniq.Formulator
{
    /// <summary>
    /// The formulator inversion-of-control container, initializes
    /// and resolves view-model dependencies automatically.
    /// </summary>
	public static class Container
    {
        private static IServiceProvider _sp;
        private static IConfigurationRoot _config;

        internal static Assembly SharedAssembly { get; private set; }
        internal static Assembly PlatformAssembly { get; private set; }

        /// <summary>
        /// Configures a new inversion-of-control container from the provided <see cref="IStartup"/>.
        /// </summary>
        /// <typeparam name="TApp"></typeparam>
        /// <typeparam name="TStartup"></typeparam>
        public static TApp Init<TApp, TStartup>()
            where TApp : Application
            where TStartup : IStartup, new()
        {
            SharedAssembly = Assembly.GetAssembly(typeof(TStartup));
            PlatformAssembly = Assembly.GetCallingAssembly();

            var configBuilder = new ConfigurationBuilder();
            var serviceCollection = new ServiceCollection();
            var startup = new TStartup();

            startup.Configure(configBuilder);
            _config = configBuilder.Build();

            startup.RegisterDependencies(serviceCollection, _config);
            serviceCollection.AddSingleton<TApp>();
            _sp = serviceCollection.BuildServiceProvider();

            return Resolve<TApp>();
        }

        /// <summary>
        /// Configures a new inversion-of-control container from the provided <see cref="IStartup"/>.
        /// </summary>
        /// <typeparam name="TApp"></typeparam>
        /// <typeparam name="TSharedStartup"></typeparam>
        /// <typeparam name="TPlatformStartup"></typeparam>
        /// <returns></returns>
        public static TApp Init<TApp, TSharedStartup, TPlatformStartup>()
            where TApp : Application
            where TSharedStartup : IStartup, new()
            where TPlatformStartup : IStartup, new()
        {
            SharedAssembly = Assembly.GetAssembly(typeof(TSharedStartup));
            PlatformAssembly = Assembly.GetCallingAssembly();

            var configBuilder = new ConfigurationBuilder();
            var serviceCollection = new ServiceCollection();
            var sharedStartup = new TSharedStartup();
            var platformStartup = new TPlatformStartup();

            sharedStartup.Configure(configBuilder);
            platformStartup.Configure(configBuilder);
            _config = configBuilder.Build();

            sharedStartup.RegisterDependencies(serviceCollection, _config);
            platformStartup.RegisterDependencies(serviceCollection, _config);
            serviceCollection.AddSingleton<TApp>();
            serviceCollection.AddScoped(sp => sp.CreateScope());
            _sp = serviceCollection.BuildServiceProvider();

            return Resolve<TApp>();
        }

        /// <summary>
        /// Resolve the requested service from the current scope.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
		public static TService Resolve<TService>() where TService : class
        {
            if (DesignMode.IsDesignModeEnabled)
            {
                return default;
            }
            return _sp.GetRequiredService<TService>();
        }

        public static IServiceScope CreateScope()
        {
            if (DesignMode.IsDesignModeEnabled)
            {
                return default;
            }
            return _sp.CreateScope();
        }
    }
}
