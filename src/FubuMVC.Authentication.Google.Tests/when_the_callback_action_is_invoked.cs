using FubuMVC.Authentication.OAuth;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Google.Tests
{
    [TestFixture]
    public class when_the_callback_action_is_invoked : InteractionContext<GoogleController>
    {
        private GoogleSignInSettings _settings;

        protected override void beforeEach()
        {
            _settings = new GoogleSignInSettings {ClientId = "1234", ClientSecret = "5678"};
            Services.Inject(_settings);
        }

        [Test]
        public void it_just_calls_the_google_callback()
        {
            ClassUnderTest.Callback(new GoogleLoginCallback());
            MockFor<IOAuthCallback>().AssertWasCalled(x => x.Execute(_settings));
        }
    }
}