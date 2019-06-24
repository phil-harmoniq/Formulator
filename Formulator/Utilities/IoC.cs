using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace Formulator.Utilities
{
    public static class IoC
    {
        private static IServiceProvider _sp;

        public static TDependency Resolve<TDependency>()
        {
            if (DesignMode.IsDesignModeEnabled) { return default; }
            return _sp.GetRequiredService<TDependency>();
        }

        internal static void LoadServiceProvider(IServiceProvider sp)
        {
            _sp = sp;
        }
    }
}
