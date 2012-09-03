using System;
using System.Web;
using FubuCore.Dates;
using FubuMVC.Core.Http;
using HtmlTags;

namespace FubuMVC.Authentication
{
    public class SimpleCookieTicketSource : ITicketSource
    {
        private readonly ISystemTime _systemTime;
        private readonly IEncryptor _encryptor;
        private readonly ICookies _cookies;
        public static readonly string CookieName = "FubuAuthTicket";

        public SimpleCookieTicketSource(ISystemTime systemTime, IEncryptor encryptor, ICookies cookies)
        {
            _systemTime = systemTime;
            _encryptor = encryptor;
            _cookies = cookies;
        }

        public AuthenticationTicket CurrentTicket()
        {
            try
            {
                var cookie = _cookies.Request[CookieName];
                if (cookie != null)
                {
                    var json = cookie.Value;
                    json = _encryptor.Decrypt(json);

                    return JsonUtil.Get<AuthenticationTicket>(json);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Persist(AuthenticationTicket ticket)
        {
            var cookie = basicCookie();
            cookie.Value = _encryptor.Encrypt(JsonUtil.ToJson(ticket));
            cookie.Expires = _systemTime.UtcNow().AddMonths(1);

            _cookies.Response.Add(cookie);
        }

        public void Delete()
        {
            var cookie = basicCookie();
            cookie.Expires = _systemTime.UtcNow().AddYears(-1);

            _cookies.Response.Add(cookie);
        }

        private static HttpCookie basicCookie()
        {
            return new HttpCookie(CookieName);
        }
    }
}