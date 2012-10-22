using System.Web;
using FubuCore;
using FubuCore.Dates;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Tickets.Basic
{
    public class LoginCookieService : ILoginCookieService
    {
        private readonly CookieSettings _settings;
        private readonly ICookies _cookies;
        private readonly IOutputWriter _writer;

        public LoginCookieService(CookieSettings settings, ICookies cookies, IOutputWriter writer)
        {
            _settings = settings;
            _cookies = cookies;
            _writer = writer;
        }

        public HttpCookie Current()
        {
            return _cookies.Get(_settings.Name);
        }

        public HttpCookie CreateCookie(ISystemTime clock)
        {
            var cookie = new HttpCookie(_settings.Name);
            cookie.HttpOnly = _settings.HttpOnly;
            cookie.Secure = _settings.Secure;
            cookie.Domain = _settings.Domain;

            if(_settings.Path.IsNotEmpty())
            {
                cookie.Path = _settings.Path;
            }

            cookie.Expires = _settings.ExpirationFor(clock.UtcNow());

            return cookie;
        }

        public void Update(HttpCookie cookie)
        {
            _writer.AppendCookie(cookie);
        }
    }
}