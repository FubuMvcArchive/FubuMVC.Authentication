using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication
{
    [ConfigurationType(ConfigurationType.Discovery)]
    public class RegisterAuthenticationStrategies : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var settings = graph.Settings.Get<AuthenticationSettings>();

            foreach (IContainerModel strategy in settings.Strategies)
            {
                var def = strategy.ToObjectDef();
                graph.Services.AddService(typeof(IAuthenticationStrategy), def);
            }
        }
    }
}