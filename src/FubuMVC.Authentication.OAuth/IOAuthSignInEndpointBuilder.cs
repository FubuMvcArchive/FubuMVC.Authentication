using DotNetOpenAuth.OAuth2;

namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthSignInEndpointBuilder
    {
        AuthorizationServerDescription Build(IOAuthSignInSettings settings);
    }

    public class OAuthSignInEndpointBuilder : IOAuthSignInEndpointBuilder
    {
        public AuthorizationServerDescription Build(IOAuthSignInSettings settings)
        {
            return new AuthorizationServerDescription
                {
                    AuthorizationEndpoint = settings.AuthEndpoint,
                    TokenEndpoint = settings.TokenEndpoint
                };
        }
    }
}