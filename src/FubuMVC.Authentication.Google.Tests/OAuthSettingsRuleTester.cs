using System.Linq;
using Bottles.Configuration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Google.Tests
{
    [TestFixture]
    public class OAuthSettingsRuleTester : InteractionContext<GoogleOAuthSettingsRule>
    {
        private GoogleSignInSettings theSettings;
        private BottleConfiguration theConfiguration;

        protected override void beforeEach()
        {
            theSettings = new GoogleSignInSettings();
            theConfiguration = new BottleConfiguration("Test");

            Services.Inject(theSettings);
        }

        [Test]
        public void adds_an_error_when_the_settings_are_not_set()
        {
            ClassUnderTest.Evaluate(theConfiguration);

            theConfiguration.Errors.OfType<GoogleOAuthNotConfigured>().ShouldHaveCount(1);
        }

        [Test]
        public void no_error_when_settings_are_configured()
        {
            theSettings.ClientId = "1234";
            theSettings.ClientSecret = "asdfasdf";

            ClassUnderTest.Evaluate(theConfiguration);

            theConfiguration.IsValid().ShouldBeTrue();
        }
    }
}