using FubuCore;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication
{
    public class LoginSuccessHandler : ILoginSuccessHandler
    {
        private readonly IOutputWriter _writer;

        public LoginSuccessHandler(IOutputWriter writer)
        {
            _writer = writer;
        }

        public void LoggedIn(LoginRequest request)
        {
            var url = request.Url;
            if (url.IsEmpty())
            {
                url = "~/";
            }

            _writer.RedirectToUrl(url);
        }
    }
}