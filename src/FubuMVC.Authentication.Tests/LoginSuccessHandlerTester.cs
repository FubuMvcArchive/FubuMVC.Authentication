using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class LoginSuccessHandlerTester : InteractionContext<LoginSuccessHandler>
    {
        private LoginRequest theLoginRequest;

        protected override void beforeEach()
        {
            MockFor<ICurrentHttpRequest>().Stub(x => x.HttpMethod()).Return("POST");

            theLoginRequest = new LoginRequest()
            {
                UserName = "frank",
                Url = "/where/i/wanted/to/go"
            };

            
        }

        [Test]
        public void should_redirect_the_browser_to_the_original_url()
        {
            ClassUnderTest.LoggedIn(theLoginRequest).AssertWasRedirectedTo(theLoginRequest.Url);
        }
    }
}