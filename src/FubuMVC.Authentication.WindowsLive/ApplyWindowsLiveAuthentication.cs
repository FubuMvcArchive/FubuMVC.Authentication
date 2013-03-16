using FubuMVC.Authentication.OAuth2;
using FubuMVC.ContentExtensions;
using FubuMVC.Core;

namespace FubuMVC.Authentication.WindowsLive
{
    public class ApplyWindowsLiveAuthentication : IFubuRegistryExtension
    {
        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<WindowsLiveEndpoints>();
            registry.Services<WindowsLiveServiceRegistry>();
            registry.Policies.Add<AttachDefaultWindowsLiveView>();
            registry.Extensions().For(new OAuth2ContentExtension<WindowsLiveLoginRequest>());
        }
    }
}