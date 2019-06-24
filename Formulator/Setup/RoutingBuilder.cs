using Xamarin.Forms;

namespace Formulator.Setup
{
    public class RoutingBuilder : IRoutingBuilder
    {
        public IRoutingBuilder RegisterRoute<TPage>() where TPage : Page
        {
            var type = typeof(TPage);
            Routing.RegisterRoute(type.Name, type);
            return this;
        }
    }
}
