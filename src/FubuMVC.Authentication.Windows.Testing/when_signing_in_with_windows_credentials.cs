using System.Security.Principal;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Windows.Testing
{
    [TestFixture]
    public class when_signing_in_with_windows_credentials : InteractionContext<WindowsController>
    {
        private WindowsSignInRequest theRequest;
        private string theCurrentUser;

        protected override void beforeEach()
        {
            theRequest = new WindowsSignInRequest { Url = "test/url" };
            theCurrentUser = "theuser";

            MockFor<IWindowsAuthenticationContext>().Stub(x => x.CurrentUser()).Return(theCurrentUser);

            ClassUnderTest.Login(theRequest);
        }

        [Test]
        public void marks_the_session_as_authenticated()
        {
            MockFor<IAuthenticationSession>().AssertWasCalled(x => x.MarkAuthenticated(theCurrentUser));
        }

        [Test]
        public void redirects_to_the_return_url()
        {
            MockFor<IOutputWriter>().AssertWasCalled(x => x.RedirectToUrl(theRequest.Url));
        }
    }

    public class StubIdentity : IIdentity
    {
        public StubIdentity(string name, bool isAuthenticated)
        {
            Name = name;
            IsAuthenticated = isAuthenticated;
        }

        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}