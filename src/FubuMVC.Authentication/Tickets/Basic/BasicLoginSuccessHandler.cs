namespace FubuMVC.Authentication.Tickets.Basic
{
    public class BasicLoginSuccessHandler : ILoginSuccessHandler
    {
        private readonly IBasicLoginRedirect _redirect;

        public BasicLoginSuccessHandler(IBasicLoginRedirect redirect)
        {
            _redirect = redirect;
        }

        public void LoggedIn(LoginRequest request)
        {
            _redirect.Redirect(request);
        }
    }
}