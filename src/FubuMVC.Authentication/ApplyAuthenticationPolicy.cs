using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore.Reflection;
using FubuMVC.Core.Assets.Http;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication
{
    public class ApplyAuthenticationPolicy : IConfigurationAction
    {
        private readonly Func<BehaviorChain, bool> _filter;

        public ApplyAuthenticationPolicy(Func<BehaviorChain, bool> filter)
        {
            _filter = filter;
        }

        public void Configure(BehaviorGraph graph)
        {
            graph.Behaviors
                .Where(x => !ExemptedFromAuthentication(x))
                .Where(_filter).Each( x => x.Prepend(new AuthenticationFilterNode()));
        }

        public static bool ExemptedFromAuthentication(BehaviorChain chain)
        {
            if (chain.InputType() != null && chain.InputType().HasAttribute<NotAuthenticatedAttribute>()) return true;
            if (chain.OfType<ActionCall>().Any(x => x.HasAttribute<NotAuthenticatedAttribute>())) return true;

            if (chain.OfType<ActionCall>().Any(x => x.HandlerType == typeof(AssetWriter))) return true;

            return false;
        }
    }
}