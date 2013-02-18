using System.Security.Principal;

namespace FubuMVC.Authentication.Windows
{
    public class DefaultWindowsPrincipalHandler : IWindowsPrincipalHandler
    {
        public bool Authenticated(IPrincipal principal)
        {
            return true;
        }
    }
}