using FubuMVC.Authentication.Endpoints;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests.Endpoints
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
        public void should_audit_the_request()
        {
            MockFor<ILoginAuditor>().AssertWasCalled(x => x.Audit(theLoginRequest));
        }

        [Test]
        public void should_not_allow_the_inner_behavior_to_execute()
        {
            MockFor<IActionBehavior>().AssertWasNotCalled(x => x.Invoke());
        }

        [Test]
        public void should_signal_the_success_handler()
        {
            MockFor<ILoginSuccessHandler>().AssertWasCalled(x => x.LoggedIn(theLoginRequest));
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
        public void should_audit_the_request()
        {
            MockFor<ILoginAuditor>().AssertWasCalled(x => x.Audit(theLoginRequest));
        }

        [Test]
        public void should_not_signal_the_success_handler()
        {
            MockFor<ILoginSuccessHandler>().AssertWasNotCalled(x => x.LoggedIn(theLoginRequest));
        }

        [Test]
        public void should_allow_the_inner_behavior_to_execute()
        {
            MockFor<IActionBehavior>().AssertWasCalled(x => x.Invoke());
        }
    }
}