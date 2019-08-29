using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xamarin.Forms;

namespace Formulator
{
    public static class Extensions
    {
        /// <summary>
        /// Add an embedded resource JSON file as an <see cref="IConfiguration"/> source.
        /// </summary>
        public static IConfigurationBuilder AddResourceJson(
            this IConfigurationBuilder configuration, string fileName, bool optional = false)
        {
            configuration.Add(new ResourceJsonSource(fileName, optional));
            return configuration;
        }

        /// <summary>
        /// Register a class as a <see cref="IOptions{TOptions}"/> bindable object.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddOption<TConfig>(
            this IServiceCollection services, IConfiguration config) where TConfig : class
        {
            return services.Configure<TConfig>(config.GetSection(typeof(TConfig).Name));
        }

        /// <summary>
        /// Gets the page to which an element belongs
        /// https://forums.xamarin.com/discussion/46306/is-it-possible-to-get-the-page-that-an-element-lives-on
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="element">Element.</param>
        public static Page GetParentPage(this VisualElement element)
        {
            if (element != null)
            {
                var parent = element.Parent;
                while (parent != null)
                {
                    if (parent is Page)
                    {
                        return parent as Page;
                    }
                    parent = parent.Parent;
                }
            }
            return null;
        }
    }
}
