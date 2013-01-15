using System.Security.Principal;
using System.Web;

namespace FubuMVC.Authentication.Windows
{
    public class AspNetWindowsAuthenticationContext : IWindowsAuthenticationContext
    {
        public string CurrentUser()
        {
            var context = HttpContext.Current;
            var identity = context.Request.LogonUserIdentity;

            if (identity == null)
            {
                return string.Empty;
            }

            if (identity.IsAnonymous)
            {
                context.User = new WindowsPrincipal(WindowsIdentity.GetAnonymous());
            }
            else
            {
                context.User = new WindowsPrincipal(identity);
            }

            return context.User.Identity.Name;
        }
    }
}