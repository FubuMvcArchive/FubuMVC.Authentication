using FubuCore;
using FubuMVC.Authentication.Auditing;
using FubuMVC.Authentication.Cookies;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Endpoints
{
    public class LoginController
    {
        private readonly ILoginCookies _cookies;
        private readonly IAuthenticationService _service;
        private readonly ILoginSuccessHandler _handler;
        private readonly ILoginAuditor _auditor;
        private readonly ILockedOutRule _lockedOutRule;

        public LoginController(ILoginCookies cookies, IAuthenticationService service, ILoginSuccessHandler handler, ILoginAuditor auditor, ILockedOutRule lockedOutRule)
        {
            _cookies = cookies;
            _service = service;
            _handler = handler;
            _auditor = auditor;
            _lockedOutRule = lockedOutRule;
        }

        public FubuContinuation post_login(LoginRequest request)
        {
            _auditor.ApplyHistory(request);

            var authenticated = _service.Authenticate(request);
            _auditor.Audit(request);

            SetRememberMeCookie(request);

            if (authenticated)
            {
                return _handler.LoggedIn(request);
            }

            return FubuContinuation.TransferTo(request, "GET");
        }

        public LoginRequest get_login(LoginRequest request)
        {
            if (request.Status == LoginStatus.NotAuthenticated)
            {
                request.Status = _lockedOutRule.IsLockedOut(request);
            }

            SetRememberMeCookie(request);

            if (request.UserName.IsEmpty())
            {
                var remembered = _cookies.User.Value;

                if (remembered.IsNotEmpty())
                {
                    request.UserName = remembered;
                    request.RememberMe = true;
                }
            }

            if (request.Status == LoginStatus.LockedOut)
            {
                request.Message = LoginKeys.LockedOut.ToString();
            }
            else if (request.Status == LoginStatus.Failed && request.Message.IsEmpty())
            {
                request.Message = LoginKeys.Unknown.ToString();
            }

            return request;
        }

        private void SetRememberMeCookie(LoginRequest request)
        {
            if (request.RememberMe && request.UserName.IsNotEmpty())
            {
                _cookies.User.Value = request.UserName;
            }
        }
    }
}