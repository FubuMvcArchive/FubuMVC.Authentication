using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime.Conditionals;
using FubuMVC.Core.View;

namespace FubuMVC.Authentication.Facebook
{
    // TODO -- Make this reusable somehow?
    // TODO -- Come back to this after the ConfigurationGraph clean up (this lets us go after view attachment)
    [ConfigurationType(ConfigurationType.Instrumentation)]
    public class AttachDefaultFacebookView : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var chain = graph.BehaviorFor<FacebookController>(x => x.Button(null));
            if (!chain.Output.HasView(typeof(Always)))
            {
                chain.Output.Writers.AddToEnd(new WriteDefaultFacebookButton());
            }
        }
    }
}