using DotNetOpenAuth.OAuth.ChannelElements;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class TwitterServiceRegistryTester
    {
        private ServiceGraph theServiceGraph;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Services<TwitterServiceRegistry>();

            theServiceGraph = BehaviorGraph.BuildFrom(registry).Services;
        }

        [Test]
        public void registers_default_ITwitterProxy()
        {
            theDefaultServiceIs<ITwitterProxy, TwitterProxy>();
        }

        [Test]
        public void registers_default_ITwitterCallback()
        {
            theDefaultServiceIs<ITwitterCallback, TwitterCallback>();
        }

        [Test]
        public void registers_default_ITwitterResponseHandler()
        {
            theDefaultServiceIs<ITwitterResponseHandler, TwitterResponseHandler>();
        }

        [Test]
        public void registers_default_ISignInEndpointBuilder()
        {
            theDefaultServiceIs<ISignInEndpointBuilder, SignInEndpointBuilder>();
        }

        [Test]
        public void registers_default_IConsumerTokenManager()
        {
            theDefaultServiceIs<IConsumerTokenManager, InMemoryTokenManager>();
        }

        private void theDefaultServiceIs<TPlugin, TImplementation>()
        {
            theServiceGraph.DefaultServiceFor<TPlugin>()
                .Type.ShouldEqual(typeof(TImplementation));
        }
    }
}