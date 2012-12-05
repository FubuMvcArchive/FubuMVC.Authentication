using FubuMVC.Authentication.Endpoints;
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
        }
    }

    // TODO -- go to windows and add the node, but it has to be first
    // TODO -- go to windows and make sure there's a way to disable it
    // TODO -- windows needs its own IWindowsPrincipalSource.Build(WindowsPrincipal)
}