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
            SetServiceIfNone<IAuthenticationFilter, AuthenticationFilter>();
            SetServiceIfNone<IPrincipalBuilder, FubuPrincipalBuilder>();
            SetServiceIfNone<IPrincipalRoles, NulloPrincipalRoles>();

            // TODO -- Break out the "Basic" stuff into a separate registry
            SetServiceIfNone<IPrincipalContext, ThreadPrincipalContext>();
            SetServiceIfNone<ITicketSource, SimpleCookieTicketSource>();
            SetServiceIfNone<IEncryptor, Encryptor>();
            SetServiceIfNone<ILoginCookies, BasicFubuLoginCookies>();
            SetServiceIfNone<ILoginFailureHandler, NulloLoginFailureHandler>();

            AddService<IAuthenticationRedirect, DefaultAuthenticationRedirect>();
            AddService<IAuthenticationRedirect, AjaxAuthenticationRedirect>();
        }
    }
}