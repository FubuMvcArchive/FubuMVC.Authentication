using FubuCore;
using FubuMVC.Authentication.Cookies;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Endpoints
{
    public class LoginController
    {
        private readonly ILoginCookies _cookies;

        public LoginController(ILoginCookies cookies)
        {
            _cookies = cookies;
        }

        [UrlPattern("login")]
        public LoginRequest Login(LoginRequest request)
        {
            if (request.RememberMe && request.UserName.IsNotEmpty())
            {
                _cookies.User.Value = request.UserName;
            }

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


    }
}