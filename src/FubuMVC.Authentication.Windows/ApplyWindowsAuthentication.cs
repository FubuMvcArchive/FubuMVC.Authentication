using FubuMVC.Core;
using FubuMVC.ContentExtensions;

namespace FubuMVC.Authentication.Windows
{
    public class ApplyWindowsAuthentication : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<WindowsActionSource>();
            registry.Services<WindowsAuthenticationServiceRegistry>();
            registry.Extensions().For(new WindowsLoginExtension());
        }
    }
}