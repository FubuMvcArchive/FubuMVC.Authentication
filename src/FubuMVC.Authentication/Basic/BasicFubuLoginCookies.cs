using FubuCore.Dates;
using FubuMVC.Core.Http;

namespace FubuMVC.Authentication.Basic
{
    public class BasicFubuLoginCookies : ILoginCookies
    {
        public static readonly string FubuRemember = "FubuRememberMe";

        private readonly ISystemTime _time;
        private readonly ICookies _cookies;

        public BasicFubuLoginCookies(ISystemTime time, ICookies cookies)
        {
            _time = time;
            _cookies = cookies;
        }

        public ICookieValue User
        {
            get { return new CookieValue(FubuRemember, _time, _cookies); }
        }
    }
}