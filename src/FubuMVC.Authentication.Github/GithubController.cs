using FubuMVC.Authentication.OAuth2;
using FubuMVC.Core;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication.Github
{
    [NotAuthenticated]
    public class GithubController
    {
        private readonly IOAuth2Callback _callback;
        private readonly IOAuth2Proxy _proxy;
        private readonly GithubSignInSettings _signInSettings;

        public GithubController(IOAuth2Callback callback, IOAuth2Proxy proxy, GithubSignInSettings signInSettings)
        {
            _callback = callback;
            _proxy = proxy;
            _signInSettings = signInSettings;
        }

        public GithubLoginRequest Button(GithubLoginRequest request)
        {
            return request;
        }


        [UrlPattern("login/github")]
        public void Login(GithubSignIn request)
        {
            _proxy.SignIn(_signInSettings);
        }

        [UrlPattern("login/github/callback")]
        public void Callback(GithubLoginCallback request)
        {
            _callback.Execute(_signInSettings);
        }
    }

    public class GithubLoginRequest : IOAuth2LoginRequest
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class GithubSignIn
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class GithubLoginCallback
    {
        [QueryString]
        public string Url { get; set; }
    }
}