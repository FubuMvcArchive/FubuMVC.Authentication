using FubuCore;

namespace FubuMVC.Authentication.Twitter
{
    public class OAuthSettings
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public bool IsConfigured()
        {
            return !ConsumerKey.IsEmpty() && !ConsumerSecret.IsEmpty();
        }
    }
}