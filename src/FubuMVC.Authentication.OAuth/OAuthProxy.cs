using System;
using System.IO;
using System.Net;
using DotNetOpenAuth.OAuth2;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.OAuth
{
    // TODO -- Harden this. It's a nightmare to test thanks to DotNetOpenAuth's internal and/or sealed types
    public class OAuthProxy : IOAuthProxy
    {
        private readonly IOAuthSignInEndpointBuilder _endpointBuilder;
        private readonly IFubuRequest _request;
        private readonly ISystemUrls _urls;

        public OAuthProxy(IOAuthSignInEndpointBuilder endpointBuilder, IFubuRequest request, ISystemUrls urls)
        {
            _endpointBuilder = endpointBuilder;
            _request = request;
            _urls = urls;
        }

        public void SignIn(IOAuthSignInSettings signInSettings)
        {
            var client = GetClient(signInSettings);
            IAuthorizationState authorization = client.ProcessUserAuthorization();
            if (authorization == null)
            {
                client.RequestUserAuthorization(signInSettings.DataRequestScopes, signInSettings.SignInCallbackEndpoint(_request, _urls));
            }
        }

        private WebServerClient GetClient(IOAuthSignInSettings signInSettings)
        {
            AuthorizationServerDescription authorizationServerDescription = _endpointBuilder.Build(signInSettings);
            var client = new WebServerClient(authorizationServerDescription)
                {
                    ClientIdentifier = signInSettings.ClientId,
                    ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(signInSettings.ClientSecret)
                };
            return client;
        }

        public void Process(IOAuthSignInSettings signInSettings, Action<IOAuthResponse> success)
        {
            var client = GetClient(signInSettings);
            IAuthorizationState tokenResponse = client.ProcessUserAuthorization();
            if (tokenResponse == null) return;

            WebRequest request = WebRequest.Create(signInSettings.UserProfileEndpoint + Uri.EscapeDataString(tokenResponse.AccessToken));
            using (WebResponse response = request.GetResponse())
            {
                OAuthResponse authResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    authResponse = signInSettings.UserProfileGraph.Deserialize(responseStream);
                }

                success(authResponse);
            }
        }
    }
}