using System.Collections.Generic;
using System.Reflection;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.WindowsLive
{
    public class WindowsLiveEndpoints : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(Assembly assembly)
        {
            yield return ActionCall.For<WindowsLiveController>(x => x.Login(null));
            yield return ActionCall.For<WindowsLiveController>(x => x.Button(null));
            yield return ActionCall.For<WindowsLiveController>(x => x.Callback(null));
        }
    }
}