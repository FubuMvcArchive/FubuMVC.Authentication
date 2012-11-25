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
        public void has_to_be_authentication()
        {
            theNode.Category.ShouldEqual(BehaviorCategory.Authentication);
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