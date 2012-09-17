namespace FubuMVC.Authentication.Tickets.Basic
{
    public class NulloLoginFailureHandler : ILoginFailureHandler
    {
        public void Handle(LoginRequest request, ILoginCookies cookies, AuthenticationSettings settings)
        {
            // no-op
        }
    }
}