using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore.Logging;
using FubuCore;

namespace FubuMVC.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<IAuthenticationStrategy> _strategies;

        public AuthenticationService(ILogger logger, IEnumerable<IAuthenticationStrategy> strategies)
        {
            if (!strategies.Any())
            {
                throw new ArgumentOutOfRangeException("strategies", "There must be at least one IAuthenticationStrategy");
            }

            _logger = logger;
            _strategies = strategies;
        }

        public AuthResult TryToApply()
        {
            foreach (var strategy in _strategies)
            {
                var result = strategy.TryToApply();
                _logger.Debug(() => "Authentication returned {0} from strategy {1}".ToFormat(result, strategy));
                if (result.IsDeterministic())
                {
                    return result;
                }
            }

            _logger.Debug(() => "No authentication strategies were able to make a deterministic authentication result");
            return new AuthResult{Success = false};
        }

        public bool Authenticate(LoginRequest request)
        {
            return _strategies.Any(x => x.Authenticate(request));
        }
    }
}