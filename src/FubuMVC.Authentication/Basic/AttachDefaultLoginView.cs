using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime.Conditionals;

namespace FubuMVC.Authentication.Basic
{
    // TODO -- Come back to this after the ConfigurationGraph clean up (this lets us go after view attachment)
    [ConfigurationType(ConfigurationType.Instrumentation)]
    public class AttachDefaultLoginView : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var chain = graph.BehaviorFor<LoginController>(x => x.Login(null));
            if(!chain.Output.HasView(typeof(Always)))
            {
               chain.Output.Writers.AddToEnd(new WriteDefaultLogin()); 
            }
        }
    }
}