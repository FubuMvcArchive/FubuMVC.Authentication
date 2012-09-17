using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class when_twitter_authentication_succeeds : InteractionContext<TwitterCallback>
    {
        private ITwitterResponseHandler theHandlers;
        private TwitterAuthResponse theResponse;

        protected override void beforeEach()
        {
            theHandlers = MockFor<ITwitterResponseHandler>();

            theResponse = new TwitterAuthResponse(123, "test_user", new StubTokenResponse { AccessToken = "Test1234"});

            Services.Inject<ITwitterProxy>(new StubTwitterProxy(theResponse));

            ClassUnderTest.Execute();
        }
        
        [Test]
        public void does_not_call_the_failure_handler()
        {
            theHandlers.AssertWasNotCalled(x => x.Failure());
        }

        [Test]
        public void calls_the_success_handler()
        {
            theHandlers.AssertWasCalled(x => x.Success());
        }

        [Test]
        public void marks_the_session_authenticated()
        {
            MockFor<IAuthenticationSession>().AssertWasCalled(x => x.MarkAuthenticated(theResponse.ScreenName));
        }
    }

    [TestFixture]
    public class when_twitter_authentication_fails : InteractionContext<TwitterCallback>
    {
        private ITwitterResponseHandler theHandlers;

        protected override void beforeEach()
        {
            theHandlers = MockFor<ITwitterResponseHandler>();
            ClassUnderTest.Execute();
        }

        [Test]
        public void does_not_call_the_success_handler()
        {
            theHandlers.AssertWasNotCalled(x => x.Success());
        }

        [Test]
        public void calls_the_failure_handler()
        {
            theHandlers.AssertWasCalled(x => x.Failure());
        }
    }
}