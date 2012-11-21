using FubuMVC.Authentication.OAuth;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Google.Tests
{
    [TestFixture]
    public class when_handling_a_successful_google_response : InteractionContext<OAuthResponseHandler<GoogleLoginRequest>>
    {
        private GoogleLoginRequest theRequest;

        protected override void beforeEach()
        {
            theRequest = new GoogleLoginRequest { Url = "hello/there" };

            MockFor<IFubuRequest>().Stub(x => x.Get<GoogleLoginRequest>()).Return(theRequest);

            ClassUnderTest.Success(new OAuthResponse());
        }

        [Test]
        public void redirects_to_the_url()
        {
            MockFor<IOutputWriter>().AssertWasCalled(x => x.RedirectToUrl(theRequest.Url));
        }
    }

    [TestFixture]
    public class when_handling_a_failed_google_response : InteractionContext<OAuthResponseHandler<GoogleLoginRequest>>
    {
        private GoogleLoginRequest theRequest;
        private LoginRequest theLoginRequest;
        private IActionBehavior theLoginBehavior;

        protected override void beforeEach()
        {
            theRequest = new GoogleLoginRequest { Url = "hello/there" };

            MockFor<IFubuRequest>().Stub(x => x.Get<GoogleLoginRequest>()).Return(theRequest);

            theLoginBehavior = MockFor<IActionBehavior>();

            var chain = new BehaviorChain();
            chain.AddToEnd(ActionCall.For<LoginController>(x => x.Login(null)));

            var graph = new BehaviorGraph();
            graph.AddChain(chain);

            Services.Inject(graph);

            MockFor<IPartialFactory>().Stub(x => x.BuildPartial(chain)).Return(theLoginBehavior);

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