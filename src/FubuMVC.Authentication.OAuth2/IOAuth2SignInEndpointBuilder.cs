using DotNetOpenAuth.OAuth2;

namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2SignInEndpointBuilder
    {
        AuthorizationServerDescription Build(IOAuth2SignInSettings settings);
    }

    public class OAuth2SignInEndpointBuilder : IOAuth2SignInEndpointBuilder
    {
        public AuthorizationServerDescription Build(IOAuth2SignInSettings settings)
        {
            return new AuthorizationServerDescription
            {
                AuthorizationEndpoint = settings.AuthEndpoint,
                TokenEndpoint = settings.TokenEndpoint
            };
        }
    }
}