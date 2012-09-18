using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class TwitterBootstrapperTests
    {
        [Test]
        public void uses_the_specified_OAuthSettings()
        {
            var theSettings = new OAuthSettings {ConsumerKey = "blah", ConsumerSecret = "private"};
            var registry = new FubuRegistry();
            registry.Import<ApplyTwitterAuthentication>(x => x.UseOAuthSettings(theSettings));

            var graph = BehaviorGraph.BuildFrom(registry);
            graph.Services.DefaultServiceFor<OAuthSettings>().Value.ShouldEqual(theSettings);
        }
    }
}