using System.Threading.Tasks;
using Xamarin.Forms;

namespace Formulator.Utilities
{
    public interface IRouting
    {
        Task GoToAsync<TPage>() where TPage : Page;
    }
}
