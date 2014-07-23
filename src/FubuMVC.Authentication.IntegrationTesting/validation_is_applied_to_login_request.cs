using System.Linq;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.Validation;
using FubuTestingSupport;
using FubuValidation;
using NUnit.Framework;

namespace FubuMVC.Authentication.IntegrationTesting
{
    public class FakeValidationRules : ClassValidationRules<LoginRequest>
    {
        public FakeValidationRules()
        {
            Property(x => x.UserName).Required();
        }
    }

    [TestFixture]
    public class validation_is_applied_to_login_request : AuthenticationHarness
    {
        [Test]
        public void test()
        {
            var chain = BehaviorGraph.BehaviorFor<LoginController>(x => x.post_login(null));
            chain.OfType<ValidationActionFilter>().Any().ShouldBeTrue();
        }
    }
}