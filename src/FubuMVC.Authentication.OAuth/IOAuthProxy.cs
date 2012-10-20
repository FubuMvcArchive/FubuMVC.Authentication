using System;

namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthProxy
    {
        void SignIn(IOAuthSignInSettings signInSettings);
        void Process(IOAuthSignInSettings signInSettings, Action<IOAuthResponse> success);
    }
}