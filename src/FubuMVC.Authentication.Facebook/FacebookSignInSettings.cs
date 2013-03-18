using System;
using FubuMVC.Authentication.OAuth2;

namespace FubuMVC.Authentication.Facebook
{
    public class FacebookSignInSettings : OAuth2SignInSettings
    {
        public FacebookSignInSettings()
        {
            AuthEndpoint = new Uri("https://graph.facebook.com/oauth/authorize");
            TokenEndpoint = new Uri("https://graph.facebook.com/oauth/access_token");
            UserProfileEndpoint = new Uri("https://graph.facebook.com/me?access_token=");
            SignInCallbackEndpoint = (request, urls) => new Uri(urls.FullUrlFor(request.Get<FacebookLoginCallback>()));
            DataRequestScopes = new[] {"email"};
            UserProfileGraph = new FacebookGraph();
        }
    }
}