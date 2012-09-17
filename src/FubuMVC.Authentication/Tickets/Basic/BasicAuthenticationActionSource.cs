using System.Collections.Generic;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Tickets.Basic
{
    public class BasicAuthenticationActionSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            yield return ActionCall.For<LoginController>(x => x.Login(null));
            yield return ActionCall.For<LogoutController>(x => x.Logout(null));
        }
    }
}