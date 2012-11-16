using System.Collections.Generic;
using System.Reflection;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Google
{
    public class GoogleEndpoints : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(Assembly assembly)
        {
            yield return ActionCall.For<GoogleController>(x => x.Login(null));
            yield return ActionCall.For<GoogleController>(x => x.Button(null));
            yield return ActionCall.For<GoogleController>(x => x.Callback(null));
        }
    }
}