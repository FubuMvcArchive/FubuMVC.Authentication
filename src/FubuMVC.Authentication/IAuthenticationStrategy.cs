namespace FubuMVC.Authentication
{
    public interface IAuthenticationStrategy
    {
        bool TryToApply();
        bool Authenticate(LoginRequest request);
    }
}