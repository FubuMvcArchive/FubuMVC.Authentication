namespace FubuMVC.Authentication.Tickets.Basic
{
    public interface ILoginFailureHandler
    {
        void Handle(LoginRequest request, ILoginCookies cookies, AuthenticationSettings settings);
    }
}