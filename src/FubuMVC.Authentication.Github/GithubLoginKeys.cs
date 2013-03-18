using FubuLocalization;

namespace FubuMVC.Authentication.Github
{
    public class GithubLoginKeys : StringToken
    {
        public static readonly GithubLoginKeys LoginWithGithub = new GithubLoginKeys("Login with Github");

        protected GithubLoginKeys(string defaultValue)
            : base(null, defaultValue, namespaceByType: true)
        {
        }
    }
}