using FubuMVC.Authentication.OAuth;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Google
{
    [NotAuthenticated]
    public class GoogleController
    {
        private readonly IOAuthCallback _callback;
        private readonly IOAuthProxy _proxy;
        private readonly GoogleSignInSettings _signInSettings;

        public GoogleController(IOAuthCallback callback, IOAuthProxy proxy, GoogleSignInSettings signInSettings)
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

    public class GoogleLoginRequest : IOAuthLoginRequest
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