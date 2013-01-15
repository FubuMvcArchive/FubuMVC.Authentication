using System.Security.Principal;

namespace FubuMVC.Authentication.Windows
{
    // This is probably going to vary by the host (i.e., ASP.NET vs. Self Host vs. OWIN). Good luck w/ OWIN.
    public interface IWindowsAuthenticationContext
    {
        WindowsPrincipal Current();
    }
}