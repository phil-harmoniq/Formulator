using Xamarin.Forms;

namespace Formulator
{
    public class RouteBuilder
    {
        public RouteBuilder RegisterRoute<TPage>() where TPage : Page
        {
            var type = typeof(TPage);
            Routing.RegisterRoute(type.Name.ToLower(), type);
            return this;
        }
    }
}
