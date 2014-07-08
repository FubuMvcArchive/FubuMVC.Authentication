using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication
{
    public class LoggedInUserRedirector
    {
        private readonly IAuthenticationRedirect _redirector;
        private readonly IPrincipalContext _principal;

        public LoggedInUserRedirector(IAuthenticationRedirect redirector, IPrincipalContext principal)
        {
            _redirector = redirector;
            _principal = principal;
        }

        public FubuContinuation Execute()
        {
            return _principal.Current == null ? FubuContinuation.NextBehavior() : _redirector.Redirect();
        }
    }
}