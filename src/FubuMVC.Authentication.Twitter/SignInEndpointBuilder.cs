using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace FubuMVC.Authentication.Twitter
{
    public class SignInEndpointBuilder : ISignInEndpointBuilder
    {
        private readonly TwitterSignInSettings _settings;

        public SignInEndpointBuilder(TwitterSignInSettings settings)
        {
            _settings = settings;
        }

        public ServiceProviderDescription Build()
        {
            return new ServiceProviderDescription
                       {
                           RequestTokenEndpoint = new MessageReceivingEndpoint(_settings.RequestToken, HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
                           UserAuthorizationEndpoint = new MessageReceivingEndpoint(_settings.UserAuthorization, HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
                           AccessTokenEndpoint = new MessageReceivingEndpoint(_settings.AccessToken, HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
                           TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement()  }
                       };
        }
    }
}