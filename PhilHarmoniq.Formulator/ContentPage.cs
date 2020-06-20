using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhilHarmoniq.Formulator
{
    /// <summary>
    /// The Formulator <see cref="Container"/> will automatically create service scopes and
    /// resolve dependencies for pages deriving from this <see cref="ContentPage{TViewModel}"/>.
    /// Each instance of a view-model is a unique scope.
    /// </summary>
    /// <typeparam name="TViewModel">Defines the view-model you wish to set as the page's binding
    /// context. The view-model's dependency scope will be managed by the Formulator
    /// <see cref="Container"/>.</typeparam>
    [DesignTimeVisible(true)]
    public class ContentPage<TViewModel> : ContentPage where TViewModel : class
    {
        private IServiceScope _scope;

        /// <summary>
        /// The current view-model resolved using the Formulator <see cref="Container"/>.
        /// </summary>
        protected TViewModel ViewModel => (TViewModel)BindingContext;

        public ContentPage()
        {
            if (!DesignMode.IsDesignModeEnabled)
            {
                _scope = Container.CreateScope();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = _scope.Resolve<TViewModel>();
        }
    }
}
