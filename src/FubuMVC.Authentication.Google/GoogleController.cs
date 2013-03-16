using FubuMVC.Authentication.OAuth2;
using FubuMVC.Core;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication.Google
{
    [NotAuthenticated]
    public class GoogleController
    {
        private readonly IOAuth2Callback _callback;
        private readonly IOAuth2Proxy _proxy;
        private readonly GoogleSignInSettings _signInSettings;

        public GoogleController(IOAuth2Callback callback, IOAuth2Proxy proxy, GoogleSignInSettings signInSettings)
        {
            _callback = callback;
            _proxy = proxy;
            _signInSettings = signInSettings;
        }

        public GoogleLoginRequest Button(GoogleLoginRequest request)
        {
            return request;
        }


        [UrlPattern("login/google")]
        public void Login(GoogleSignIn request)
        {
            _proxy.SignIn(_signInSettings);
        }

        [UrlPattern("login/google/callback")]
        public void Callback(GoogleLoginCallback request)
        {
            _callback.Execute(_signInSettings);
        }
    }

    public class GoogleLoginRequest : IOAuth2LoginRequest
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class GoogleSignIn
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class GoogleLoginCallback
    {
        [QueryString]
        public string Url { get; set; }
    }
}