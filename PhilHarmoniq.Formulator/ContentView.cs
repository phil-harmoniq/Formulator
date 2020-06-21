using Xamarin.Forms;

namespace PhilHarmoniq.Formulator
{
    public class ContentView<TViewModel> : ContentView where TViewModel : class
    {
        /// <summary>
        /// The current view-model resolved using the Formulator <see cref="Container"/>.
        /// </summary>
        protected TViewModel ViewModel { get; private set; }

        /// <summary>
        /// The base page containing this component.
        /// </summary>
        protected Page ParentPage { get; private set; }

        public ContentView()
        {
            ParentPage = this.GetParentPage();
            BindingContext = ViewModel = Container.Resolve<TViewModel>();
        }
    }
}
