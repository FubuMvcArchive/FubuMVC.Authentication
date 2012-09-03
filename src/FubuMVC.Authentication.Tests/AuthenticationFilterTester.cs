using System.Security.Principal;
using FubuTestingSupport;
using NUnit.Framework;

using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class when_authenticating_and_there_is_not_a_previous_authentication_token : InteractionContext<AuthenticationFilter>
    {
		private AuthenticationFilterResult theResult;

        protected override void beforeEach()
        {
            MockFor<IAuthenticationSession>().Stub(x => x.PreviouslyAuthenticatedUser())
                .Return(null);

            theResult = ClassUnderTest.Authenticate();
        }

        [Test]
        public void should_redirect()
        {
        	theResult.ShouldEqual(AuthenticationFilterResult.Redirect);
        }
    }

    [TestFixture]
    public class when_authenticating_and_there_is_a_previously_authenticated_user : InteractionContext<AuthenticationFilter>
    {
        private string theUserName;
        private IPrincipal thePrincipal;
		private AuthenticationFilterResult theResult;

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
			theResult.ShouldEqual(AuthenticationFilterResult.Continue);
        }

        [Test]
        public void should_set_the_principal_for_the_authenticated_user()
        {
            MockFor<IPrincipalContext>().AssertWasCalled(x => x.Current = thePrincipal);
        }
    }
}