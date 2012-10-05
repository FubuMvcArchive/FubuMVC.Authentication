using System.Web;
using FubuCore.Dates;

namespace FubuMVC.Authentication.Tickets.Basic
{
    public interface ILoginCookieService
    {
        HttpCookie Current();
        HttpCookie CreateCookie(ISystemTime clock);

        void Update(HttpCookie cookie);
    }
}