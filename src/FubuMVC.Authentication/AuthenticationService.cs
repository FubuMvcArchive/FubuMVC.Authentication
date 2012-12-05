using System;
using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEnumerable<IAuthenticationStrategy> _strategies;

        public AuthenticationService(IEnumerable<IAuthenticationStrategy> strategies)
        {
            if (!strategies.Any())
            {
                throw new ArgumentOutOfRangeException("strategies", "There must be at least one IAuthenticationStrategy");
            }

            _strategies = strategies;
        }

        public bool TryToApply()
        {
            return _strategies.Any(x => x.TryToApply());
        }

        public bool Authenticate(LoginRequest request)
        {
            return _strategies.Any(x => x.Authenticate(request));
        }
    }
}