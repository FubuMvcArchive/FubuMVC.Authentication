using Bottles.Configuration;

namespace FubuMVC.Authentication.Twitter
{
    public class OAuthSettingsRule : IBottleConfigurationRule
    {
        private readonly OAuthSettings _settings;

        public OAuthSettingsRule(OAuthSettings settings)
        {
            _settings = settings;
        }

        public void Evaluate(BottleConfiguration configuration)
        {
            if(!_settings.IsConfigured())
            {
                configuration.RegisterError(new OAuthNotConfigured());
            }
        }
    }

    public class OAuthNotConfigured : BottleConfigurationError
    {
        public override string ToString()
        {
            return "OAuth is not configured for Twitter usage. You can configure the OAuthSettings class and add it to your container or use the UseOAuthSettings method on the ApplyTwitterAuthentication class.";
        }
    }
}