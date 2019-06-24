using System.Threading.Tasks;
using Xamarin.Forms;

namespace Formulator.Utilities
{
    internal class RoutingProxy : IRouting
    {
        public async Task GoToAsync<TPage>() where TPage : Page
        {
            await Shell.Current.GoToAsync($"//{typeof(TPage).Name}");
        }

        //public async Task PopAsync()
        //{
        //    await Navigation.
        //}
    }
}
