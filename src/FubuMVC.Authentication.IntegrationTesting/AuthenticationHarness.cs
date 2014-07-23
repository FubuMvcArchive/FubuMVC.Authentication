using FubuMVC.Authentication.Membership;
using FubuMVC.Core;
using FubuMVC.Core.Endpoints;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Urls;
using FubuMVC.Katana;
using FubuMVC.StructureMap;
using NUnit.Framework;
using StructureMap;

namespace FubuMVC.Authentication.IntegrationTesting
{

    public class AuthenticationHarness
    {
        private IContainer theContainer;
        private EmbeddedFubuMvcServer server;

        protected virtual void configure(Core.FubuRegistry registry)
        {
            registry.Actions.IncludeType<SampleController>();
            registry.Import<ApplyAuthentication>();


            registry.AlterSettings<AuthenticationSettings>(_ => {
                _.Strategies.AddToEnd(MembershipNode.For<InMemoryMembershipRepository>());
            });
        }

        public BehaviorGraph BehaviorGraph
        {
            get
            {
                return Container.GetInstance<BehaviorGraph>();
            }
        }

        [SetUp]
        public void AuthenticationSetup()
        {
            var registry = new FubuRegistry();
            configure(registry);

            theContainer = new Container();

            server = FubuApplication.For(registry).StructureMap(theContainer).RunEmbeddedWithAutoPort();

            beforeEach();
        }

        protected EndpointDriver endpoints
        {
            get
            {
                return server.Endpoints;
            }
        }

        [TearDown]
        public void TearDown()
        {
            server.Dispose();
        }

        protected virtual void beforeEach()
        {
        }


        public IContainer Container { get { return theContainer; } }

        public IUrlRegistry Urls { get { return theContainer.GetInstance<IUrlRegistry>(); } }
    }
}