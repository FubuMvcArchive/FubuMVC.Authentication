using Bottles.Configuration;
using FubuMVC.Authentication.OAuth;
using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.Google
{
    public class GoogleServiceRegistry : ServiceRegistry
    {
        public GoogleServiceRegistry()
        {
            SetServiceIfNone<IOAuthProxy, OAuthProxy>();

            SetServiceIfNone<IOAuthCallback, OAuthCallback>();
            SetServiceIfNone<IOAuthSignInEndpointBuilder, OAuthSignInEndpointBuilder>();
            SetServiceIfNone<IOAuthResponseHandler, OAuthResponseHandler<GoogleLoginRequest>>();

            ConfigureRequirements(x => x.AddRule<GoogleOAuthSettingsRule>());
        }
    }


    public class GoogleOAuthSettingsRule : IBottleConfigurationRule
    {
        private readonly GoogleSignInSettings _settings;

        public GoogleOAuthSettingsRule(GoogleSignInSettings settings)
        {
            _settings = settings;
        }

        public void Evaluate(BottleConfiguration configuration)
        {
            if (!_settings.IsConfigured())
            {
                configuration.RegisterError(new GoogleOAuthNotConfigured());
            }
        }

    }

    public class GoogleOAuthNotConfigured : BottleConfigurationError
    {
        public override string ToString()
        {
            return "OAuth2 is not configured for Google usage. You can create a GoogleSignInSettings in your FubuRegistry class constructor and add it to your container Services.";
        }
    }
}