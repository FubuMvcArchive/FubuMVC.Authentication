using System.Security.Principal;
using Bottles.Configuration;
using FubuCore;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class RequiredServicesTester
    {
        private InMemoryServiceLocator theServices;
        private RequiredServices theRule;
        private BottleConfiguration theConfiguration;

        [SetUp]
        public void SetUp()
        {
            theServices = new InMemoryServiceLocator();
            theRule = new RequiredServices(theServices);

            theConfiguration = new BottleConfiguration(GetType().Namespace);
        }

        [Test]
        public void valid_configuration_when_types_are_registered()
        {
            theServices.Add<IAuthenticationService>(new StubAuthenticationService());
            theServices.Add<IPrincipalBuilder>(new StubPrincipalBuilder());
            theRule.Evaluate(theConfiguration);

            theConfiguration.IsValid().ShouldBeTrue();
        }

        [Test]
        public void invalid_when_missing_the_authentication_service()
        {
            theServices.Add<IPrincipalBuilder>(new StubPrincipalBuilder());
            theRule.Evaluate(theConfiguration);

            theConfiguration.IsValid().ShouldBeFalse();
            theConfiguration.MissingServices.ShouldContain(x => x.ServiceType == typeof(IAuthenticationService));
        }

        [Test]
        public void invalid_when_missing_the_principal_builder()
        {
            theServices.Add<IAuthenticationService>(new StubAuthenticationService());
            theRule.Evaluate(theConfiguration);

            theConfiguration.IsValid().ShouldBeFalse();
            theConfiguration.MissingServices.ShouldContain(x => x.ServiceType == typeof(IPrincipalBuilder));
        }
    }

    public class StubAuthenticationService : IAuthenticationService
    {
        public bool Authenticate(LoginRequest request)
        {
            throw new System.NotImplementedException();
        }
    }

    public class StubPrincipalBuilder : IPrincipalBuilder
    {
        public IPrincipal Build(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}