using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class when_going_to_get_login : InteractionContext<LoginPageAccessFilter>
    {
        private FubuContinuation theContinuation;

        [Test]
        public void when_authenticated_with_restricted_access_to_login_should_continue_chain()
        {
            MockFor<ISecurityContext>().Stub(x => x.IsAuthenticated()).Return(true);
            MockFor<AuthenticationSettings>().AllowAccessToLogin = true;
            theContinuation = ClassUnderTest.LoginAccessFilter();
            theContinuation.AssertWasContinuedToNextBehavior();
        }

        [Test]
        public void when_authenticated_without_restricted_access_to_login_should_redirect_to_home()
        {
            MockFor<ISecurityContext>().Stub(x => x.IsAuthenticated()).Return(true);
            MockFor<AuthenticationSettings>().AllowAccessToLogin = false;
            theContinuation = ClassUnderTest.LoginAccessFilter();
            theContinuation.AssertWasRedirectedTo("~/");
        }

        [Test]
        public void when_not_authenticated_should_continue_chain()
        {
            MockFor<ISecurityContext>().Stub(x => x.IsAuthenticated()).Return(false);
            theContinuation = ClassUnderTest.LoginAccessFilter();
            theContinuation.AssertWasContinuedToNextBehavior();
        }
    }
}
