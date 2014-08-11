using FubuMVC.Authentication.Endpoints;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication
{
    [ConfigurationType(ConfigurationType.InjectNodes)]
    public class LoginPageAccessPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var chain = graph.BehaviorFor<LoginController>(x => x.get_login(null));
            if (chain == null)
                return;
            chain.Prepend(ActionFilter.For<LoginPageAccessFilter>(a => a.LoginAccessFilter())); 
        }
    }
}
