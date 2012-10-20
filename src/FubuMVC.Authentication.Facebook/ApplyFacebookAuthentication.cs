using FubuMVC.Authentication.OAuth;
using FubuMVC.ContentExtensions;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Facebook
{
    public class ApplyFacebookAuthentication : IFubuRegistryExtension
    {
        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<FacebookEndpoints>();
            registry.Services<FacebookServiceRegistry>();
            registry.Policies.Add<AttachDefaultFacebookView>();
            registry.Extensions().For(new OAuthContentExtension<FacebookLoginRequest>());
        }
    }
}