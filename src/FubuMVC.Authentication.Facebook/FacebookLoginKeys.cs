using FubuLocalization;

namespace FubuMVC.Authentication.Facebook
{
    public class FacebookLoginKeys : StringToken
    {
        public static readonly FacebookLoginKeys LoginWithFacebook = new FacebookLoginKeys("Login with Facebook");

        protected FacebookLoginKeys(string defaultValue)
            : base(null, defaultValue, namespaceByType: true)
        {
        }
    }
}