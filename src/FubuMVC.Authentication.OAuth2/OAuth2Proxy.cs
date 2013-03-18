using System;
using System.IO;
using System.Net;
using DotNetOpenAuth.OAuth2;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.OAuth2
{
    // TODO -- Harden this. It's a nightmare to test thanks to DotNetOpenAuth's internal and/or sealed types
    public class OAuth2Proxy : IOAuth2Proxy
    {
        private readonly IOAuth2SignInEndpointBuilder _endpointBuilder;
        private readonly IFubuRequest _request;
        private readonly ISystemUrls _urls;

        public OAuth2Proxy(IOAuth2SignInEndpointBuilder endpointBuilder, IFubuRequest request, ISystemUrls urls)
        {
            _endpointBuilder = endpointBuilder;
            _request = request;
            _urls = urls;
        }

        public void SignIn(IOAuth2SignInSettings signInSettings)
        {
            var client = GetClient(signInSettings);
            IAuthorizationState authorization = client.ProcessUserAuthorization();
            if (authorization == null)
            {
                client.RequestUserAuthorization(signInSettings.DataRequestScopes, signInSettings.SignInCallbackEndpoint(_request, _urls));
            }
        }

        private WebServerClient GetClient(IOAuth2SignInSettings signInSettings)
        {
            AuthorizationServerDescription authorizationServerDescription = _endpointBuilder.Build(signInSettings);
            var client = new WebServerClient(authorizationServerDescription)
                {
                    ClientIdentifier = signInSettings.ClientId,
                    ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(signInSettings.ClientSecret)
                };
            return client;
        }

        public void Process(IOAuth2SignInSettings signInSettings, Action<IOAuth2Response> success)
        {
            var client = GetClient(signInSettings);
            IAuthorizationState tokenResponse = client.ProcessUserAuthorization();
            if (tokenResponse == null || tokenResponse.AccessToken == null) return;

            WebRequest request = WebRequest.Create(signInSettings.UserProfileEndpoint + Uri.EscapeDataString(tokenResponse.AccessToken));
            using (WebResponse response = request.GetResponse())
            {
                OAuth2Response authResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    authResponse = signInSettings.UserProfileGraph.Deserialize(responseStream);
                }

                success(authResponse);
            }
        }
    }
}