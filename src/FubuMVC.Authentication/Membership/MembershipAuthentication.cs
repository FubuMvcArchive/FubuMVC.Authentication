using System.Security.Principal;

namespace FubuMVC.Authentication.Membership
{
    public class MembershipAuthentication : IAuthenticationStrategy, IPrincipalBuilder, ICredentialsAuthenticator
    {
        private readonly IMembershipRepository _membership;
        private readonly BasicAuthentication _inner;

        public MembershipAuthentication(IAuthenticationSession session, IPrincipalContext context, IMembershipRepository membership)
        {
            _membership = membership;
            _inner = new BasicAuthentication(session, context, this, this);
        }

        public bool TryToApply()
        {
            return _inner.TryToApply();
        }

        public bool Authenticate(LoginRequest request)
        {
            return _inner.Authenticate(request);
        }

        public bool AuthenticateCredentials(LoginRequest request)
        {
            return _membership.MatchesCredentials(request);
        }

        public IMembershipRepository Membership
        {
            get { return _membership; }
        }

        public IPrincipal Build(string userName)
        {
            var user = _membership.FindByName(userName);

            return new FubuPrincipal(user);
        }
    }
}