namespace FubuMVC.Authentication
{
    public interface ILoginSuccessHandler
    {
        void LoggedIn(LoginRequest request);
    }
}