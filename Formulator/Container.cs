using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Formulator
{
    public static class Container
    {
        private const string _globalScope = "Global";
        private static IServiceProvider _sp;
        private static IConfigurationRoot _config;
        private static Dictionary<string, IServiceScope> _scopes;

        /// <summary>
        /// Configures a new inversion-of-control container from the provided <see cref="IStartup"/>
        /// </summary>
        /// <typeparam name="TAppConfig"></typeparam>
        public static TApp Init<TApp, TAppConfig>()
            where TApp : Application
            where TAppConfig : IStartup, new()
        {
            var appConfig = new TAppConfig();
            var configBuilder = new ConfigurationBuilder();
            appConfig.Configure(configBuilder);
            _config = configBuilder.Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<TApp>();
            appConfig.RegisterDependencies(serviceCollection, _config);
            _sp = serviceCollection.BuildServiceProvider();
            appConfig.RegisterRoutes(new RouteBuilder(), _config);

            _scopes = new Dictionary<string, IServiceScope>();
            _scopes.Add(_globalScope, _sp.CreateScope());
            return _scopes[_globalScope].ServiceProvider.GetRequiredService<TApp>();
        }

        public static T Resolve<T>()
        {
            if (DesignMode.IsDesignModeEnabled) { return default; }
            return _scopes[_globalScope].ServiceProvider.GetRequiredService<T>();
        }

        public static T Resolve<T>(this Page page)
        {
            var scopeName = page.GetType().Name;
            var scope = _scopes.FirstOrDefault(x => x.Key == scopeName);
            if (scope.Equals(default(KeyValuePair<string, IServiceScope>)))
            {
                throw new InvalidOperationException($"Scope '{scopeName}' doesn't exist");
            }
            return _scopes[scopeName].ServiceProvider.GetRequiredService<T>();
        }

        public static void CreateScope(this Page page)
        {
            var scopeName = page.GetType().Name;
            if (_scopes.ContainsKey(scopeName))
            {
                throw new InvalidOperationException($"Scope '{scopeName}' already exists");
            }
            _scopes.Add(scopeName, _sp.CreateScope());
        }

        public static void DisposeScope(this Page page)
        {
            var scopeName = page.GetType().Name;
            var scope = _scopes.FirstOrDefault(x => x.Key == scopeName);
            if (scope.Equals(default(KeyValuePair<string, IServiceScope>)))
            {
                throw new InvalidOperationException($"Scope '{scopeName}' doesn't exist");
            }
            _scopes[scopeName].Dispose();
            _scopes.Remove(scopeName);
        }

        public static void ReloadConfiguration()
        {
            _config.Reload();
        }
    }
}
