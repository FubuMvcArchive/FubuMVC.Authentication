using System.Collections.Generic;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsActionSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            yield return ActionCall.For<WindowsController>(x => x.Login(null));
        }
    }
}