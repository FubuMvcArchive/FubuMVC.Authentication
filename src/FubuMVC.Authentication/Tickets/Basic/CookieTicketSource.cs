using System;
using FubuCore.Dates;
using HtmlTags;

namespace FubuMVC.Authentication.Tickets.Basic
{
    public class CookieTicketSource : ITicketSource
    {
        private readonly ISystemTime _systemTime;
        private readonly IEncryptor _encryptor;
        private readonly ILoginCookieService _cookies;

        public CookieTicketSource(ISystemTime systemTime, IEncryptor encryptor, ILoginCookieService cookies)
        {
            _systemTime = systemTime;
            _encryptor = encryptor;
            _cookies = cookies;
        }

        public AuthenticationTicket CurrentTicket()
        {
            try
            {
                var cookie = _cookies.Current();
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
            var cookie = _cookies.CreateCookie(_systemTime);
            cookie.Value = _encryptor.Encrypt(JsonUtil.ToJson(ticket));

            _cookies.Update(cookie);
        }

        public void Delete()
        {
            var cookie = _cookies.CreateCookie(_systemTime);
            cookie.Expires = _systemTime.UtcNow().AddYears(-1);

            _cookies.Update(cookie);
        }
    }
}