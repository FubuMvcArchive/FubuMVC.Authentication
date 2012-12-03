using FubuMVC.Authentication.Membership;
using FubuMVC.Authentication.Membership.FlatFile;
using FubuMVC.Authentication.Tickets;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication
{
    public class AuthenticationServiceRegistry : ServiceRegistry
    {
        public AuthenticationServiceRegistry()
        {
            SetServiceIfNone<IAuthenticationSession, TicketAuthenticationSession>();

            // TODO -- Break out the "Basic" stuff into a separate registry
            SetServiceIfNone<IPrincipalContext, ThreadPrincipalContext>();
            SetServiceIfNone<ITicketSource, CookieTicketSource>();
            SetServiceIfNone<IEncryptor, Encryptor>();
            SetServiceIfNone<ILoginCookies, BasicFubuLoginCookies>();
            SetServiceIfNone<ILoginCookieService, LoginCookieService>();
            SetServiceIfNone<IBasicLoginRedirect, BasicLoginRedirect>();
            SetServiceIfNone<ILoginSuccessHandler, BasicLoginSuccessHandler>();
            SetServiceIfNone<ILoginFailureHandler, NulloLoginFailureHandler>();
            SetServiceIfNone<IAuthenticationRedirector, AuthenticationRedirector>();
            SetServiceIfNone<IAuthenticationService, MembershipAuthenticationService>();

            SetServiceIfNone<IMembershipRepository, FlatFileMembershipRepository>();
        }
    }
}