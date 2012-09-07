using FubuMVC.Core.Http;

namespace FubuMVC.Authentication.Basic
{
    public interface ILoginCookies
    {
        ICookieValue User { get; }
    }
}