using FubuMVC.Core.Continuations;
using FubuMVC.Core.Http;

namespace FubuMVC.Authentication
{
    public class AuthenticationFilter
    {
        private readonly IAuthenticationService _authentication;
        private readonly ICurrentChain _currentChain;
        private readonly IAuthenticationRedirector _redirector;

        public AuthenticationFilter(IAuthenticationRedirector redirector, IAuthenticationService authentication, ICurrentChain currentChain)
        {
            _redirector = redirector;
            _authentication = authentication;
            _currentChain = currentChain;
        }

        public FubuContinuation Authenticate()
        {
            if (_currentChain.IsInPartial()) return FubuContinuation.NextBehavior();

            var result = _authentication.TryToApply();
            if (result.Continuation != null) return result.Continuation;

            return result.Success ? FubuContinuation.NextBehavior() : _redirector.Redirect();
        }
    }
}