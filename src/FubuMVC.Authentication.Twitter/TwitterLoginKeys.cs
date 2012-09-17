using FubuLocalization;

namespace FubuMVC.Authentication.Twitter
{
    public class TwitterLoginKeys : StringToken
    {
        public static readonly TwitterLoginKeys LoginWithTwitter = new TwitterLoginKeys("Login with Twitter");

        protected TwitterLoginKeys(string defaultValue)
            : base(null, defaultValue, namespaceByType: true)
        {
        }
    }
}