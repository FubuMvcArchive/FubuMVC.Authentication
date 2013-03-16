using FubuMVC.Authentication.OAuth2;
using FubuMVC.ContentExtensions;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Github
{
    public class ApplyGithubAuthentication : IFubuRegistryExtension
    {
        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<GithubEndpoints>();
            registry.Services<GithubServiceRegistry>();
            registry.Policies.Add<AttachDefaultGithubView>();
            registry.Extensions().For(new OAuth2ContentExtension<GithubLoginRequest>());
        }
    }
}