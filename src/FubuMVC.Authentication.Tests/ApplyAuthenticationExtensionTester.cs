using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class ApplyAuthenticationExtensionTester
    {
        private FubuRegistry theRegistry;

        [SetUp]
        public void SetUp()
        {
            theRegistry = new FubuRegistry();
        }

        private BehaviorGraph theGraph
        {
            get { return BehaviorGraph.BuildFrom(theRegistry); }
        }

        [Test]
        public void adds_the_redirect()
        {
            theRegistry.Import<ApplyAuthentication>(x => x.RedirectWith<StubRedirect>());
            theGraph.Services.ServicesFor<IAuthenticationRedirect>().Any(x => x.Type == typeof (StubRedirect)).ShouldBeTrue();
        }

        public class StubRedirect : IAuthenticationRedirect
        {
            public bool Applies()
            {
                throw new System.NotImplementedException();
            }

            public FubuContinuation Redirect()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}