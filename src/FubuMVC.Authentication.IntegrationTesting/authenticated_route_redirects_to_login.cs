using System.Net;
using FubuMVC.Core.Urls;
using FubuMVC.TestingHarness;
using FubuTestingSupport;
using NUnit.Framework;
using StructureMap;

namespace FubuMVC.Authentication.IntegrationTesting
{
    [TestFixture]
    public class unauthenticated_request_against_an_authenticated_route : FubuRegistryHarness
    {
        private IContainer theContainer;

        protected override void configure(Core.FubuRegistry registry)
        {
            registry.Actions.IncludeType<AuthenticatedController>();
            registry.Import<ApplyAuthentication>();
        }

        protected override void configureContainer(IContainer container)
        {
            theContainer = container;
        }

        [Test]
        public void redirects_to_login()
        {
            var response = endpoints.Get<AuthenticatedController>(x => x.get_some_route(null));
            var text = response.ReadAsText();
            response.StatusCode.ShouldEqual(HttpStatusCode.Redirect);

            var loginUrl = theContainer.GetInstance<IUrlRegistry>().UrlFor(new LoginRequest());
            response.ResponseHeaderFor(HttpResponseHeader.Location).ShouldEqual(loginUrl);
        }
    }

    public class TargetModel { }

    public class AuthenticatedController
    {
        public TargetModel get_some_route(TargetModel target)
        {
            return target;
        }
    }
}