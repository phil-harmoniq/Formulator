using Xamarin.Forms;

namespace Formulator.Setup
{
    public interface IRoutingBuilder
    {
        IRoutingBuilder RegisterRoute<TPage>() where TPage : Page;
    }
}
