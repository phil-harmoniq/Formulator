using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Formulator.Setup
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Add a concrete object to be used as a configuration provider
        /// </summary>
        /// <param name="obj">The objected to be registered</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddObject<T>(this IConfigurationBuilder builder, T obj)
        {
            var config = new ConfigurationBuilder().Build();
            config.Bind(obj);
            return builder.AddConfiguration(config);
        }
        /// <summary>
        /// Add concrete objects to be used as configuration providers
        /// </summary>
        /// <param name="obj">The objected to be registered</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddObjects(this IConfigurationBuilder builder, params object[] objects)
        {
            var config = new ConfigurationBuilder().Build();
            foreach (var obj in objects) { config.Bind(obj); }
            return builder.AddConfiguration(config);
        }

        public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder builder, Action<Dictionary<string, string>> action)
        {
            var dict = new Dictionary<string, string>();
            action(dict);
            builder.AddInMemoryCollection(dict);
            return builder;
        }

        public static T Resolve<T>(this IConfiguration config)
        {
            return config.GetSection(typeof(T).Name).Get<T>();
        }

        /// <summary>
        /// Register the given type with the IOptions provider
        /// </summary>
        /// <typeparam name="T">The type to be registered</typeparam>
        public static IServiceCollection AddConfigOption<T>(this IServiceCollection services, IConfiguration config) where T : class
        {
            return services.Configure<T>(config.GetSection(typeof(T).FullName));
        }
    }
}
