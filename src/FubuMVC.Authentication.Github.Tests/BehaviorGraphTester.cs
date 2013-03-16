using FubuCore;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Github.Tests
{
    [TestFixture]
    public class BehaviorGraphTester
    {
        [Test]
        public void chain_contains_an_object_def()
        {
            var graph = BehaviorGraph.BuildFrom(registry =>
                {
                    registry.Import<ApplyAuthentication>();
                    registry.Import<ApplyGithubAuthentication>();
                });

            var chain = graph.BehaviorFor<GithubController>(x => x.Button(null));
            var def = chain.As<IContainerModel>().ToObjectDef();

            def.ShouldNotBeNull();
        }
    }
}