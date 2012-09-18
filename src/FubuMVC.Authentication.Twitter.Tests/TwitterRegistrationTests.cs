using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class TwitterRegistrationTests
    {
        [Test]
        public void twitter_has_a_partial_output_by_default()
        {
            var registry = new FubuRegistry();
            registry.Import<ApplyAuthentication>();
            registry.Import<ApplyTwitterAuthentication>();

            var graph = BehaviorGraph.BuildFrom(registry);
            var chain = graph.BehaviorFor<TwitterController>(x => x.Button(null));

            chain.HasOutput().ShouldBeTrue();
        }
    }
}