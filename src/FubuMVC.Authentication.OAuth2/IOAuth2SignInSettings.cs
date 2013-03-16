using System;

using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2SignInSettings
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        Uri AuthEndpoint { get; }
        Uri TokenEndpoint { get; }
        Uri UserProfileEndpoint { get; }
        string[] DataRequestScopes { get; }
        IOAuth2Graph UserProfileGraph { get; }
        Func<IFubuRequest, ISystemUrls, Uri> SignInCallbackEndpoint { get; }
        bool IsConfigured();
    }
}