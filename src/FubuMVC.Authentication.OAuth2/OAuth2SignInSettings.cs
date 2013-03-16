using System;
using FubuCore;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.OAuth2
{
    public class OAuth2SignInSettings : IOAuth2SignInSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public Uri AuthEndpoint { get; set; }
        public Uri TokenEndpoint { get; set; }
        public Uri UserProfileEndpoint { get; set; }
        public string[] DataRequestScopes { get; set; }
        public IOAuth2Graph UserProfileGraph { get; set; }
        public Func<IFubuRequest, ISystemUrls, Uri> SignInCallbackEndpoint { get; set; }

        public bool IsConfigured()
        {
            return !this.ClientId.IsEmpty() && !this.ClientSecret.IsEmpty();
        }
    }
}