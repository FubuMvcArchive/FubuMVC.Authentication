using FubuLocalization;

namespace FubuMVC.Authentication.Google
{
    public class GoogleLoginKeys : StringToken
    {
        public static readonly GoogleLoginKeys LoginWithGoogle = new GoogleLoginKeys("Login with Google");

        protected GoogleLoginKeys(string defaultValue)
            : base(null, defaultValue, namespaceByType: true)
        {
        }
    }
}