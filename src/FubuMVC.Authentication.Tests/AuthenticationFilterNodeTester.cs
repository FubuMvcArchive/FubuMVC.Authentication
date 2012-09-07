using System.Linq;
using FubuCore;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class AuthenticationFilterNodeTester
    {
        private AuthenticationFilterNode theNode;

        [SetUp]
        public void SetUp()
        {
            theNode = new AuthenticationFilterNode();
        }

        [Test]
        public void alters_the_node_during_the_build_up()
        {
            var dependency = new SomeDependency();
            theNode.Modify(def => def.DependencyByValue(dependency));

            var theObjectDef = theNode.As<IContainerModel>().ToObjectDef();
            theObjectDef
                .Dependencies
                .OfType<ValueDependency>()
                .Any(x => x.Value == dependency)
                .ShouldBeTrue();
        }

        [Test]
        public void replaces_a_service_that_is_not_explicitly_registered()
        {
            theNode.ReplaceService<IAuthenticationService, ReplacementAuthenticationService>();
            var theObjectDef = theNode.As<IContainerModel>().ToObjectDef();
            var target = theObjectDef
                .Dependencies
                .OfType<ConfiguredDependency>()
                .First();

            target.DependencyType.ShouldEqual(typeof (IAuthenticationService));
            target.Definition.Type.ShouldEqual(typeof (ReplacementAuthenticationService));
        }

        [Test]
        public void replaces_a_service_that_is_already_registered()
        {
            theNode.ReplaceService<IAuthenticationFilter, AuthenticationFilter>();
            theNode.ReplaceService<IAuthenticationFilter, ReplacementAuthenticationFilter>();

            var theObjectDef = theNode.As<IContainerModel>().ToObjectDef();
            var target = theObjectDef
                .Dependencies
                .OfType<ConfiguredDependency>()
                .First();

            target.DependencyType.ShouldEqual(typeof(IAuthenticationFilter));
            target.Definition.Type.ShouldEqual(typeof(ReplacementAuthenticationFilter));
        }

        public class ReplacementAuthenticationFilter : IAuthenticationFilter
        {
            public AuthenticationFilterResult Authenticate()
            {
                throw new System.NotImplementedException();
            }
        }

        public class ReplacementAuthenticationService : IAuthenticationService
        {
            public bool Authenticate(LoginRequest request)
            {
                throw new System.NotImplementedException();
            }
        }

        public class SomeDependency { }
    }
}