using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Authentication
{
    [ConfigurationType(ConfigurationType.InjectNodes)]
    public class ApplyAuthenticationPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var settings = graph.Settings.Get<AuthenticationSettings>();
            var filter = settings.ExcludeChains.As<IChainFilter>();

            graph.Behaviors
                 .Where(x => !filter.Matches(x))
                 .Each(x => x.Prepend(new AuthenticationFilterNode()));
        }
    }
}