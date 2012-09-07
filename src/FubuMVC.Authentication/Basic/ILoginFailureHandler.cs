namespace FubuMVC.Authentication.Basic
{
    public interface ILoginFailureHandler
    {
        void Handle(LoginRequest request, ILoginCookies cookies, AuthenticationSettings settings);
    }
}