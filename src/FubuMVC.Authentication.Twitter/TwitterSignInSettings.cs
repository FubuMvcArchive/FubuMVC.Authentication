namespace FubuMVC.Authentication.Twitter
{
    public class TwitterSignInSettings
    {
        public TwitterSignInSettings()
        {
            RequestToken = url("oauth/request_token");
            UserAuthorization = url("oauth/authenticate");
            AccessToken = url("oauth/access_token");
        }

        private static string url(string relative)
        {
            return "http://twitter.com/" + relative;
        }

        public string RequestToken { get; set; }
        public string UserAuthorization { get; set; }
        public string AccessToken { get; set; }
    }
}