using System;

namespace FubuMVC.Authentication.Twitter.Tests
{
    public class StubTwitterProxy : ITwitterProxy
    {
        private readonly TwitterAuthResponse _response;

        public StubTwitterProxy(TwitterAuthResponse response)
        {
            _response = response;
        }

        public void SignIn()
        {
            // no-op
        }

        public void Process(Action<TwitterAuthResponse> success)
        {
            success(_response);
        }
    }
}