using FubuMVC.Core.Http;

namespace FubuMVC.Authentication.Cookies
{
    public interface ILoginCookies
    {
        ICookieValue User { get; }
    }
}