using FubuMVC.Authentication.OAuth;
using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.Google
{
    public class GoogleServiceRegistry : ServiceRegistry
    {
        public GoogleServiceRegistry()
        {
            SetServiceIfNone<IOAuthProxy, OAuthProxy>();

            SetServiceIfNone<IOAuthCallback, OAuthCallback>();
            SetServiceIfNone<IOAuthSignInEndpointBuilder, OAuthSignInEndpointBuilder>();
            SetServiceIfNone<IOAuthResponseHandler, OAuthResponseHandler<GoogleLoginRequest>>();
            SetServiceIfNone<ISystemUrls, SystemUrls>();
        }
    }
}