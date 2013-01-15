using System.Security.Principal;
using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication.Windows
{
    public interface IWindowsAuthentication
    {
        FubuContinuation Authenticate(WindowsSignInRequest request, WindowsPrincipal principal);
    }
}