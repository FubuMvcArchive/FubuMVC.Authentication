using FubuCore;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Tickets.Basic
{
    public class LoginController
    {
        private readonly ILoginCookies _cookies;
        private readonly ILoginFailureHandler _failureHandler;
        private readonly AuthenticationSettings _settings;

        public LoginController(ILoginCookies cookies, ILoginFailureHandler failureHandler, AuthenticationSettings settings)
        {
            _cookies = cookies;
            _failureHandler = failureHandler;
            _settings = settings;
        }

        [UrlPattern("login")]
        public LoginRequest Login(LoginRequest request)
        {
            if (request.UserName.IsEmpty())
            {
                var remembered = _cookies.User.Value;

                if (remembered.IsNotEmpty())
                {
                    request.UserName = remembered;
                    request.RememberMe = true;
                }
            }

            if (request.Status != LoginStatus.Failed)
            {
                return request;
            }

            _failureHandler.Handle(request, _cookies, _settings);

            if (request.Message.IsEmpty())
            {
                request.Message = LoginKeys.Unknown.ToString();
            }

            return request;
        }
    }
}