using FubuMVC.Authentication.Endpoints;
using FubuMVC.Authentication.Membership;
using FubuMVC.Core;

namespace FubuMVC.Authentication
{
    public class ApplyAuthentication : IFubuRegistryExtension
    {
        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Policies.Add<RegisterAuthenticationStrategies>();

            registry.Services<AuthenticationServiceRegistry>();
            registry.Policies.Add(new ApplyAuthenticationPolicy());
            registry.Policies.Add<FormsAuthenticationEndpointsRegistration>();
            registry.Policies.Add<AttachLoginBehaviorToLoginController>();
            registry.Policies.Add<AttachDefaultLoginView>();

            registry.AlterSettings<AuthenticationSettings>(x => {
                x.Strategies.AddToEnd<MembershipNode>();
            });
        }
    }
}