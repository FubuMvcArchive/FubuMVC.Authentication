using FubuMVC.Core.Http;

namespace FubuMVC.Authentication.Tickets.Basic
{
    public interface ILoginCookies
    {
        ICookieValue User { get; }
    }
}