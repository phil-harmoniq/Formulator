using System;
using Xamarin.Forms;

namespace Formulator
{
    /// <summary>
    /// The Formulator <see cref="Container"/> will automatically create service scopes and
    /// resolve dependencies for pages deriving from this <see cref="ContentPage{TViewModel}"/>.
    /// Each instance of a view-model is a unique scope.
    /// </summary>
    /// <typeparam name="TViewModel">Defines the view-model you wish to set as the page's binding
    /// context. The view-model dependency scoep will automatically be managed by the Formulator
    /// <see cref="Container"/>.</typeparam>
    public class ContentPage<TViewModel> : ContentPage, IDisposable where TViewModel : class
    {
        /// <summary>
        /// The current view-model resolved using the Formulator <see cref="Container"/>.
        /// </summary>
        protected TViewModel ViewModel { get; private set; }

        public void Dispose()
        {
            BindingContext = ViewModel = null;
            this.DisposeScope();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.CreateScope();
			BindingContext = ViewModel = Container.Resolve<TViewModel>();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BindingContext = ViewModel = null;
            this.DisposeScope();
        }
    }
}
