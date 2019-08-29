using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;

namespace Formulator
{
    /// <summary>
    /// The formulator inversion-of-control container, initializes
    /// and resolves view-model dependencies automatically.
    /// </summary>
	public static class Container
    {
        private static IServiceProvider _sp;
        private static IConfigurationRoot _config;
        private static IServiceScope _currentScope;
        private static IDictionary<string, IServiceScope> _scopes;
        private static string _currentScopeName;

        internal static Assembly StartupAssembly { get; private set; }

        /// <summary>
        /// Configures a new inversion-of-control container from the provided <see cref="IStartup"/>.
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        public static TApp Init<TApp, TStartup>()
            where TApp : Application
            where TStartup : IStartup, new()
        {
            StartupAssembly = Assembly.GetAssembly(typeof(TStartup));
            var startup = new TStartup();
            var configBuilder = new ConfigurationBuilder();
            startup.Configure(configBuilder);
            _config = configBuilder.Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<TApp>();
            startup.RegisterDependencies(serviceCollection, _config);
            _sp = serviceCollection.BuildServiceProvider();
            _scopes = new Dictionary<string, IServiceScope>();

            return Resolve<TApp>();
        }

        /// <summary>
        /// Reloads all registered <see cref="IConfiguration"/> providers.
        /// </summary>
		public static void ReloadConfiguration()
        {
            _config.Reload();
        }

        /// <summary>
        /// Resolve the requested service from the current scope.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
		public static TService Resolve<TService>() where TService : class
        {
            return (_currentScope?.ServiceProvider ?? _sp).GetRequiredService<TService>();
        }

        internal static void CreateScope(this Page page)
        {
            var scopeName = page.GetType().Name;
            var scope = _sp.CreateScope();
            _scopes.Add(scopeName, scope);
            _currentScope = scope;
            _currentScopeName = scopeName;
        }

        internal static void DisposeScope(this Page page)
        {
            var scopeName = page.GetType().Name;
            if (scopeName == _currentScopeName)
            {
                _currentScope = null;
                _currentScopeName = null;
            }

            if (_scopes.ContainsKey(scopeName))
            {
                _scopes[scopeName].Dispose();
                _scopes.Remove(scopeName);
            }
        }
    }
}
