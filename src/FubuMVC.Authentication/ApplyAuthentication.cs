using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Core;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication
{
    public class ApplyAuthentication : IFubuRegistryExtension
    {
        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Services<AuthenticationServiceRegistry>();
            registry.Policies.Add(new ApplyAuthenticationPolicy());
            registry.Policies.Add<FormsAuthenticationEndpointsRegistration>();
            registry.Policies.Add<AttachLoginBehaviorToLoginController>();
            registry.Policies.Add<AttachDefaultLoginView>();
        }

    }
}