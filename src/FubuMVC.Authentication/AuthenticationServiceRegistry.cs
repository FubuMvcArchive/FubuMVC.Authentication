using FubuMVC.Authentication.Basic;
using FubuMVC.Authentication.Tickets;
using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication
{
    public class AuthenticationServiceRegistry : ServiceRegistry
    {
        public AuthenticationServiceRegistry()
        {
            SetServiceIfNone<IAuthenticationSession, TicketAuthenticationSession>();
            SetServiceIfNone<IAuthenticationFilter, AuthenticationFilter>();

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