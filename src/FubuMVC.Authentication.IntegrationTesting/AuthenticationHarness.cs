using FubuMVC.Core.Urls;
using FubuMVC.TestingHarness;
using NUnit.Framework;
using StructureMap;

namespace FubuMVC.Authentication.IntegrationTesting
{
    public class AuthenticationHarness : FubuRegistryHarness
    {
        private IContainer theContainer;

        protected override void configure(Core.FubuRegistry registry)
        {
            registry.Actions.IncludeType<SampleController>();
            registry.Import<ApplyAuthentication>();
        }

        [SetUp]
        public void AuthenticationSetup()
        {
            beforeEach();
        }

        protected virtual void beforeEach()
        {
        }

        protected override void configureContainer(IContainer container)
        {
            theContainer = container;
        }

        public IContainer Container { get { return theContainer; } }

        public IUrlRegistry Urls { get { return theContainer.GetInstance<IUrlRegistry>(); } }
    }
}