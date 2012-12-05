namespace FubuMVC.Authentication
{
    public interface IAuthenticationService
    {
        bool TryToApply();
        bool Authenticate(LoginRequest request);
    }
}