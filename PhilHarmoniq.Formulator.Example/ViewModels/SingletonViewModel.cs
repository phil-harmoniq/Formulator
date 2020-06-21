using PhilHarmoniq.Formulator.Example.Services;
using PropertyChanged;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhilHarmoniq.Formulator.Example.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SingletonViewModel
    {
        private readonly IGuidService _guidService;

        public string Guid { get; set; }
        public ICommand GenerateClicked { get; }

        public SingletonViewModel() { }

        public SingletonViewModel(IGuidService guidService)
        {
            _guidService = guidService;
            Guid = _guidService.Generate().ToString();
            GenerateClicked = new Command(Generate);
        }

        public void Generate()
        {
            Guid = _guidService.Generate().ToString();
        }
    }
}
