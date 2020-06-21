using PhilHarmoniq.Formulator;
using PhilHarmoniq.Formulator.Example.ViewModels;
using Xamarin.Forms.Xaml;

namespace PhilHarmoniq.Formulator.Example.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransientPage : ContentPage<TransientViewModel>
    {
        public TransientPage()
        {
            InitializeComponent();
        }
    }
}