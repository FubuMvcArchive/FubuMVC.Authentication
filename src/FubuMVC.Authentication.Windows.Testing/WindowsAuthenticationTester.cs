using System.Security.Principal;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Windows.Testing
{
    [TestFixture]
    public class WindowsAuthenticationTester : InteractionContext<WindowsAuthentication>
    {
        private FubuContinuation theContinuation;
        private WindowsPrincipal thePrincipal;
        private WindowsSignInRequest theRequest;

        protected override void beforeEach()
        {
            thePrincipal = new WindowsPrincipal(WindowsIdentity.GetAnonymous());
            theRequest = new WindowsSignInRequest();

            theContinuation = ClassUnderTest.Authenticate(theRequest, thePrincipal);
        }

        [Test]
        public void invokes_the_principal_handler()
        {
            MockFor<IWindowsPrincipalHandler>().AssertWasCalled(x => x.Authenticated(thePrincipal));
        }

        [Test]
        public void redirects_the_redirect_url()
        {
            theContinuation.AssertWasRedirectedTo(theRequest.Url);
        }

        [Test]
        public void marks_the_session_as_authenticated()
        {
            MockFor<IAuthenticationSession>().AssertWasCalled(x => x.MarkAuthenticated(thePrincipal.Identity.Name));
        }
    }
}