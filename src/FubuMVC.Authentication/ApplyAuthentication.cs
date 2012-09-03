using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication
{
    public class ApplyAuthentication : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<AuthenticationActionSource>();

            // TODO -- need to generalize this as we pull it out later
            registry.Policies.Add(new ApplyAuthenticationPolicy(x => true));

            registry.Configure(graph =>
            {
                graph.Behaviors
                    .Where(x => x.InputType() == typeof (LoginRequest))
                    .Each(x => x.Prepend(Process.For<LoginBehavior>()));
            });

            registry.Policies.Add(new ReorderBehaviorsPolicy{
                WhatMustBeBefore = node => node is AuthenticationFilterNode,
                WhatMustBeAfter = node => node is AuthorizationNode
            });
        }
    }
}