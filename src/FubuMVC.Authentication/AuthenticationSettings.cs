using System;
using System.Collections.Generic;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;
using System.Linq;
using FubuCore;
using FubuMVC.Core.Security;
using FubuCore.Reflection;

namespace FubuMVC.Authentication
{
    [ApplicationLevel]
    public class AuthenticationSettings
    {
        private readonly ChainPredicate _exclusions = new ChainPredicate();

        public AuthenticationSettings()
        {
            _exclusions.Matching<NotAuthenticatedFilter>();

            ExpireInMinutes = 180;
            SlidingExpiration = true;

            MaximumNumberOfFailedAttempts = 3;
            CooloffPeriodInMinutes = 60;
        }

        public ChainPredicate ExcludeChains
        {
            get { return _exclusions; }
        }

        public bool ShouldBeExcluded(BehaviorChain chain)
        {
            return _exclusions.As<IChainFilter>().Matches(chain);
        }

        public bool SlidingExpiration { get; set; }
        public int ExpireInMinutes { get; set; }

        public int MaximumNumberOfFailedAttempts { get; set; }
        public int CooloffPeriodInMinutes { get; set; }

        // *should* only be for testing
        public bool NeverExpires { get; set; }
    }

    [Title("Any action with the [NotAuthenticated] attribute")]
    public class NotAuthenticatedFilter : IChainFilter
    {
        public bool Matches(BehaviorChain chain)
        {
            return chain.Calls.Any(ActionIsExempted);
        }

        public static bool ActionIsExempted(ActionCall call)
        {
            if (call.HasAttribute<NotAuthenticatedAttribute>()) return true;

            if (call.InputType() != null && call.InputType().HasAttribute<NotAuthenticatedAttribute>())
            {
                return true;
            }

            return false;
        }
    }
    
}