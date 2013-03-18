using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.OAuth2
{
    public class OAuth2ServiceRegistry : ServiceRegistry
    {
        public OAuth2ServiceRegistry()
        {
            SetServiceIfNone<IOAuth2Proxy, OAuth2Proxy>();
            SetServiceIfNone<IOAuth2Callback, OAuth2Callback>();
            SetServiceIfNone<IOAuth2SignInEndpointBuilder, OAuth2SignInEndpointBuilder>();
            SetServiceIfNone<ISystemUrls, SystemUrls>();
        }
    }
}