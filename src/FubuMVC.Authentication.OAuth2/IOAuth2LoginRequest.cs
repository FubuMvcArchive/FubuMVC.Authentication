using FubuMVC.Core;

namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2LoginRequest
    {
        [QueryString]
        string Url { get; set; }
    }
}