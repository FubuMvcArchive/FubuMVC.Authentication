using System.Security.Principal;

namespace FubuMVC.Authentication
{
    public interface IPrincipalRoles
    {
        bool Has(IPrincipal principal, string role);
    }
}