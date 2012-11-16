using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.OAuth.Tests
{
    [TestFixture]
    public class AssemblyNeedsTheFubuModuleAttribute
    {
        [Test]
        public void the_attribute_exists()
        {
            var assembly = typeof(IOAuthProxy).Assembly;

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
            registry.Services<OAuthServiceRegistry>();

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