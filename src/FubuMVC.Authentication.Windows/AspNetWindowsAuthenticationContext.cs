using System.Web;

namespace FubuMVC.Authentication.Windows
{
    public class AspNetWindowsAuthenticationContext : IWindowsAuthenticationContext
    {
        public string CurrentUser()
        {
            return HttpContext.Current.Request.ServerVariables["LOGON_USER"];
        }
    }
}