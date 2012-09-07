using FubuMVC.Authentication.Basic;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests.Basic
{
    [TestFixture]
    public class when_running_in_a_get : InteractionContext<LoginBehavior>
    {
        protected override void beforeEach()
        {
            ClassUnderTest.InsideBehavior = MockFor<IActionBehavior>();

            MockFor<ICurrentHttpRequest>().Stub(x => x.HttpMethod()).Return("GET");
        
            ClassUnderTest.Invoke();
        }

        [Test]
        public void the_inner_behavior_should_fire()
        {
            MockFor<IActionBehavior>().AssertWasCalled(x => x.Invoke());
        }

        [Test]
        public void should_not_even_try_to_authenticate()
        {
            MockFor<IAuthenticationService>().AssertWasNotCalled(x => x.Authenticate(null), x => x.IgnoreArguments());
        }
    }

    [TestFixture]
    public class when_successfully_authenticating : InteractionContext<LoginBehavior>
    {
        private LoginRequest theLoginRequest;

        protected override void beforeEach()
        {
            ClassUnderTest.InsideBehavior = MockFor<IActionBehavior>();

            MockFor<ICurrentHttpRequest>().Stub(x => x.HttpMethod()).Return("POST");

            theLoginRequest = new LoginRequest(){
                UserName = "frank",
                Url = "/where/i/wanted/to/go"
            };
            MockFor<IFubuRequest>().Stub(x => x.Get<LoginRequest>()).Return(theLoginRequest);

            MockFor<IAuthenticationService>().Stub(x => x.Authenticate(theLoginRequest)).Return(true);

            ClassUnderTest.Invoke();
        }

        [Test]
        public void should_mark_the_login_request_as_successful()
        {
            theLoginRequest.Status.ShouldEqual(LoginStatus.Succeeded);
        }

        [Test]
        public void should_mark_the_session_as_authenticated()
        {
            MockFor<IAuthenticationSession>().AssertWasCalled(x => x.MarkAuthenticated(theLoginRequest.UserName));
        }

        [Test]
        public void should_redirect_the_browser_to_the_original_url()
        {
            MockFor<IOutputWriter>().AssertWasCalled(x => x.RedirectToUrl(theLoginRequest.Url));
        }

        [Test]
        public void should_not_allow_the_inner_behavior_to_execute()
        {
            MockFor<IActionBehavior>().AssertWasNotCalled(x => x.Invoke());
        }
    }

    [TestFixture]
    public class when_UNsuccessfully_authenticating : InteractionContext<LoginBehavior>
    {
        private LoginRequest theLoginRequest;

        protected override void beforeEach()
        {
            ClassUnderTest.InsideBehavior = MockFor<IActionBehavior>();

            MockFor<ICurrentHttpRequest>().Stub(x => x.HttpMethod()).Return("POST");

            theLoginRequest = new LoginRequest()
            {
                UserName = "frank",
                Url = "/where/i/wanted/to/go",
                NumberOfTries = 2
            };
            MockFor<IFubuRequest>().Stub(x => x.Get<LoginRequest>()).Return(theLoginRequest);

            MockFor<IAuthenticationService>().Stub(x => x.Authenticate(theLoginRequest)).Return(false);

            ClassUnderTest.Invoke();
        }

        [Test]
        public void should_mark_the_login_request_as_failed()
        {
            theLoginRequest.Status.ShouldEqual(LoginStatus.Failed);
        }

        [Test]
        public void should_NOT_mark_the_session_as_authenticated()
        {
            MockFor<IAuthenticationSession>().AssertWasNotCalled(x => x.MarkAuthenticated(theLoginRequest.UserName));
        }

        [Test]
        public void should_not_redirect_the_browser_to_the_original_url()
        {
            MockFor<IOutputWriter>().AssertWasNotCalled(x => x.RedirectToUrl(theLoginRequest.Url));
        }

        [Test]
        public void should_allow_the_inner_behavior_to_execute()
        {
            MockFor<IActionBehavior>().AssertWasCalled(x => x.Invoke());
        }

        [Test]
        public void should_increment_the_number_of_retries()
        {
            theLoginRequest.NumberOfTries.ShouldEqual(3);
        }
    }
}