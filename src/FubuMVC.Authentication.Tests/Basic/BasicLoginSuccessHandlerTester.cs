using FubuMVC.Authentication.Tickets.Basic;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests.Basic
{
    [TestFixture]
    public class BasicLoginSuccessHandlerTester : InteractionContext<BasicLoginSuccessHandler>
    {
        private LoginRequest theRequest;

        protected override void beforeEach()
        {
            theRequest = new LoginRequest();

            ClassUnderTest.LoggedIn(theRequest);
        }

        [Test]
        public void just_calls_the_login_redirect()
        {
            MockFor<IBasicLoginRedirect>().AssertWasCalled(x => x.Redirect(theRequest));
        }
    }
}