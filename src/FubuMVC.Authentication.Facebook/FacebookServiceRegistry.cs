using Bottles.Configuration;
using FubuMVC.Authentication.OAuth;
using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.Facebook
{
    public class FacebookServiceRegistry : ServiceRegistry
    {
        public FacebookServiceRegistry()
        {
            SetServiceIfNone<IOAuthProxy, OAuthProxy>();
            SetServiceIfNone<IOAuthCallback, OAuthCallback>();
            SetServiceIfNone<IOAuthSignInEndpointBuilder, OAuthSignInEndpointBuilder>();
            SetServiceIfNone<IOAuthResponseHandler, OAuthResponseHandler<FacebookLoginRequest>>();

            ConfigureRequirements(x => x.AddRule<FacebookOAuthSettingsRule>());
        }
    }


    public class FacebookOAuthSettingsRule : IBottleConfigurationRule
    {
        private readonly FacebookSignInSettings _settings;

        public FacebookOAuthSettingsRule(FacebookSignInSettings settings)
        {
            _settings = settings;
        }

        #region IBottleConfigurationRule Members

        public void Evaluate(BottleConfiguration configuration)
        {
            if (!_settings.IsConfigured())
            {
                configuration.RegisterError(new FacebookOAuthNotConfigured());
            }
        }

        #endregion
    }

    public class FacebookOAuthNotConfigured : BottleConfigurationError
    {
        public override string ToString()
        {
            return "OAuth2 is not configured for Facebook usage. You can create a FacebookSignInSettings in your FubuRegistry class constructor and add it to your container Services.";
        }
    }
}