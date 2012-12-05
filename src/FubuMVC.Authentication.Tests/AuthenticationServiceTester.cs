using System;
using FubuTestingSupport;
using NUnit.Framework;
using System.Collections.Generic;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class AuthenticationServiceTester : InteractionContext<AuthenticationService>
    {
        private IAuthenticationStrategy[] theStrategies;

        protected override void beforeEach()
        {
            theStrategies = Services.CreateMockArrayFor<IAuthenticationStrategy>(4);
        }

        [Test]
        public void constructor_function_throws_exception_if_there_are_no_strategies()
        {
            Exception<ArgumentOutOfRangeException>.ShouldBeThrownBy(() => {
                new AuthenticationService(new IAuthenticationStrategy[0]);
            });
        }

        [Test]
        public void try_to_apply_fails_if_all_fail()
        {
            theStrategies.Each(x => x.Stub(o => o.TryToApply()).Return(false));

            ClassUnderTest.TryToApply().ShouldBeFalse();
        }


        [Test]
        public void try_to_apply_succeeds_if_any_succeeds()
        {
            theStrategies[3].Stub(x => x.TryToApply()).Return(true);

            ClassUnderTest.TryToApply().ShouldBeTrue();
        }

        [Test]
        public void authenticate_fails_if_all_fail()
        {
            var request = new LoginRequest();

            theStrategies.Each(x => x.Stub(o => o.Authenticate(request)).Return(false));

            ClassUnderTest.Authenticate(request).ShouldBeFalse();
        }


        [Test]
        public void authenticate_succeeds_if_any_succeeds()
        {
            var request = new LoginRequest();

            theStrategies[3].Stub(x => x.Authenticate(request)).Return(true);

            ClassUnderTest.Authenticate(request).ShouldBeTrue();
        }
    }
}