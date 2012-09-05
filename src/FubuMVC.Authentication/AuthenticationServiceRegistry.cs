using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication
{
    public class AuthenticationServiceRegistry : ServiceRegistry
    {
        public AuthenticationServiceRegistry()
        {
            SetServiceIfNone<IAuthenticationSession, TicketAuthenticationSession>();
            SetServiceIfNone<IPrincipalContext, ThreadPrincipalContext>();
            SetServiceIfNone<ITicketSource, SimpleCookieTicketSource>();

            AddService<IAuthenticationRedirect, DefaultAuthenticationRedirect>();
            AddService<IAuthenticationRedirect, AjaxAuthenticationRedirect>();
        }
    }
}