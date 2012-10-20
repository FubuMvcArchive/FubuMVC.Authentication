using System.Collections.Generic;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Facebook
{
    public class FacebookEndpoints : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            yield return ActionCall.For<FacebookController>(x => x.Login(null));
            yield return ActionCall.For<FacebookController>(x => x.Button(null));
            yield return ActionCall.For<FacebookController>(x => x.Callback(null));
        }
    }
}