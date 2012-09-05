using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class AuthenticationServiceRegistryTester
    {
        private ServiceGraph theServiceGraph;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Services<AuthenticationServiceRegistry>();

            theServiceGraph = BehaviorGraph.BuildFrom(registry).Services;
        }

        [Test]
        public void registers_default_IAuthenticationSession()
        {
            theDefaultServiceIs<IAuthenticationSession, TicketAuthenticationSession>();
        }

        [Test]
        public void registers_default_IPrincipalContext()
        {
            theDefaultServiceIs<IPrincipalContext, ThreadPrincipalContext>();
        }

        [Test]
        public void registers_default_ITicketSource()
        {
            theDefaultServiceIs<ITicketSource, SimpleCookieTicketSource>();
        }

        [Test]
        public void registers_default_redirects()
        {
            theServiceGraph
                .ServicesFor<IAuthenticationRedirect>()
                .Select(x => x.Type)
                .ShouldHaveTheSameElementsAs(typeof(DefaultAuthenticationRedirect), typeof(AjaxAuthenticationRedirect));
        }

        private void theDefaultServiceIs<TPlugin, TImplementation>()
        {
            theServiceGraph.DefaultServiceFor<TPlugin>()
                .Type.ShouldEqual(typeof(TImplementation));
        }
    }
}