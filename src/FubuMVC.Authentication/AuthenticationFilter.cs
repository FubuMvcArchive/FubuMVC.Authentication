using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using FubuCore;
using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication
{
    public class AuthenticationFilter
    {
        private readonly IPrincipalContext _context;
        private readonly IAuthenticationRedirector _redirector;
        private readonly IAuthenticationSession _session;
        private readonly IPrincipalBuilder _builder;

        public AuthenticationFilter(IAuthenticationSession session, IPrincipalBuilder builder,
                                    IPrincipalContext context, IAuthenticationRedirector redirector)
        {
            _session = session;
            _builder = builder;
            _context = context;
            _redirector = redirector;
        }

        public FubuContinuation Authenticate()
        {
            string userName = _session.PreviouslyAuthenticatedUser();
            if (userName.IsNotEmpty())
            {
                _session.MarkAccessed();
                IPrincipal principal = _builder.Build(userName);
                _context.Current = principal;

                return FubuContinuation.NextBehavior();
            }

            return _redirector.Redirect();
        }
    }
}