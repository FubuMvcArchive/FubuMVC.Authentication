using System.Security.Principal;
using FubuMVC.Authentication.Membership;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;

using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class when_authenticating_and_there_is_not_a_previous_authentication_token : InteractionContext<AuthenticationFilter>
    {
        private FubuContinuation theResult;
        private FubuContinuation theRedirect;

        protected override void beforeEach()
        {
            MockFor<IAuthenticationSession>().Stub(x => x.PreviouslyAuthenticatedUser())
                .Return(null);

            theRedirect = FubuContinuation.RedirectTo<LoginRequest>();

            MockFor<IAuthenticationRedirector>().Stub(x => x.Redirect())
                                                .Return(theRedirect);

            theResult = ClassUnderTest.Authenticate();
        }

        [Test]
        public void should_redirect_based_on_what_IAuthenticationRedirector_decides()
        {
            theResult.ShouldBeTheSameAs(theRedirect);
        }
    }

    [TestFixture]
    public class when_authenticating_and_there_is_a_previously_authenticated_user : InteractionContext<AuthenticationFilter>
    {
        private string theUserName;
        private IPrincipal thePrincipal;
        private FubuContinuation theResult;

        protected override void beforeEach()
        {
            theUserName = "a user";

            MockFor<IAuthenticationSession>().Stub(x => x.PreviouslyAuthenticatedUser())
                .Return(theUserName);

            thePrincipal = MockFor<IPrincipal>();


            MockFor<IPrincipalBuilder>().Stub(x => x.Build(theUserName))
                .Return(thePrincipal);

            theResult = ClassUnderTest.Authenticate();
        }

        [Test]
        public void should_mark_the_session_as_accessed_for_sliding_expirations()
        {
            MockFor<IAuthenticationSession>().AssertWasCalled(x => x.MarkAccessed());
        }

        [Test]
        public void should_continue()
        {
			theResult.AssertWasContinuedToNextBehavior();
        }

        [Test]
        public void should_set_the_principal_for_the_authenticated_user()
        {
            MockFor<IPrincipalContext>().AssertWasCalled(x => x.Current = thePrincipal);
        }
    }
}