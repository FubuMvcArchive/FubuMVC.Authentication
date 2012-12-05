using System.Security.Principal;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class when_successfully_trying_to_apply_authentication : InteractionContext<BasicAuthentication>
    {
        private string theUserName;
        private IPrincipal thePrincipal;
        private bool theResult;

        protected override void beforeEach()
        {
            theUserName = "a user";

            MockFor<IAuthenticationSession>().Stub(x => x.PreviouslyAuthenticatedUser())
                .Return(theUserName);

            thePrincipal = MockFor<IPrincipal>();


            MockFor<IPrincipalBuilder>().Stub(x => x.Build(theUserName))
                .Return(thePrincipal);

            theResult = ClassUnderTest.TryToApply();
        }


        [Test]
        public void should_mark_the_session_as_accessed_for_sliding_expirations()
        {
            MockFor<IAuthenticationSession>().AssertWasCalled(x => x.MarkAccessed());
        }

        [Test]
        public void was_successful()
        {
            theResult.ShouldBeTrue();
        }

        [Test]
        public void should_set_the_principal_for_the_authenticated_user()
        {
            MockFor<IPrincipalContext>().AssertWasCalled(x => x.Current = thePrincipal);
        }
    }

    [TestFixture]
    public class when_unsuccessfully_trying_to_apply_authentication : InteractionContext<BasicAuthentication>
    {
        private bool theResult;

        protected override void beforeEach()
        {
            MockFor<IAuthenticationSession>().Stub(x => x.PreviouslyAuthenticatedUser())
                .Return(null);

            theResult = ClassUnderTest.TryToApply();
        }

        [Test]
        public void should_return_false_because_no_user_is_previously_authenticated()
        {
            theResult.ShouldBeFalse();
        }
    }

    [TestFixture]
    public class when_the_authentication_fails : InteractionContext<BasicAuthentication>
    {
        private LoginRequest theLoginRequest;
        private bool theResult;

        protected override void beforeEach()
        {
            theLoginRequest = new LoginRequest()
            {
                UserName = "frank",
                Url = "/where/i/wanted/to/go",
                NumberOfTries = 2
            };

            MockFor<ICredentialsAuthenticator>().Stub(x => x.AuthenticateCredentials(theLoginRequest))
                                                .Return(false);

            theResult = ClassUnderTest.Authenticate(theLoginRequest);
        }

        [Test]
        public void should_mark_the_login_request_as_failed()
        {
            theLoginRequest.Status.ShouldEqual(LoginStatus.Failed);
        }

        [Test]
        public void should_NOT_mark_the_session_as_authenticated()
        {
            MockFor<IAuthenticationSession>().AssertWasNotCalled(x => x.MarkAuthenticated(theLoginRequest.UserName));
        }

        [Test]
        public void should_increment_the_number_of_retries()
        {
            theLoginRequest.NumberOfTries.ShouldEqual(3);
        }

        [Test]
        public void the_result_was_not_successful()
        {
            theResult.ShouldBeFalse();
        }
    }

    [TestFixture]
    public class when_authentication_succeeds : InteractionContext<BasicAuthentication>
    {
        private LoginRequest theLoginRequest;
        private bool theResult;

        protected override void beforeEach()
        {
            theLoginRequest = new LoginRequest()
            {
                UserName = "frank",
                Url = "/where/i/wanted/to/go"
            };

            MockFor<ICredentialsAuthenticator>().Stub(x => x.AuthenticateCredentials(theLoginRequest))
                                                .Return(true);

            theResult = ClassUnderTest.Authenticate(theLoginRequest);
        }

        [Test]
        public void should_mark_the_login_request_as_successful()
        {
            theLoginRequest.Status.ShouldEqual(LoginStatus.Succeeded);
        }

        [Test]
        public void should_mark_the_session_as_authenticated()
        {
            MockFor<IAuthenticationSession>().AssertWasCalled(x => x.MarkAuthenticated(theLoginRequest.UserName));
        }

        [Test]
        public void the_return_value_should_be_true()
        {
            theResult.ShouldBeTrue();
        }
    }
}