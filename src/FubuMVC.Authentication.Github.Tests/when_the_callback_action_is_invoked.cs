using FubuMVC.Authentication.OAuth2;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Github.Tests
{
    [TestFixture]
    public class when_the_callback_action_is_invoked : InteractionContext<GithubController>
    {
        private GithubSignInSettings _settings;

        protected override void beforeEach()
        {
            _settings = new GithubSignInSettings { ClientId = "1234", ClientSecret = "5678" };
            Services.Inject(_settings);
        }

        [Test]
        public void it_just_calls_the_Github_callback()
        {
            ClassUnderTest.Callback(new GithubLoginCallback());
            MockFor<IOAuth2Callback>().AssertWasCalled(x => x.Execute(_settings));
        }
    }
}