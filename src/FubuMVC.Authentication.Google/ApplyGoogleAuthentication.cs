using FubuMVC.Authentication.OAuth2;
using FubuMVC.ContentExtensions;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Google
{
    public class ApplyGoogleAuthentication : IFubuRegistryExtension
    {
        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<GoogleEndpoints>();
            registry.Services<GoogleServiceRegistry>();
            registry.Policies.Add<AttachDefaultGoogleView>();
            registry.Extensions().For(new OAuth2ContentExtension<GoogleLoginRequest>());
        }
    }
}