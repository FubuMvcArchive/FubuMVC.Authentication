using System;
using FubuCore;
using FubuMVC.Authentication.OAuth;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Google
{
    public class GoogleSignInSettings : IOAuthSignInSettings
    {
        public GoogleSignInSettings()
        {
            AuthEndpoint = AuthUrl("auth");
            TokenEndpoint = AuthUrl("token");
            UserProfileEndpoint = ReqUrl("oauth2/v2/userinfo?access_token=");
            SignInCallbackEndpoint = (request, urls) => new Uri(urls.FullUrlFor(request.Get<GoogleLoginCallback>()));
            DataRequestScopes = new[] { ReqUrl("auth/userinfo.email").ToString() };
            UserProfileGraph = new GoogleGraph();
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

        private static Uri AuthUrl(string relative)
        {
            return new Uri("https://accounts.google.com/o/oauth2/" + relative);
        }

        private static Uri ReqUrl(string relative)
        {
            return new Uri("https://www.googleapis.com/" + relative);
        }
    }
}