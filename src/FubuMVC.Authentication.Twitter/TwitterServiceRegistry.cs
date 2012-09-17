using DotNetOpenAuth.OAuth.ChannelElements;
using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.Twitter
{
    public class TwitterServiceRegistry : ServiceRegistry
    {
        public TwitterServiceRegistry()
        {
            SetServiceIfNone<ITwitterProxy, TwitterProxy>();
            SetServiceIfNone<ITwitterCallback, TwitterCallback>();
            SetServiceIfNone<ITwitterResponseHandler, TwitterResponseHandler>();
            SetServiceIfNone<ISignInEndpointBuilder, SignInEndpointBuilder>();
            SetServiceIfNone<IConsumerTokenManager, InMemoryTokenManager>();
            SetServiceIfNone<ISystemUrls, SystemUrls>();

            ConfigureRequirements(x => x.AddRule<OAuthSettingsRule>());
        }
    }
}