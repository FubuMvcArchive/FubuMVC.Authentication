using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class when_handling_a_successful_twitter_response : InteractionContext<TwitterResponseHandler>
    {
        private TwitterLoginRequest theRequest;

        protected override void beforeEach()
        {
            theRequest = new TwitterLoginRequest {Url = "hello/there"};

            MockFor<IFubuRequest>().Stub(x => x.Get<TwitterLoginRequest>()).Return(theRequest);

            ClassUnderTest.Success();
        }

        [Test]
        public void redirects_to_the_url()
        {
            MockFor<IOutputWriter>().AssertWasCalled(x => x.RedirectToUrl(theRequest.Url));
        }
    }

    [TestFixture]
    public class when_handling_a_failed_twitter_response : InteractionContext<TwitterResponseHandler>
    {
        private TwitterLoginRequest theRequest;
        private LoginRequest theLoginRequest;
        private IActionBehavior theLoginBehavior;

        protected override void beforeEach()
        {
            theRequest = new TwitterLoginRequest { Url = "hello/there" };

            MockFor<IFubuRequest>().Stub(x => x.Get<TwitterLoginRequest>()).Return(theRequest);

            theLoginBehavior = MockFor<IActionBehavior>();
            MockFor<IPartialFactory>().Stub(x => x.BuildPartial(typeof (LoginRequest))).Return(theLoginBehavior);

            ClassUnderTest.Failure();

            theLoginRequest = (LoginRequest)MockFor<IFubuRequest>().GetArgumentsForCallsMadeOn(x => x.Set(Arg<LoginRequest>.Is.NotNull))[0][0];
        }

        [Test]
        public void sets_the_url()
        {
            theLoginRequest.Url.ShouldEqual(theRequest.Url);
        }

        [Test]
        public void sets_the_message()
        {
            theLoginRequest.Message.ShouldEqual(LoginKeys.Unknown.ToString());
        }

        [Test]
        public void transfers_to_the_login_chain()
        {
            theLoginBehavior.AssertWasCalled(x => x.InvokePartial());
        }
    }
}