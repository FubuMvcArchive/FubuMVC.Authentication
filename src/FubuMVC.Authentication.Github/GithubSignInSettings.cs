using System;
using FubuMVC.Authentication.OAuth2;

namespace FubuMVC.Authentication.Github
{
    public class GithubSignInSettings : OAuth2SignInSettings
    {
        public GithubSignInSettings()
        {
            AuthEndpoint = new Uri("https://github.com/login/oauth/authorize");
            TokenEndpoint = new Uri("https://github.com/login/oauth/access_token");
            UserProfileEndpoint = new Uri("https://api.github.com/user?access_token=");
            SignInCallbackEndpoint = (request, urls) => new Uri(urls.FullUrlFor(request.Get<GithubLoginCallback>()));
            DataRequestScopes = new[] { "user.email" };
            UserProfileGraph = new GithubGraph();
        }
    }
}