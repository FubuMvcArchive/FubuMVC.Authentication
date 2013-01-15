using System.Net;
using System.Security.Principal;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Windows.Testing
{
    [TestFixture]
    public class WindowsControllerTester : InteractionContext<WindowsController>
    {
        private WindowsPrincipal thePrincipal;
        private FubuContinuation theContinuation;
        private WindowsSignInRequest theRequest;

        protected override void beforeEach()
        {
            thePrincipal = new WindowsPrincipal(WindowsIdentity.GetAnonymous());
            theContinuation = FubuContinuation.EndWithStatusCode(HttpStatusCode.Accepted);
            theRequest = new WindowsSignInRequest();

            MockFor<IWindowsAuthenticationContext>().Stub(x => x.Current()).Return(thePrincipal);
            MockFor<IWindowsAuthentication>().Stub(x => x.Authenticate(theRequest, thePrincipal)).Return(theContinuation);
        }

        [Test]
        public void just_delegates_to_the_strategy()
        {
            ClassUnderTest.Login(theRequest).ShouldEqual(theContinuation);
        }
    }
}