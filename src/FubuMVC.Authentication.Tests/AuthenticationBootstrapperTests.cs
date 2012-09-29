using FubuCore;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Authentication.Windows;
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
        public void the_fubu_principal_builder_is_registered()
        {
            theGraphWithBasicAuthentication.Services.DefaultServiceFor<IPrincipalBuilder>()
                .Type.ShouldEqual(typeof (FubuPrincipalBuilder));
        }

        [Test]
        public void the_nullo_principal_roles_is_registered()
        {
            theGraphWithBasicAuthentication.Services.DefaultServiceFor<IPrincipalRoles>()
                .Type.ShouldEqual(typeof(NulloPrincipalRoles));
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

        [Test]
        public void basic_login_redirect_is_registered()
        {
            theGraphWithBasicAuthentication.Services.DefaultServiceFor<IBasicLoginRedirect>()
                .Type.ShouldEqual(typeof(BasicLoginRedirect));
        }

        [Test]
        public void basic_login_success_handler_is_registered()
        {
            theGraphWithBasicAuthentication.Services.DefaultServiceFor<ILoginSuccessHandler>()
                .Type.ShouldEqual(typeof(BasicLoginSuccessHandler));
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

    [TestFixture]
    public class setup_with_windows_authentication
    {
        private BehaviorGraph theGraphWithWindowsAuthentication;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Import<ApplyAuthentication>(x => x.IncludeWindowsAuthentication());

            theGraphWithWindowsAuthentication = BehaviorGraph.BuildFrom(registry);
        }

        [Test]
        public void the_windows_action_call_is_registered()
        {
            theGraphWithWindowsAuthentication.BehaviorFor<WindowsController>(x => x.Login(null)).ShouldNotBeNull();
        }

        [Test]
        public void registers_the_windows_authentication_context()
        {
            theGraphWithWindowsAuthentication.Services.DefaultServiceFor<IWindowsAuthenticationContext>()
                .Type.ShouldEqual(typeof (AspNetWindowsAuthenticationContext));
        }
    }
}