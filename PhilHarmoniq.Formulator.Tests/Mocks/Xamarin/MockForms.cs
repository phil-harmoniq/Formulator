using Xamarin.Forms;

namespace PhilHarmoniq.Formulator.Tests.Mocks.Xamarin
{
    public class MockForms
    {
        public static void Init()
        {
            Device.Info = new MockDeviceInfo();
            Device.PlatformServices = new MockPlatformServices();

            DependencyService.Register<MockResourcesProvider>();
            DependencyService.Register<MockDeserializer>();
        }
    }
}
