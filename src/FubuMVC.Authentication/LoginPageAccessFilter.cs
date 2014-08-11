using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication
{
    public class LoginPageAccessFilter
    {
        private readonly AuthenticationSettings _settings;

        public LoginPageAccessFilter(AuthenticationSettings settings)
        {
            _settings = settings;
        }

        public FubuContinuation LoginAccessFilter()
        {
            return _settings.LoginAccessMode
                ?FubuContinuation.NextBehavior()
                :FubuContinuation.RedirectTo("~/");

        }
    }
}
