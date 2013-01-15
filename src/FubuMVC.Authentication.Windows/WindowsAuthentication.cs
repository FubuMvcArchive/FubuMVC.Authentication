using System.Security.Principal;
using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication.Windows
{
    public interface IWindowsPrincipalHandler
    {
        void Authenticated(WindowsPrincipal principal);
    }

    public class NulloWindowsPrincipalHandler : IWindowsPrincipalHandler
    {
        public void Authenticated(WindowsPrincipal principal)
        {
            // no-op
        }
    }

    public class WindowsAuthentication : IWindowsAuthentication
    {
        private readonly IAuthenticationSession _session;
        private readonly IWindowsPrincipalHandler _handler;

        public WindowsAuthentication(IAuthenticationSession session, IWindowsPrincipalHandler handler)
        {
            _session = session;
            _handler = handler;
        }


        public FubuContinuation Authenticate(WindowsSignInRequest request, WindowsPrincipal principal)
        {
            _session.MarkAuthenticated(principal.Identity.Name);
            _handler.Authenticated(principal);

            return FubuContinuation.RedirectTo(request.Url);
        }
    }
}