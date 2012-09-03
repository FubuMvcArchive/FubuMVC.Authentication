namespace FubuMVC.Authentication
{
    public interface IAuthenticationService
    {
        bool Authenticate(LoginRequest request);
    }
}