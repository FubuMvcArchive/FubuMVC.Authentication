using FubuMVC.Authentication.Basic;
using FubuMVC.Authentication.Tickets;
using StructureMap.Configuration.DSL;

namespace FubuMVC.Authentication
{
    public class AuthenticationRegistry : Registry
    {
        public AuthenticationRegistry()
        {
            //For<IAuthenticationService>().Use<OurAuthenticationService>();
            For<IAuthenticationSession>().Use<TicketAuthenticationSession>();
            //For<IPrincipalBuilder>().Use<OurPrincipalBuilder>();
            For<IPrincipalContext>().Use<ThreadPrincipalContext>();
            For<ITicketSource>().Use<SimpleCookieTicketSource>();

            For<IAuthenticationRedirect>().Add<DefaultAuthenticationRedirect>();
            For<IAuthenticationRedirect>().Add<AjaxAuthenticationRedirect>();
        }
    }
}