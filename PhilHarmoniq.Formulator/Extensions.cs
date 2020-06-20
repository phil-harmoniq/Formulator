using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace PhilHarmoniq.Formulator
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
    }

    internal static class InternalExtensions
    {
        public static T Resolve<T>(this IServiceScope scope)
        {
            if (DesignMode.IsDesignModeEnabled)
            {
                return default;
            }
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}
