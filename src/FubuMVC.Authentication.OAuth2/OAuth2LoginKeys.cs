using FubuLocalization;

namespace FubuMVC.Authentication.OAuth2
{
    public class OAuth2LoginKeys : StringToken
    {
        public static readonly OAuth2LoginKeys Failed = new OAuth2LoginKeys("Login with the provider failed to authenticate. Please try again.");

        protected OAuth2LoginKeys(string defaultValue)
            : base(null, defaultValue, namespaceByType: true)
        {
        }
    }
}