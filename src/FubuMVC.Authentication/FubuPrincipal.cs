using System.Security.Principal;

namespace FubuMVC.Authentication
{
    public class FubuPrincipal : IPrincipal, IIdentity
    {
        private readonly IPrincipalRoles _roles;

        public FubuPrincipal(IPrincipalRoles roles, string username)
        {
            Name = username;
            _roles = roles;
        }

        public bool IsInRole(string role)
        {
            return _roles.Has(this, role);
        }

        public IIdentity Identity
        {
            get { return this; }
        }

        public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public static FubuPrincipal Current()
        {
            var context = new ThreadPrincipalContext();
            return context.Current as FubuPrincipal;
        }
    }
}