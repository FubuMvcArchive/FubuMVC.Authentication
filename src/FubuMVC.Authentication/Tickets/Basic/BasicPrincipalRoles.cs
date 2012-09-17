using System.Security.Principal;

namespace FubuMVC.Authentication.Tickets.Basic
{
    public class NulloPrincipalRoles : IPrincipalRoles
    {
        public bool Has(IPrincipal principal, string role)
        {
            return false;
        }
    }
}