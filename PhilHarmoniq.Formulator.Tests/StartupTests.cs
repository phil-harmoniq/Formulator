using PhilHarmoniq.Formulator.Example;
using PhilHarmoniq.Formulator.Tests.Mocks.Xamarin;
using Xunit;

namespace PhilHarmoniq.Formulator.Tests
{
    public class StartupTests
    {
        [Fact]
        public void ContainerInit()
        {
            MockForms.Init();
            Assert.NotNull(Container.Init<App, Startup>());
        }
    }
}
