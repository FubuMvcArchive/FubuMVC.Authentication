using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication
{
    public class LoginPageAccessFilter
    {
        private readonly AuthenticationSettings _settings;
        private readonly ISecurityContext _security;

        public LoginPageAccessFilter(AuthenticationSettings settings, ISecurityContext security)
        {
            _settings = settings;
            _security = security;
        }

        public FubuContinuation LoginAccessFilter()
        {
            if (!_security.IsAuthenticated())
                return FubuContinuation.NextBehavior();

            return _settings.AllowAccessToLogin
                ?FubuContinuation.NextBehavior()
                :FubuContinuation.RedirectTo("~/");
        }
    }
}
