using FubuMVC.Core;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Windows
{
    [NotAuthenticated]
    public class WindowsController
    {
        private readonly IAuthenticationSession _session;
        private readonly IWindowsAuthenticationContext _windowsContext;
        private readonly IOutputWriter _writer;

        public WindowsController(IAuthenticationSession session, IWindowsAuthenticationContext windowsContext, IOutputWriter writer)
        {
            _session = session;
            _windowsContext = windowsContext;
            _writer = writer;
        }

        [UrlPattern("login/windows")]
        public void Login(WindowsSignInRequest request)
        {
            var currentUser = _windowsContext.CurrentUser();
            _session.MarkAuthenticated(currentUser);

            _writer.RedirectToUrl(request.Url ?? "~/");
        }
    }
}