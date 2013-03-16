using FubuMVC.Authentication.OAuth2;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Facebook.Tests
{
    [TestFixture]
    public class when_the_callback_action_is_invoked : InteractionContext<FacebookController>
    {
        private FacebookSignInSettings _settings;

        protected override void beforeEach()
        {
            _settings = new FacebookSignInSettings { ClientId = "1234", ClientSecret = "5678" };
            Services.Inject(_settings);
        }

        [Test]
        public void it_just_calls_the_Facebook_callback()
        {
            ClassUnderTest.Callback(new FacebookLoginCallback());
            MockFor<IOAuth2Callback>().AssertWasCalled(x => x.Execute(_settings));
        }
    }
}