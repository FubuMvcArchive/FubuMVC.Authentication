using FubuMVC.Authentication.OAuth2;
using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.WindowsLive
{
    public class WindowsLiveServiceRegistry : ServiceRegistry
    {
        public WindowsLiveServiceRegistry()
        {
            SetServiceIfNone<IOAuth2Proxy, OAuth2Proxy>();
            SetServiceIfNone<IOAuth2Callback, OAuth2Callback>();
            SetServiceIfNone<IOAuth2SignInEndpointBuilder, OAuth2SignInEndpointBuilder>();
            SetServiceIfNone<ISystemUrls, SystemUrls>();
            SetServiceIfNone<IOAuth2ResponseHandler, OAuth2ResponseHandler<WindowsLiveLoginRequest>>();
        }
    }
}