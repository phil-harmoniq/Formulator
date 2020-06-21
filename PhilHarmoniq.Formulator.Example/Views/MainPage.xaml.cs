using PhilHarmoniq.Formulator.Example.ViewModels;
using Xamarin.Forms.Xaml;

namespace PhilHarmoniq.Formulator.Example.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}