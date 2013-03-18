using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.OAuth2.Tests
{
    [TestFixture]
    public class AssemblyNeedsTheFubuModuleAttribute
    {
        [Test]
        public void the_attribute_exists()
        {
            var assembly = typeof(IOAuth2Proxy).Assembly;

            assembly.GetCustomAttributes(typeof(FubuModuleAttribute), true)
                .Any().ShouldBeTrue();
        }
    }

    [TestFixture]
    public class TwitterServiceRegistryTester
    {
        private ServiceGraph theServiceGraph;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Services<OAuth2ServiceRegistry>();

            theServiceGraph = BehaviorGraph.BuildFrom(registry).Services;
        }

        [Test]
        public void registers_default_ISystemUrls()
        {
            theDefaultServiceIs<ISystemUrls, SystemUrls>();
        }

        private void theDefaultServiceIs<TPlugin, TImplementation>()
        {
            theServiceGraph.DefaultServiceFor<TPlugin>()
                .Type.ShouldEqual(typeof(TImplementation));
        }
    }
}