using PhilHarmoniq.Formulator.Example.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhilHarmoniq.Formulator.Example.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingletonPage : ContentPage<SingletonViewModel>
    {
        public SingletonPage()
        {
            InitializeComponent();
        }
    }
}