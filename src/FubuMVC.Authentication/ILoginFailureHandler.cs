namespace FubuMVC.Authentication
{
    public interface ILoginFailureHandler
    {
        void Handle(LoginRequest request, AuthenticationTicket ticket, AuthenticationSettings settings);
    }
}