using System.Security.Principal;

namespace FubuMVC.Authentication.Windows
{
    public interface IWindowsPrincipalHandler
    {
        bool Authenticated(IPrincipal principal);
    }
}