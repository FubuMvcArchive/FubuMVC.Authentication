using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Authentication.Utilities;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Authentication
{
    public class LoggedInUserRedirectOnLoginPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Actions()
                .Where(x => x.InputIs<LoginRequest>())
                .Each(x => x.AddBefore(ActionFilter.For<LoggedInUserRedirector>(y => y.Execute())));
        }
    }
}