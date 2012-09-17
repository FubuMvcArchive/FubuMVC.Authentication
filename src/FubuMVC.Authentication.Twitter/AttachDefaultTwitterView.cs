using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime.Conditionals;

namespace FubuMVC.Authentication.Twitter
{
    // TODO -- Make this reusable somehow?
    // TODO -- Come back to this after the ConfigurationGraph clean up (this lets us go after view attachment)
    [ConfigurationType(ConfigurationType.Instrumentation)]
    public class AttachDefaultTwitterView : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var chain = graph.BehaviorFor<TwitterController>(x => x.Partial(null));
            if (!chain.Output.HasView(typeof(Always)))
            {
                chain.Output.Writers.AddToEnd(new WriteDefaultTwitterButton());
            }
        }
    }
}