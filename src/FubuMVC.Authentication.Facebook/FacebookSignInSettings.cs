using System;
using FubuCore;
using FubuMVC.Authentication.OAuth;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Facebook
{
    public class FacebookSignInSettings : IOAuthSignInSettings
    {
        public FacebookSignInSettings()
        {
            AuthEndpoint = Url("oauth/authorize");
            TokenEndpoint = Url("oauth/access_token");
            UserProfileEndpoint = Url("me?access_token=");
            SignInCallbackEndpoint = (request, urls) => new Uri(urls.FullUrlFor(request.Get<FacebookLoginCallback>()));
            DataRequestScopes = new[] {"email"};
            UserProfileGraph = new FacebookGraph();
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public Uri AuthEndpoint { get; private set; }
        public Uri TokenEndpoint { get; private set; }
        public Uri UserProfileEndpoint { get; private set; }
        public string[] DataRequestScopes { get; private set; }
        public IOAuthGraph UserProfileGraph { get; private set; }
        public Func<IFubuRequest, ISystemUrls, Uri> SignInCallbackEndpoint { get; set; }

        public bool IsConfigured()
        {
            return !ClientId.IsEmpty() && !ClientSecret.IsEmpty();
        }

        private static Uri Url(string relative)
        {
            return new Uri("https://graph.facebook.com/" + relative);
        }
    }
}