using System.Linq;
using FubuCore.Descriptions;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Endpoints
{
    [Title("Adds the default endpoints for basic authentication if they do not already exist in the BehaviorGraph")]
    [ConfigurationType(ConfigurationType.Discovery)]
    public class FormsAuthenticationEndpointsRegistration : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            if (!HasLogin(graph))
            {
                graph.AddChain().AddToEnd(ActionCall.For<LoginController>(x => x.get_login(null)));
                graph.AddChain().AddToEnd(ActionCall.For<LoginController>(x => x.post_login(null)));
            }

            if (!HasLogout(graph))
            {
                graph.AddChain().AddToEnd(ActionCall.For<LogoutController>(x => x.Logout(null)));
            }
        }

        public static bool HasLogin(BehaviorGraph graph)
        {
            return graph.Behaviors.Any(x => typeof (LoginRequest) == x.InputType());
        }

        public static bool HasLogout(BehaviorGraph graph)
        {
            return graph.Behaviors.Any(x => typeof (LogoutRequest) == x.InputType());
        }
    }
}