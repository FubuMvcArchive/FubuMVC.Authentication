using FubuMVC.Authentication.Membership;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests.Membership
{
    [TestFixture]
    public class MembershipAuthenticationServiceTester : InteractionContext<MembershipAuthenticationService>
    {
        [Test]
        public void authentication_is_a_straight_up_delegation_positive()
        {
            var request = new LoginRequest();
            MockFor<IMembershipRepository>().Stub(x => x.MatchesCredentials(request))
                                            .Return(true);


            ClassUnderTest.Authenticate(request).ShouldBeTrue();
        }

        [Test]
        public void authentication_is_a_straight_up_delegation_negative()
        {
            var request = new LoginRequest();
            MockFor<IMembershipRepository>().Stub(x => x.MatchesCredentials(request))
                                            .Return(false);


            ClassUnderTest.Authenticate(request).ShouldBeFalse();
        }

        [Test]
        public void build_principal()
        {
            var user = new UserInfo
            {
                UserName = "ralph"

            };

            var model = new AuthenticatedModel();
            user.Set(model);

            MockFor<IMembershipRepository>().Stub(x => x.FindByName(user.UserName))
                                            .Return(user);

            var principal = ClassUnderTest.Build(user.UserName);

            var fubuPrincipal = principal.ShouldBeOfType<FubuPrincipal>();
            fubuPrincipal.Identity.Name.ShouldEqual(user.UserName);

            fubuPrincipal.Get<AuthenticatedModel>().ShouldBeTheSameAs(model);
        }
    }
}