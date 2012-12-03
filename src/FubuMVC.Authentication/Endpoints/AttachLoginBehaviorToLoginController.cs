using System.Collections.Generic;
using System.Linq;
using FubuCore.Descriptions;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Endpoints
{
    [Title("Applies LoginBehavior to the login screen")]
    [ConfigurationType(ConfigurationType.Explicit)]
    public class AttachLoginBehaviorToLoginController : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Behaviors
                 .Where(x => x.InputType() == typeof (LoginRequest))
                 .Each(x => x.Prepend(Process.For<LoginBehavior>()));
        }
    }
}