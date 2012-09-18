using System.Collections.Generic;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Twitter
{
    public class TwitterEndpoints : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            yield return ActionCall.For<TwitterController>(x => x.Login(null));
            yield return ActionCall.For<TwitterController>(x => x.Button(null));
            yield return ActionCall.For<TwitterController>(x => x.Callback(null));
        }
    }
}