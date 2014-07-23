using FubuMVC.Authentication.OAuth2;
using FubuMVC.Core;
using FubuMVC.Core.UI.Extensions;

namespace FubuMVC.Authentication.Google
{
    public class ApplyGoogleAuthentication : IFubuRegistryExtension
    {
        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<GoogleEndpoints>();
            registry.Services<GoogleServiceRegistry>();
            registry.Extensions().For(new OAuth2ContentExtension<GoogleLoginRequest>());
        }
    }
}