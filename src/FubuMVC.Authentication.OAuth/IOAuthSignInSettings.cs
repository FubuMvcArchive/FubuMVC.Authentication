using System;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthSignInSettings
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        Uri AuthEndpoint { get; }
        Uri TokenEndpoint { get; }
        Uri UserProfileEndpoint { get; }
        string[] DataRequestScopes { get; }
        IOAuthGraph UserProfileGraph { get; }
        Func<IFubuRequest, ISystemUrls, Uri> SignInCallbackEndpoint { get; }
        bool IsConfigured();
    }
}