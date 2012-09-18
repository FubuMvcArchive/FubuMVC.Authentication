using FubuCore;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class debugging
    {
        [Test]
        public void check_the_chain()
        {
            var graph = BehaviorGraph.BuildFrom(registry =>
                                                    {
                                                        registry.Import<ApplyAuthentication>();
                                                        registry.Import<ApplyTwitterAuthentication>();
                                                    });

            var chain = graph.BehaviorFor<TwitterController>(x => x.Button(null));
            var def = chain.As<IContainerModel>().ToObjectDef();

            def.ShouldNotBeNull();
        }
    }
}