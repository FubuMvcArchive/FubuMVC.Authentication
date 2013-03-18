using System;
using FubuMVC.Authentication.OAuth2;

namespace FubuMVC.Authentication.Google
{
    public class GoogleSignInSettings : OAuth2SignInSettings
    {
        public GoogleSignInSettings()
        {
            AuthEndpoint = new Uri("https://accounts.google.com/o/oauth2/auth");
            TokenEndpoint = new Uri("https://accounts.google.com/o/oauth2/token");
            UserProfileEndpoint = new Uri("https://www.googleapis.com/" + "oauth2/v2/userinfo?access_token=");
            SignInCallbackEndpoint = (request, urls) => new Uri(urls.FullUrlFor(request.Get<GoogleLoginCallback>()));
            DataRequestScopes = new[] { new Uri("https://www.googleapis.com/auth/userinfo.email").ToString() };
            UserProfileGraph = new GoogleGraph();
        }
    }
}