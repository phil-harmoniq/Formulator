using Xamarin.Forms;

namespace Formulator
{
    public class ContentView<TViewModel> : ContentView
    {
        public ContentView()
        {
            var parent = this.GetParentPage();
            BindingContext = parent.Resolve<TViewModel>();
        }
    }
}
