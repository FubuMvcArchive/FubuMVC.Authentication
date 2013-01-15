using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsAuthenticationServiceRegistry : ServiceRegistry
    {
        public WindowsAuthenticationServiceRegistry()
        {
            SetServiceIfNone<IWindowsAuthenticationContext, AspNetWindowsAuthenticationContext>();
            SetServiceIfNone<IWindowsPrincipalHandler, NulloWindowsPrincipalHandler>();
            SetServiceIfNone<IWindowsAuthentication, WindowsAuthentication>();
        }
    }
}