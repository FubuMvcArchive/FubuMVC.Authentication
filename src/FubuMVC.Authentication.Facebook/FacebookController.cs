using FubuMVC.Authentication.OAuth;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Facebook
{
    [NotAuthenticated]
    public class FacebookController
    {
        private readonly IOAuthCallback _callback;
        private readonly IOAuthProxy _proxy;
        private readonly FacebookSignInSettings _signInSettings;

        public FacebookController(IOAuthCallback callback, IOAuthProxy proxy, FacebookSignInSettings signInSettings)
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

    public class FacebookLoginRequest : IOAuthLoginRequest
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