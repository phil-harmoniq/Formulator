using Xamarin.Forms;

namespace Formulator
{
    public class ContentPage<TViewModel> : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.CreateScope();
            BindingContext = this.Resolve<TViewModel>();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BindingContext = null;
            this.DisposeScope();
        }
    }
}
