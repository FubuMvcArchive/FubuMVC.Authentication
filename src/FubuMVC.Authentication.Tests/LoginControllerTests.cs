using System;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class when_going_to_the_login_screen_with_a_remembered_user : InteractionContext<LoginController>
    {
        private AuthenticationTicket theTicket;

        protected override void beforeEach()
        {
            theTicket = new AuthenticationTicket();
            MockFor<ITicketSource>().Stub(x => x.CurrentTicket()).Return(theTicket);
        }

        [Test]
        public void should_set_the_remembered_user_name_on_the_login_request_if_it_is_not_already_set()
        {
            var request = new LoginRequest()
            {
                UserName = null
            };

            theTicket.UserName = "jeremy";

            ClassUnderTest.Login(request);

            request.UserName.ShouldEqual("jeremy");
            request.RememberMe.ShouldBeTrue();
        }

        [Test]
        public void should_not_set_the_remembered_user_name_if_there_is_already_a_different_name()
        {
            var request = new LoginRequest()
            {
                UserName = "josh"
            };

            theTicket.UserName = "jeremy";

            ClassUnderTest.Login(request);

            request.UserName.ShouldEqual("josh");
            request.RememberMe.ShouldBeFalse();
        }

        [Test]
        public void does_nothing_if_there_is_no_remembered_user()
        {
            var request = new LoginRequest()
            {
                UserName = null
            };

            theTicket.UserName = null;

            ClassUnderTest.Login(request);

            request.UserName.ShouldBeNull();
            request.RememberMe.ShouldBeFalse();
        }
    }

    [TestFixture]
    public class LoginControllerTester : InteractionContext<LoginController>
    {
        private LoginRequest theRequest;
        private AuthenticationSettings theSettings;
        private AuthenticationTicket theTicket;

        protected override void beforeEach()
        {
            LocalSystemTime = DateTime.Today.AddHours(10);

            theSettings = new AuthenticationSettings();
            Services.Inject(theSettings);

            MockFor<ITicketSource>().Stub(x => x.CurrentTicket()).Return(theTicket = new AuthenticationTicket());

            theRequest = new LoginRequest();
        }


        [Test]
        public void show_initial_screen()
        {
            var request = new LoginRequest();
            ClassUnderTest.Login(request).ShouldBeTheSameAs(request);

            request.Message.ShouldBeNull();
        }

        [Test]
        public void delegate_to_the_failure_handler()
        {
            theRequest.Status = LoginStatus.Failed;
            Services.Inject<ILoginFailureHandler>(new StubLoginFailureHandler());

            ClassUnderTest.Login(theRequest);

            theRequest.Message.ShouldEqual(StubLoginFailureHandler.MESSAGE);
        }

        [Test]
        public void uses_the_unknown_message_when_no_message_is_set()
        {
            theRequest.Status = LoginStatus.Failed;
            ClassUnderTest.Login(theRequest);

            theRequest.Message.ShouldEqual(LoginKeys.Unknown.ToString());
        }

        public class StubLoginFailureHandler : ILoginFailureHandler
        {
            public const string MESSAGE = "Test";

            public void Handle(LoginRequest request, AuthenticationTicket ticket, AuthenticationSettings settings)
            {
                request.Message = MESSAGE;
            }
        }
    }
}