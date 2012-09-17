using FubuCore;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class out_of_the_box_authentication_setup
    {
        private BehaviorGraph theGraphWithBasicAuthentication;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Import<ApplyAuthentication>();

            theGraphWithBasicAuthentication = BehaviorGraph.BuildFrom(registry);
        }

        [Test]
        public void the_flat_file_auth_service_is_registered()
        {
            theGraphWithBasicAuthentication.Services.DefaultServiceFor<IAuthenticationService>()
                .Type.ShouldEqual(typeof (FlatFileAuthenticationService));
        }

        [Test]
        public void the_basic_fubu_principal_builder_is_registered()
        {
            theGraphWithBasicAuthentication.Services.DefaultServiceFor<IPrincipalBuilder>()
                .Type.ShouldEqual(typeof (BasicFubuPrincipalBuilder));
        }

        [Test]
        public void login_endpoint_is_added()
        {
            theGraphWithBasicAuthentication.BehaviorFor(typeof (LoginRequest)).ShouldNotBeNull();
        }

        [Test]
        public void logout_endpoint_is_added()
        {
            theGraphWithBasicAuthentication.BehaviorFor(typeof(LogoutRequest)).ShouldNotBeNull();
        }

        // everything else is covered by the service registry tester
    }

    [TestFixture]
    public class opting_out_of_the_basic_endpoints
    {
        private BehaviorGraph theGraphWithoutEndpoints;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Import<ApplyAuthentication>(x => x.DoNotIncludeEndpoints());

            theGraphWithoutEndpoints = BehaviorGraph.BuildFrom(registry);
        }

        [Test]
        public void login_endpoint_is_not_added()
        {
            Exception<FubuException>.ShouldBeThrownBy(() => theGraphWithoutEndpoints.BehaviorFor(typeof(LoginRequest)))
                .ErrorCode.ShouldEqual(2150);
        }

        [Test]
        public void logout_endpoint_is_not_added()
        {
            Exception<FubuException>.ShouldBeThrownBy(() => theGraphWithoutEndpoints.BehaviorFor(typeof(LogoutRequest)))
                .ErrorCode.ShouldEqual(2150);
        }
    }

    [TestFixture]
    public class authentication_with_custom_services
    {
        private BehaviorGraph theGraphWithCustomServices;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Import<ApplyAuthentication>(x =>
                                                     {
                                                         x.AuthenticateWith<StubAuthenticationService>();
                                                         x.BuildPrincipalWith<StubPrincipalBuilder>();
                                                     });

            theGraphWithCustomServices = BehaviorGraph.BuildFrom(registry);
        }

        [Test]
        public void the_auth_service_is_registered()
        {
            theGraphWithCustomServices.Services.DefaultServiceFor<IAuthenticationService>()
                .Type.ShouldEqual(typeof(StubAuthenticationService));
        }

        [Test]
        public void the_principal_builder_is_registered()
        {
            theGraphWithCustomServices.Services.DefaultServiceFor<IPrincipalBuilder>()
                .Type.ShouldEqual(typeof(StubPrincipalBuilder));
        }
    }
}