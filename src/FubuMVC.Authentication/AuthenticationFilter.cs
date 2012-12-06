using System.Diagnostics;
using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication
{
    public class AuthenticationFilter
    {
        private readonly IAuthenticationService _authentication;
        private readonly IAuthenticationRedirector _redirector;

        public AuthenticationFilter(IAuthenticationRedirector redirector, IAuthenticationService authentication)
        {
            _redirector = redirector;
            _authentication = authentication;
        }

        public FubuContinuation Authenticate()
        {
            return _authentication.TryToApply() ? FubuContinuation.NextBehavior() : _redirector.Redirect();
        }
    }
}