namespace FubuMVC.Authentication
{
    public interface ILockedOutRule
    {
        LoginStatus IsLockedOut(LoginRequest request);
        void ProcessFailure(LoginRequest request);
    }

    public class LockedOutRule : ILockedOutRule
    {
        public LoginStatus IsLockedOut(LoginRequest request)
        {
            return LoginStatus.NotAuthenticated;
        }

        public void ProcessFailure(LoginRequest request)
        {
        }
    }
}