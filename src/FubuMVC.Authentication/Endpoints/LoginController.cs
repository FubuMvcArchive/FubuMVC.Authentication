using FubuCore;
using FubuMVC.Authentication.Cookies;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Endpoints
{
    public class LoginController
    {
        private readonly ILoginCookies _cookies;
        private readonly AuthenticationSettings _settings;

        public LoginController(ILoginCookies cookies, AuthenticationSettings settings)
        {
            _cookies = cookies;
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

            if (request.Message.IsEmpty())
            {
                request.Message = LoginKeys.Unknown.ToString();
            }

            return request;
        }
    }
}