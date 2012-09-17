using System.Linq;
using Bottles.Configuration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class OAuthSettingsRuleTester : InteractionContext<OAuthSettingsRule>
    {
        private OAuthSettings theSettings;
        private BottleConfiguration theConfiguration;

        protected override void beforeEach()
        {
            theSettings = new OAuthSettings();
            theConfiguration = new BottleConfiguration("Test");

            Services.Inject(theSettings);
        }

        [Test]
        public void adds_an_error_when_the_settings_are_not_set()
        {
            ClassUnderTest.Evaluate(theConfiguration);

            theConfiguration.Errors.OfType<OAuthNotConfigured>().ShouldHaveCount(1);
        }

        [Test]
        public void no_error_when_settings_are_configured()
        {
            theSettings.ConsumerKey = "1234";
            theSettings.ConsumerSecret = "asdfasdf";

            ClassUnderTest.Evaluate(theConfiguration);

            theConfiguration.IsValid().ShouldBeTrue();
        }
    }
}