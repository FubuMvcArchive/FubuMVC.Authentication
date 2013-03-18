using System;

namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2Proxy
    {
        void SignIn(IOAuth2SignInSettings signInSettings);
        void Process(IOAuth2SignInSettings signInSettings, Action<IOAuth2Response> success);
    }
}