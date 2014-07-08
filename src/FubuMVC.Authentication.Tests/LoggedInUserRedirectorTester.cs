using System;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Security.Principal;
using FubuMVC.Authentication.Membership;
using FubuMVC.Core.Registration.Nodes;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class LoggedInUserRedirectorTester
    {
        private IAuthenticationRedirect _redirector;
        private IPrincipalContext _principal;
        private LoggedInUserRedirector _theRedirector;

        [SetUp]
        public void BeforeEach()
        {
            _redirector = MockRepository.GenerateMock<IAuthenticationRedirect>();
            _principal = MockRepository.GenerateMock<IPrincipalContext>();
            _theRedirector = new LoggedInUserRedirector(_redirector, _principal);
        }

        [Test]
        public void it_continues_if_we_have_no_principal()
        {
            _principal.Stub(x => x.Current).Return(null);
            var theContinuation = _theRedirector.Execute();
            theContinuation.AssertWasContinuedToNextBehavior();
        }

        [Test]
        public void it_redirects_if_we_already_have_a_principal()
        {
            _principal.Stub(x => x.Current).Return(new GenericPrincipal(new GenericIdentity("just a guy"), new string[0]));
            _theRedirector.Execute();
            _redirector.AssertWasCalled(x => x.Redirect());
        }
    }
}