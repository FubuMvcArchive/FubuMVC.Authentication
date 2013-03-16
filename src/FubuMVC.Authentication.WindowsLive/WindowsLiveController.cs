using FubuMVC.Authentication.OAuth2;
using FubuMVC.Core;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication.WindowsLive
{
    [NotAuthenticated]
    public class WindowsLiveController
    {
        private readonly IOAuth2Callback _callback;
        private readonly IOAuth2Proxy _proxy;
        private readonly WindowsLiveSignInSettings _signInSettings;

        public WindowsLiveController(IOAuth2Callback callback, IOAuth2Proxy proxy, WindowsLiveSignInSettings signInSettings)
        {
            _callback = callback;
            _proxy = proxy;
            _signInSettings = signInSettings;
        }

        public WindowsLiveLoginRequest Button(WindowsLiveLoginRequest request)
        {
            return request;
        }


        [UrlPattern("login/windowslive")]
        public void Login(WindowsLiveSignIn request)
        {
            _proxy.SignIn(_signInSettings);
        }

        [UrlPattern("login/windowslive/callback")]
        public void Callback(WindowsLiveLoginCallback request)
        {
            _callback.Execute(_signInSettings);
        }
    }

    public class WindowsLiveLoginRequest : IOAuth2LoginRequest
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class WindowsLiveSignIn
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class WindowsLiveLoginCallback
    {
        [QueryString]
        public string Url { get; set; }
    }
}