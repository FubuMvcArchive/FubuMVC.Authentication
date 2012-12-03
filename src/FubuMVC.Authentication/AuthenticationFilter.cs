using System.Security.Principal;
using FubuCore;
using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication
{
    public class AuthenticationFilter
    {
        private readonly IAuthenticationService _authentication;
        private readonly IPrincipalContext _context;
        private readonly IAuthenticationRedirector _redirector;
        private readonly IAuthenticationSession _session;

        public AuthenticationFilter(IAuthenticationSession session, IAuthenticationService authentication,
                                    IPrincipalContext context, IAuthenticationRedirector redirector)
        {
            _session = session;
            _authentication = authentication;
            _context = context;
            _redirector = redirector;
        }

        public FubuContinuation Authenticate()
        {
            string userName = _session.PreviouslyAuthenticatedUser();
            if (userName.IsNotEmpty())
            {
                _session.MarkAccessed();
                IPrincipal principal = _authentication.Build(userName);
                _context.Current = principal;

                return FubuContinuation.NextBehavior();
            }

            return _redirector.Redirect();
        }
    }
}