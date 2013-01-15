using System.Security.Principal;
using System.Web;

namespace FubuMVC.Authentication.Windows
{
    public class AspNetWindowsAuthenticationContext : IWindowsAuthenticationContext
    {
        private readonly HttpContextBase _context;

        public AspNetWindowsAuthenticationContext(HttpContextBase context)
        {
            _context = context;
        }

        public WindowsPrincipal Current()
        {
            var identity = _context.Request.LogonUserIdentity;

            return identity == null ? null : new WindowsPrincipal(identity);
        }
    }
}