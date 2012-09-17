using System;

namespace FubuMVC.Authentication.Twitter
{
    public interface ITwitterProxy
    {
        void SignIn();
        void Process(Action<TwitterAuthResponse> success);
    }
}