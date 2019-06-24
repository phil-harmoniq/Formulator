using Xamarin.Forms;

namespace Formulator.Setup
{
    public static class RoutingBuilderExtensions
    {
        public static void RegisterRoute<TView>(string route)
        {
            Routing.RegisterRoute(route, typeof(TView));
        }
    }
}
