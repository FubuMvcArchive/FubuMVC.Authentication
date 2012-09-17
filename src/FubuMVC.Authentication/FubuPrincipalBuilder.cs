using System.Security.Principal;

namespace FubuMVC.Authentication
{
    public class FubuPrincipalBuilder : IPrincipalBuilder
    {
        private readonly IPrincipalRoles _roles;

        public FubuPrincipalBuilder(IPrincipalRoles roles)
        {
            _roles = roles;
        }

        public IPrincipal Build(string userName)
        {
            return new FubuPrincipal(_roles, userName);
        }
    }
}