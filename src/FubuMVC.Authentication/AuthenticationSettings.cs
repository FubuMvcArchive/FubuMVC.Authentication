using System;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;
using FubuCore;

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
}