using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests.Basic
{
    [TestFixture]
    public class BasicLoginRedirectTester : InteractionContext<BasicLoginRedirect>
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

            ClassUnderTest.Redirect(theLoginRequest);
        }

        [Test]
        public void should_redirect_the_browser_to_the_original_url()
        {
            MockFor<IOutputWriter>().AssertWasCalled(x => x.RedirectToUrl(theLoginRequest.Url));
        }
    }
}