using FubuMVC.Core;

namespace FubuMVC.Authentication.Twitter
{
    [NotAuthenticated]
    public class TwitterController
    {
        private readonly ITwitterCallback _callback;
        private readonly ITwitterProxy _proxy;

        public TwitterController(ITwitterCallback callback, ITwitterProxy proxy)
        {
            _callback = callback;
            _proxy = proxy;
        }

        public TwitterLoginRequest Button(TwitterLoginRequest request)
        {
            return request;
        }

        [UrlPattern("login/twitter")]
        public void Login(TwitterSignIn request)
        {
            _proxy.SignIn();
        }

        [UrlPattern("login/twitter/callback")]
        public void Callback(TwitterLoginCallback request)
        {
            _callback.Execute();
        }
    }

    public class TwitterLoginRequest
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class TwitterSignIn
    {
        [QueryString]
        public string Url { get; set; }
    }

    public class TwitterLoginCallback
    {
        [QueryString]
        public string Url { get; set; }
    }
}