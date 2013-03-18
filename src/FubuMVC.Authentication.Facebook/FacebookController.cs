using FubuMVC.Authentication.OAuth2;
using FubuMVC.Core;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication.Facebook
{
    [NotAuthenticated]
    public class FacebookController
    {
        private readonly IOAuth2Callback _callback;
        private readonly IOAuth2Proxy _proxy;
        private readonly FacebookSignInSettings _signInSettings;

        public FacebookController(IOAuth2Callback callback, IOAuth2Proxy proxy, FacebookSignInSettings signInSettings)
        {
            _callback = callback;
            _proxy = proxy;
            _signInSettings = signInSettings;
        }

        public FacebookLoginRequest Button(FacebookLoginRequest request)
        {
            return request;
        }


        [UrlPattern("login/facebook")]
        public void Login(FacebookSignIn request)
        {
            _proxy.SignIn(_signInSettings);
        }

        [UrlPattern("login/facebook/callback")]
        public void Callback(FacebookLoginCallback request)
        {
            _callback.Execute(_signInSettings);
        }
    }

    public class FacebookLoginRequest : IOAuth2LoginRequest
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class FacebookSignIn
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class FacebookLoginCallback
    {
        [QueryString]
        public string Url { get; set; }
    }
}