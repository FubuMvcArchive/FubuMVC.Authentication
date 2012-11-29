using FubuMVC.Core;
using FubuMVC.ContentExtensions;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsAuthFubuRegistryExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<WindowsActionSource>();
            registry.Services<WindowsRegistry>();

            registry.Extensions().For(new WindowsLoginExtension());
        }
    }
}