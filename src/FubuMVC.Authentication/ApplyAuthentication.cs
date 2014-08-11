using FubuMVC.Authentication.Endpoints;
using FubuMVC.Authentication.Membership;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using System.Linq;

namespace FubuMVC.Authentication
{
    public class ApplyAuthentication : IFubuRegistryExtension
    {
        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Policies.Add<RegisterAuthenticationStrategies>();

            registry.Services<AuthenticationServiceRegistry>();
            registry.Policies.Add(new ApplyAuthenticationPolicy());
            registry.Policies.Add(new LoginPageAccessPolicy());
            registry.Policies.Add<FormsAuthenticationEndpointsRegistration>();
            registry.Policies.Add<AttachDefaultLoginView>();
			registry.Policies.Add<ApplyPassThroughAuthenticationPolicy>();

            registry.Policies.Add<AddDefaultMembershipAuthentication>();
        }
    }

    [ConfigurationType(ConfigurationType.Discovery)]
    public class AddDefaultMembershipAuthentication : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Settings.Alter<AuthenticationSettings>(settings => {
                if (settings.MembershipEnabled == MembershipStatus.Disabled) return;

                if (!settings.Strategies.OfType<MembershipNode>().Any())
                {
                    settings.Strategies.InsertFirst(new MembershipNode());
                }
            });
        }
    }
}