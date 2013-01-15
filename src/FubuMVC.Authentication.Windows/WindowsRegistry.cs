using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsRegistry : ServiceRegistry
    {
        public WindowsRegistry()
        {
            SetServiceIfNone<IWindowsAuthenticationContext, AspNetWindowsAuthenticationContext>();
            //AddService<IAuthenticationStrategy, WindowsAuthenticationStrategy>();
            //SetServiceIfNone<IWindowsPrincipalBuilder, PassthroughWindowsPrincipalBuilder>();
        }
    }
}