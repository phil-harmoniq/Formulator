using Microsoft.Extensions.Configuration;
using Xamarin.Forms;

namespace Formulator
{
    public static class Extensions
    {

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

        public static IConfigurationBuilder AddResourceJson(
            this IConfigurationBuilder configuration, string fileName, bool optional = false)
        {
            configuration.Add(new ResourceJsonSource(fileName, optional));
            return configuration;
        }
    }
}
