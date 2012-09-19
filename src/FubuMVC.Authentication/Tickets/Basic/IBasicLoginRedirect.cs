using FubuCore;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Tickets.Basic
{
    public interface IBasicLoginRedirect
    {
        void Redirect(LoginRequest request);
    }

    public class BasicLoginRedirect : IBasicLoginRedirect
    {
        private readonly IOutputWriter _writer;

        public BasicLoginRedirect(IOutputWriter writer)
        {
            _writer = writer;
        }

        public void Redirect(LoginRequest request)
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