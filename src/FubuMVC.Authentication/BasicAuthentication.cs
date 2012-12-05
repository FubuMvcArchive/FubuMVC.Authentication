using System.Security.Principal;
using FubuCore;

namespace FubuMVC.Authentication
{
    public class BasicAuthentication : IAuthenticationStrategy
    {
        private readonly IAuthenticationSession _session;
        private readonly IPrincipalContext _context;
        private readonly IPrincipalBuilder _builder;
        private readonly ICredentialsAuthenticator _authenticator;

        public BasicAuthentication(IAuthenticationSession session, IPrincipalContext context, IPrincipalBuilder builder, ICredentialsAuthenticator authenticator)
        {
            _session = session;
            _context = context;
            _builder = builder;
            _authenticator = authenticator;
        }

        public bool TryToApply()
        {
            string userName = _session.PreviouslyAuthenticatedUser();
            if (userName.IsNotEmpty())
            {
                _session.MarkAccessed();
                IPrincipal principal = _builder.Build(userName);
                _context.Current = principal;

                return true;
            }

            return false;
        }

        public bool Authenticate(LoginRequest request)
        {
            if (_authenticator.AuthenticateCredentials(request))
            {
                request.Status = LoginStatus.Succeeded;
                _session.MarkAuthenticated(request.UserName);
            }
            else
            {
                request.Status = LoginStatus.Failed;
                request.NumberOfTries++;
            }

            return request.Status == LoginStatus.Succeeded;
        }
    }
}