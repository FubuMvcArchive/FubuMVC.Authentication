using System.Net;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using NUnit.Framework;
using FubuTestingSupport;

namespace FubuMVC.Authentication.Tests.Endpoints
{
    [TestFixture]
    public class FormsAuthenticationEndpointsRegistrationTester
    {
        [Test]
        public void apply_will_not_add_the_endpoints_if_the_graph_already_has_login_and_logout()
        {
            var graph = BehaviorGraph.BuildFrom(x => {
                x.Actions.IncludeType<LoginEndpoint>();
                x.Import<ApplyAuthentication>();
            });

            graph.BehaviorFor<LoginController>(x => x.get_login(null)).ShouldBeNull();
            graph.BehaviorFor<LogoutController>(x => x.Logout(null)).ShouldBeNull();
        }

        [Test]
        public void will_add_the_endpoints_if_the_graph_does_not_already_have_login_and_logout()
        {
            var graph = BehaviorGraph.BuildFrom(x =>
            {
                x.Actions.IncludeType<NothingEndpoint>();
                x.Import<ApplyAuthentication>();
            });

            graph.BehaviorFor<LoginController>(x => x.get_login(null)).ShouldNotBeNull();
            graph.BehaviorFor<LogoutController>(x => x.Logout(null)).ShouldNotBeNull();
        }

        [Test]
        public void has_login_negative()
        {
            FormsAuthenticationEndpointsRegistration.HasLogin(new BehaviorGraph())
                .ShouldBeFalse();
        }

        [Test]
        public void has_login_positive()
        {
            var graph = new BehaviorGraph();
            graph.AddChain().AddToEnd(ActionCall.For<LoginEndpoint>(x => x.get_login(null)));

            FormsAuthenticationEndpointsRegistration.HasLogin(graph)
                .ShouldBeTrue();
        }

        [Test]
        public void has_logout_negative()
        {
            FormsAuthenticationEndpointsRegistration.HasLogout(new BehaviorGraph())
                .ShouldBeFalse();
        }

        [Test]
        public void has_logout_positive()
        {
            var graph = new BehaviorGraph();
            graph.AddChain().AddToEnd(ActionCall.For<LoginEndpoint>(x => x.get_logout(null)));

            FormsAuthenticationEndpointsRegistration.HasLogout(graph)
                .ShouldBeTrue();
        }
    }

    public class LoginEndpoint
    {
        public LoginRequest get_login(LoginRequest request)
        {
            return request;
        }

        public FubuContinuation get_logout(LogoutRequest request)
        {
            return FubuContinuation.EndWithStatusCode(HttpStatusCode.Accepted);
        }
    }

    public class NothingEndpoint
    {
        public string get_nothing()
        {
            return "nothing";
        }
    }
}