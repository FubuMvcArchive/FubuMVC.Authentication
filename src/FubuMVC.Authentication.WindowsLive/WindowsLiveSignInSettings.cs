using System;
using FubuMVC.Authentication.OAuth2;

namespace FubuMVC.Authentication.WindowsLive
{
    public class WindowsLiveSignInSettings : OAuth2SignInSettings
    {
        public WindowsLiveSignInSettings()
        {
            AuthEndpoint = new Uri("https://oauth.live.com/authorize");
            TokenEndpoint = new Uri("https://oauth.live.com/token");
            UserProfileEndpoint = new Uri("https://apis.live.net/v5.0/me?access_token=");
            SignInCallbackEndpoint = (request, urls) => new Uri(urls.FullUrlFor(request.Get<WindowsLiveLoginCallback>()));
            DataRequestScopes = new[] { "wl.signin", "wl.basic", "wl.emails" };
            UserProfileGraph = new WindowsLiveGraph();
        }        
    }
}