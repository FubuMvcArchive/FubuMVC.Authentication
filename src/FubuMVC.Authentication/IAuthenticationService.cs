namespace FubuMVC.Authentication
{
    public interface IAuthenticationService
    {
        AuthResult TryToApply();
        bool Authenticate(LoginRequest request);
    }
}